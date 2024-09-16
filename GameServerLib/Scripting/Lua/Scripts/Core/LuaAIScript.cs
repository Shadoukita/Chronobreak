using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using GameServerCore;
using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.Logging;
using Chronobreak.GameServer.Scripting.CSharp.Converted;
using Chronobreak.GameServer.Scripting.Lua;
using log4net;
using MoonSharp.Interpreter;
using Newtonsoft.Json;

namespace GameServerLib.Scripting.Lua.Scripts;

[MoonSharpModule]
internal class LuaAIScript : CAIScript
{
    private static readonly ILog Logger = LoggerProvider.GetLogger();
    private readonly Table _aiScriptGlobals;
    private readonly Dictionary<string, RunningTimer> _timersLua = [];

    private int _leashCounter;
    private Vector3 _leashedOrientation;
    private Vector2 _leashedPos;

    private readonly Dictionary<string, Dictionary<int, MethodInfo>> _methodsCache = [];

    private readonly string _scriptName;
    public LuaAIScript(string scriptName)
    {
        _scriptName = scriptName;
        _aiScriptGlobals = LuaScriptEngine.CreateTableReferringGlobal();
        _aiScriptGlobals.Set("L_G", DynValue.NewTable(LuaScriptEngine.NewTable()));
        RegisterAllMethods();
    }

    public new void Init(ObjAIBase owner)
    {
        _aiScriptGlobals["me"] = owner;
        _aiScriptGlobals["canMove"] = owner.CanMove();


        //ACC: This is missing in Minion.lua for some reason but is required, I have copied it from Hero.lua, so ít might not be 100% accurate
        if (_scriptName == "Minion.lua")
        {
            LuaScriptEngine.DoLuaString(@"
function TimerCheckAttack()
  if GetState() == AI_HALTED then
    return
  end
  if GetState() == AI_SOFTATTACK or GetState() == AI_HARDATTACK or GetState() == AI_TAUNTED or GetState() == AI_CHARMED then
    if IsTargetLost() == true or GetTarget() == nil then
      if LastAutoAttackFinished() == false then
        InitTimer(""TimerCheckAttack"", 0.1, true)
        return false
      end
      newTarget = FindTargetInAcR()
      if newTarget ~= nil then
        if GetState() == AI_CHARMED then
          SetStateAndCloseToTarget(AI_CHARMED, newTarget)
        elseif CanSeeMe(newTarget) then
          SetStateAndCloseToTarget(AI_SOFTATTACK, newTarget)
        end
        return true
      else
        if GetState() == AI_CHARMED then
          SpellBuffRemoveType(me, BUFF_Taunt)
        end
        NetSetState(AI_STANDING)
        return true
      end
      return true
    end
    if TargetInAttackRange() == true then
      TurnOnAutoAttack(GetTarget())
      return true
    end
    if TargetInCancelAttackRange() == false then
      TurnOffAutoAttack(STOPREASON_MOVING)
    end
  elseif IsMoving() then
    return false
  end
  InitTimer(""TimerCheckAttack"", 0.1, true)
end

function TimerDistanceScan()
    local a, b
    a = GetState()
    if a == AI_HALTED then
        return
    end
    if a == AI_STANDING then
        b = GetTargetOrFindTargetInAcR()
        if b ~= nil and CanSeeMe(b) then
            SetStateAndCloseToTarget(AI_SOFTATTACK, b)
            return true
        end
    end
    if a == AI_MOVE then
        if IsMovementStopped() then
            b = GetTargetOrFindTargetInAcR()
            if b ~= nil and CanSeeMe(b) then
                SetStateAndCloseToTarget(AI_SOFTATTACK, b)
                TurnOnAutoAttack(b)
                return true
            end
            NetSetState(AI_IDLE)
            return false
        end
    end
    if a == AI_ATTACKMOVESTATE then
        b = GetTargetOrFindTargetInAcR()
        if b ~= nil then
            SetStateAndCloseToTarget(AI_SOFTATTACK, b)
            return true
        elseif DistanceBetweenObjectAndTargetPosSq(me) <= 100 then
            NetSetState(AI_STANDING)
            ClearTargetPosInPos()
            return true
        end
    end
    if a == AI_ATTACK_GOING_TO_LAST_KNOWN_LOCATION then
        b = GetLostTargetIfVisible()
        if b ~= nil then
            SetStateAndCloseToTarget(AI_HARDATTACK, b)
        end
    end
end", _aiScriptGlobals);
        }

        LuaScriptEngine.DoScript(_scriptName, _aiScriptGlobals);
        base.Init(owner);
        CallLua("OnInit");
    }

    public override bool OnInit()
    {
        return CallLua<bool>("OnInit");
    }

    public override void HaltAI()
    {
        CallLua("HaltAI");
    }

    public override bool OnOrder(OrderType orderType, AttackableUnit target, Vector2 position)
    {
        return CallLua<bool>("OnOrder", orderType, target, position);
    }

    public override void OnTargetLost(LostTargetEvent eventType, AttackableUnit target)
    {
        //yes, there is two.
        CallLua("OnTargetLost", eventType, target);
        CallLua("OnLostTarget", eventType, target);
    }

    public override void OnAICommand()
    {
        CallLua("OnAICommand");
    }

    public new void OnCollisionTerrain()
    {
        CallLua("OnCollisionTerrain");
    }

    public override void OnStoppedMoving()
    {
        CallLua("OnStoppedMoving");
    }

    public override void OnCallForHelp(ObjAIBase attacker, ObjAIBase victim)
    {
        if (Me is BaseTurret)
        {
            CallLua("OnReceiveImportantCallForHelp", victim, attacker);
            CallLua("OnCallForHelp", victim, attacker);
        }
        else
        {
            CallLua("OnCallForHelp", victim, attacker);
        }
    }

    public override void OnLeashedCallForHelp(ObjAIBase attacker, ObjAIBase victim)
    {
        CallLua("LeashedCallForHelp", victim, attacker);
    }

    public override void OnReachedDestinationForGoingToLastLocation()
    {
        CallLua("OnReachedDestinationForGoingToLastLocation");
    }

    public override void OnCanMove()
    {
        CallLua("OnCanMove");
    }

    public void SetMyLeashedPos()
    {
        _leashedPos = Me.Position;
    }

    public void SetMyLeashedOrientation()
    {
        _leashedOrientation = Me.Direction;
    }

    public Vector2 GetMyPos()
    {
        return Me.Position;
    }

    public void SetStateAndMoveToLeashedPos(AIState state)
    {
        SetState(state);
        SetTarget(null);
        Me.CancelAutoAttack(false, true);
        Me.IssueOrDelayOrder(OrderType.MoveTo, null, _leashedPos);
    }

    public int GetLeashCounter()
    {
        return _leashCounter;
    }

    public MinionRoamState GetOriginalState()
    {
        return OriginalMinionRoamState;
    }

    public bool GetCanIMove()
    {
        return Me.CanMove();
    }

    public bool GetCanMove()
    {
        return GetCanIMove();
    }

    public bool GetCanMove(ObjAIBase unit)
    {
        return unit.CanMove();
    }

    public void SetLeashCounter(int newValue)
    {
        _leashCounter = newValue;
    }

    public void SetLeashOrientation()
    {
        Me.FaceDirection(_leashedOrientation);
    }

    public Vector2 GetMyLeashedPos()
    {
        return _leashedPos;
    }

    private float GetDistToLeashedPos()
    {
        return Vector2.Distance(_leashedPos, Me.Position);
    }

    private float GetDistToRetreat()
    {
        return GetDistToLeashedPos();
    }

    protected float DistanceBetweenObjectAndTargetPosSq()
    {
        if (TargetPos != null)
            return Vector2.DistanceSquared(Me.Position, TargetPos.Value);
        return 0;
    }

    protected void TeleportToObj(ObjAIBase target)
    {
        Me.TeleportTo(target.Position);
    }

    protected bool TargetIsMoving()
    {
        return !GetTarget()?.IsPathEnded() ?? false;
    }

    protected bool OwnerIsMoving()
    {
        var owner = GetOwner();
        if (owner == null) return false;
        return Functions.TargetIsMoving(owner);
    }

    protected void AssignTargetPosInPos()
    {
        TargetPos = Me.LastIssueOrderPosition;
    }

    protected bool IsMovementStopped()
    {
        return Me.IsPathEnded();
    }

    private void Say(string text)
    {
        //Logger.Debug(text);
    }

    protected float GetHP()
    {
        return Me.Stats.CurrentHealth;
    }

    protected void SetHP(float hp)
    {
        Me.Stats.CurrentHealth = Math.Clamp(hp, 0, Me.Stats.HealthPoints.Total);
    }

    protected float GetMaxHP()
    {
        return Me.Stats.HealthPoints.Total;
    }

    protected Vector2 MakeWanderPoint(Vector2 pos)
    {
        var normalizedDirection = (Me.Position - pos).Normalized();
        var fearVector = normalizedDirection * _aiScriptGlobals.RawGet("FEAR_WANDER_DISTANCE").ToObject<float>();
        var fearPoint = Me.Position + fearVector;
        return fearPoint;
    }

    private void InitTimer(string timerName, float delay, bool repeat)
    {
        if (_timersLua.TryGetValue(timerName, out var runningTimer))
        {
            runningTimer.Reset();
        }
        else
        {
            var timer = new RunningTimer(() => CallLua(timerName), delay, repeat, true);
            _timersLua[timerName] = timer;
        }
    }

    private void StopTimer(string timerName)
    {
        if (_timersLua.TryGetValue(timerName, out var timer))
            timer.Running = false;
    }

    private void ResetAndStartTimer(string timerName)
    {
        if (_timersLua.TryGetValue(timerName, out var timer))
            timer.Reset();
    }

    public override void OnCanAttack()
    {
        CallLua("OnCanAttack");
    }

    public override void OnTauntBegin()
    {
        CallLua("Event", "OnTauntBegin");
        CallLua("OnTauntBegin");
    }

    public override void OnTauntEnd()
    {
        CallLua("Event", "OnTauntEnd");
        CallLua("OnTauntEnd");
    }

    public override void OnFearBegin()
    {
        CallLua("Event", "OnFearBegin");
        CallLua("OnFearBegin");
    }

    public override void OnFearEnd()
    {
        CallLua("Event", "OnFearEnd");
        CallLua("OnFearEnd");
    }

    public override void OnCharmBegin()
    {
        CallLua("Event", "OnCharmBegin");
        CallLua("OnCharmBegin");
    }

    public override void OnCharmEnd()
    {
        CallLua("Event", "OnCharmEnd");
        CallLua("OnCharmEnd");
    }

    private void DoLuaShared(string sharedName)
    {
        LuaScriptEngine.DoScript(sharedName + ".lua", _aiScriptGlobals);
    }

    private void Error(string errorName)
    {
        //Logger.Error(errorName);
    }
    private void PrintToChat(string message)
    {
        //Logger.Info(message);
    }
    public override void OnUpdate()
    {
        foreach (var runningTimer in _timersLua)
        {
            runningTimer.Value.Update();
        }
        CallLua("Update");
    }

    public override bool OnCollisionEnemy(AttackableUnit target)
    {
        return CallLua<bool>("OnCollisionEnemy", target);
    }

    public override bool OnCollisionOther(AttackableUnit target)
    {
        return CallLua<bool>("OnCollisionOther", target);
    }

    private void FindTargetOrMove()
    {
        var target = FindTargetInAcR();
        if (target == null)
        {
            SetStateAndMoveToForwardNav(AIState.AI_ATTACKMOVESTATE);
        }
        else
        {
            SetStateAndCloseToTarget(AIState.AI_ATTACK, target);
        }
    }

    protected float DistanceBetweenObjectCenterAndPoint(AttackableUnit target, Vector2 position)
    {
        return Vector2.Distance(target.Position, position);
    }

    protected void SpellIncLevel(int slot)
    {
        Me.LevelUpSpell((byte)slot);
    }

    protected void ServerCastSpellOnPos(int slot, Vector3 pos)
    {
        Me.Spells[slot].TryCast(null, pos, pos);
    }

    protected new AttackableUnit? FindTargetInAcR()
    {
        return FindTargetInAcR(Me);
    }

    [IgnoreLuaMethod]
    private void RegisterAllMethods()
    {
        var currentType = GetType();
        const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;


        foreach (var methodGroup in currentType.GetMethods(bindingFlags)
                     .Where(m => !m.IsVirtual && !m.IsAbstract)
                     .GroupBy(m => m.Name))
        {
            // Check if all method in the group has the IgnoreLuaMethodAttribute and continue if so
            if (methodGroup.All(m => m.GetCustomAttribute(typeof(IgnoreLuaMethodAttribute)) != null)) continue;

            if (_aiScriptGlobals.Get(methodGroup.Key) != DynValue.Nil) continue;

            foreach (var methodInfo in methodGroup)
            {
                if (methodInfo.GetCustomAttribute(typeof(IgnoreLuaMethodAttribute)) != null) continue;
                if (_methodsCache.ContainsKey(methodGroup.Key))
                {
                    _methodsCache[methodGroup.Key].Add(methodInfo.GetParameters().Length, methodInfo);
                }
                else
                {
                    _methodsCache.Add(methodGroup.Key, []);
                    _methodsCache[methodGroup.Key].Add(methodInfo.GetParameters().Length, methodInfo);
                }
            }

            var isAction = methodGroup.First().ReturnType == typeof(void);
            if (isAction)
            {
                void DynMethod(CallbackArguments args)
                {
                    try
                    {
                        var methodToCall = FindMethod(methodGroup.Key, args.Count);
                        if (methodToCall != null)
                        {
                            methodToCall.Invoke(this, ConvertArgs(methodToCall, args));
                            return;
                        }
                        Logger.Error($"Tried to access an unimplemented method {methodGroup.Key} with arguments {JsonConvert.SerializeObject(args.GetArray().Select(a => a.ToObject()))}");
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e);
                    }
                }
                _aiScriptGlobals[methodGroup.Key] = (Action<CallbackArguments>)DynMethod;
            }
            else
            {
                object? DynMethod(CallbackArguments args)
                {
                    try
                    {
                        var methodToCall = FindMethod(methodGroup.Key, args.Count);
                        if (methodToCall != null)
                        {
                            return methodToCall.Invoke(this, ConvertArgs(methodToCall, args));
                        }
                        Logger.Error($"Tried to access an unimplemented method {methodGroup.Key} with arguments {JsonConvert.SerializeObject(args.GetArray().Select(a => a.ToObject()))}");
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e);
                    }
                    return default;
                }
                _aiScriptGlobals[methodGroup.Key] = (Func<CallbackArguments, object?>)DynMethod;
            }
        }

        //HACK: Roam state is a number in lua scripts, not a global enum
        _aiScriptGlobals["GetRoamState"] = () => (int)GetRoamState();

        //HACK: Map11, for river regions
        if (_scriptName.Equals("rivercrab.lua", StringComparison.InvariantCultureIgnoreCase))
        {
            _aiScriptGlobals["GetMyPos"] = () => GetMyPos().ToVector3(0);
            _aiScriptGlobals["GetDistSquared"] = (Func<Vector3, Vector3, float>)((v1, v2) => v1.ToVector2().DistanceSquared(v2.ToVector2()));
        }

        object[]? ConvertArgs(MethodInfo method, CallbackArguments args)
        {
            var methodArgs = method.GetParameters();
            if (methodArgs.Length == 0) return null;
            var clrObjects = new object[methodArgs.Length];
            for (var i = 0; i < methodArgs.Length; i++)
            {
                clrObjects[i] = args.RawGet(i, true).ToObject(methodArgs[i].ParameterType);
            }
            return clrObjects;
        }

        MethodInfo? FindMethod(string methodName, int argsLength)
        {
            var allMethods = _methodsCache[methodName];
            if (!allMethods.TryGetValue(argsLength, out var methodToCall))
            {
                int lowerKey = -1;
                foreach (var length in allMethods.Keys)
                {
                    if (length < argsLength && (lowerKey == -1 || length > lowerKey))
                    {
                        lowerKey = length;
                    }
                }
                if (lowerKey != -1)
                {
                    methodToCall = _methodsCache[methodName][lowerKey];
                }
            }
            return methodToCall;
        }
    }

    [IgnoreLuaMethod]
    public DynValue? CallLua(string functionName, params object[] args)
    {
        try
        {
            return _aiScriptGlobals.Get(functionName)?.Function?.Call(args);
        }
        catch (ScriptRuntimeException e)
        {
            Logger.Error(e.DecoratedMessage);
            return null;
        }
    }

    [IgnoreLuaMethod]
    public T? CallLua<T>(string functionName, params object[] args)
    {
        var result = CallLua(functionName, args);
        if (result == null) return default;
        if (result.IsNil()) return default;
        return result.ToObject<T>();
    }
}
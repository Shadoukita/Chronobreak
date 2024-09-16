
using GameServerCore;
using GameServerCore.Domain;
using GameServerCore.Enums;
using Chronobreak.GameServer.API;
using Chronobreak.GameServer.GameObjects;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using GameServerLib.Scripting.Lua;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;
using E = GameServerCore.Extensions;

namespace Chronobreak.GameServer.Scripting.CSharp.Converted;

public partial class CAIScript : IAIScript
{
    //temp placement
    private static readonly Random random = new();


    private readonly Dictionary<AttackableUnit, float> _ignoreTargetList = [];
    private readonly Dictionary<Action, RunningTimer> _timers = [];

    protected ObjAIBase Me = null!;
    protected Vector2? TargetPos;
    protected MinionRoamState OriginalMinionRoamState;

    private ObjAIBase? _tauntTarget;
    private AttackableUnit? _lostTarget;

    private Vector2 _fearLeashPoint;

    protected virtual float FEAR_WANDER_DISTANCE => 0;
    protected virtual float FLEE_RUN_DISTANCE => 0;
    protected virtual float FAR_MOVEMENT_DISTANCE => 0;
    protected virtual float MINION_MAX_VISION_DISTANCE => 0;
    protected virtual float TELEPORT_DISTANCE => 0;

    private float _petReturnRadius = 0;

    public virtual AIScriptMetaData AIScriptMetaData { get; } = new() { HandlesCallsForHelp = true };

    public void Init(ObjAIBase owner)
    {
        Me = owner;
        ApiEventManager.OnTakeDamage.AddListener(this, Me, data => OnTakeDamage(data.Attacker));
        ApiEventManager.OnUnitBuffActivated.AddListener(this, Me, (_, buff) =>
        {
            switch (buff.BuffType)
            {
                case BuffType.TAUNT:
                    _tauntTarget = buff.SourceUnit;
                    OnTauntBegin();
                    break;
                case BuffType.FEAR:
                    _fearLeashPoint = buff.SourceUnit.Position;
                    OnFearBegin();
                    break;
                case BuffType.CHARM:
                    OnCharmBegin();
                    break;
            }
        });

        ApiEventManager.OnUnitBuffDeactivated.AddListener(this, Me, (_, buff) =>
        {
            switch (buff.BuffType)
            {
                case BuffType.TAUNT:
                    OnTauntEnd();
                    break;
                case BuffType.FEAR:
                    OnFearEnd();
                    break;
                case BuffType.CHARM:
                    OnCharmEnd();
                    break;
            }
        });
        if (owner is Minion m)
        {
            OriginalMinionRoamState = m.RoamState;
        }
    }

    public virtual void Activate()
    {
        OnActivate(Me);
        OnInit();
    }

    public virtual void Deactivate(bool expired)
    {
        foreach (var runningTimer in _timers) runningTimer.Value.Running = false;
    }

    public virtual void Update()
    {
        foreach (var runningTimer in _timers)
        {
            runningTimer.Value.Update();
        }

        foreach (var (target, ignoreDiff) in _ignoreTargetList)
        {
            if (ignoreDiff > 0)
            {
                _ignoreTargetList[target] -= Game.Time.DeltaTime;
            }
        }
        OnUpdate();
    }

    public virtual bool OnOrder(OrderType orderType, AttackableUnit target, Vector2 position)
    {
        return false;
    }

    public void TargetLost(LostTargetEvent eventType, AttackableUnit target)
    {
        _lostTarget = target;
        OnTargetLost(eventType, target);
    }

    public virtual void OnTargetLost(LostTargetEvent eventType, AttackableUnit target)
    {
    }

    public virtual void OnStoppedMoving()
    {
    }

    public virtual void OnCallForHelp(ObjAIBase attacker, ObjAIBase victim)
    {
    }

    public virtual void OnLeashedCallForHelp(ObjAIBase attacker, ObjAIBase victim)
    {
    }

    public virtual void OnAICommand()
    {
    }

    [IgnoreLuaMethod]
    public void OnCollision(AttackableUnit target)
    {
        if (target.Team == Me.Team)
            OnCollisionOther(target);
        else
            OnCollisionEnemy(target);
    }

    [IgnoreLuaMethod]
    public void OnCollisionTerrain()
    {
    }

    protected AIState GetState()
    {
        return Me.GetAIState();
    }

    protected void SetState(AIState to)
    {
        SetState(to, false);
    }

    [IgnoreLuaMethod]
    protected void SetState(AIState to, bool publish)
    {
        Me.SetAIState(to, publish);
    }

    protected void SetRoamState(MinionRoamState to)
    {
        if (Me is Minion minion)
        {
            minion.SetRoamState(to);
        }
    }

    [IgnoreLuaMethod]
    protected MinionRoamState GetRoamState()
    {
        if (Me is Minion minion)
        {
            return minion.RoamState;
        }
        return 0;
    }

    private int _currentWaypointIndex = 0;

    [IgnoreLuaMethod]
    private Vector2 GetLaneMinionWaypoint(LaneMinion minion)
    {
        bool WaypointReached()
        {
            Vector2 currentWaypoint = minion.PathingWaypoints[_currentWaypointIndex];

            //HACK: I don't have a better way of setting waypoint as completed at the moment (ACC)
            //This is just a circle radius and when minion enters it, it is marked as completed
            //Value 600 works for summoner's rift good, not so for Twisted Treeline (reworked)
            if (Vector2.Distance(minion.Position, currentWaypoint) < 600f)
            {
                return true;
            }

            return false;
        }

        if (WaypointReached())
        {
            _currentWaypointIndex++;
        }

        return minion.PathingWaypoints[_currentWaypointIndex];
    }

    protected void SetStateAndMoveToForwardNav(AIState state)
    {
        SetState(state);
        if (Me is LaneMinion minion)
        {
            Me.IssueOrDelayOrder(OrderType.AttackTo, null, GetLaneMinionWaypoint(minion), fromAiScript: true);
        }
    }

    [IgnoreLuaMethod]
    protected void InitTimer(Action callback, float interval, bool repeat)
    {
        var timer = new RunningTimer(callback, interval, repeat, true);
        _timers[timer.Callback] = timer;
    }

    [IgnoreLuaMethod]
    protected void StopTimer(Action callback)
    {
        if (_timers.TryGetValue(callback, out var timer))
            timer.Running = false;
    }

    [IgnoreLuaMethod]
    protected void ResetAndStartTimer(Action callback)
    {
        if (_timers.TryGetValue(callback, out var timer))
            timer.Reset();
    }

    protected void AddToIgnore(float duration)
    {
        var target = GetTarget();
        if (target != null) _ignoreTargetList[target] = duration * 1000;
    }

    protected AttackableUnit? SetStateAndCloseToTarget(AIState state, AttackableUnit? target)
    {
        if (target == null)
            return null;

        var order = GetMoveOrderForAiState(state);

        if (ShouldCancelAutoAttackForMoveOrderAndState(state, order))
        {
            TurnOffAutoAttack(StopReason.IMMEDIATELY);
        }

        SetState(state);
        Me.IssueOrDelayOrder(order, target, target.Position, fromAiScript: true);

        return target;
    }

    protected void SetStateAndMove(AIState state, Vector2 pos)
    {
        SetState(state);
        var order = GetMoveOrderForAiState(state);

        if (ShouldCancelAutoAttackForMoveOrderAndState(state, order))
        {
            TurnOffAutoAttack(StopReason.IMMEDIATELY);
        }

        Me.IssueOrDelayOrder(order, null, pos, fromAiScript: true);
    }

    protected void NetSetState(AIState state)
    {
        SetState(state, true);
    }

    protected Vector2 MakeWanderPoint(Vector2 pos, float dist)
    {
        var normalizedDirection = (Me.Position - pos).Normalized();
        var fearVector = normalizedDirection * dist;
        var fearPoint = Me.Position + fearVector;
        return fearPoint;
    }

    protected Vector2 MakeFleePoint()
    {
        //Currently, we are not implementing "Flee" as league does, we just do Fear which is the same?
        //So any flee references such as this function are unused.
        var pos = Me.Position3D;
        var radius = FEAR_WANDER_DISTANCE;
        var innerRadius = 100f;

        if (innerRadius > radius)
        {
            (radius, innerRadius) = (innerRadius, radius);
        }

        var fleePoint = pos +
                        (E.FromAngle(random.NextSingle() * 360f - 180f) *
                         (random.NextSingle() * (radius - innerRadius) + innerRadius)).ToVector3(0);

        return fleePoint.ToVector2();
    }

    protected Vector2 GetFearLeashPoint()
    {
        return _fearLeashPoint;
    }

    protected bool IsMoving()
    {
        return !Me.IsPathEnded();
    }

    protected void ClearTargetPosInPos()
    {
        TargetPos = null;
    }

    protected bool IsTargetIgnored(AttackableUnit? target)
    {
        if (target == null) return false;
        if (_ignoreTargetList.TryGetValue(target, out var diff)) return diff > 0;
        return false;
    }

    [IgnoreLuaMethod]
    protected AttackableUnit? FindTargetInAcRUsingGoldRedirectTarget()
    {
        var target = GetGoldRedirectTarget(Me);
        return target == null ? null : FindTargetInAcR(target);
    }


    protected AttackableUnit? FindTargetInAcRWithFilter(AttackableUnit owner, AITargetType targetType)
    {
        if (owner is Champion)
            return GetTarget((ObjAIBase)owner) ?? FindTargetInAcRInternal(owner, targetType: targetType);

        return FindTargetInAcRInternal(owner, targetType: targetType);
    }

    protected AttackableUnit? FindTargetInAcR(AttackableUnit owner)
    {
        if (owner is Champion champion)
            return GetTarget(champion) ?? FindTargetInAcRInternal(champion);

        return FindTargetInAcRInternal(owner);
    }

    [IgnoreLuaMethod]
    private static IEnumerable<AttackableUnit> GetUnitsInRange(Vector2 position, float range)
    {
        //If collision detector is disabled, function does not work
        return ApiFunctionManager.EnumerateUnitsInRange(position, range);
    }

    [IgnoreLuaMethod]
    private static float GetAcquisitionRangeInternal(AttackableUnit searcher, AttackableUnit owner)
    {
        if (searcher is Champion b)
        {
            return b.MoveOrder == OrderType.AttackMove ? owner.Stats.AcquisitionRange.Total : owner.Stats.Range.Total;
        }
        return owner.Stats.AcquisitionRange.Total;
    }

    [IgnoreLuaMethod]
    private AttackableUnit? FindTargetInAcRInternal(AttackableUnit sourceToSearchFrom, Vector2? positionOverride = null, float overrideRadius = 0, AITargetType targetType = AITargetType.AI_TARGET_NONE)
    {
        var state = GetState();

        //To prevent acquiring targets if user has stopped using stop command
        if (state is AIState.AI_STOP or AIState.AI_STANDING && Me is Champion)
        {
            return null;
        }


        var maxDistanceReal = overrideRadius != 0 ? overrideRadius : GetAcquisitionRangeInternal(Me, sourceToSearchFrom) + sourceToSearchFrom.CollisionRadius;
        //Search distance is more due to how collision handler and QuadTree work
        var maxDistanceSearch = maxDistanceReal + 100;

        var position = positionOverride ?? Vector2.Zero;
        if (position == Vector2.Zero)
        {
            position = sourceToSearchFrom.Position;
        }

        var units = GetUnitsInRange(position, maxDistanceSearch).ToList();

        int GetTargetPriority(AttackableUnit target, AttackableUnit victim = null)
        {
            if (targetType != AITargetType.AI_TARGET_NONE)
            {
                switch (targetType)
                {
                    case AITargetType.AI_TARGET_MINIONS when target is Minion:
                    case AITargetType.AI_TARGET_HEROES when target is Champion:
                    case AITargetType.AI_TARGET_DAMPENER when target is Inhibitor:
                    case AITargetType.AI_TARGET_TURRETS when target is BaseTurret:
                    case AITargetType.AI_TARGET_HQ when target is Nexus:
                        return 1;
                }
            }
            return Me.GetTargetPriority(target, victim);
        }


        AttackableUnit? SearchForTarget(IEnumerable<AttackableUnit> targetUnits, float searchDistance)
        {
            var currentTargetPriority = 999;
            AttackableUnit? nextTarget = null;
            foreach (var unit in targetUnits)
            {
                if (IsTargetIgnored(unit)) continue;

                if (!Me.IsValidTarget(unit))
                    continue;

                //To prevent auto-attacking jungle monsters if auto-attack acquire is enabled
                if (state is AIState.AI_MOVE or AIState.AI_IDLE && unit is NeutralMinion && Me is Champion)
                    continue;

                var distance = Vector2.Distance(position, unit.Position);
                if (distance > searchDistance)
                    continue;

                var ally = unit is ObjAIBase enemy && enemy.TargetUnit?.Team == Me.Team ? enemy.TargetUnit : null;
                var priority = GetTargetPriority(unit, ally);
                if (priority < currentTargetPriority)
                {
                    nextTarget = unit;
                    currentTargetPriority = priority;
                }
            }

            return nextTarget;
        }

        return SearchForTarget(units, maxDistanceReal);
    }

    [IgnoreLuaMethod]
    private AttackableUnit? FindTargetInAcR(AITargetType targetType)
    {
        return FindTargetInAcRWithFilter(Me, targetType);
    }

    public AttackableUnit? FindTargetInAcR()
    {
        return FindTargetInAcR(Me);
    }

    // Only used in BaronMinionAI
    protected AttackableUnit? FindTargetInAcRWithFilter(AITargetType aiTargetType)
    {
        return FindTargetInAcR(aiTargetType);
    }

    protected AttackableUnit? GetTarget()
    {
        return GetTarget(Me);
    }

    protected AttackableUnit? GetTarget(ObjAIBase owner)
    {
        //TODO: lost target?
        var tauntTarget = _tauntTarget;
        if (tauntTarget != null)
        {
            if (IsTargetIgnored(tauntTarget)) return IsTargetIgnored(owner.TargetUnit) ? null : owner.TargetUnit;
            return tauntTarget;
        }

        return IsTargetIgnored(owner.TargetUnit) ? null : owner.TargetUnit;
    }

    protected AttackableUnit? GetTargetOrFindTargetInAcR()
    {
        return FindTargetInAcR();
    }

    protected bool TargetInAttackRange()
    {
        return ObjectInAttackRange(GetTarget());
    }

    protected bool ObjectInAttackRange(AttackableUnit? target)
    {
        if (target == null) return false;
        var totalAttackRange = Me.GetTotalAttackRange();
        return
            Vector2.DistanceSquared(Me.Position, target.Position)
            <= totalAttackRange * totalAttackRange;
    }

    /// <summary>
    /// Warning! Returns an inverted bool if the target is in cancel attack range (Riot's scripts..)
    /// </summary>
    /// <returns></returns>
    protected bool TargetInCancelAttackRange()
    {
        var target = GetTarget();
        if (target == null) return true;
        var totalCancelRange = Me.GetTotalCancelAttackRange();
        return !(Vector2.DistanceSquared(Me.Position, target.Position) > totalCancelRange * totalCancelRange);
    }

    protected AttackableUnit? GetTauntTarget()
    {
        return _tauntTarget;
    }

    protected AttackableUnit? GetLostTargetIfVisible()
    {
        //TODO: Fix this with a vision system rework for nearsight
        return _lostTarget != null ? _lostTarget.IsVisibleByTeam(Me.Team) ? _lostTarget : null : null;
    }

    protected AttackableUnit? FindTargetNearPosition(Vector2 position, float radius)
    {
        return FindTargetInAcRInternal(Me, position, radius);
    }

    protected float DistanceBetweenMeAndObject(GameObject? obj)
    {
        if (obj == null) return 0;
        return Me.Position.Distance(obj.Position);
    }

    protected void TurnOnAutoAttack(AttackableUnit target)
    {
        if (!Me.AutoAttackOn)
        {
            Me.AutoAttackOn = true;
            Me.SetTargetUnit(target, true);
        }
    }

    protected void TurnOffAutoAttack(StopReason reason)
    {
        if (Me.AutoAttackOn)
        {
            Me.AutoAttackOn = false;

            if (reason == StopReason.TARGET_LOST)
            {
                _lostTarget = Me.TargetUnit;
            }

            Me.CancelAutoAttack(false);
        }
    }

    protected bool LastAutoAttackFinished()
    {
        return Me.AutoAttackSpell.State is SpellState.COOLDOWN or SpellState.READY;
    }

    protected float DistanceBetweenMeAndTarget()
    {
        var target = GetTarget();
        if (target == null) return 0;
        return DistanceBetweenMeAndObject(target);
    }

    protected void ClearTarget()
    {
        Me.SetTargetUnit(null, true);
    }

    protected bool TargetPositionInAttackRange(Vector2 position)
    {
        var totalAttackRange = Me.GetTotalAttackRange();
        return Vector2.DistanceSquared(Me.Position, position) <= totalAttackRange * totalAttackRange;
    }

    protected void TurnOnAutoAttackTerrain(Vector2 position, bool sustained)
    {
        //TODO: Figure the auto-attack terrain move orders
    }

    protected bool IsLocationAutoAttacker()
    {
        //TODO: Figure the auto-attack terrain move orders
        return false;
    }

    protected OrderType GetOrder()
    {
        return Me.MoveOrder;
    }

    protected Vector2? GetOrderPosition()
    {
        return Me.LastIssueOrderPosition;
    }

    protected void SetTarget(AttackableUnit target)
    {
        Me.SetTargetUnit(target, true);
    }

    protected bool IsTargetLost()
    {
        //TODO: ignored != target lost?
        return GetTarget() == null;
    }

    protected virtual Vector2 GetPosition()
    {
        return Me.Position;
    }

    protected AttackableUnit? GetGoldRedirectTarget()
    {
        return GetGoldRedirectTarget(Me);
    }

    protected AttackableUnit? GetOwner()
    {
        if (Me is Minion { Owner.Stats.IsDead: false } unit)
        {
            return unit.Owner;
        }

        return null;
    }

    protected AttackableUnit? GetGoldRedirectTarget(AttackableUnit? target)
    {
        if (target == null)
            return null;

        return !target.GoldOwner.Owner.Stats.IsDead ? target.GoldOwner.Owner : null;
    }

    protected bool IsAutoAcquireTargetEnabled()
    {
        return Me.AutoAttackAutoAcquireTarget;
    }

    protected bool CanSeeMe(AttackableUnit target)
    {
        //TODO: centralize nearsight
        return Me.IsVisibleByTeam(target.Team);
    }

    protected float GetPetReturnRadius(AttackableUnit pet)
    {
        if (_petReturnRadius != 0)
        {
            return _petReturnRadius;
        }
        return GlobalData.ObjAIBaseVariables.DefaultPetReturnRadius;
    }

    protected void SetLastPosPointWithObj(AttackableUnit target)
    {
        TargetPos = target.Position;
    }

    protected void SetPetReturnRadius(AttackableUnit owner, float returnRadius)
    {
        _petReturnRadius = returnRadius;
    }

    protected void SetStateAndMoveInPos(AIState state)
    {
        TargetPos = Me.LastIssueOrderPosition;
        SetState(state);
        if (TargetPos != null)
        {
            var order = GetMoveOrderForAiState(state);

            if (ShouldCancelAutoAttackForMoveOrderAndState(state, order))
            {
                TurnOffAutoAttack(StopReason.IMMEDIATELY);
            }

            Me.IssueOrDelayOrder(order, null, TargetPos.Value, fromAiScript: true);
        }
    }

    protected void SetStateAndMoveInPos(AIState state, Vector2 pos)
    {
        SetState(state);
        TargetPos = pos;

        var order = GetMoveOrderForAiState(state);

        if (ShouldCancelAutoAttackForMoveOrderAndState(state, order))
        {
            TurnOffAutoAttack(StopReason.IMMEDIATELY);
        }

        Me.IssueOrDelayOrder(order, Me.TargetUnit, pos, fromAiScript: true);
    }

    protected void AssignTargetPosInPos(Vector2 pos)
    {
        TargetPos = pos;
    }

    [IgnoreLuaMethod]
    private static OrderType GetMoveOrderForAiState(AIState state)
    {
        switch (state)
        {
            case AIState.AI_SOFTATTACK:
                return OrderType.AttackTo;
            case AIState.AI_HARDATTACK:
                return OrderType.AttackTo;
            case AIState.AI_ATTACKMOVESTATE:
                return OrderType.AttackMove;
            case AIState.AI_STANDING:
                return OrderType.Hold;
            case AIState.AI_GUARD:
                return OrderType.AttackMove;
            case AIState.AI_ATTACK:
                return OrderType.AttackTo;
            case AIState.AI_HARDIDLE:
                return OrderType.Hold;
            case AIState.AI_HARDIDLE_ATTACKING:
                return OrderType.AttackTo;
            case AIState.AI_TAUNTED:
                return OrderType.AttackTo;
            case AIState.AI_ATTACKMOVE_ATTACKING:
                return OrderType.AttackTo;
            case AIState.AI_ATTACK_GOING_TO_LAST_KNOWN_LOCATION:
                return OrderType.AttackMove;
            case AIState.AI_HALTED:
                return OrderType.Hold;
            case AIState.AI_ATTACK_HERO:
                return OrderType.AttackTo;
            case AIState.AI_PET_ATTACK:
                return OrderType.AttackTo;
            case AIState.AI_PET_ATTACKMOVE:
                return OrderType.AttackMove;
            case AIState.AI_PET_ATTACKMOVE_ATTACKING:
                return OrderType.AttackTo;
            case AIState.AI_PET_HARDATTACK:
                return OrderType.PetHardAttack;
            case AIState.AI_PET_HARDIDLE:
                return OrderType.PetHardStop;
            case AIState.AI_PET_HARDIDLE_ATTACKING:
                return OrderType.AttackTo;
            case AIState.AI_PET_HARDMOVE:
                return OrderType.PetHardMove;
            case AIState.AI_PET_HARDRETURN:
                return OrderType.PetHardReturn;
            case AIState.AI_PET_HARDSTOP:
                return OrderType.PetHardStop;
            case AIState.AI_PET_HOLDPOSITION:
                return OrderType.Hold;
            case AIState.AI_PET_HOLDPOSITION_ATTACKING:
                return OrderType.AttackTo;
            case AIState.AI_PET_RETURN_ATTACKING:
                return OrderType.AttackTo;
            case AIState.AI_STOP:
                return OrderType.Hold;
            default:
                return OrderType.MoveTo;
        }
    }

    [IgnoreLuaMethod]
    private static bool ShouldCancelAutoAttackForMoveOrderAndState(AIState state, OrderType order)
    {

        switch (state)
        {
            case AIState.AI_HARDIDLE_ATTACKING:
            case AIState.AI_ATTACKMOVE_ATTACKING:
            case AIState.AI_PET_HARDIDLE_ATTACKING:
            case AIState.AI_PET_HOLDPOSITION_ATTACKING:
            case AIState.AI_PET_RETURN_ATTACKING:
                return false;
        }

        switch (order)
        {
            case OrderType.OrderNone:
            case OrderType.Hold:
            case OrderType.MoveTo:
            case OrderType.PetHardMove:
            case OrderType.PetHardReturn:
            case OrderType.Stop:
            case OrderType.PetHardStop:
                return true;
            default:
                return false;
        }
    }

    protected class RunningTimer : IUpdate
    {
        private float _diff;
        private bool _hasTicked;

        public RunningTimer(Action callback, float interval, bool repeat, bool running)
        {
            Callback = callback;
            Interval = interval * 1000;
            Repeat = repeat;
            Running = running;
        }

        public Action Callback { get; }
        private float Interval { get; }
        private bool Repeat { get; }
        public bool Running { get; set; }

        public void Update()
        {
            if (!Running || (!Repeat && _hasTicked)) return;
            _diff += Game.Time.DeltaTime;

            if (_diff >= Interval)
            {
                _diff = 0;
                Callback();
                _hasTicked = true;
            }
        }

        public void Reset(bool run = true)
        {
            _diff = 0;
            Running = run;
        }
    }
}
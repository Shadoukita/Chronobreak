using System.Numerics;
using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings.Barracks;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.Logging;
using log4net;
using MoonSharp.Interpreter;
using Chronobreak.GameServer.Scripting.Lua;
using System.Linq;

namespace MapScripts;

public class LuaLevelScript : ILevelScript
{
    private static readonly ILog _logger = LoggerProvider.GetLogger();
    private Table? LevelScript;

    public float InitialSpawnTime { get; private set; }

    internal void OnScriptInit()
    {
        LevelScript = LuaScriptEngine.CreateTableReferringGlobal();
        LuaScriptEngine.DoScript("LevelScript.lua", LevelScript);
        DynValue initialTime = LevelScript?.Get("INITIAL_TIME_TO_SPAWN") ?? DynValue.NewNumber(0);
        InitialSpawnTime = (int)initialTime.Number;
    }

    public DynValue? CallLuaFunction(string functionName, params object[] args)
    {
        if (LevelScript is null)
        {
            return null;
        }

        try
        {
            return LevelScript?.Get(functionName)?.Function?.Call(args);
        }
        catch (ScriptRuntimeException e)
        {
            _logger.Error(e.DecoratedMessage);
            return null;
        }
    }

    public virtual void OnPostLevelLoad()
    {
        CallLuaFunction("OnPostLevelLoad");
    }
    public virtual void OnLevelInit()
    {
        CallLuaFunction("OnLevelInit");
    }
    public virtual void OnLevelInitServer()
    {
        CallLuaFunction("OnLevelInitServer");
    }
    public virtual void BarrackReactiveEvent(TeamId team, Lane lane)
    {
        CallLuaFunction("BarrackReactiveEvent", (uint)team, (int)lane);
    }
    public virtual void HandleDestroyedObject(AttackableUnit destroyed)
    {
        CallLuaFunction("HandleDestroyedObject", destroyed);
    }
    public virtual void DisableSuperMinions(TeamId team, Lane lane)
    {
        CallLuaFunction("DisableSuperMinions", (uint)team, (int)lane);
    }
    public virtual InitMinionSpawnInfo GetInitSpawnInfo(Lane lane, TeamId team)
    {
        InitMinionSpawnInfo toReturn = new();
        Table? SpawnInfo = CallLuaFunction("GetInitSpawnInfo", (int)lane, (uint)team)?.Table;

        if (SpawnInfo is null)
        {
            return toReturn;
        }

        toReturn.WaveSpawnInterval = (int)SpawnInfo.Get("WaveSpawnRate").Number;
        toReturn.MinionSpawnInterval = (int)SpawnInfo.Get("SingleMinionSpawnDelay").Number;
        toReturn.IsDestroyed = SpawnInfo.Get("IsDestroyed").Boolean;

        Table MinionInfoTable = SpawnInfo.Get("MinionInfoTable").Table;
        foreach (DynValue key in MinionInfoTable.Keys)
        {
            string minionType = key.String;
            Table MinionInfo = MinionInfoTable.Get(minionType).Table;
            string minionName = MinionInfo.Get("MinionName").String;
            Table DefaultInfo = MinionInfo.Get("DefaultInfo").Table;

            MinionData data = new()
            {
                //Check
                CoreName = minionName,
                //Check
                Name = minionType,

                NumToSpawnForWave = (int)DefaultInfo.Get("DefaultNumPerWave").Number,
                Armor = (int)DefaultInfo.Get("Armor").Number,
                GoldGiven = (int)DefaultInfo.Get("GoldGiven").Number,
                BonusAttack = (int)DefaultInfo.Get("DamageBonus").Number
            };

            toReturn.InitialMinionData.Add(minionType, data);
        }

        return toReturn;
    }
    public virtual MinionSpawnInfo GetMinionSpawnInfo(Lane lane, int waveCount, TeamId teamID)
    {
        MinionSpawnInfo toReturn = new();
        Table? RetValue = CallLuaFunction("GetMinionSpawnInfo", (int)lane, waveCount, 0 /*UNK*/, (int)teamID, -1 /*UNK*/)?.Table;

        if (RetValue is null)
        {
            return toReturn;
        }

        toReturn.IsDestroyed = RetValue.Get("IsDestroyed").Boolean;
        toReturn.ExperienceRadius = RetValue.Get("ExperienceRadius").ToObject<int>();
        toReturn.GoldRadius = RetValue.Get("GoldRadius").ToObject<int>();

        Table MinionInfoTable = RetValue.Get("MinionInfoTable").Table;
        foreach (DynValue key in MinionInfoTable.Keys)
        {
            string type = key.String;
            Table MinionInfo = MinionInfoTable.Get(type).Table;
            string name = MinionInfo.Get("MinionName").String;

            MinionData minionInfo = new()
            {
                CoreName = name,
                Name = type,
                NumToSpawnForWave = (int)MinionInfo.Get("NumPerWave").ToObject<int>(),
                Armor = MinionInfo.Get("Armor").ToObject<float>(),
                MagicResistance = MinionInfo.Get("MagicResistance").ToObject<float>(),
                BonusAttack = MinionInfo.Get("DamageBonus").ToObject<int>(),
                BonusHealth = MinionInfo.Get("HPBonus").ToObject<int>(),
                ExpGiven = MinionInfo.Get("ExpGiven").ToObject<float>(),
                GoldGiven = MinionInfo.Get("GoldGiven").ToObject<float>(),
                LocalGoldGiven = MinionInfo.Get("LocalGoldGiven").ToObject<float>()
            };

            toReturn.MinionData.Add(type, minionInfo);
        }

        Table SpawnOrderMinionNames = RetValue.Get("SpawnOrderMinionNames").Table;
        if (SpawnOrderMinionNames is null)
        {
            _logger.Error("No SpawnOrderMinionNames found from GetMinionSpawnInfo");
        }
        else
        {
            if (SpawnOrderMinionNames.Values.Any())
            {
                foreach (var minionTable in SpawnOrderMinionNames.Values)
                {
                    toReturn.MinionSpawnOrder.Add(minionTable.String);
                }
            }
        }

        return toReturn;
    }
    public virtual void InitializeNeutralMinionInfo()
    {
        CallLuaFunction("InitializeNeutralMinionInfo");
    }
    public virtual void NeutralMinionDeath(string minionName, ObjAIBase killer, Vector3 position)
    {
        CallLuaFunction("NeutralMinionDeath", minionName, killer, position);
    }
}

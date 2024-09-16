using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings.Barracks;
using MoonSharp.Interpreter;

namespace MapScripts;

//There's an extremely high chance(if not 100%) minions won't render at all on 4.20 clients, due to how they handle models/laneminion types
//This script could come in handy in child projects that try to run versions older than 4.20, that used the old barrack system
//95% of the code here is a translation from League's 1.0.0.126 decompiled client
public class LuaLevelScriptLegacyBarracks : LuaLevelScript
{
    //This callback doesn't exist in the old system, only `GetMinionSpawnInfo`, so inside it we actually call `GetMinionSpawnInfo`
    public override InitMinionSpawnInfo GetInitSpawnInfo(Lane lane, TeamId team)
    {
        InitMinionSpawnInfo toReturn = new();
        Table? retValue = CallLuaFunction("GetMinionSpawnInfo", (int)lane, -1, (int)team)?.Table;

        if (retValue is null)
        {
            return toReturn;
        }

        //Unhardcode?
        toReturn.InitialMinionData = new()
        {
            { "MELEE", new() },
            { "ARCHER", new() },
            { "CASTER", new() },
            { "SUPER", new() }
        };

        toReturn.WaveSpawnInterval = retValue.Get("WaveSpawnRate").ToObject<int>();
        toReturn.MinionSpawnInterval = retValue.Get("SingleMinionSpawnDelay").ToObject<int>();

        toReturn.InitialMinionData["MELEE"].NumToSpawnForWave = retValue.Get("NumOfMeleeMinionsPerWave").ToObject<int>();
        toReturn.InitialMinionData["ARCHER"].NumToSpawnForWave = retValue.Get("NumOfMeleeMinionsPerWave").ToObject<int>();
        toReturn.InitialMinionData["CASTER"].NumToSpawnForWave = retValue.Get("NumOfMeleeMinionsPerWave").ToObject<int>();
        toReturn.InitialMinionData["SUPER"].NumToSpawnForWave = retValue.Get("NumOfMeleeMinionsPerWave").ToObject<int>();

        toReturn.InitialMinionData["MELEE"].CoreName = retValue.Get("MeleeMinionName").String;
        toReturn.InitialMinionData["ARCHER"].CoreName = retValue.Get("ArcherMinionName").String;
        toReturn.InitialMinionData["CASTER"].CoreName = retValue.Get("CasterMinionName").String;
        toReturn.InitialMinionData["SUPER"].CoreName = retValue.Get("SuperMinionName").String;

        toReturn.IsDestroyed = retValue.Get("IsDestroyed").Boolean;

        toReturn.InitialMinionData["MELEE"].BonusHealth = retValue.Get("MeleeHPBonus").ToObject<int>();
        toReturn.InitialMinionData["MELEE"].BonusAttack = retValue.Get("MeleeDamageBonus").ToObject<int>();

        toReturn.InitialMinionData["ARCHER"].BonusHealth = retValue.Get("ArcherHPBonus").ToObject<int>();
        toReturn.InitialMinionData["ARCHER"].BonusAttack = retValue.Get("ArcherDamageBonus").ToObject<int>();

        toReturn.InitialMinionData["CASTER"].BonusHealth = retValue.Get("CasterHPBonus").ToObject<int>();
        toReturn.InitialMinionData["CASTER"].BonusAttack = retValue.Get("CasterDamageBonus").ToObject<int>();

        toReturn.InitialMinionData["SUPER"].BonusHealth = retValue.Get("SuperHPBonus").ToObject<int>();
        toReturn.InitialMinionData["SUPER"].BonusAttack = retValue.Get("SuperDamageBonus").ToObject<int>();

        toReturn.InitialMinionData["MELEE"].Armor = retValue.Get("MeleeMinionArmor").ToObject<int>();
        toReturn.InitialMinionData["ARCHER"].Armor = retValue.Get("ArcherMinionArmor").ToObject<int>();
        toReturn.InitialMinionData["CASTER"].Armor = retValue.Get("CasterMinionArmor").ToObject<int>();
        toReturn.InitialMinionData["SUPER"].Armor = retValue.Get("SuperMinionArmor").ToObject<int>();

        toReturn.InitialMinionData["MELEE"].MagicResistance = retValue.Get("MeleeMinionMagicResistance").ToObject<int>();
        toReturn.InitialMinionData["ARCHER"].MagicResistance = retValue.Get("ArcherMinionMagicResistance").ToObject<int>();
        toReturn.InitialMinionData["CASTER"].MagicResistance = retValue.Get("CasterMinionMagicResistance").ToObject<int>();
        toReturn.InitialMinionData["SUPER"].MagicResistance = retValue.Get("SuperMinionMagicResistance").ToObject<int>();

        //Custom
        toReturn.InitialMinionData["MELEE"].SpawnTypeOverride = true;
        toReturn.InitialMinionData["ARCHER"].SpawnTypeOverride = true;
        toReturn.InitialMinionData["CASTER"].SpawnTypeOverride = true;
        toReturn.InitialMinionData["SUPER"].SpawnTypeOverride = true;

        return toReturn;
    }

    public override MinionSpawnInfo GetMinionSpawnInfo(Lane lane, int waveCount, TeamId teamID)
    {
        MinionSpawnInfo toReturn = new();
        Table? retValue = CallLuaFunction("GetMinionSpawnInfo", (int)lane, waveCount, (int)teamID)?.Table;

        if (retValue is null)
        {
            return toReturn;
        }

        //Unhardcode?
        toReturn.MinionData = new()
        {
            { "MELEE", new() },
            { "ARCHER", new() },
            { "CASTER", new() },
            { "SUPER", new() }
        };

        //Unhardcode?
        toReturn.MinionSpawnOrder =
        [
            "SUPER",
            "MELEE",
            "ARCHER",
            "CASTER"
        ];

        toReturn.MinionData["MELEE"].NumToSpawnForWave = retValue.Get("NumOfMeleeMinionsPerWave").ToObject<int>();
        toReturn.MinionData["ARCHER"].NumToSpawnForWave = retValue.Get("NumOfArcherMinionsPerWave").ToObject<int>();
        toReturn.MinionData["CASTER"].NumToSpawnForWave = retValue.Get("NumOfCasterMinionsPerWave").ToObject<int>();
        toReturn.MinionData["SUPER"].NumToSpawnForWave = retValue.Get("NumOfSuperMinionsPerWave").ToObject<int>();

        toReturn.IsDestroyed = retValue.Get("IsDestroyed").Boolean;

        toReturn.MinionData["MELEE"].ExpGiven = retValue.Get("MeleeExpGiven").ToObject<int>();
        toReturn.MinionData["MELEE"].GoldGiven = retValue.Get("MeleeGoldGiven").ToObject<int>();
        toReturn.MinionData["MELEE"].BonusHealth = retValue.Get("MeleeHPBonus").ToObject<int>();
        toReturn.MinionData["MELEE"].BonusAttack = retValue.Get("MeleeDamageBonus").ToObject<int>();

        toReturn.MinionData["ARCHER"].ExpGiven = retValue.Get("ArcherExpGiven").ToObject<int>();
        toReturn.MinionData["ARCHER"].GoldGiven = retValue.Get("ArcherGoldGiven").ToObject<int>();
        toReturn.MinionData["ARCHER"].BonusHealth = retValue.Get("ArcherHPBonus").ToObject<int>();
        toReturn.MinionData["ARCHER"].BonusAttack = retValue.Get("ArcherDamageBonus").ToObject<int>();

        toReturn.MinionData["CASTER"].ExpGiven = retValue.Get("CasterExpGiven").ToObject<int>();
        toReturn.MinionData["CASTER"].GoldGiven = retValue.Get("CasterGoldGiven").ToObject<int>();
        toReturn.MinionData["CASTER"].BonusHealth = retValue.Get("CasterHPBonus").ToObject<int>();
        toReturn.MinionData["CASTER"].BonusAttack = retValue.Get("CasterDamageBonus").ToObject<int>();

        toReturn.MinionData["SUPER"].ExpGiven = retValue.Get("SuperExpGiven").ToObject<int>();
        toReturn.MinionData["SUPER"].GoldGiven = retValue.Get("SuperGoldGiven").ToObject<int>();
        toReturn.MinionData["SUPER"].BonusHealth = retValue.Get("SuperHPBonus").ToObject<int>();
        toReturn.MinionData["SUPER"].BonusAttack = retValue.Get("SuperDamageBonus").ToObject<int>();

        toReturn.ExperienceRadius = retValue.Get("ExperienceRadius").ToObject<int>();

        toReturn.MinionData["MELEE"].Armor = retValue.Get("MeleeMinionArmor").ToObject<int>();
        toReturn.MinionData["ARCHER"].Armor = retValue.Get("ArcherMinionArmor").ToObject<int>();
        toReturn.MinionData["CASTER"].Armor = retValue.Get("CasterMinionArmor").ToObject<int>();
        toReturn.MinionData["SUPER"].Armor = retValue.Get("SuperMinionArmor").ToObject<int>();

        toReturn.MinionData["MELEE"].MagicResistance = retValue.Get("MeleeMinionMagicResistance").ToObject<int>();
        toReturn.MinionData["ARCHER"].MagicResistance = retValue.Get("ArcherMinionMagicResistance").ToObject<int>();
        toReturn.MinionData["CASTER"].MagicResistance = retValue.Get("CasterMinionMagicResistance").ToObject<int>();
        toReturn.MinionData["SUPER"].MagicResistance = retValue.Get("SuperMinionMagicResistance").ToObject<int>();

        //Custom?
        toReturn.MinionData["MELEE"].CoreName = retValue.Get("MeleeMinionName").String;
        toReturn.MinionData["ARCHER"].CoreName = retValue.Get("ArcherMinionName").String;
        toReturn.MinionData["CASTER"].CoreName = retValue.Get("CasterMinionName").String;
        toReturn.MinionData["SUPER"].CoreName = retValue.Get("SuperMinionName").String;

        toReturn.MinionData["MELEE"].SpawnTypeOverride = true;
        toReturn.MinionData["ARCHER"].SpawnTypeOverride = true;
        toReturn.MinionData["CASTER"].SpawnTypeOverride = true;
        toReturn.MinionData["SUPER"].SpawnTypeOverride = true;

        return toReturn;
    }
}

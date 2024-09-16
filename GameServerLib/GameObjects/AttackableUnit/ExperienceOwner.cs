using Chronobreak.GameServer;
using Chronobreak.GameServer.API;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.Logging;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameServerLib.GameObjects;

public interface IExperienceOwner
{
    Experience Experience { get; }
}

public class Experience
{
    private static readonly ILog _logger = LoggerProvider.GetLogger();

    private const float EvilUnkownLevelDifferenceBonusExpMultiplier = 0.1455f;

    public float Exp;
    public int Level = 1;
    public AttackableUnit Owner;
    public readonly SpellTrainingPoints SpellTrainingPoints;
    readonly List<float> ExpNeededPerLevel = Game.Map.MapData.ExpCurve;
    public readonly int LevelCap = Game.Map.GameMode.MapScriptMetadata.MaxLevel;
    public float BaseExpMultiplier = Game.Map.MapData.BaseExpMultiple;
    public float LevelDifferenceExpMultiplier = Game.Map.MapData.LevelDifferenceExpMultiple;
    public float MinimumExpMultiplier = Game.Map.MapData.MinimumExpMultiple;

    public Experience(AttackableUnit owner)
    {
        Owner = owner;
        SpellTrainingPoints = new();
    }

    public float GetEXPGrantedFromChampion(Champion c)
    {
        int cLevel = c.Experience.Level;
        float EXP = (c.Experience.ExpNeededPerLevel[cLevel] - c.Experience.ExpNeededPerLevel[cLevel - 1]) * BaseExpMultiplier;

        float levelDifference = cLevel - Level;

        if (cLevel < 0)
        {
            EXP -= EXP * MathF.Min(LevelDifferenceExpMultiplier * levelDifference, MinimumExpMultiplier);
        }
        else if (cLevel > 0)
        {
            EXP += EXP * (levelDifference * EvilUnkownLevelDifferenceBonusExpMultiplier);
        }

        return EXP;
    }

    public void AddEXP(float toAdd, bool notify = true, bool baseExpMultiplier = false)
    {
        //Check
        if (baseExpMultiplier)
        {
            toAdd *= BaseExpMultiplier;
        }

        Exp = Math.Clamp(Exp + toAdd, 0, ExpNeededPerLevel.LastOrDefault());

        if (notify && Owner is Champion c)
        {
            Game.PacketNotifier.NotifyUnitAddEXP(c, toAdd);
        }

        byte levelsToUpgrade = 0;
        while (Exp >= ExpNeededPerLevel.ElementAtOrDefault(Level + levelsToUpgrade) && Level + levelsToUpgrade < LevelCap)
        {
            levelsToUpgrade++;
        }

        if (levelsToUpgrade > 0)
        {
            LevelUp(levelsToUpgrade);
        }
    }

    public void LevelUp(byte ammount = 1)
    {
        int oldLevel = Level;
        Level = Math.Clamp(Level + ammount, 0, LevelCap);

        if (Level - oldLevel > 0)
        {
            if (Owner is Champion c)
            {
                c.StatsLevelUp(oldLevel, ammount);
                SpellTrainingPoints.AddTrainingPoints(ammount);
                Game.PacketNotifier.NotifyNPC_LevelUp(c);

                //Direct replication call is probably a hack
                //TODO: Check why this is here
                c.Replication.Update();
                Game.PacketNotifier.NotifyOnReplication(c, partial: false);

                ApiEventManager.OnLevelUp.Publish(c);

                _logger.Debug($"Experience Owner {c.Name} leveled up to {Level}");
            }
        }
    }
}


public class SpellTrainingPoints
{
    public byte TrainingPoints { get; private set; }
    public byte TotalTrainingPoints { get; private set; }
    public SpellTrainingPoints()
    {
        TrainingPoints = 1;
    }

    public void AddTrainingPoints(byte ammount = 1)
    {
        TrainingPoints += ammount;
        TotalTrainingPoints += ammount;
    }

    public void SpendTrainingPoint()
    {
        TrainingPoints--;
    }
}

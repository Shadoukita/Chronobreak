using System.Collections.Generic;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.Logging;
using log4net;

namespace Chronobreak.GameServer.Inventory;

public class TalentInventory
{
    public readonly Dictionary<string, Talent> Talents;
    private static readonly ILog _logger = LoggerProvider.GetLogger();

    public void Activate(ObjAIBase owner)
    {
        foreach (var talent in Talents.Values)
        {
            talent.Script.OnActivate(owner, talent.Rank);
        }
    }

    public TalentInventory()
    {
        Talents = new(80);
    }

    public TalentInventory(Dictionary<int, int>? talentsConfig) : this()
    {
        foreach (var pair in talentsConfig ?? [])
        {
            byte level;
            if (pair.Value > 0 && pair.Value <= 255)
            {
                level = (byte)pair.Value;
            }
            else
            {
                _logger.Warn(
                $"Invalid Talent Rank for Talent {pair.Key}! " +
                $"Please use ranks between 1 and {byte.MaxValue}! " +
                "Defaulting to Rank 1...");
                level = 1;
            }

            TalentData? data = ContentManager.GetTalentData(pair.Key.ToString("D")) ?? new(pair.Key.ToString());

            if (data is null)
            {
                _logger.Warn($"Invalid Talent Id: {pair.Key}");
                continue;
            }

            if (!Talents.TryAdd(data.Id, new Talent(data, level)))
            {
                _logger.Warn($"Talent {data.Id} cannot not be added to Inventory twice!");
            }
        }
    }
}
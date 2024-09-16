using System.Collections.Generic;

namespace Chronobreak.GameServer.Inventory;

public class RuneInventory
{
    public Dictionary<int, int> Runes { get; }

    public RuneInventory()
    {
        Runes = [];
    }

    public RuneInventory(Dictionary<int, int>? runesConfig) : this()
    {
        foreach (var pair in runesConfig ?? [])
        {
            Runes.Add(pair.Key, pair.Value);
        }
    }

    public void Add(int runeSlotId, int runeId)
    {
        Runes.Add(runeSlotId, runeId);
    }
}

using Chronobreak.GameServer;
using System;
using Chronobreak.GameServer.GameObjects;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects.AttackableUnits;

namespace GameServerLib.GameObjects;

public interface IGoldOwner
{
    GoldOwner GoldOwner { get; }
}

public class GoldOwner
{
    const float MAX_GOLD_AMMOUNT = 100000.0f;
    public float Gold { get; internal set; }
    public float TotalGoldEarned { get; internal set; }
    public float TotalGoldSpent { get; internal set; }
    public AttackableUnit Owner { get; init; }

    public GoldOwner(AttackableUnit owner)
    {
        Owner = owner;
        AddGold(GlobalData.ObjAIBaseVariables.StartingGold, false);
    }

    public void AddGold(float gold, bool notify = true, GameObject source = null)
    {
        float oldGold = Gold;
        Gold = Math.Min(Gold + gold, MAX_GOLD_AMMOUNT);
        TotalGoldEarned += Gold - oldGold;

        if (notify && Owner is Champion c)
        {
            source ??= c;
            Game.PacketNotifier.NotifyUnitAddGold(c, source, Gold - oldGold);
        }
    }

    internal void SpendGold(float gold)
    {
        Gold -= gold;
        TotalGoldSpent += gold;
    }
}

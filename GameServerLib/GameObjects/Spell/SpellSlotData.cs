using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using System.Collections.Generic;
using System.Linq;

namespace Chronobreak.GameServer.GameObjects.SpellNS;

public partial class Spell
{
    private int ClientId => (Caster as Champion)?.ClientId ?? -1;

    private int _iconIndex = 0;
    public int IconIndex
    {
        get => _iconIndex;
        set
        {
            if (_iconIndex != value)
            {
                _iconIndex = value;
                if (ClientId != -1)
                {
                    Game.PacketNotifier.NotifyChangeSlotSpellData
                    (
                        ClientId, Caster, Slot,
                        ChangeSlotSpellDataType.IconIndex, newIconIndex: value
                    );
                }
            }
        }
    }

    public bool Sealed { get; set; }

    private bool _enabled = false;
    public bool Enabled
    {
        get => _enabled;
        set
        {
            if (_enabled != value)
            {
                _enabled = value;

                if (IsVisibleSpell)
                {
                    if (IsSummonerSpell)
                    {
                        Caster.Stats.SetSummonerSpellEnabled((byte)Slot, _enabled);
                    }
                    else
                    {
                        Caster.Stats.SetSpellEnabled((byte)Slot, _enabled);
                    }
                }
            }
        }
    }

    private TargetingType _targetingType;
    public TargetingType TargetingType
    {
        get => _targetingType;
        set
        {
            if (_targetingType != value)
            {
                _targetingType = value;
                if (ClientId != -1)
                {
                    Game.PacketNotifier.NotifyChangeSlotSpellData(ClientId, Caster, Slot, ChangeSlotSpellDataType.TargetingType, targetingType: value);
                }
            }
        }
    }

    public float CastRange => GetCastRange(Level); //TODO: Networking
    public float GetCastRange(int level)
    {
        //TODO: Check
        if (Data.CastRange[0] == Data.CastRange[1])
        {
            return GetByLevel(Data.CastRange, level);
        }
        return Data.CastRange.First();
    }

    public List<AttackableUnit> OffsetTargets = [];
    public void ChangeOffsetTarget(AttackableUnit unit, bool isAdding = true, bool removeAll = false)
    {
        if (removeAll)
        {
            OffsetTargets.Clear();
        }

        if (isAdding)
        {
            if (!OffsetTargets.Contains(unit))
            {
                OffsetTargets.Add(unit);
            }
        }
        else
        {
            OffsetTargets.Remove(unit);
        }
        Game.PacketNotifier.NotifyChangeSlotSpellData(ClientId, Caster, Slot, ChangeSlotSpellDataType.OffsetTarget, IsSummonerSpell, offsetTargets: OffsetTargets.Select(u => u.NetId).ToList());
    }

    public void ChangeOffsetTargetList(List<AttackableUnit> newList)
    {
        OffsetTargets.Clear();
        OffsetTargets.AddRange(newList);
        Game.PacketNotifier.NotifyChangeSlotSpellData(ClientId, Caster, Slot, ChangeSlotSpellDataType.OffsetTarget, IsSummonerSpell, offsetTargets: OffsetTargets.Select(u => u.NetId).ToList());
    }
}
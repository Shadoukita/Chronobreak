namespace ItemPassives
{
    public class ItemID_3037 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                if (!IsDead(owner))
                {
                    float nextBuffVars_ManaRegenBonus = 1.44f;
                    if (owner is Champion)
                    {
                        foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                        {
                            if (unit == owner)
                            {
                                AddBuff(attacker, unit, new Buffs.ManaManipulatorAuraSelf(nextBuffVars_ManaRegenBonus), 1, 1, 1.1f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                            }
                            else
                            {
                                AddBuff(attacker, unit, new Buffs.ManaManipulatorAuraFriend(nextBuffVars_ManaRegenBonus), 1, 1, 1.1f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                            }
                        }
                    }
                    else
                    {
                        foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                        {
                            ObjAIBase caster = GetPetOwner((Pet)owner);
                            if (unit == owner)
                            {
                                if (GetBuffCountFromCaster(owner, caster, nameof(Buffs.ManaManipulatorAuraFriend)) == 0)
                                {
                                    AddBuff(attacker, unit, new Buffs.ManaManipulatorAuraSelf(nextBuffVars_ManaRegenBonus), 1, 1, 4.1f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                                }
                            }
                            else if (unit != caster)
                            {
                                AddBuff(attacker, unit, new Buffs.ManaManipulatorAuraFriend(nextBuffVars_ManaRegenBonus), 1, 1, 4.1f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                            }
                        }
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class _3037 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
        EffectEmitter manaManipulator;
        public override void OnActivate()
        {
            SpellEffectCreate(out manaManipulator, out _, "ZettasManaManipulator_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, caster.Team, default, default, true, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(manaManipulator);
        }
    }
}
namespace ItemPassives
{
    public class ItemID_3099 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                if (!IsDead(owner))
                {
                    float nextBuffVars_ManaRegenMod = 2.4f;
                    float nextBuffVars_CooldownReduction = -0.1f;
                    if (owner is Champion)
                    {
                        foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                        {
                            if (unit == owner)
                            {
                                AddBuff(owner, unit, new Buffs.SoulShroudAuraSelf(nextBuffVars_ManaRegenMod, nextBuffVars_CooldownReduction), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                            }
                            else
                            {
                                AddBuff(owner, unit, new Buffs.SoulShroudAuraFriend(nextBuffVars_ManaRegenMod, nextBuffVars_CooldownReduction), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
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
                                if (GetBuffCountFromCaster(owner, caster, nameof(Buffs.SoulShroudAuraFriend)) == 0)
                                {
                                    AddBuff(owner, unit, new Buffs.SoulShroudAuraSelf(nextBuffVars_ManaRegenMod, nextBuffVars_CooldownReduction), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                                }
                            }
                            if (unit == owner)
                            {
                                AddBuff(owner, unit, new Buffs.SoulShroudAuraSelf(nextBuffVars_ManaRegenMod, nextBuffVars_CooldownReduction), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                            }
                            else if (unit != caster)
                            {
                                AddBuff(owner, unit, new Buffs.SoulShroudAuraFriend(nextBuffVars_ManaRegenMod, nextBuffVars_CooldownReduction), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
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
    public class _3099 : BuffScript
    {
        EffectEmitter soulShroudParticle;
        public override void OnActivate()
        {
            SpellEffectCreate(out soulShroudParticle, out _, "ZettasManaManipulator_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, caster.Team, default, default, true, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(soulShroudParticle);
        }
    }
}
namespace ItemPassives
{
    public class ItemID_3050 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                if (!IsDead(owner))
                {
                    float nextBuffVars_LifeStealMod = 0.2f;
                    float nextBuffVars_AttackSpeedMod = 0.2f;
                    int nextBuffVars_HealthRegenMod = 6;
                    if (owner is Champion)
                    {
                        foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                        {
                            if (unit == owner)
                            {
                                AddBuff(owner, unit, new Buffs.RallyingBannerAuraSelf(nextBuffVars_LifeStealMod, nextBuffVars_AttackSpeedMod, nextBuffVars_HealthRegenMod), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                            }
                            else
                            {
                                AddBuff(owner, unit, new Buffs.RallyingBannerAuraFriend(nextBuffVars_LifeStealMod, nextBuffVars_AttackSpeedMod, nextBuffVars_HealthRegenMod), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
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
                                if (GetBuffCountFromCaster(owner, caster, nameof(Buffs.RallyingBannerAuraFriend)) == 0)
                                {
                                    AddBuff(owner, unit, new Buffs.RallyingBannerAuraSelf(nextBuffVars_LifeStealMod, nextBuffVars_AttackSpeedMod, nextBuffVars_HealthRegenMod), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                                }
                            }
                            else if (unit != caster)
                            {
                                AddBuff(owner, unit, new Buffs.RallyingBannerAuraFriend(nextBuffVars_LifeStealMod, nextBuffVars_AttackSpeedMod, nextBuffVars_HealthRegenMod), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                            }
                        }
                    }
                    int nextBuffVars_ArmorMod = -20;
                    foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.RallyingBanner), false))
                    {
                        AddBuff(owner, unit, new Buffs.RallyingBanner(nextBuffVars_ArmorMod), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.SHRED, 0, true, true, false);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class _3050 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Stark's Fervor Aura",
            BuffTextureName = "3050_Rallying_Banner.dds",
        };
        EffectEmitter starkSelfParticle;
        public override void OnActivate()
        {
            SpellEffectCreate(out starkSelfParticle, out _, "RallyingBanner_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            if (owner.Team == TeamId.TEAM_ORDER)
            {
                SpellEffectCreate(out starkSelfParticle, out _, "RallyingBanner_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_ORDER, default, owner, true, owner, default, default, owner, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out starkSelfParticle, out _, "RallyingBanner_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_CHAOS, default, default, true, owner, default, default, owner, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(starkSelfParticle);
        }
    }
}
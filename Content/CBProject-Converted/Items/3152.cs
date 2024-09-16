namespace ItemPassives
{
    public class ItemID_3152 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                int nextBuffVars_AP_Buff = 30;
                float nextBuffVars_SpellVamp_Buff = 0.25f;
                if (owner is Champion)
                {
                    foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                    {
                        if (unit == owner)
                        {
                            AddBuff(owner, unit, new Buffs.WillOfTheAncientsSelf(nextBuffVars_AP_Buff, nextBuffVars_SpellVamp_Buff), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                        else
                        {
                            if (!IsDead(owner))
                            {
                                AddBuff(owner, unit, new Buffs.WillOfTheAncientsFriendly(nextBuffVars_AP_Buff, nextBuffVars_SpellVamp_Buff), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                            }
                        }
                    }
                }
                else
                {
                    if (!IsDead(owner))
                    {
                        foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                        {
                            ObjAIBase caster = GetPetOwner((Pet)owner);
                            if (unit == owner)
                            {
                                if (GetBuffCountFromCaster(owner, caster, nameof(Buffs.WillOfTheAncientsFriendly)) == 0)
                                {
                                    AddBuff(owner, unit, new Buffs.WillOfTheAncientsSelf(nextBuffVars_AP_Buff, nextBuffVars_SpellVamp_Buff), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                                }
                            }
                            if (unit == owner)
                            {
                            }
                            else if (unit != caster)
                            {
                                AddBuff(owner, unit, new Buffs.WillOfTheAncientsFriendly(nextBuffVars_AP_Buff, nextBuffVars_SpellVamp_Buff), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
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
    public class _3152 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "WillOftheAncients",
            BuffTextureName = "3050_Rallying_Banner.dds",
        };
        EffectEmitter starkSelfParticle;
        public override void OnActivate()
        {
            SpellEffectCreate(out starkSelfParticle, out _, "RallyingBanner_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(starkSelfParticle);
        }
    }
}
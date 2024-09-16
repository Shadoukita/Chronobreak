namespace ItemPassives
{
    public class ItemID_3097 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                if (!IsDead(owner))
                {
                    if (owner is Champion)
                    {
                        foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                        {
                            if (owner == unit)
                            {
                                AddBuff(owner, unit, new Buffs.EmblemOfValorParticle(), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                            }
                            else
                            {
                                AddBuff(owner, unit, new Buffs.EmblemOfValor(), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
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
                                if (GetBuffCountFromCaster(owner, caster, nameof(Buffs.EmblemOfValor)) == 0)
                                {
                                    AddBuff(owner, unit, new Buffs.EmblemOfValorParticle(), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                                }
                            }
                            if (unit == owner)
                            {
                                AddBuff(owner, unit, new Buffs.EmblemOfValorParticle(), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                            }
                            else if (unit != caster)
                            {
                                AddBuff(owner, unit, new Buffs.EmblemOfValor(), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
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
    public class _3097 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
        EffectEmitter emblemParticle;
        public override void OnActivate()
        {
            SpellEffectCreate(out emblemParticle, out _, "RallyingBanner_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, caster.Team, default, default, true, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(emblemParticle);
        }
    }
}
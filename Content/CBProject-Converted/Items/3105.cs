namespace ItemPassives
{
    public class ItemID_3105 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                int nextBuffVars_MagicResistBonus = 15;
                int nextBuffVars_ArmorBonus = 12;
                int nextBuffVars_DamageBonus = 8;
                if (owner is Champion)
                {
                    foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                    {
                        if (unit == owner)
                        {
                            AddBuff(attacker, unit, new Buffs.AegisoftheLegionAuraSelf(nextBuffVars_MagicResistBonus, nextBuffVars_ArmorBonus, nextBuffVars_DamageBonus), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                        else
                        {
                            if (!IsDead(owner))
                            {
                                AddBuff(attacker, unit, new Buffs.AegisoftheLegionAuraFriend(nextBuffVars_MagicResistBonus, nextBuffVars_ArmorBonus, nextBuffVars_DamageBonus), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
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
                                if (GetBuffCountFromCaster(owner, caster, nameof(Buffs.AegisoftheLegionAuraFriend)) == 0)
                                {
                                    AddBuff(attacker, unit, new Buffs.AegisoftheLegionAuraSelf(nextBuffVars_MagicResistBonus, nextBuffVars_ArmorBonus, nextBuffVars_DamageBonus), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                                }
                            }
                            else if (unit != caster)
                            {
                                AddBuff(attacker, unit, new Buffs.AegisoftheLegionAuraFriend(nextBuffVars_MagicResistBonus, nextBuffVars_ArmorBonus, nextBuffVars_DamageBonus), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
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
    public class _3105 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
        EffectEmitter aegis;
        public override void OnActivate()
        {
            SpellEffectCreate(out aegis, out _, "ZettasManaManipulator_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, caster.Team, default, default, true, owner, default, default, owner, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(aegis);
        }
    }
}
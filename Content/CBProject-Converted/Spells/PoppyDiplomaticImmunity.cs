namespace Spells
{
    public class PoppyDiplomaticImmunity : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 6, 7, 8 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            AddBuff(owner, target, new Buffs.PoppyDITarget(), 1, 1, effect0[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false);
            AddBuff(owner, owner, new Buffs.PoppyDITargetDmg(), 1, 1, effect0[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class PoppyDiplomaticImmunity : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            AutoBuffActivateEvent = "",
            BuffName = "PoppyDiplomaticImmunity",
            BuffTextureName = "Poppy_DiplomaticImmunity.dds",
        };
        EffectEmitter particle;
        EffectEmitter particle2;
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            return
            owner.Team == attacker.Team ||
            GetBuffCountFromCaster(attacker, owner, nameof(Buffs.PoppyDITarget)) > 0 ||
            type == BuffType.COMBAT_ENCHANCER;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            ObjAIBase caster = GetBuffCasterUnit();
            if (teamOfOwner == TeamId.TEAM_ORDER)
            {
                SpellEffectCreate(out particle, out _, "DiplomaticImmunity_buf.troy", default, TeamId.TEAM_ORDER, 500, 0, TeamId.TEAM_ORDER, default, default, true, owner, default, default, owner, default, default, false);
                foreach (Champion unit in GetChampions(TeamId.TEAM_CHAOS, default, true))
                {
                    if (unit == caster)
                    {
                        SpellEffectCreate(out particle2, out _, "DiplomaticImmunity_tar.troy", default, TeamId.TEAM_ORDER, 500, 0, TeamId.TEAM_CHAOS, default, unit, true, owner, default, default, owner, default, default, false);
                    }
                    else
                    {
                        SpellEffectCreate(out particle2, out _, "DiplomaticImmunity_buf.troy", default, TeamId.TEAM_ORDER, 500, 0, TeamId.TEAM_CHAOS, default, unit, true, owner, default, default, owner, default, default, false);
                    }
                }
            }
            else
            {
                SpellEffectCreate(out particle, out _, "DiplomaticImmunity_buf.troy", default, TeamId.TEAM_CHAOS, 500, 0, TeamId.TEAM_CHAOS, default, default, true, owner, default, default, owner, default, default, false);
                foreach (Champion unit in GetChampions(TeamId.TEAM_ORDER, default, true))
                {
                    if (unit == caster)
                    {
                        SpellEffectCreate(out particle2, out _, "DiplomaticImmunity_tar.troy", default, TeamId.TEAM_CHAOS, 500, 0, TeamId.TEAM_ORDER, default, unit, true, owner, default, default, owner, default, default, false);
                    }
                    else
                    {
                        SpellEffectCreate(out particle2, out _, "DiplomaticImmunity_buf.troy", default, TeamId.TEAM_ORDER, 500, 0, TeamId.TEAM_ORDER, default, unit, true, owner, default, default, owner, default, default, false);
                    }
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (GetBuffCountFromCaster(attacker, owner, nameof(Buffs.PoppyDITarget)) == 0)
            {
                damageAmount -= damageAmount;
            }
        }
    }
}
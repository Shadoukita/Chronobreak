namespace Spells
{
    public class CardmasterStack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class CardmasterStack : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "CardMasterStack",
            BuffTextureName = "Cardmaster_RapidToss.dds",
            IsDeathRecapSource = true,
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float bonusDamage;
        float cooldownBonus;
        float attackSpeedBonus;
        public CardmasterStack(float bonusDamage = default, float cooldownBonus = default, float attackSpeedBonus = default)
        {
            this.bonusDamage = bonusDamage;
            this.cooldownBonus = cooldownBonus;
            this.attackSpeedBonus = attackSpeedBonus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.cooldownBonus);
            //RequireVar(this.bonusDamage);
            //RequireVar(this.attackSpeedBonus);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, attackSpeedBonus);
            IncPercentCooldownMod(owner, cooldownBonus);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not BaseTurret)
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.CardmasterStackParticle)) > 0)
                {
                    SpellBuffRemove(owner, nameof(Buffs.CardmasterStackParticle), (ObjAIBase)owner);
                    TeamId teamID = GetTeamID_CS(owner);
                    BreakSpellShields(target);
                    ApplyDamage(attacker, target, bonusDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.4f, 1, false, false, attacker);
                    SpellEffectCreate(out _, out _, "CardmasterStackAttack_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false);
                }
                else
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.CardMasterStackHolder(), 4, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
        }
    }
}
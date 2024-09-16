namespace Spells
{
    public class ShenFeint : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 50, 100, 150, 200, 250 };
        float[] effect1 = { 2.5f, 2.5f, 2.5f, 2.5f, 2.5f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float baseDamageBlock = effect0[level - 1];
            float abilityPower = GetFlatMagicDamageMod(owner);
            float bonusHealth = abilityPower * 0.75f;
            float damageBlock = baseDamageBlock + bonusHealth;
            float nextBuffVars_DamageBlock = damageBlock;
            AddBuff(attacker, target, new Buffs.ShenFeint(nextBuffVars_DamageBlock), 1, 1, effect1[level - 1], BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class ShenFeint : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "shen_Feint_self.troy", },
            BuffName = "Shen Feint Buff",
            BuffTextureName = "Shen_Feint.dds",
            OnPreDamagePriority = 3,
        };
        float damageBlock;
        bool willRemove;
        float oldArmorAmount;
        public ShenFeint(float damageBlock = default, bool willRemove = default)
        {
            this.damageBlock = damageBlock;
            this.willRemove = willRemove;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageBlock);
            IncreaseShield(owner, damageBlock, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            if (!willRemove)
            {
                SpellEffectCreate(out _, out _, "shen_Feint_self_deactivate.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
            }
            if (damageBlock > 0)
            {
                RemoveShield(owner, damageBlock, true, true);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            oldArmorAmount = damageBlock;
            if (damageBlock >= damageAmount)
            {
                damageBlock -= damageAmount;
                damageAmount = 0;
                oldArmorAmount -= damageBlock;
                ReduceShield(owner, oldArmorAmount, true, true);
            }
            else
            {
                TeamId teamID = GetTeamID_CS(owner);
                damageAmount -= damageBlock;
                damageBlock = 0;
                ReduceShield(owner, oldArmorAmount, true, true);
                SpellEffectCreate(out _, out _, "shen_Feint_block.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}
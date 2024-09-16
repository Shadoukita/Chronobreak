namespace Spells
{
    public class SkarnerExoskeleton : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 70, 115, 160, 205, 250 };
        float[] effect1 = { 0.15f, 0.17f, 0.19f, 0.21f, 0.23f };
        float[] effect2 = { 0.3f, 0.35f, 0.4f, 0.45f, 0.5f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float baseDamageBlock = effect0[level - 1];
            PlayAnimation("Spell2", 0, owner, false, false, false);
            AddBuff(owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            float abilityPower = GetFlatMagicDamageMod(owner);
            float bonusHealth = abilityPower * 0.6f;
            float damageBlock = baseDamageBlock + bonusHealth;
            float nextBuffVars_DamageBlock = damageBlock;
            float nextBuffVars_MSBonus = effect1[level - 1];
            float nextBuffVars_ASBonus = effect2[level - 1];
            AddBuff(owner, owner, new Buffs.SkarnerExoskeleton(nextBuffVars_DamageBlock, nextBuffVars_MSBonus, nextBuffVars_ASBonus), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class SkarnerExoskeleton : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "R_sub_hand", "L_sub_hand", "chest", "tail", },
            AutoBuffActivateEffect = new[] { "Skarner_Exoskeleton_buf_r_arm.troy", "Skarner_Exoskeleton_buf_l_arm.troy", "Skarner_Exoskeleton_body.troy", "Skarner_Exoskeleton_tail.troy", },
            BuffName = "SkarnerExoskeleton",
            BuffTextureName = "SkarnerExoskeleton.dds",
        };
        float damageBlock;
        float mSBonus;
        float aSBonus;
        EffectEmitter partname; // UNUSED
        float oldArmorAmount;
        public SkarnerExoskeleton(float damageBlock = default, float mSBonus = default, float aSBonus = default)
        {
            this.damageBlock = damageBlock;
            this.mSBonus = mSBonus;
            this.aSBonus = aSBonus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageBlock);
            //RequireVar(this.mSBonus);
            //RequireVar(this.aSBonus);
            IncreaseShield(owner, damageBlock, true, true);
            IncPercentMovementSpeedMod(owner, mSBonus);
            IncPercentAttackSpeedMod(owner, aSBonus);
        }
        public override void OnDeactivate(bool expired)
        {
            if (damageBlock > 0)
            {
                RemoveShield(owner, damageBlock, true, true);
            }
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            SpellEffectCreate(out partname, out _, "Skarner_Exoskeleon_Shatter.troy", default, TeamId.TEAM_NEUTRAL, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, target, default, default, true, false, false, false, false);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, mSBonus);
            IncPercentAttackSpeedMod(owner, aSBonus);
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
                damageAmount -= damageBlock;
                damageBlock = 0;
                ReduceShield(owner, oldArmorAmount, true, true);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}
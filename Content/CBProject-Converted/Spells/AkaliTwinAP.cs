namespace Buffs
{
    public class AkaliTwinAP : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "R_hand", null, "", },
            AutoBuffActivateEffect = new[] { "", "akali_twinDisciplines_AP_buf.troy", },
            BuffName = "AkaliTwinAP",
            BuffTextureName = "AkaliTwinDisciplines.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        float akaliAP;
        float bonusDmgPerc;
        float addBonusDmgPerc;
        float dmgMult;
        float lastTimeExecuted;
        public AkaliTwinAP(float akaliAP = default)
        {
            this.akaliAP = akaliAP;
        }
        public override void OnActivate()
        {
            //RequireVar(this.akaliAP);
            bonusDmgPerc = 0.08f;
            akaliAP -= 20;
            addBonusDmgPerc = akaliAP / 600;
            dmgMult = bonusDmgPerc + addBonusDmgPerc;
            float dmgMultTooltip = 100 * dmgMult;
            SetBuffToolTipVar(1, dmgMultTooltip);
        }
        public override void OnUpdateActions()
        {
            akaliAP = GetFlatMagicDamageMod(owner);
            akaliAP -= 20;
            addBonusDmgPerc = akaliAP / 600;
            dmgMult = bonusDmgPerc + addBonusDmgPerc;
            if (ExecutePeriodically(2, ref lastTimeExecuted, false))
            {
                float dmgMultTooltip = 100 * dmgMult;
                SetBuffToolTipVar(1, dmgMultTooltip);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            float tAD = GetTotalAttackDamage(attacker);
            float damageToDeal = tAD * dmgMult;
            if (target is not BaseTurret)
            {
                ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
            }
        }
    }
}
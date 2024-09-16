namespace Buffs
{
    public class SpectralFury : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "R_hand", "root", "L_hand", },
            AutoBuffActivateEffect = new[] { "spectral_fury_activate.troy", "SpectralFury_wpn_new.troy", "spectral_fury_activate_speed.troy", "SpectralFury_wpn_new.troy", },
            BuffName = "SpectralFury",
            BuffTextureName = "3142_Youmus_Spectral_Blade.dds",
        };
        public override void OnActivate()
        {
            //RequireVar(charVars.SpectralCount);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, 0.5f);
            IncPercentMovementSpeedMod(owner, 0.2f);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (charVars.SpectralCount >= 2)
            {
            }
            else if (IsMelee(owner))
            {
                if (GetBuffCountFromCaster(owner, default, nameof(Buffs.JudicatorRighteousFury)) == 0)
                {
                    float spectralDuration = GetBuffRemainingDuration(owner, nameof(Buffs.SpectralFury));
                    AddBuff(attacker, owner, new Buffs.SpectralFury(), 1, 1, 2 + spectralDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                    charVars.SpectralCount++;
                }
            }
        }
    }
}
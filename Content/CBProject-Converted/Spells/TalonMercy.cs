namespace Buffs
{
    public class TalonMercy : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", "", "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "BladeRogue_CheatDeath",
            BuffTextureName = "22.dds",
            PersistsThroughDeath = true,
        };
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase)
            {
                bool isCCD = false;
                if (HasBuffOfType(target, BuffType.SLOW))
                {
                    isCCD = true;
                }
                if (HasBuffOfType(target, BuffType.STUN))
                {
                    isCCD = true;
                }
                if (HasBuffOfType(target, BuffType.CHARM))
                {
                    isCCD = true;
                }
                if (HasBuffOfType(target, BuffType.SUPPRESSION))
                {
                    isCCD = true;
                }
                if (isCCD)
                {
                    damageAmount *= 1.1f;
                }
            }
        }
    }
}
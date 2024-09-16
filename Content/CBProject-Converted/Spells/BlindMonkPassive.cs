namespace Buffs
{
    public class BlindMonkPassive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "L_hand", "R_hand", },
            AutoBuffActivateEffect = new[] { "blindMonk_passive_buf.troy", "blindMonk_passive_buf.troy", },
            BuffName = "BlindMonkFlurry",
            BuffTextureName = "BlindMonkPassive.dds",
        };
        float totalHits;
        public override void OnActivate()
        {
            IncPercentAttackSpeedMod(owner, 0.5f);
            totalHits = 2;
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, 0.5f);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            IncPAR(owner, 15, PrimaryAbilityResourceType.Energy);
            totalHits--;
            if (totalHits == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}
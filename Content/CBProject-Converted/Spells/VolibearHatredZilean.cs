namespace Buffs
{
    public class VolibearHatredZilean : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VolibearHatredZilean",
            BuffTextureName = "VolibearHatredZilean.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnKill(AttackableUnit target)
        {
            if (GetBuffCountFromCaster(target, target, nameof(Buffs.VolibearHatred)) > 0)
            {
                IncGold(attacker, 10);
            }
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (GetBuffCountFromCaster(target, target, nameof(Buffs.VolibearHatred)) > 0)
            {
                damageAmount *= 1.01f;
            }
        }
    }
}
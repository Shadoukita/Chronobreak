namespace Buffs
{
    public class VolibearHatred : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VolibearHatred",
            BuffTextureName = "VolibearHatred.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (GetBuffCountFromCaster(target, attacker, nameof(Buffs.VolibearHatredZilean)) > 0)
            {
                damageAmount *= 1.01f;
            }
        }
        public override void OnKill(AttackableUnit target)
        {
            if (GetBuffCountFromCaster(target, attacker, nameof(Buffs.VolibearHatredZilean)) > 0)
            {
                AddBuff(attacker, attacker, new Buffs.VolibearKillsZilean(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                IncGold(attacker, 11);
            }
        }
    }
}
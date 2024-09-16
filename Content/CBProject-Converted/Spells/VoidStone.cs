namespace Buffs
{
    public class VoidStone : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VoidStone",
            BuffTextureName = "Kassadin_VoidStone.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        int attackSpeedBoost; // UNUSED
        public override void OnActivate()
        {
            attackSpeedBoost = 0;
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (damageType == DamageType.DAMAGE_TYPE_MAGICAL && damageAmount > 0)
            {
                float attackSpeedBoost = damageAmount * 0.0015f; // UNUSED
                damageAmount *= charVars.MagicAbsorb;
                AddBuff((ObjAIBase)owner, owner, new Buffs.VoidStoneAttackSpeedBoost(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}
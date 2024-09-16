namespace ItemPassives
{
    public class ItemID_3117 : ItemScript
    {
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff(owner, owner, new Buffs.BootsOfMobilityDebuff(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff(owner, owner, new Buffs.BootsOfMobilityDebuff(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
        public override void OnDeactivate()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.BootsOfMobilityDebuff)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.BootsOfMobilityDebuff), owner);
            }
        }
    }
}
namespace Buffs
{
    public class _3117 : BuffScript
    {
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            if (
                owner.Team != attacker.Team
                && type 
                is BuffType.DAMAGE
                or BuffType.FEAR
                or BuffType.CHARM
                or BuffType.POLYMORPH
                or BuffType.SILENCE
                or BuffType.SLEEP
                or BuffType.SNARE
                or BuffType.STUN
                or BuffType.SLOW
            ){
                AddBuff((ObjAIBase)owner, owner, new Buffs.BootsOfMobilityDebuff(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
            return true;
        }
    }
}
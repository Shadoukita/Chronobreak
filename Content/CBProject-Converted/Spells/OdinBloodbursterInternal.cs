namespace Buffs
{
    public class OdinBloodbursterInternal : BuffScript
    {
        public override void OnActivate()
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.OdinBloodbursterInternal));
            if (count >= 3)
            {
                SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.OdinBloodbursterInternal), 0);
                AddBuff(attacker, attacker, new Buffs.OdinBloodbursterBuff(), 3, 1, 7, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}
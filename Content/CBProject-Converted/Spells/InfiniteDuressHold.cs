namespace Buffs
{
    public class InfiniteDuressHold : BuffScript
    {
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            return owner.Team == attacker.Team || type == BuffType.COMBAT_ENCHANCER;
        }
        public override void OnActivate()
        {
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
        }
    }
}
namespace Buffs
{
    public class Destealth : BuffScript
    {
        public override void OnActivate()
        {
            SpellBuffClear(owner, nameof(Buffs.Stealth));
            SetStealthed(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetStealthed(owner, false);
        }
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (type == BuffType.INVISIBILITY)
            {
                returnValue = false;
            }
            return returnValue;
        }
    }
}
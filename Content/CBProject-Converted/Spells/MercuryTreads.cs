namespace Buffs
{
    public class MercuryTreads : BuffScript
    {
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                if (type == BuffType.SNARE)
                {
                    duration *= 0.65f;
                }
                if (type == BuffType.SLOW)
                {
                    duration *= 0.65f;
                }
                if (type == BuffType.FEAR)
                {
                    duration *= 0.65f;
                }
                if (type == BuffType.CHARM)
                {
                    duration *= 0.65f;
                }
                if (type == BuffType.SLEEP)
                {
                    duration *= 0.65f;
                }
                if (type == BuffType.STUN)
                {
                    duration *= 0.65f;
                }
                if (type == BuffType.TAUNT)
                {
                    duration *= 0.65f;
                }
            }
            return returnValue;
        }
    }
}
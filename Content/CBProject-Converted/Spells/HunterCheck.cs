namespace Buffs
{
    public class HunterCheck : BuffScript
    {
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                if (type == BuffType.COMBAT_DEHANCER)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
                else if (type == BuffType.DAMAGE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
                else if (type == BuffType.FEAR)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
                else if (type == BuffType.CHARM)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
                else if (type == BuffType.POLYMORPH)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
                else if (type == BuffType.SILENCE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
                else if (type == BuffType.SLEEP)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
                else if (type == BuffType.SNARE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
                else if (type == BuffType.STUN)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
                else if (type == BuffType.SLOW)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
            }
            return returnValue;
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
        }
    }
}
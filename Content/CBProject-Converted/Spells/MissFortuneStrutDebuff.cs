namespace Buffs
{
    public class MissFortuneStrutDebuff : BuffScript
    {
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                if (type == BuffType.DAMAGE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else if (type == BuffType.FEAR)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else if (type == BuffType.CHARM)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else if (type == BuffType.POLYMORPH)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else if (type == BuffType.SILENCE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else if (type == BuffType.SLEEP)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else if (type == BuffType.SNARE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else if (type == BuffType.STUN)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else if (type == BuffType.SLOW)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.MissFortuneStrut)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.MissFortuneStrut), (ObjAIBase)owner);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrut(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}
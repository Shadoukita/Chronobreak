namespace Buffs
{
    public class GarenRecoupDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                if (type == BuffType.DAMAGE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.FEAR)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.CHARM)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.POLYMORPH)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.SILENCE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.SLEEP)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.SNARE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.STUN)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.SLOW)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            SpellBuffRemove(owner, nameof(Buffs.GarenRecouperate1), (ObjAIBase)owner, 0);
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecoupDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
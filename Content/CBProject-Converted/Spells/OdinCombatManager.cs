namespace Buffs
{
    public class OdinCombatManager : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (target.Team != owner.Team)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCombatActive(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                if (target != owner)
                {
                }
            }
            if (GetBuffCountFromCaster(target, target, nameof(Buffs.OdinCombatActive)) > 0 && target != owner)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCombatActive(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCombatActive(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void OnDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            if (target != owner)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCombatActive(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
        public override void OnKill(AttackableUnit target)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCombatActive(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, HitResult hitResult)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCombatActive(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void OnMiss(AttackableUnit target)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCombatActive(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            if (GetBuffCountFromCaster(target, target, nameof(Buffs.OdinCombatActive)) > 0 && target != owner)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCombatActive(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            return returnValue;
        }
    }
}
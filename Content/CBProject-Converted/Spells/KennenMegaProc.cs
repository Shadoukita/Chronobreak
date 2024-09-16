namespace Spells
{
    public class KennenMegaProc : SpellScript
    {
        float[] effect0 = { 0.4f, 0.5f, 0.6f, 0.7f, 0.8f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss)
            {
                float attackDamage;
                if (target is ObjAIBase)
                {
                    if (target is not BaseTurret)
                    {
                        if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.KennenDoubleStrikeLive)) > 0)
                        {
                            AddBuff(owner, target, new Buffs.KennenMarkofStorm(), 5, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            float damageMod = effect0[level - 1];
                            attackDamage = GetTotalAttackDamage(owner);
                            float procDamage = attackDamage * damageMod;
                            ApplyDamage(attacker, target, attackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
                            ApplyDamage(attacker, target, procDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                            SpellBuffRemove(owner, nameof(Buffs.KennenDoubleStrikeLive), owner);
                            RemoveOverrideAutoAttack(owner, true);
                            charVars.Count = 0;
                        }
                        else
                        {
                            attackDamage = GetTotalAttackDamage(owner);
                            ApplyDamage(attacker, target, attackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
                        }
                    }
                    else
                    {
                        attackDamage = GetTotalAttackDamage(owner);
                        ApplyDamage(attacker, target, attackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
                    }
                }
                else
                {
                    attackDamage = GetTotalAttackDamage(owner);
                    ApplyDamage(attacker, target, attackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
                }
            }
        }
    }
}
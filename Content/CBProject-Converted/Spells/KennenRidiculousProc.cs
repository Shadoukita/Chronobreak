namespace Buffs
{
    public class KennenRidiculousProc : BuffScript
    {
        float[] effect0 = { 0.6f, 0.7f, 0.8f, 0.9f, 1 };
        public override void OnDeactivate(bool expired)
        {
            int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float damageMods = effect0[level - 1];
            float attackDamage = GetTotalAttackDamage(attacker);
            float superDamage = attackDamage * damageMods;
            ApplyDamage(attacker, target, superDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
            AddBuff(attacker, target, new Buffs.KennenMarkofStorm(), 5, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
        }
    }
}
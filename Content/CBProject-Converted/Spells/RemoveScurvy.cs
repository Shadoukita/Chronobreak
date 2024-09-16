namespace Spells
{
    public class RemoveScurvy : SpellScript
    {
        int[] effect0 = { 80, 150, 220, 290, 360 };
        public override void SelfExecute()
        {
            SpellBuffRemoveType(owner, BuffType.STUN);
            SpellBuffRemoveType(owner, BuffType.CHARM);
            SpellBuffRemoveType(owner, BuffType.FEAR);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.SNARE);
            SpellBuffRemoveType(owner, BuffType.SLOW);
            SpellBuffRemoveType(owner, BuffType.TAUNT);
            SpellBuffRemoveType(owner, BuffType.POLYMORPH);
            SpellBuffRemoveType(owner, BuffType.SILENCE);
            SpellBuffRemoveType(owner, BuffType.SUPPRESSION);
            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float healLevel = effect0[level - 1];
            float abilityPower = GetFlatMagicDamageMod(owner);
            abilityPower *= 1;
            float healAmount = healLevel + abilityPower;
            IncHealth(owner, healAmount, owner);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.PirateScurvy)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.PirateScurvy), owner);
            }
        }
    }
}
namespace CharScripts
{
    public class CharScriptDrMundo : CharScript
    {
        int[] effect0 = { -50, -60, -70, -80, -90 };
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            string name = GetSpellName(spell);
            if (name == nameof(Spells.InfectedCleaverMissile))
            {
                int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float healthCost = effect0[level - 1];
                IncHealth(owner, healthCost, owner);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.Nevershade(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}
namespace CharScripts
{
    public class CharScriptXerath : CharScript
    {
        int[] effect0 = { 8, 8, 8, 8, 8 };
        float[] effect1 = { 0.1f, 0.15f, 0.2f, 0.25f, 0.3f };
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (!spellVars.DoesntTriggerSpellCasts)
            {
                int slot = GetSpellSlot(spell);
                if (slot == 1)
                {
                    int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    float buffDuration = effect0[level - 1];
                    float nextBuffVars_MagicPen = effect1[level - 1];
                    AddBuff(owner, owner, new Buffs.XerathLocusOfPower(nextBuffVars_MagicPen), 1, 1, buffDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.XerathAscended(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void OnResurrect()
        {
            AddBuff(owner, owner, new Buffs.XerathParticleBuff(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}
namespace CharScripts
{
    public class CharScriptBlitzcrank : CharScript
    {
        float lastTime2Executed;
        public override void OnUpdateActions()
        {
            int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (!IsDead(owner) && level > 0)
            {
                float cooldown = GetSlotSpellCooldownTime(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (cooldown <= 0)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.StaticField)) == 0)
                    {
                        AddBuff(owner, owner, new Buffs.StaticField(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                    }
                }
            }
            if (ExecutePeriodically(1, ref lastTime2Executed, true))
            {
                float blitzAP = GetFlatMagicDamageMod(owner);
                SetSpellToolTipVar(blitzAP, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            spellName = GetSpellName(spell);
            if (spellName == nameof(Spells.RocketGrab))
            {
                AddBuff(owner, owner, new Buffs.Root(), 1, 1, 0.6f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, target, new Buffs.ManaBarrierIcon(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}
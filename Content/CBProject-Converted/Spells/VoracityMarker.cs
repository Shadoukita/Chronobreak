namespace Buffs
{
    public class VoracityMarker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "",
            NonDispellable = true,
        };
        public override void OnActivate()
        {
            if (owner is not Champion)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes))
            {
                if (GetBuffCountFromCaster(unit, unit, nameof(Buffs.Voracity)) > 0)
                {
                    SpellEffectCreate(out _, out _, "katarina_spell_refresh_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, false);
                    IncGold(unit, 50);
                    SetSlotSpellCooldownTime((ObjAIBase)unit, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                    SetSlotSpellCooldownTime((ObjAIBase)unit, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                    SetSlotSpellCooldownTime((ObjAIBase)unit, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                    float dLCooldown = GetSlotSpellCooldownTime((ObjAIBase)unit, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if (dLCooldown > 15)
                    {
                        dLCooldown -= 15;
                        SetSlotSpellCooldownTime((ObjAIBase)unit, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, dLCooldown);
                    }
                    else
                    {
                        SetSlotSpellCooldownTime((ObjAIBase)unit, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                    }
                }
            }
        }
    }
}
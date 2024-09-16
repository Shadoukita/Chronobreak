namespace Buffs
{
    public class ViktorAugmentQ : BuffScript
    {
        public override void OnActivate()
        {
            SetSlotSpellIcon(0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, 2);
            //TeamId ownerTeam = GetTeamID(owner); // UNUSED
        }
        public override void OnUpdateStats()
        {
            //float ownerMaxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            //float healthtoAdd = ownerMaxHealth * 0.1f; // UNUSED
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            spellName = GetSpellName(spell);
            float nextBuffVars_MoveSpeedMod = 0.3f;
            if (spellName == nameof(Spells.ViktorPowerTransfer))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.Haste(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            /*
            else if(spellName == nameof(Spells.HexMageCatalyst))
            {
            }
            else if(spellName == nameof(Spells.HexMageChaosCharge))
            {
            }
            else if(spellName == nameof(Spells.HexMageChaoticStorm))
            {
            }
            */
        }
    }
}
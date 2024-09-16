namespace Spells
{
    public class QuicksilverSash : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            SpellEffectCreate(out _, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            DispellNegativeBuffs(owner);
            int slotCheck = 0;
            while (slotCheck <= 5)
            {
                string name = GetSlotSpellName(owner, slotCheck, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
                if (name == nameof(Spells.QuicksilverSash))
                {
                    SetSlotSpellCooldownTimeVer2(90, slotCheck, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
                }
                slotCheck++;
            }
        }
    }
}
namespace Buffs
{
    public class QuicksilverSash : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "",
            BuffTextureName = "",
        };
    }
}
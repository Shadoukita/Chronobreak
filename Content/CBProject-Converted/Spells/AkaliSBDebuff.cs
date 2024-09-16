namespace Buffs
{
    public class AkaliSBDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Global_Slow.troy", },
            BuffName = "AkaliTwilightShroudDebuff",
            BuffTextureName = "AkaliTwilightShroud.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float movementSpeed;
        float attackSpeed;
        float[] effect0 = { -0.14f, -0.18f, -0.22f, -0.26f, -0.3f };
        int[] effect1 = { 0, 0, 0, 0, 0 };
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            movementSpeed = effect0[level - 1];
            attackSpeed = effect1[level - 1];
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, movementSpeed);
            IncPercentMultiplicativeAttackSpeedMod(owner, attackSpeed);
        }
    }
}
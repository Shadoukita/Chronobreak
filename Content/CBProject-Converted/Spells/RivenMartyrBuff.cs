namespace Spells
{
    public class RivenMartyrBuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 22f, 18f, 14f, 10f, 6f, },
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class RivenMartyrBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "l_hand", "r_hand", },
            AutoBuffActivateEffect = new[] { "enrage_buf.troy", "enrage_buf.troy", },
            BuffName = "BlindMonkSafeguard",
        };
        float speedBoost;
        public RivenMartyrBuff(float speedBoost = default)
        {
            this.speedBoost = speedBoost;
        }
        public override void OnActivate()
        {
            //RequireVar(this.speedBoost);
            IncPercentAttackSpeedMod(owner, speedBoost);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, speedBoost);
        }
    }
}
namespace Spells
{
    public class SummonerReviveSpeedBoost : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class SummonerReviveSpeedBoost : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "ArmordilloSpin.troy", "Powerball_buf.troy", },
            AutoBuffActivateEffectFlags = EffCreate.UPDATE_ORIENTATION,
            BuffName = "SummonerReviveSpeedBoost",
            BuffTextureName = "Summoner_revive.dds",
            SpellToggleSlot = 1,
        };
        float moveSpeedMod;
        public SummonerReviveSpeedBoost(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
        }
        public override void OnUpdateStats()
        {
            moveSpeedMod -= 0.026f;
            if (moveSpeedMod < 0)
            {
                moveSpeedMod = 0;
            }
            IncPercentMultiplicativeMovementSpeedMod(owner, moveSpeedMod);
        }
    }
}
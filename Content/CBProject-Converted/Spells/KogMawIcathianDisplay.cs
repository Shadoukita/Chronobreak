namespace Spells
{
    public class KogMawIcathianDisplay : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class KogMawIcathianDisplay : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "KogMawIcathianDisplay",
            BuffTextureName = "KogMaw_IcathianSurprise.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            int levelDamage = GetLevel(owner);
            float bonusDamage = levelDamage * 25;
            float totalDamage = bonusDamage + 100;
            SetBuffToolTipVar(1, totalDamage);
        }
    }
}
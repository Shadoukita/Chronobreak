namespace Spells
{
    public class ViktorGravitonFieldStun : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class ViktorGravitonFieldStun : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "Stun_glb.troy", },
            BuffName = "ViktorGravitonStun",
            BuffTextureName = "ViktorGravitonFieldAUG.dds",
            PopupMessage = new[] { "game_floatingtext_Stunned", },
        };
        public override void OnActivate()
        {
            SetStunned(owner, true);
            PauseAnimation(owner, true);
            float duration = GetBuffRemainingDuration(owner, nameof(Buffs.ViktorGravitonFieldStun)); // UNUSED
        }
        public override void OnDeactivate(bool expired)
        {
            SetStunned(owner, false);
            PauseAnimation(owner, false);
        }
    }
}
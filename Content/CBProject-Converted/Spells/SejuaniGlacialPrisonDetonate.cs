namespace Spells
{
    public class SejuaniGlacialPrisonDetonate : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class SejuaniGlacialPrisonDetonate : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "BUFFBONE_GLB_GROUND_LOC", },
            AutoBuffActivateEffect = new[] { "Stun_glb.troy", "", },
            BuffName = "SejuaniGlacialPrison",
            BuffTextureName = "Sejuani_GlacialPrison.dds",
            PopupMessage = new[] { "game_floatingtext_Stunned", },
        };
        EffectEmitter crystalineParticle;
        public override void OnActivate()
        {
            SetStunned(owner, true);
            PauseAnimation(owner, true);
            SpellEffectCreate(out crystalineParticle, out _, "sejuani_ult_tar_04.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, owner, "BUFFBONE_GLB_GROUND_LOC", default, attacker, "Bird_head", default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStunned(owner, false);
            PauseAnimation(owner, false);
            SpellEffectRemove(crystalineParticle);
        }
        public override void OnUpdateStats()
        {
            SetStunned(owner, true);
            PauseAnimation(owner, true);
        }
    }
}
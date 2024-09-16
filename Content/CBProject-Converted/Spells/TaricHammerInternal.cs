namespace Spells
{
    public class TaricHammerInternal : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "", },
        };
    }
}
namespace Buffs
{
    public class TaricHammerInternal : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "weapon", },
            AutoBuffActivateEffect = new[] { "Taric_HammerInternal.troy", },
            BuffName = "TaricHammerInternal",
            BuffTextureName = "",
        };
        EffectEmitter particle;
        public override void OnActivate()
        {
            SpellEffectCreate(out particle, out _, "Taric_HammerInternal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "weapon", default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
        }
    }
}
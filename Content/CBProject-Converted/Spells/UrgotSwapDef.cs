namespace Spells
{
    public class UrgotSwapDef : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 22f, 20f, 18f, 16f, 14f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class UrgotSwapDef : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "UrgotSwapDef",
            BuffTextureName = "UrgotPositionReverser.dds",
        };
        float defInc;
        EffectEmitter particle1;
        public UrgotSwapDef(float defInc = default)
        {
            this.defInc = defInc;
        }
        public override void OnActivate()
        {
            SpellEffectCreate(out particle1, out _, "UrgotSwapDef.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle1);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, defInc);
            IncFlatSpellBlockMod(owner, defInc);
        }
    }
}
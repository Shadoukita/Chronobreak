namespace Spells
{
    public class UdyrBearActivation : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class UdyrBearActivation : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "BearStance.troy", },
            BuffName = "UdyrBearActivation",
            BuffTextureName = "Udyr_BearStance.dds",
        };
        float moveSpeedMod;
        EffectEmitter bearparticle;
        float[] effect0 = { 0.15f, 0.18f, 0.21f, 0.24f, 0.27f };
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            moveSpeedMod = effect0[level - 1];
            SpellEffectCreate(out bearparticle, out _, "PrimalCharge.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(bearparticle);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, moveSpeedMod);
        }
    }
}
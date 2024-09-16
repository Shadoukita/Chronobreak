namespace Spells
{
    public class RenektonRageReady : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class RenektonRageReady : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "RenekthonCleaveReady",
            BuffTextureName = "Renekton_Predator.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
            SpellToggleSlot = 1,
        };
        EffectEmitter rHand;
        EffectEmitter lHand;
        public override void OnActivate()
        {
            SpellEffectCreate(out rHand, out _, "Renekton_Rage_State.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_Hand", default, target, default, default, false);
            SpellEffectCreate(out lHand, out _, "Renekton_Rage_State.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_Hand", default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(lHand);
            SpellEffectRemove(rHand);
        }
    }
}
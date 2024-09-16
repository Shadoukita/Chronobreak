namespace Spells
{
    public class HextechSweeperSight : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class HextechSweeperSight : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "global_Watched.troy", "", },
            BuffName = "HextechSweeper",
            BuffTextureName = "Nidalee_OnTheProwl.dds",
        };
        Region bubbleID;
        public override void OnActivate()
        {
            TeamId team = GetTeamID_CS(attacker);
            bubbleID = AddUnitPerceptionBubble(team, 300, owner, 6, default, default, true);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
        }
    }
}
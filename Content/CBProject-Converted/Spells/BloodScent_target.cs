namespace Buffs
{
    public class BloodScent_target : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Blood Scent",
            BuffTextureName = "Wolfman_Bloodscent.dds",
        };
        EffectEmitter particle;
        Region bubbleStuff;
        public override void OnActivate()
        {
            TeamId casterID = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle, out _, "wolfman_bloodscent_marker.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false);
            bubbleStuff = AddUnitPerceptionBubble(casterID, 1000, owner, 120, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            RemovePerceptionBubble(bubbleStuff);
        }
    }
}
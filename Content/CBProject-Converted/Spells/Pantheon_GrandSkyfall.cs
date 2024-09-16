namespace Spells
{
    public class Pantheon_GrandSkyfall : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            ChannelDuration = 3f,
        };
    }
}
namespace Buffs
{
    public class Pantheon_GrandSkyfall : BuffScript
    {
        Vector3 targetPos;
        Region bubbleID;
        public Pantheon_GrandSkyfall(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
            //RequireVar(this.particle);
            Vector3 targetPos = this.targetPos;
            TeamId teamOfOwner = GetTeamID_CS(owner);
            bubbleID = AddPosPerceptionBubble(teamOfOwner, 700, targetPos, 6, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
        }
    }
}
namespace Spells
{
    public class GateMarker : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 90f, 75f, 60f, 45f, 30f, },
            ChannelDuration = 4f,
        };
    }
}
namespace Buffs
{
    public class GateMarker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Gate Target",
            BuffTextureName = "Cardmaster_Premonition.dds",
        };
        EffectEmitter teleportParticle;
        public override void OnActivate()
        {
            SpellEffectCreate(out teleportParticle, out _, "GateMarker.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            SetNoRender(owner, true);
            SetGhosted(owner, true);
            SetGhosted(owner, true);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetTargetable(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetTargetable(owner, true);
            SpellEffectRemove(teleportParticle);
        }
    }
}
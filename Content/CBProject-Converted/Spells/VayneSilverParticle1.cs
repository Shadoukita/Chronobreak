namespace Buffs
{
    public class VayneSilverParticle1 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
        };
        EffectEmitter globeOne;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out globeOne, out _, "vayne_W_ring1.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(globeOne);
        }
    }
}
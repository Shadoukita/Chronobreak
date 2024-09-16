namespace Buffs
{
    public class RenektonBloodSplatterTarget : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "",
            BuffTextureName = "",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            TeamId ownerVar = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "RenektonSliceDice_tar.troy", default, ownerVar, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, owner, default, default, true);
        }
    }
}
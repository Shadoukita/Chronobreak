namespace Buffs
{
    public class TrundlePainBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TrundlePainBuff",
            BuffTextureName = "Trundle_Agony.dds",
        };
        EffectEmitter mRShield;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out mRShield, out _, "TrundleUlt_self_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(mRShield);
        }
    }
}
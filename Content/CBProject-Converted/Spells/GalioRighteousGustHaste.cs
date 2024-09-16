namespace Buffs
{
    public class GalioRighteousGustHaste : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "GalioRighteousGustHaste",
            BuffTextureName = "Galio_RighteousGust.dds",
        };
        float moveSpeedMod;
        EffectEmitter buffVFXAlly;
        EffectEmitter buffVFXEnemy;
        public GalioRighteousGustHaste(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            IncPercentMultiplicativeMovementSpeedMod(owner, moveSpeedMod);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out buffVFXAlly, out _, "galio_windTunnel_speed_buf.troy", default, GetEnemyTeam(teamID), 0, 0, teamID, default, owner, false, owner, "root", default, owner, default, default, false);
            SpellEffectCreate(out buffVFXEnemy, out _, "galio_windTunnel_speed_buf_team_red.tro", default, teamID, 0, 0, GetEnemyTeam(teamID), default, owner, false, owner, "root", default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(buffVFXAlly);
            SpellEffectRemove(buffVFXEnemy);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, moveSpeedMod);
        }
    }
}
namespace Buffs
{
    public class JarvanIVDragonStrikeSound : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
        EffectEmitter dragonStrikeSound;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out dragonStrikeSound, out _, "JarvanDemacianStandard_flag_hit.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(dragonStrikeSound);
        }
    }
}
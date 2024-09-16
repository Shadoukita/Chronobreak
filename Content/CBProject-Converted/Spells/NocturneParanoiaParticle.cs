namespace Buffs
{
    public class NocturneParanoiaParticle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        public override void OnDeactivate(bool expired)
        {
            float duration;
            FadeOutColorFadeEffect(1, TeamId.TEAM_UNKNOWN);
            TeamId teamOfOwner = GetTeamID_CS(owner);
            foreach (Champion unit in GetChampions(GetEnemyTeam(teamOfOwner), nameof(Buffs.NocturneParanoiaParticle), true))
            {
                duration = GetBuffRemainingDuration(unit, nameof(Buffs.NocturneParanoiaParticle));
                if (duration > 0.5f)
                {
                    FadeInColorFadeEffect(75, 0, 0, 1, 0.3f, teamOfOwner);
                }
            }
        }
    }
}
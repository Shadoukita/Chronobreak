namespace Buffs
{
    public class TurretChampionDelta : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        int numAlliedChampions;
        public override void OnUpdateStats()
        {
            if (ExecutePeriodically(10, ref lastTimeExecuted, false))
            {
                int numHostileChampions = 0;
                TeamId teamID = GetTeamID_CS(owner);
                numAlliedChampions = GetNumberOfHeroesOnTeam(teamID, false, true);
                numHostileChampions = GetNumberOfHeroesOnTeam(GetEnemyTeam(teamID), false, true);
                if (numAlliedChampions > numHostileChampions)
                {
                    AddBuff(attacker, target, new Buffs.NegativeTurretDelta(), 1, 1, 21, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    SpellBuffClear(owner, nameof(Buffs.PositiveTurretDelta));
                }
                else if (numAlliedChampions < numHostileChampions)
                {
                    AddBuff(attacker, target, new Buffs.PositiveTurretDelta(), 1, 1, 21, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    SpellBuffClear(owner, nameof(Buffs.NegativeTurretDelta));
                }
                else
                {
                    SpellBuffClear(owner, nameof(Buffs.PositiveTurretDelta));
                    SpellBuffClear(owner, nameof(Buffs.NegativeTurretDelta));
                }
            }
        }
    }
}
namespace Buffs
{
    public class TurretBonusHealth : BuffScript
    {
        float bonusHealth;
        float bubbleSize;
        Region thisBubble; // UNUSED
        public TurretBonusHealth(float bonusHealth = default, float bubbleSize = default)
        {
            this.bonusHealth = bonusHealth;
            this.bubbleSize = bubbleSize;
        }
        public override void OnActivate()
        {
            //RequireVar(this.bonusHealth);
            //RequireVar(this.bubbleSize);
        }
        public override void OnUpdateStats()
        {
            int numChampions;
            TeamId teamID = GetTeamID_CS(owner);
            thisBubble = AddUnitPerceptionBubble(teamID, bubbleSize, owner, 25000, default, default, true);
            numChampions = GetNumberOfHeroesOnTeam(GetEnemyTeam(teamID), true, true);
            numChampions = Math.Min(5, numChampions);
            float bonusHealth = numChampions * this.bonusHealth;
            IncPermanentFlatHPPoolMod(owner, bonusHealth);
            SpellBuffRemoveCurrent(owner);
        }
    }
}
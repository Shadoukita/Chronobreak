namespace Buffs
{
    public class IsNinja : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Is Ninja",
            BuffTextureName = "GSB_stealth.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float damageMod;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            damageMod = 0;
        }
        public override void OnUpdateStats()
        {
            IncFlatHPPoolMod(owner, damageMod);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(20, ref lastTimeExecuted, true))
            {
                TeamId teamID = GetTeamID_CS(owner);
                float numOtherNinjas = -1;
                foreach (Champion unit in GetChampions(teamID, nameof(Buffs.IsNinja), true))
                {
                    numOtherNinjas++;
                }
                damageMod = numOtherNinjas * -1;
            }
        }
    }
}
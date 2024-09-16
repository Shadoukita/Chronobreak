namespace Buffs
{
    public class RegenerationRuneBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Regenerationpotion_itm.troy", },
            BuffName = "RegenerationRune",
            BuffTextureName = "Sona_AriaofPerseverance.dds",
        };
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SpellEffectCreate(out _, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            float missingHealthPercent = 1 - healthPercent;
            float healthToRestore = 20 * missingHealthPercent;
            healthToRestore = Math.Max(5, healthToRestore);
            IncHealth(owner, healthToRestore, owner);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
                float missingHealthPercent = 1 - healthPercent;
                float healthToRestore = 5 * missingHealthPercent;
                healthToRestore = Math.Max(1, healthToRestore);
                IncHealth(owner, healthToRestore, owner);
            }
        }
    }
}
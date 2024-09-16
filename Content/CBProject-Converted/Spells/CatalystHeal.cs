namespace Buffs
{
    public class CatalystHeal : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CatalystHeal",
            BuffTextureName = "3010_Catalyst_the_Protector.dds",
        };
        EffectEmitter cp1;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SpellEffectCreate(out cp1, out _, "env_manaheal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(cp1);
        }
        public override void OnUpdateStats()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, false))
            {
                IncHealth(owner, 15.625f, owner);
                IncPAR(owner, 12.5f, PrimaryAbilityResourceType.MANA);
            }
        }
    }
}
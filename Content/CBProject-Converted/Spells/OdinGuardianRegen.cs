namespace Spells
{
    public class OdinGuardianRegen : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class OdinGuardianRegen : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "GarenRecouperate",
            BuffTextureName = "Garen_Perseverance.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        EffectEmitter part;
        float lastTimeExecuted;
        int[] effect0 = { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
        public override void OnActivate()
        {
            SpellEffectCreate(out part, out _, "garen_heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(part);
        }
        public override void OnUpdateStats()
        {
            int level = GetLevel(owner);
            int hPRegen = effect0[level - 1]; // UNUSED
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
                if (healthPercent < 1)
                {
                    float maxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
                    float healthToInc = maxHealth * 0.005f;
                    IncHealth(owner, healthToInc, owner);
                }
            }
        }
    }
}
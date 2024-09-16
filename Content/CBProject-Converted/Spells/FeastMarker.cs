namespace Buffs
{
    public class FeastMarker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        EffectEmitter particle;
        float lastTimeExecuted;
        int[] effect0 = { 0, 0, 0 };
        int[] effect1 = { 300, 475, 650 };
        public override void OnActivate()
        {
            SpellEffectCreate(out particle, out _, "feast_tar_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, owner, default, default, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
        }
        public override void OnUpdateStats()
        {
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, false))
            {
                int count = GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.Feast));
                int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float healthPerStack = effect0[level - 1];
                float feastBase = effect1[level - 1];
                float bonusFeastHealth = healthPerStack * count;
                float feastHealth = bonusFeastHealth + feastBase;
                float targetHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                if (feastHealth < targetHealth)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else
                {
                    float time = GetSlotSpellCooldownTime(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if (time > 0)
                    {
                        SpellBuffRemoveCurrent(owner);
                    }
                }
            }
        }
    }
}
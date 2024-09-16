namespace Spells
{
    public class BloodScent_internal : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            TriggersSpellCasts = false,
            SpellVOOverrideSkins = new[] { "HyenaWarwick", },
        };
    }
}
namespace Buffs
{
    public class BloodScent_internal : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Blood Awareness",
            BuffTextureName = "20.dds",
            PersistsThroughDeath = true,
            SpellToggleSlot = 3,
        };
        float lastTimeExecuted;
        float[] effect0 = { 0.2f, 0.25f, 0.3f, 0.35f, 0.4f };
        public override void OnDeactivate(bool expired)
        {
            SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 4);
        }
        public override void OnUpdateActions()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level >= 1 && !IsDead(owner) && ExecutePeriodically(2, ref lastTimeExecuted, false))
            {
                float baseRange = level * 800;
                float range = baseRange + 700;
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, range, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
                {
                    float maxHealth = GetMaxHealth(unit, PrimaryAbilityResourceType.MANA);
                    float health = GetHealth(unit, PrimaryAbilityResourceType.MANA);
                    float healthPercent = health / maxHealth;
                    if (healthPercent <= 0.5f)
                    {
                        AddBuff(attacker, unit, new Buffs.BloodScent_target(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        float nextBuffVars_MoveSpeedBuff = effect0[level - 1];
                        AddBuff(attacker, attacker, new Buffs.BloodScent(nextBuffVars_MoveSpeedBuff), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.HASTE, 0, true, false, false);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class UdyrPhoenixActivation : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "UdyrPhoenixActivation",
            BuffTextureName = "Udyr_PhoenixStance.dds",
            IsDeathRecapSource = true,
        };
        float abilityPowerInc;
        float attackDamageInc;
        float lastTimeExecuted;
        int[] effect0 = { 16, 24, 32, 40, 48 };
        int[] effect1 = { 8, 12, 16, 20, 24 };
        int[] effect2 = { 15, 25, 35, 45, 55 };
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            abilityPowerInc = effect0[level - 1];
            attackDamageInc = effect1[level - 1];
            SetBuffToolTipVar(1, abilityPowerInc);
            SetBuffToolTipVar(2, attackDamageInc);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, attackDamageInc);
            IncFlatMagicDamageMod(owner, abilityPowerInc);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SpellEffectCreate(out _, out _, "Udyr_Phoenix_nova.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, effect2[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.25f, 0, false, false, attacker);
                }
            }
        }
    }
}
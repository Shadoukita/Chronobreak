namespace Spells
{
    public class CassiopeiaDeadlyCadence : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class CassiopeiaDeadlyCadence : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "CassiopeiaDeadlyCadence",
            BuffTextureName = "Cassiopeia_DeadlyCadence.dds",
            NonDispellable = true,
        };
        float percentMod;
        public override void OnActivate()
        {
            percentMod = -0.1f;
            float curCost = GetPARMultiplicativeCostInc(owner, 0, SpellSlotType.SpellSlots, PrimaryAbilityResourceType.MANA);
            float cost = curCost + percentMod;
            float tooltip = cost * -100;
            SetBuffToolTipVar(1, tooltip);
            SetPARMultiplicativeCostInc(owner, 0, SpellSlotType.SpellSlots, cost, PrimaryAbilityResourceType.MANA);
            SetPARMultiplicativeCostInc(owner, 1, SpellSlotType.SpellSlots, cost, PrimaryAbilityResourceType.MANA);
            SetPARMultiplicativeCostInc(owner, 2, SpellSlotType.SpellSlots, cost, PrimaryAbilityResourceType.MANA);
            SetPARMultiplicativeCostInc(owner, 3, SpellSlotType.SpellSlots, cost, PrimaryAbilityResourceType.MANA);
        }
        public override void OnDeactivate(bool expired)
        {
            SetPARMultiplicativeCostInc(owner, 0, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            SetPARMultiplicativeCostInc(owner, 1, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            SetPARMultiplicativeCostInc(owner, 2, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            SetPARMultiplicativeCostInc(owner, 3, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
        }
    }
}
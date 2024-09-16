namespace Buffs
{
    internal class InternalTestBuff : CBuffScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new();

        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "InternalTestBuff",
            BuffTextureName = "GSB_invulnerability.dds",
            PersistsThroughDeath = true
        };

        public override void OnActivate()
        {
            if(target.Stats.PrimaryAbilityResourceType == PrimaryAbilityResourceType.MANA) 
            {
                SetPARMultiplicativeCostInc(target, 0, SpellSlotType.SpellSlots, -1, PrimaryAbilityResourceType.MANA);
                SetPARMultiplicativeCostInc(target, 1, SpellSlotType.SpellSlots, -1, PrimaryAbilityResourceType.MANA);
                SetPARMultiplicativeCostInc(target, 2, SpellSlotType.SpellSlots, -1, PrimaryAbilityResourceType.MANA);
                SetPARMultiplicativeCostInc(target, 3, SpellSlotType.SpellSlots, -1, PrimaryAbilityResourceType.MANA);
            }
            else if(target.Stats.PrimaryAbilityResourceType == PrimaryAbilityResourceType.Energy)
            {
                SetPARMultiplicativeCostInc(target, 0, SpellSlotType.SpellSlots, -1, PrimaryAbilityResourceType.Energy);
                SetPARMultiplicativeCostInc(target, 1, SpellSlotType.SpellSlots, -1, PrimaryAbilityResourceType.Energy);
                SetPARMultiplicativeCostInc(target, 2, SpellSlotType.SpellSlots, -1, PrimaryAbilityResourceType.Energy);
                SetPARMultiplicativeCostInc(target, 3, SpellSlotType.SpellSlots, -1, PrimaryAbilityResourceType.Energy);
            }

            IncPermanentPercentCooldownMod(target, -0.8f);
            owner.Stats.Tenacity.IncPercentBasePerm(0.25f);
            IncPermanentFlatMovementSpeedMod(target, 60);
        }

        public override void OnUpdateStats()
        {
            if (IsMelee(target))
            {
                IncFlatCritDamageMod(owner, 0.25f);
            }

            //SetBuffToolTipVar<float>(Buff, 0, 100);
            //SetBuffToolTipVar<float>(Buff, 1,  80);
            //SetBuffToolTipVar<float>(Buff, 2, 25);
            //SetBuffToolTipVar<float>(Buff, 3, 60);
            //SetBuffToolTipVar<float>(Buff, 4, 35);
        }
    }
}

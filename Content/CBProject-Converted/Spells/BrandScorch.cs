namespace Spells
{
    public class BrandScorch : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class BrandScorch : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "weapon", "head", },
            AutoBuffActivateEffect = new[] { "Flamesword.troy", "RighteousFuryHalo_buf.troy", },
            BuffName = "JudicatorRighteousFury",
            BuffTextureName = "Judicator_RighteousFury.dds",
        };
        public override void OnActivate()
        {
            SetDodgePiercing(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetDodgePiercing(owner, false);
        }
        public override void OnPreAttack(AttackableUnit target)
        {
            RemoveOverrideAutoAttack(owner, false);
            if (target is ObjAIBase && target is not BaseTurret)
            {
                OverrideAutoAttack(3, SpellSlotType.ExtraSlots, owner, 1, true);
            }
        }
    }
}
namespace Spells
{
    public class EvelynnQ : HateSpike { }
    public class HateSpike : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
        };
        public override bool CanCast()
        {
            foreach (AttackableUnit unit in GetRandomUnitsInArea(owner, owner.Position3D, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                return true;
            }
            return false;
        }
        public override void SelfExecute()
        {
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 355, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, 1, default, true))
            {
                SpellCast(owner, unit, owner.Position3D, owner.Position3D, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class HateSpike : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Rapid Fire",
            BuffTextureName = "Tristana_headshot.dds",
        };
    }
}
namespace Buffs
{
    public class SowTheWindCastMarker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SowTheWindCastMarker",
        };
        public override void OnActivate()
        {
            SetTargetingType(1, SpellSlotType.SpellSlots, default, TargetingType.Self, owner);
        }
        public override void OnDeactivate(bool expired)
        {
            SetTargetingType(1, SpellSlotType.SpellSlots, default, TargetingType.Target, owner);
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            spellName = GetSpellName(spell);
            if (spellName == nameof(Spells.SowTheWind))
            {
                ObjAIBase caster = GetBuffCasterUnit();
                SpellBuffRemove(caster, nameof(Buffs.SowTheWind), (ObjAIBase)owner);
                SpellBuffRemove(owner, nameof(Buffs.SowTheWindCastMarker), caster);
            }
        }
    }
}
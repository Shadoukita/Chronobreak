namespace Spells
{
    public class NocturneParanoiaTarget : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class NocturneParanoiaTarget : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            AutoBuffActivateEvent = "DeathsCaress_buf.prt",
            BuffName = "NocturneParanoiaTarget",
            BuffTextureName = "Nocturne_Paranoia.dds",
        };
        float sightReduction;
        public NocturneParanoiaTarget(float sightReduction = default)
        {
            this.sightReduction = sightReduction;
        }
        public override void OnActivate()
        {
            //RequireVar(this.sightReduction);
            //RequireVar(this.spellLevel);
            IncPermanentFlatBubbleRadiusMod(owner, sightReduction);
        }
        public override void OnDeactivate(bool expired)
        {
            float sightReduction = this.sightReduction * -1;
            IncPermanentFlatBubbleRadiusMod(owner, sightReduction);
        }
    }
}
namespace Spells
{
    public class BandagetossFlingCaster : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 18f, 16f, 14f, 12f, 10f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class BandagetossFlingCaster : BuffScript
    {
        EffectEmitter particleID;
        public BandagetossFlingCaster(EffectEmitter particleID = default)
        {
            this.particleID = particleID;
        }
        public override void OnActivate()
        {
            //RequireVar(this.particleID);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particleID);
        }
    }
}
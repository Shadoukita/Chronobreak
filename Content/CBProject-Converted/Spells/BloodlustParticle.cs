namespace Spells
{
    public class BloodlustParticle : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            SpellFXOverrideSkins = new[] { "TryndamereDemonsword", },
        };
    }
}
namespace Buffs
{
    public class BloodlustParticle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Blood Lust",
            BuffTextureName = "DarkChampion_Bloodlust.dds",
        };
        EffectEmitter particle;
        public override void OnActivate()
        {
            SpellEffectCreate(out particle, out _, "bloodLust_flame.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
        }
    }
}
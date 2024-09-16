namespace Buffs
{
    public class OdinDragonBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinDragonBuff",
            BuffTextureName = "Averdrian_AstralBeam.dds",
        };
        float damageIncMod;
        EffectEmitter buffParticle;
        float aPIncMod;
        public override void OnActivate()
        {
            damageIncMod = 40;
            SpellEffectCreate(out buffParticle, out _, "nashor_rune_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
            aPIncMod = 40;
            IncFlatPhysicalDamageMod(owner, damageIncMod);
            IncFlatMagicDamageMod(owner, aPIncMod);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(buffParticle);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, damageIncMod);
            IncFlatMagicDamageMod(owner, aPIncMod);
        }
    }
}
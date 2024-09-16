namespace Buffs
{
    public class GalioIdolOfDurandTaunt : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffTextureName = "Galio_IdolOfDurand.dds",
        };
        EffectEmitter tauntVFX;
        public override void OnActivate()
        {
            SpellEffectCreate(out tauntVFX, out _, "galio_taunt_unit_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(tauntVFX);
        }
    }
}
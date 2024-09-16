namespace Buffs
{
    public class DeathLotusSound : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "DeathLotusSound",
            BuffTextureName = "Katarina_DeathLotus.dds",
        };
        EffectEmitter deathLotus;
        public override void OnActivate()
        {
            SpellEffectCreate(out deathLotus, out _, "katarinaDeathLotus_indicator_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(deathLotus);
        }
    }
}
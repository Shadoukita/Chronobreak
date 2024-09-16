namespace Buffs
{
    public class ConsecrationAuraNoParticle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Consecration",
            BuffTextureName = "Soraka_Consecration.dds",
        };
        public override void OnActivate()
        {
            IncPermanentFlatSpellBlockMod(owner, 16);
            SpellEffectCreate(out _, out _, "ConsecrationAura_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, default, default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            IncPermanentFlatSpellBlockMod(owner, -16);
        }
    }
}
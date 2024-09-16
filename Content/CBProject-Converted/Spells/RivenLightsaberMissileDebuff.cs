namespace Spells
{
    public class RivenLightsaberMissileDebuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class RivenLightsaberMissileDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "GLOBAL_SLOW.troy", },
            BuffName = "EzrealEssenceFluxDebuff",
            BuffTextureName = "Ezreal_EssenceFlux.dds",
        };
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            if (owner is Champion)
            {
                SpellEffectCreate(out _, out _, "exile_ult_mis_tar.troy ", "exile_ult_mis_tar.troy ", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            }
            SpellEffectCreate(out _, out _, "exile_ult_mis_tar_minion.troy ", "exile_ult_mis_tar_minion.troy ", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
        }
    }
}
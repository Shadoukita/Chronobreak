namespace Spells
{
    public class RivenTriCleaveDamageDebuff2 : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 22f, 18f, 14f, 10f, 6f, },
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class RivenTriCleaveDamageDebuff2 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "BlindMonkSafeguard",
        };
        public override void OnActivate()
        {
            TeamId ownerVar = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "exile_Q_tar_03.troy", default, ownerVar, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, owner, default, default, true, false, false, false, false);
            SpellEffectCreate(out _, out _, "exile_Q_tar_04.troy", default, ownerVar, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, owner, default, default, true, false, false, false, false);
        }
    }
}
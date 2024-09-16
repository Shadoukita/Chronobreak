namespace Spells
{
    public class BlindMonkQOneChaos : SpellScript
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
    public class BlindMonkQOneChaos : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "global_Watched.troy", },
            BuffName = "BlindMonkSonicWave",
            BuffTextureName = "BlindMonkQOne.dds",
        };
        Region bubbleID;
        Region bubbleID2;
        EffectEmitter slow;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            bubbleID = AddUnitPerceptionBubble(teamID, 400, owner, 20, default, default, false);
            bubbleID2 = AddUnitPerceptionBubble(teamID, 50, owner, 20, default, default, true);
            ApplyAssistMarker(attacker, owner, 10);
            SpellEffectCreate(out _, out _, "blindMonk_Q_resonatingStrike_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false);
            SpellEffectCreate(out _, out _, "blindMonk_Q_resonatingStrike_tar_blood.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false);
            SpellEffectCreate(out slow, out _, "blindMonk_Q_tar_indicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
            RemovePerceptionBubble(bubbleID2);
            SpellEffectRemove(slow);
            if (GetBuffCountFromCaster(attacker, owner, nameof(Buffs.BlindMonkQManager)) > 0)
            {
                SpellBuffRemove(attacker, nameof(Buffs.BlindMonkQManager), (ObjAIBase)owner);
            }
        }
    }
}
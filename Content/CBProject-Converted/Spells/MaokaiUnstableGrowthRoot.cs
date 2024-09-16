namespace Buffs
{
    public class MaokaiUnstableGrowthRoot : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "MaokaiUnstableGrowthRoot",
            BuffTextureName = "GreenTerror_SpikeSlam.dds",
            PopupMessage = new[] { "game_floatingtext_Snared", },
        };
        EffectEmitter rootParticleEffect2;
        EffectEmitter rootParticleEffect;
        public override void OnActivate()
        {
            SetCanMove(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
            TeamId teamOfOwner = GetTeamID_CS(owner); // UNUSED
            SpellEffectCreate(out rootParticleEffect2, out _, "maokai_elementalAdvance_root_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            SpellEffectCreate(out rootParticleEffect, out _, "maokai_elementalAdvance_root_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            SpellEffectRemove(rootParticleEffect2);
            SpellEffectRemove(rootParticleEffect);
        }
        public override void OnUpdateStats()
        {
            SetCanMove(owner, false);
        }
    }
}
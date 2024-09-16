namespace Buffs
{
    public class YorickDeathGripTarget : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "GLOBAL_SLOW.TROY", },
            BuffName = "Wall of Pain Slow",
            BuffTextureName = "Lich_WallOfPain.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        EffectEmitter rootParticleEffect2;
        EffectEmitter rootParticleEffect;
        public override void OnActivate()
        {
            SetCanMove(owner, false);
            SpellEffectCreate(out rootParticleEffect2, out _, "SwainShadowGraspRootTemp.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            SpellEffectCreate(out rootParticleEffect, out _, "swain_shadowGrasp_magic.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
        }
        public override void OnUpdateStats()
        {
            SetCanMove(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            SpellEffectRemove(rootParticleEffect2);
            SpellEffectRemove(rootParticleEffect);
        }
    }
}
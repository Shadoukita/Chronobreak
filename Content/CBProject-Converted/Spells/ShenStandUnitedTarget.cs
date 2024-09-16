namespace Buffs
{
    public class ShenStandUnitedTarget : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Shen Stand United Target",
            BuffTextureName = "Shen_StandUnited.dds",
        };
        EffectEmitter particle;
        public override void OnActivate()
        {
            SpellEffectCreate(out particle, out _, "shen_Teleport_target_v2.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position3D, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
        }
    }
}
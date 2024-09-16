namespace Buffs
{
    public class TutorialDamagePlayerBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "FallenOne_tar.troy", },
            BuffName = "FallenOne",
            BuffTextureName = "Lich_DeathRay.dds",
        };
        public override void OnDeactivate(bool expired)
        {
            SpellEffectCreate(out _, out _, "FallenOne_nova.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            ApplyDamage((ObjAIBase)owner, owner, 200, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1);
        }
    }
}
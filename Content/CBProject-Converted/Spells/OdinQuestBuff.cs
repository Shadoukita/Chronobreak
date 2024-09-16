namespace Buffs
{
    public class OdinQuestBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinQuestBuff",
            BuffTextureName = "Odin_MarkoftheConqueror.dds",
            NonDispellable = true,
        };
        EffectEmitter buffParticle;
        public override void OnActivate()
        {
            SpellEffectCreate(out buffParticle, out _, "odin_quest_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(buffParticle);
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            damageAmount *= 1.1f;
        }
    }
}
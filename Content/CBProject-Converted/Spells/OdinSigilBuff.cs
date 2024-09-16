namespace Buffs
{
    public class OdinSigilBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "OdinRedBuff",
            BuffTextureName = "48thSlave_WaveOfLoathing.dds",
            NonDispellable = true,
        };
        EffectEmitter buffParticle;
        public override void OnActivate()
        {
            IncScaleSkinCoef(0.25f, owner);
            SpellEffectCreate(out buffParticle, out _, "NeutralMonster_buf_red_offense.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(buffParticle);
        }
        public override void OnUpdateStats()
        {
            IncScaleSkinCoef(0.25f, owner);
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            string targetName = GetUnitSkinName(target);
            if (targetName != "OdinChaosGuardian" && targetName != "OdinOrderGuardian" && targetName != "OdinNeutralGuardian" && targetName != "OdinShrineBomb")
            {
                damageAmount *= 1.4f;
            }
        }
    }
}
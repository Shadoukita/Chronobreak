namespace Spells
{
    public class UrgotEntropyPassive : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class UrgotEntropyPassive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "UrgotEntropyDebuff",
            BuffTextureName = "Urgot_Passive.dds",
        };
        EffectEmitter particle1;
        public override void OnActivate()
        {
            SpellEffectCreate(out particle1, out _, "UrgotEntropy_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle1);
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (damageType != DamageType.DAMAGE_TYPE_TRUE)
            {
                damageAmount *= 0.85f;
            }
        }
    }
}
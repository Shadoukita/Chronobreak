namespace Spells
{
    public class PotionOfElusiveness : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            AddBuff(owner, owner, new Buffs.PotionOfElusiveness(), 1, 1, 240, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class PotionOfElusiveness : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "PotionofElusiveness_itm.troy", },
            BuffName = "Elixer of Elusiveness",
            BuffTextureName = "2038_Potion_of_Elusiveness.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float bonusAttackSpeed;
        public override void OnActivate()
        {
            int level = GetLevel(owner);
            float bonusAttackSpeed = level * 0.0059f;
            this.bonusAttackSpeed = bonusAttackSpeed + 0.114f;
            IncPermanentPercentAttackSpeedMod(owner, this.bonusAttackSpeed);
            IncPermanentFlatCritChanceMod(owner, 0.08f);
        }
        public override void OnDeactivate(bool expired)
        {
            float bonusAttackSpeed = -1 * this.bonusAttackSpeed;
            IncPermanentPercentAttackSpeedMod(owner, bonusAttackSpeed);
            IncPermanentFlatCritChanceMod(owner, -0.08f);
        }
    }
}
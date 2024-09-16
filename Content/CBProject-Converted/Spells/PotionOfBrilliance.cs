namespace Spells
{
    public class PotionOfBrilliance : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            AddBuff(owner, owner, new Buffs.PotionOfBrilliance(), 1, 1, 240, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class PotionOfBrilliance : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "PotionofBrilliance_itm.troy", },
            BuffName = "Elixer of Brilliance",
            BuffTextureName = "2039_Potion_of_Brilliance.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float bonusAP;
        public override void OnActivate()
        {
            int level = GetLevel(owner);
            float bonusAP = level * 1.18f;
            this.bonusAP = bonusAP + 18.82f;
            IncPermanentFlatMagicDamageMod(owner, this.bonusAP);
            IncPermanentPercentCooldownMod(owner, -0.1f);
        }
        public override void OnDeactivate(bool expired)
        {
            float bonusAP = -1 * this.bonusAP;
            IncPermanentFlatMagicDamageMod(owner, bonusAP);
            IncPermanentPercentCooldownMod(owner, 0.1f);
        }
    }
}
namespace Spells
{
    public class PotionOfGiantStrength : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            AddBuff(owner, owner, new Buffs.PotionOfGiantStrength(), 1, 1, 240, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class PotionOfGiantStrength : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "PotionofGiantStrength_itm.troy", },
            BuffName = "Elixer of Fortitude",
            BuffTextureName = "2037_Potion_of_Giant_Strength.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float bonusHealth;
        public override void OnActivate()
        {
            int level = GetLevel(owner);
            float bonusHealth = level * 5.59f;
            this.bonusHealth = bonusHealth + 134.41f;
            IncPermanentFlatHPPoolMod(owner, this.bonusHealth);
            IncPermanentFlatPhysicalDamageMod(owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            float bonusHealth = -1 * this.bonusHealth;
            IncPermanentFlatHPPoolMod(owner, bonusHealth);
            IncPermanentFlatPhysicalDamageMod(owner, -10);
        }
    }
}
namespace Spells
{
    public class ViktorHexCore : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = false,
        };
    }
}
namespace Buffs
{
    public class ViktorHexCore : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            charVars.ManaToADD = 0;
            charVars.HealthToADD = 0;
            int ownerLevel = GetLevel(owner);
            charVars.BonusForItem = ownerLevel * 3;
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            float manaCost = GetPARCost(spell);
            float maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            float currHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA); // UNUSED
            float maxHealthReturn = maxHealth * 0.2f; // UNUSED
            float maxPAR = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
            float currPAR = GetPAR(owner, PrimaryAbilityResourceType.MANA); // UNUSED
            float maxPARReturn = maxPAR * 0.2f; // UNUSED
            float currentADDMana = manaCost * 0.1f;
            float currentADDHealth = manaCost * 0.1f;
            charVars.ManaToADD += currentADDMana;
            charVars.HealthToADD += currentADDHealth;
        }
        public override void OnLevelUp()
        {
            int ownerLevel = GetLevel(owner);
            charVars.BonusForItem = ownerLevel * 3;
        }
    }
}
namespace Spells
{
    public class Masochism : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 25, 35, 45, 55, 65 };
        int[] effect1 = { -25, -35, -45, -55, -65 };
        float[] effect2 = { 0.4f, 0.55f, 0.7f, 0.85f, 1 };
        int[] effect3 = { 40, 55, 70, 85, 100 };
        public override bool CanCast()
        {
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level > 0)
            {
                int healthCost = effect0[level - 1];
                float temp1 = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                return temp1 >= healthCost;
            }
            return true; //TODO: Verify
        }
        public override void SelfExecute()
        {
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float healthCost = effect1[level - 1];
            float nextBuffVars_DamageMod = effect2[level - 1];
            int nextBuffVars_BaseIncrease = effect3[level - 1];
            IncHealth(owner, healthCost, owner);
            AddBuff(attacker, target, new Buffs.Masochism(nextBuffVars_DamageMod, nextBuffVars_BaseIncrease), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class Masochism : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "R_hand", "L_hand", "", "", },
            AutoBuffActivateEffect = new[] { "dr_mundo_masochism_cas.troy", "dr_mundo_masochism_cas.troy", },
            BuffName = "Masochism",
            BuffTextureName = "DrMundo_Masochism.dds",
            IsDeathRecapSource = true,
        };
        float damageMod;
        float baseIncrease;
        public Masochism(float damageMod = default, float baseIncrease = default)
        {
            this.damageMod = damageMod;
            this.baseIncrease = baseIncrease;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageMod);
            //RequireVar(this.baseIncrease);
            OverrideAutoAttack(1, SpellSlotType.ExtraSlots, owner, 1, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveOverrideAutoAttack(owner, true);
        }
        public override void OnUpdateStats()
        {
            float baseIncrease = this.baseIncrease;
            float damageMod = this.damageMod;
            float health = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            float healthMissing = 1 - health;
            float rawDamage = 100 * healthMissing;
            float damageBonus = damageMod * rawDamage;
            IncFlatPhysicalDamageMod(owner, damageBonus);
            IncFlatPhysicalDamageMod(owner, baseIncrease);
        }
    }
}
namespace Spells
{
    public class Bloodlust : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 30f, 26f, 22f, 18f, 14f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "TryndamereDemonsword", },
        };
        int[] effect0 = { 30, 40, 50, 60, 70 };
        float[] effect1 = { 0.5f, 0.95f, 1.4f, 1.85f, 2.3f };
        public override void SelfExecute()
        {
            float currentFury = GetPAR(owner, PrimaryAbilityResourceType.Other);
            float baseHeal = effect0[level - 1];
            float healthPerFury = effect1[level - 1];
            float healthToRestore = currentFury * healthPerFury;
            healthToRestore += baseHeal;
            float spellPower = GetFlatMagicDamageMod(owner);
            float abilityPowerMod = 1.5f * spellPower;
            healthToRestore += abilityPowerMod;
            IncHealth(owner, healthToRestore, owner);
            SpellEffectCreate(out _, out _, "Tryndamere_Heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            float furyToRemove = -1 * currentFury;
            IncPAR(owner, furyToRemove, PrimaryAbilityResourceType.Other);
        }
    }
}
namespace Buffs
{
    public class Bloodlust : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "Bloodlust",
            BuffTextureName = "DarkChampion_Bloodlust.dds",
            SpellToggleSlot = 1,
        };
        float damageMod;
        float critDamageMod;
        public Bloodlust(float damageMod = default, float critDamageMod = default)
        {
            this.damageMod = damageMod;
            this.critDamageMod = critDamageMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageMod);
            //RequireVar(this.critDamageMod);
            int count = GetBuffCountFromAll(owner, nameof(Buffs.Bloodlust));
            float totalDamage = count * damageMod;
            float totalCritDamage = count * critDamageMod;
            totalCritDamage *= 100;
            SetBuffToolTipVar(1, totalDamage);
            SetBuffToolTipVar(2, totalCritDamage);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, damageMod);
            IncFlatCritDamageMod(owner, critDamageMod);
        }
    }
}
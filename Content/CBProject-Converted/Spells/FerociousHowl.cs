namespace Spells
{
    public class FerociousHowl : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 120f, 100f, 80f, 10f, 10f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 0.5f, 0.6f, 0.7f };
        int[] effect1 = { 60, 75, 90 };
        int[] effect2 = { 7, 7, 7 };
        public override void SelfExecute()
        {
            SpellBuffRemoveType(owner, BuffType.STUN);
            SpellBuffRemoveType(owner, BuffType.SILENCE);
            SpellBuffRemoveType(owner, BuffType.TAUNT);
            SpellBuffRemoveType(owner, BuffType.POLYMORPH);
            SpellBuffRemoveType(owner, BuffType.SLOW);
            SpellBuffRemoveType(owner, BuffType.SNARE);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.FEAR);
            SpellBuffRemoveType(owner, BuffType.CHARM);
            SpellBuffRemoveType(owner, BuffType.SUPPRESSION);
            float nextBuffVars_DamageReduction = effect0[level - 1];
            float nextBuffVars_bonusDamage = effect1[level - 1];
            AddBuff(owner, owner, new Buffs.FerociousHowl(nextBuffVars_DamageReduction, nextBuffVars_bonusDamage), 1, 1, effect2[level - 1], BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.AlistarTrample(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, false, false, false);
        }
    }
}
namespace Buffs
{
    public class FerociousHowl : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "pelvis", },
            AutoBuffActivateEffect = new[] { "minatuar_unbreakableWill_cas.troy", "feroscioushowl_cas2.troy", },
            BuffName = "Ferocious Howl",
            BuffTextureName = "Minotaur_FerociousHowl.dds",
        };
        float damageReduction;
        float bonusDamage;
        public FerociousHowl(float damageReduction = default, float bonusDamage = default)
        {
            this.damageReduction = damageReduction;
            this.bonusDamage = bonusDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageReduction);
            //RequireVar(this.bonusDamage);
        }
        public override void OnUpdateStats()
        {
            IncPercentMagicReduction(owner, damageReduction);
            IncPercentPhysicalReduction(owner, damageReduction);
            IncFlatPhysicalDamageMod(owner, bonusDamage);
        }
    }
}
namespace Spells
{
    public class VolibearR : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.75f,
            SpellDamageRatio = 0.75f,
        };
        int[] effect0 = { 75, 115, 155 };
        int[] effect1 = { 0, 0, 0 };
        int[] effect2 = { 4, 5, 6 };
        int[] effect3 = { 12, 12, 12 };
        public override void SelfExecute()
        {
            int nextBuffVars_VolibearRDamage = effect0[level - 1];
            int nextBuffVars_VolibearRSpeed = effect1[level - 1];
            float nextBuffVars_VolibearRRatio = 0.3f;
            int volibearRCharges = effect2[level - 1]; // UNUSED
            AddBuff(owner, owner, new Buffs.VolibearRApplicator(nextBuffVars_VolibearRDamage, nextBuffVars_VolibearRSpeed, nextBuffVars_VolibearRRatio), 1, 1, effect3[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            CancelAutoAttack(owner, true);
        }
    }
}
namespace Buffs
{
    public class VolibearR : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "VolibearR",
            BuffTextureName = "Minotaur_Pulverize.dds",
            PopupMessage = new[] { "game_floatingtext_Knockup", },
        };
    }
}
namespace Spells
{
    public class GravesPassiveGrit : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class GravesPassiveGrit : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "GravesTrueGrit.troy", },
            BuffName = "GravesPassiveGrit",
            BuffTextureName = "GravesTrueGrit.dds",
        };
        float lastTimeExecuted;
        public override void OnDeactivate(bool expired)
        {
            SpellBuffClear(owner, nameof(Buffs.GravesPassiveGrit));
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, charVars.ArmorAmount);
            IncFlatSpellBlockMod(owner, charVars.ArmorAmount);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                float count = GetBuffCountFromAll(owner, nameof(Buffs.GravesPassiveGrit));
                count--;
                float total = count * charVars.ArmorAmount;
                SetBuffToolTipVar(1, total);
            }
        }
        public override void OnUpdateAmmo()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.GravesPassiveCounter)) == 0)
            {
                SpellBuffClear(owner, nameof(Buffs.GravesPassiveGrit));
            }
        }
    }
}
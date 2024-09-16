namespace Spells
{
    public class GravesPassiveCounter : SpellScript
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
    public class GravesPassiveCounter : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "GravesPassiveCounter",
            BuffTextureName = "GravesTrueGrit.dds",
        };
        float lastTimeExecuted;
        public override void OnActivate()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.GravesPassiveGrit)) == 0)
            {
                AddBuff(attacker, owner, new Buffs.GravesPassiveGrit(), 11, 2, 4, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            }
            charVars.ArmorAmountNeg = charVars.ArmorAmount * -1;
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, charVars.ArmorAmountNeg);
            IncFlatSpellBlockMod(owner, charVars.ArmorAmountNeg);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                int count = GetBuffCountFromAll(owner, nameof(Buffs.GravesPassiveGrit));
                if (count < 11)
                {
                    AddBuff(attacker, owner, new Buffs.GravesPassiveGrit(), 11, 1, 4, BuffAddType.STACKS_AND_CONTINUE, BuffType.COUNTER, 0, true, false, false);
                }
            }
        }
    }
}
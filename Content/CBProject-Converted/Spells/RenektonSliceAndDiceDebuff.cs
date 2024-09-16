namespace Spells
{
    public class RenektonSliceAndDiceDebuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class RenektonSliceAndDiceDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "RenektonSliceAndDiceShred",
            BuffTextureName = "Renekton_Dice.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float armorShred;
        public RenektonSliceAndDiceDebuff(float armorShred = default)
        {
            this.armorShred = armorShred;
        }
        public override void OnActivate()
        {
            //RequireVar(this.armorShred);
            IncPercentArmorMod(owner, armorShred);
        }
        public override void OnUpdateStats()
        {
            IncPercentArmorMod(owner, armorShred);
        }
    }
}
namespace Spells
{
    public class FlaskOfCrystalWater : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override bool CanCast()
        {
            float tempTable1_manaPercent = GetPARPercent(owner, PrimaryAbilityResourceType.MANA);
            return tempTable1_manaPercent <= 0.99f;
        }
        public override void SelfExecute()
        {
            AddBuff((ObjAIBase)target, target, new Buffs.FlaskOfCrystalWater(), 5, 1, 15, BuffAddType.STACKS_AND_CONTINUE, BuffType.HEAL, 0, false, false, false);
            AddBuff((ObjAIBase)target, target, new Buffs.Potion_Internal(), 1, 1, 15, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.CountManaPotion = GetBuffCountFromAll(owner, nameof(Buffs.FlaskOfCrystalWater));
        }
    }
}
namespace Buffs
{
    public class FlaskOfCrystalWater : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "ManaPotion_itm.troy", },
            BuffName = "Mana Potion",
            BuffTextureName = "2004_Flask_of_Crystal_Water.dds",
        };
        public override void OnDeactivate(bool expired)
        {
            if (charVars.CountManaPotion >= 2)
            {
                int stacksToAdd = charVars.CountManaPotion - 1;
                AddBuff((ObjAIBase)owner, owner, new Buffs.FlaskOfCrystalWater(), 5, stacksToAdd, 15, BuffAddType.STACKS_AND_RENEWS, BuffType.HEAL, 0, true, false, false);
                AddBuff((ObjAIBase)target, target, new Buffs.Potion_Internal(), 1, 1, 15, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                charVars.CountManaPotion = 0;
            }
        }
    }
}
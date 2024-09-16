using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects.StatsNS;

namespace Chronobreak.GameServer.Inventory
{
    public class ItemData : StatsModifier
    {
        // Meta
        public int Id { get; init; }
        public string Name { get; init; }

        // General
        public int MaxStacks { get; init; }
        public int Price { get; init; }
        public string ItemGroup { get; init; }
        public bool Consumed { get; init; }
        public string SpellName { get; init; }
        public float SellBackModifier { get; init; }

        // Recipes
        public int[] RecipeItem { get; init; }

        // Not from data
        public ItemRecipe Recipe { get; init; }
        public int TotalPrice => Recipe.TotalPrice;

        public bool ClearUndoHistoryOnActivate { get; init; }

        public ItemData(INIContentFile file)
        {
            Name = file.Name;
            Id = int.Parse(file.Name);

            MaxStacks = file.GetValue("Data", "MaxStack", 1);
            Price = file.GetValue("Data", "Price", 0);
            ItemGroup = file.GetValue("Data", "ItemGroup", "");
            Consumed = file.GetValue("Data", "Consumed", false);
            SpellName = file.GetValue("Data", "SpellName", "");
            SellBackModifier = file.GetValue("Data", "SellBackModifier", 0.7f);

            RecipeItem = new int[4];
            RecipeItem[0] = file.GetValue("Data", "RecipeItem1", -1);
            RecipeItem[1] = file.GetValue("Data", "RecipeItem2", -1);
            RecipeItem[2] = file.GetValue("Data", "RecipeItem3", -1);
            RecipeItem[3] = file.GetValue("Data", "RecipeItem4", -1);

            AbilityPower.FlatBonus = file.GetFloat("Data", "FlatMagicDamageMod");
            AbilityPower.PercentBonus = file.GetFloat("Data", "PercentMagicDamageMod");

            Armor.FlatBonus = file.GetFloat("Data", "FlatArmorMod");
            Armor.PercentBonus = file.GetFloat("Data", "PercentArmorMod");

            AttackDamage.FlatBonus = file.GetFloat("Data", "FlatPhysicalDamageMod");
            AttackDamage.PercentBonus = file.GetFloat("Data", "PercentPhysicalDamageMod");
            AttackSpeedMultiplier.PercentBonus = file.GetFloat("Data", "PercentAttackSpeedMod");

            CriticalChance.PercentBonus = file.GetFloat("Data", "FlatCritChanceMod");
            CriticalDamage.PercentBonus = file.GetFloat("Data", "FlatCritDamageMod");
            CriticalDamage.PercentMultiplicativeBonus = file.GetFloat("Data", "PercentCritDamageMod");

            HealthPoints.FlatBonus = file.GetFloat("Data", "FlatHPPoolMod");
            HealthPoints.PercentBonus = file.GetFloat("Data", "PercentHPPoolMod");
            HealthRegeneration.PercentBonus = file.GetFloat("Data", "PercentBaseHPRegenMod");

            LifeSteal.PercentBonus = file.GetFloat("Data", "PercentLifeStealMod");

            ManaPoints.FlatBonus = file.GetFloat("Data", "FlatMPPoolMod");
            ManaPoints.PercentBonus = file.GetFloat("Data", "PercentMPPoolMod");
            ManaRegeneration.PercentBonus = file.GetFloat("Data", "PercentBaseMPRegenMod");

            MagicPenetration.FlatBonus = file.GetFloat("Data", "FlatMagicPenetrationMod");
            MagicResist.FlatBonus = file.GetFloat("Data", "FlatSpellBlockMod");
            MagicResist.PercentBonus = file.GetFloat("Data", "PercentSpellBlockMod");

            MoveSpeed.FlatBonus = file.GetFloat("Data", "FlatMovementSpeedMod");
            MoveSpeed.PercentBonus = file.GetFloat("Data", "PercentMovementSpeedMod");

            ExpBonus.PercentBonus = file.GetFloat("Data", "PercentEXPBonus");

            ClearUndoHistoryOnActivate = file.GetBool("Data", "ClearUndoHistoryOnActivate");

            Recipe = new(this);
        }
    }
}
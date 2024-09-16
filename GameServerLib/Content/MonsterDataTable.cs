using Chronobreak.GameServer.Content;
using System.Collections.Generic;

namespace GameServerLib.Content
{
    class MonsterDataTable
    {
        internal readonly Dictionary<int, float> AbilityPower = [];
        internal readonly Dictionary<int, float> Armor = [];
        internal readonly Dictionary<int, float> AttackDamage = [];
        internal readonly Dictionary<int, float> Experience = [];
        internal readonly Dictionary<int, float> Gold = [];
        internal readonly Dictionary<int, float> Health = [];
        internal readonly Dictionary<int, float> MagicResist = [];

        internal MonsterDataTable(INIContentFile file)
        {
            ReadData(AbilityPower, "AbilityPower", file);
            ReadData(Armor, "Armor", file);
            ReadData(AttackDamage, "AttackDamage", file);
            ReadData(Experience, "Experience", file);
            ReadData(Gold, "Gold", file);
            ReadData(Health, "Health", file);
            ReadData(MagicResist, "MagicResist", file);
        }

        private static void ReadData(Dictionary<int, float> data, string section, INIContentFile file)
        {
            for (int i = 0; file.HasMentionOf(section, $"Level{i}"); i++)
            {
                data.Add(i, file.GetFloat(section, $"Level{i}"));
            }
        }
    }
}
using System;
using Newtonsoft.Json;
using Chronobreak.GameServer.Content;
using JO = Newtonsoft.Json.JsonObjectAttribute;
using MS = Newtonsoft.Json.MemberSerialization;

namespace Chronobreak.GameServer.GameObjects.StatsNS
{
    public class BaseStats
    {
        [JO(MS.OptIn)]
        public abstract class SimpleStat : Stat
        {
            public float BaseValue { set => _baseFlatPerm = value; }
            public float PercentBase { set => _basePercentPerm = value; }
            public float FlatBonus { set => _flatBonusPerm = value; }
            public float PercentBonus { set => _percentBonusPerm = value; }

            public void IncBaseValuePerm(float by) => _baseFlatPerm += by;
            public void MulBaseValuePerm(float by) => _baseFlatPerm *= by;
            public void IncPercentBasePerm(float by) => _basePercentPerm += by;
            public void IncFlatBonus(float by) => _flatBonusTemp += by;
            public void IncFlatBonusPerm(float by) => _flatBonusPerm += by;
            public void IncPercentBonus(float by) => _percentBonusTemp += by;
            public void IncPercentBonusPerm(float by) => _percentBonusPerm += by;
        }
        [JO(MS.OptIn)]
        public abstract class SimplePercentStat : Stat
        {
            public float PercentBase { set => _baseFlatPerm = value; }
            public float PercentBonus { set => _flatBonusPerm = value; }
            public float PercentMultiplicativeBase { set => _basePercentPerm = value; }
            public float PercentMultiplicativeBonus { set => _percentBonusPerm = value; }

            public void IncPercentBasePerm(float by) => _baseFlatPerm += by;
            public void IncPercentBonus(float by) => _flatBonusTemp += by;
            public void IncPercentBonusPerm(float by) => _flatBonusPerm += by;
            public void IncPercentMultiplicativeBasePerm(float by) => _basePercentPerm += by;
            public void IncPercentMultiplicativeBonus(float by) => _percentBonusTemp += by;
            public void IncPercentMultiplicativeBonusPerm(float by) => _percentBonusPerm += by;
        }

        public ADStat AttackDamage { get; } = new();
        [JO(MS.OptIn)]
        public class ADStat : SimpleStat
        {
            public float Total => _totalBase + _totalBonus;
            public float TotalBase => _totalBase;
            public float TotalBonus => _totalBonus;
            public new float FlatBonus
            {
                set => base.FlatBonus = value;
                get => _flatBonus;
            }
            public new float PercentBonus
            {
                set => base.PercentBonus = value;
                get => _percentBonus;
            }
        }

        public APStat AbilityPower { get; } = new();
        [JO(MS.OptIn)]
        public class APStat : SimpleStat
        {
            public float Total => _totalFlat * (1 + _totalPercent);
            public float TotalBase => _baseFlat * (1 + _basePercent);
        }

        public ResistStat Armor { get; } = new();
        public ResistStat MagicResist { get; } = new();
        [JO(MS.OptIn)]
        public class ResistStat : SimpleStat
        {
            public float Total => _totalFlat * (1 + _totalPercent);
        }

        public DividedStat PhysicalReduction { get; } = new();
        public DividedStat MagicalReduction { get; } = new();
        public DividedStat ExpBonus { get; } = new();
        [JO(MS.OptIn)]
        public class DividedStat : SimpleStat
        {
            public float TotalFlat => _totalFlat;
            public float TotalPercent => _totalPercent;
        }
        public PenetrationStat ArmorPenetration { get; } = new();
        public PenetrationStat MagicPenetration { get; } = new();
        [JO(MS.OptIn)]
        public class PenetrationStat : DividedStat
        {
            public new float PercentBonus
            {
                set => base.PercentBonus = value;
                get => _percentBonus;
            }
        }

        public CDRStat CooldownReduction { get; } = new();
        [JO(MS.OptIn)]
        public class CDRStat : SimplePercentStat
        {
            public float Total => Math.Max(
                GlobalData.GlobalCharacterDataConstants.PercentCooldownModMinimum,
                _totalFlat * (1 + _totalPercent)
            );
        }

        public PercentStat CriticalDamage { get; } = new();
        public PercentStat LifeSteal { get; } = new();
        public PercentStat SpellVamp { get; } = new();
        public PercentStat Tenacity { get; } = new();
        [JO(MS.OptIn)]
        public class PercentStat : SimplePercentStat
        {
            public float Total => _totalFlat * (1 + _totalPercent);
        }

        public ChanceStat CriticalChance { get; } = new();
        public ChanceStat DodgeChance { get; } = new();
        public ChanceStat MissChance { get; } = new();
        [JO(MS.OptIn)]
        public class ChanceStat : SimplePercentStat
        {
            public float Total => Math.Clamp(_totalFlat * (1 + _totalPercent), 0, 1);
        }

        public PoolStat HealthPoints { get; } = new();
        public PoolStat ManaPoints { get; } = new();
        [JO(MS.OptIn)]
        public class PoolStat : SimpleStat
        {
            public float Total => _totalFlat * (1 + _totalPercent);
            public new float FlatBonus
            {
                set => base.FlatBonus = value;
                get => _flatBonus;
            }
        }

        public RegenStat HealthRegeneration { get; } = new();
        public RegenStat ManaRegeneration { get; } = new();
        public RegenStat ExpGivenOnDeath { get; } = new(); //TODO:
        public RegenStat GoldGivenOnDeath { get; } = new(); //TODO:
        public RegenStat GoldPerSecond { get; } = new();
        [JO(MS.OptIn)]
        public class RegenStat : SimpleStat
        {
            public float Total => _totalFlat * (1 + _totalPercent);
            public float TotalBase => _baseFlat * (1 + _basePercent);
        }

        public RangeStat AcquisitionRange { get; } = new();
        public RangeStat PerceptionRange { get; } = new();
        public RangeStat Range { get; } = new(); //TODO: Rename back to AttackRange
        [JO(MS.OptIn)]
        public class RangeStat : SimpleStat
        {
            public float Total => _totalFlat * (1 + _totalPercent);
            public float TotalPercent => _totalPercent;
            public float TotalBonus => Total - _baseFlat;
            public new float FlatBonus
            {
                set => this.FlatBonus = value;
                get => _flatBonus;
            }
        }

        public MultiplierStat AttackSpeedMultiplier { get; } = new();
        public MultiplierStat Size { get; } = new();
        [JO(MS.OptIn)]
        public class MultiplierStat : SimplePercentStat
        {
            public float Total => (1 + _totalFlat) * (1 + _totalPercent);
        }

        public MoveSpeed MoveSpeed { get; } = new();

        [JsonIgnore] protected internal Stat[] Stats;

        public BaseStats()
        {
            Stats ??= new Stat[]
            {
                AbilityPower,
                AcquisitionRange,
                Armor,
                ArmorPenetration,
                AttackDamage,
                AttackSpeedMultiplier,
                CooldownReduction,
                CriticalChance,
                CriticalDamage,
                GoldGivenOnDeath,
                GoldPerSecond,
                HealthPoints,
                HealthRegeneration,
                LifeSteal,
                MagicPenetration,
                MagicResist,
                ManaPoints,
                ManaRegeneration,
                PerceptionRange,
                Range,
                Size,
                SpellVamp,
                Tenacity,
                PhysicalReduction,
                MagicalReduction,
                MissChance,
                DodgeChance
            };
        }
    }
}

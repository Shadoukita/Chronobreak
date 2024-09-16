using System;
using System.Linq;
//using System.Text.Json.Serialization;
using GameServerCore.Enums;

namespace Chronobreak.GameServer.Content
{
    public class PassiveData
    {
        public string PassiveAbilityName { get; set; } = "";
        public int[] PassiveLevels { get; set; } = new int[6];
        public string PassiveLuaName { get; set; } = "";
        public string PassiveNameStr { get; set; } = "";

        //TODO: Extend into handling several passives, when we decide on a format for that case.
    }

    public class CharData
    {
        public float AbilityPowerIncPerLevel { get; init; } = 0.0f;
        public float AcquisitionRange { get; init; } = 750.0f;
        public bool AllyCanUse { get; init; } = false;
        public bool AlwaysVisible { get; init; } = false;
        public bool AlwaysUpdatePAR { get; init; } = false;
        public float Armor { get; init; } = 1.0f;
        public float ArmorPerLevel { get; init; } = 0.0f;
        public float AttackAutoInterruptPercent { get; init; } = 0.2f;
        public float AttackCastTime { get; init; } = 0.0f;
        public float AttackDelayCastOffsetPercent { get; init; } = 0.0f;
        public float AttackDelayCastOffsetPercentAttackSpeedRatio { get; init; } = 1.0f;
        public float AttackDelayOffsetPercent { get; init; } = 0.0f;
        public float AttackRange { get; init; } = 100.0f;
        public float AttackSpeedPerLevel { get; init; } = 0.0f;
        public float AttackTotalTime { get; init; } = 0.0f;
        public float BaseAbilityPower { get; init; } = 0.0f;
        public float BaseAttackProbability { get; init; } = 1.0f;
        public float BaseCritChance { get; init; } = 0.0f;
        public float BaseDamage { get; init; } = 10.0f;
        public float BaseDodge { get; init; } = 0.0f;
        public float BaseFactorHPRegen { get; init; } = 0.0f;
        public float BaseMissChance { get; init; } = 0.0f;
        public int BaseMoveSpeed { get; init; } = 100;
        public float BaseHp { get; init; } = 100.0f;
        public float BaseMp { get; init; } = 100.0f;
        public float BaseStaticHpRegen { get; init; } = 1.0f;
        public float BaseStaticMpRegen { get; init; } = 1.0f;
        public float BaseFactorPARRegen { get; init; } = 0.0f; //BaseFactorMPRegen on character files
        public float CooldownSpellSlot { get; init; } = 0.0f;
        public float CritDamageMultiplier { get; init; } = 2.0f;
        public float CritAttackDelayCastOffsetPercent { get; init; } = 0.0f;
        public float CritAttackDelayCastOffsetPercentAttackSpeedRatio { get; init; } = 0.0f;
        public float CritAttackDelayOffsetPercent { get; init; } = 0.0f;
        public float CritAttackProbability { get; init; } = 0.0f;
        public float DamagePerLevel { get; init; } = 0.0f;
        public float DeathTime { get; init; } = -1.0f;
        public bool DisableContinuousTargetFacing { get; init; } = false;
        public float DodgePerLevel { get; init; } = 0.0f;
        public bool EnemyCanUse { get; init; } = false;
        public float ExpGivenOnDeath { get; init; } = 48.0f;
        public float ExperienceRadius { get; init; } = 0.0f;
        public float GameplayCollisionRadius { get; init; } = 65.0f;
        public float GlobalExpGivenOnDeath { get; init; } = 0.0f;
        public float GlobalGoldGivenOnDeath { get; init; } = 0.0f;
        public float GoldGivenOnDeath { get; init; } = 25.0f;
        public float GoldRadius { get; init; } = 0.0f;
        public string HeroUseSpell { get; init; } = string.Empty;
        public float HpPerLevel { get; init; } = 10.0f;
        public float HpRegenPerLevel { get; init; } = 0.0f;
        public bool IsMelee { get; init; } //Yes or no
        public bool Immobile { get; init; } = false;
        public bool IsTower { get; init; } = false;
        public bool IsUseable { get; init; } = false;
        public float LocalGoldGivenOnDeath { get; init; } = 0.0f;
        public float LocalExpGivenOnDeath { get; init; } = 0.0f;
        public bool MinionUseable { get; init; } = false;
        public string MinionUseSpell { get; init; } = string.Empty;
        public int MonsterDataTableID { get; init; } = 0;
        public float MpPerLevel { get; init; } = 10.0f;
        public float MpRegenPerLevel { get; init; } = 0.0f;
        public PrimaryAbilityResourceType ParType { get; init; } = PrimaryAbilityResourceType.MANA;
        public float PathfindingCollisionRadius { get; init; } = -1.0f;
        public float PerceptionBubbleRadius { get; init; } = 1350.0f;
        public bool ShouldFaceTarget { get; init; } = true;
        public float Significance { get; init; } = 0.0f;
        public float SpellBlock { get; init; } = 0.0f;
        public float SpellBlockPerLevel { get; init; } = 0.0f;
        public float TowerTargetingPriority { get; init; } = 0.0f;
        public UnitTag UnitTags { get; init; }

        public string[] SpellNames { get; init; } = new string[4];
        public string[] ExtraSpells { get; init; } = new string[16];
        public int[] MaxLevels { get; init; } = { 5, 5, 5, 3 };

        public int[][] SpellsUpLevels { get; init; } =
        {
            new[] {1, 3, 5, 7, 9, 99},
            new[] {1, 3, 5, 7, 9, 99},
            new[] {1, 3, 5, 7, 9, 99},
            new[] {6, 11, 16, 99, 99, 99}
        };

        public BasicAttackInfo[] BasicAttacks { get; init; } =
            Enumerable.Range(0, 18).Select(i => new BasicAttackInfo()).ToArray();

        // TODO: Verify if we want this to be an array.
        public PassiveData PassiveData { get; init; } = new();

        public CharData()
        {
        }

        public CharData(INIContentFile file)
        {
            string name = file.Name;

            AcquisitionRange = file.GetValue("Data", "AcquisitionRange", AcquisitionRange);

            Armor = file.GetValue("Data", "Armor", Armor);
            ArmorPerLevel = file.GetValue("Data", "ArmorPerLevel", ArmorPerLevel);

            AttackCastTime = file.GetValue("Data", "AttackCastTime", AttackCastTime);
            AttackDelayCastOffsetPercent = file.GetValue("Data", "AttackDelayCastOffsetPercent", AttackDelayCastOffsetPercent);
            AttackDelayCastOffsetPercentAttackSpeedRatio = file.GetValue("Data", "AttackDelayCastOffsetPercentAttackSpeedRatio", AttackDelayCastOffsetPercentAttackSpeedRatio);
            AttackDelayOffsetPercent = file.GetValue("Data", "AttackDelayOffsetPercent", AttackDelayOffsetPercent);
            AttackRange = file.GetValue("Data", "AttackRange", AttackRange);
            AttackSpeedPerLevel = file.GetValue("Data", "AttackSpeedPerLevel", AttackSpeedPerLevel);
            AttackTotalTime = file.GetValue("Data", "AttackTotalTime", AttackTotalTime);

            BaseAttackProbability = file.GetValue("Data", "BaseAttack_Probability", BaseAttackProbability);
            BaseDamage = file.GetValue("Data", "BaseDamage", BaseDamage);
            BaseHp = file.GetValue("Data", "BaseHP", BaseHp);
            BaseMoveSpeed = file.GetValue("Data", "MoveSpeed", BaseMoveSpeed);
            BaseMp = file.GetValue("Data", "BaseMP", BaseMp);
            BaseStaticHpRegen = file.GetValue("Data", "BaseStaticHPRegen", BaseStaticHpRegen);
            BaseStaticMpRegen = file.GetValue("Data", "BaseStaticMPRegen", BaseStaticMpRegen);

            CritAttackDelayCastOffsetPercent = file.GetValue("Data", "CritAttack_AttackDelayCastOffsetPercent", CritAttackDelayCastOffsetPercent);
            CritAttackDelayCastOffsetPercentAttackSpeedRatio = file.GetValue("Data", "CritAttack_AttackDelayCastOffsetPercentAttackSpeedRatio", CritAttackDelayCastOffsetPercentAttackSpeedRatio);
            CritAttackDelayOffsetPercent = file.GetValue("Data", "CritAttack_AttackDelayOffsetPercent", CritAttackDelayOffsetPercent);
            CritDamageMultiplier = file.GetValue("Data", "CritDamageBonus", CritDamageMultiplier);

            DamagePerLevel = file.GetValue("Data", "DamagePerLevel", DamagePerLevel);
            DisableContinuousTargetFacing = file.GetValue("Data", "DisableContinuousTargetFacing", DisableContinuousTargetFacing);
            ExpGivenOnDeath = file.GetValue("Data", "ExpGivenOnDeath", ExpGivenOnDeath);
            GameplayCollisionRadius = file.GetValue("Data", "GameplayCollisionRadius", GameplayCollisionRadius);
            GlobalExpGivenOnDeath = file.GetValue("Data", "GlobalExpGivenOnDeath", GlobalExpGivenOnDeath);
            GlobalGoldGivenOnDeath = file.GetValue("Data", "GlobalGoldGivenOnDeath", GlobalGoldGivenOnDeath);
            GoldGivenOnDeath = file.GetValue("Data", "GoldGivenOnDeath", GoldGivenOnDeath);
            HpRegenPerLevel = file.GetValue("Data", "HPRegenPerLevel", HpRegenPerLevel);
            HpPerLevel = file.GetValue("Data", "HPPerLevel", HpPerLevel);
            Immobile = file.GetValue("Data", "Imobile", Immobile);
            IsMelee = file.GetValue("Data", "IsMelee", IsMelee);

            LocalGoldGivenOnDeath = file.GetValue("Data", "LocalGoldGivenOnDeath", LocalGoldGivenOnDeath);
            MonsterDataTableID = file.GetValue("Data", "MonsterDataTableId", MonsterDataTableID);
            MpRegenPerLevel = file.GetValue("Data", "MPRegenPerLevel", MpRegenPerLevel);
            MpPerLevel = file.GetValue("Data", "MPPerLevel", MpPerLevel);
            PathfindingCollisionRadius = file.GetValue("Data", "PathfindingCollisionRadius", PathfindingCollisionRadius);
            PerceptionBubbleRadius = file.GetValue("Data", "PerceptionBubbleRadius", PerceptionBubbleRadius);
            ShouldFaceTarget = file.GetValue("Data", "ShouldFaceTarget", ShouldFaceTarget);
            SpellBlock = file.GetValue("Data", "SpellBlock", SpellBlock);
            SpellBlockPerLevel = file.GetValue("Data", "SpellBlockPerLevel", SpellBlockPerLevel);

            EnemyCanUse = file.GetValue("Useable", "EnemyCanUse", EnemyCanUse);
            AllyCanUse = file.GetValue("Useable", "AllyCanUse", AllyCanUse);
            HeroUseSpell = file.GetValue("Useable", "HeroUseSpell", HeroUseSpell);
            CooldownSpellSlot = file.GetValue("Useable", "CooldownSpellSlot", CooldownSpellSlot);
            IsUseable = file.GetValue("Useable", "IsUseable", IsUseable);
            MinionUseable = file.GetValue("Useable", "MinionUseable", MinionUseable);
            MinionUseSpell = file.GetValue("Useable", "MinionUseSpell", MinionUseSpell);

            AlwaysVisible = file.GetValue("Minion", "AlwaysVisible", AlwaysVisible);
            IsTower = file.GetValue("Minion", "IsTower", IsTower);
            AlwaysUpdatePAR = file.GetValue("Minion", "AlwaysUpdatePAR", AlwaysUpdatePAR);

            foreach (var tag in file.GetValue("Data", "UnitTags", "").Split(" | "))
            {
                Enum.TryParse(tag, out UnitTag unitTag);
                UnitTags |= unitTag;
            }

            Enum.TryParse<PrimaryAbilityResourceType>(file.GetValue("Data", "PARType", ParType.ToString()),
                out var tempPar);
            ParType = tempPar;

            for (var i = 0; i < 4; i++)
            {
                SpellNames[i] = file.GetValue("Data", $"Spell{i + 1}", "");
            }

            for (var i = 0; i < 16; i++)
            {
                ExtraSpells[i] = file.GetValue("Data", $"ExtraSpell{i + 1}", "");
            }

            for (var i = 0; i < 4; i++)
            {
                SpellsUpLevels[i] = file.GetIntArray("Data", $"SpellsUpLevels{i + 1}", SpellsUpLevels[i]);
            }

            PassiveData.PassiveLuaName = file.GetValue("Data", "Passive1LuaName", "");

            MaxLevels = file.GetIntArray("Data", "MaxLevels", MaxLevels);

            //Main AutoAttack
            BasicAttacks[0] = new BasicAttackInfo(
                AttackDelayOffsetPercent,
                AttackDelayCastOffsetPercent,
                AttackDelayCastOffsetPercentAttackSpeedRatio,
                name: name + "BasicAttack",
                attackCastTime: AttackCastTime,
                attackTotalTime: AttackTotalTime,
                probability: BaseAttackProbability
            );

            int nameIndex = 2;
            //Secondary/Extra AutoAttacks
            for (var i = 1; i < 9; i++)
            {
                var attackName = file.GetValue("Data", $"ExtraAttack{i}", "");

                //AncientGolem for example, doesn't have his ExtraAttacks explicitly defined in his file, but it has "ExtraAttack_Probability" which implies the existance of ExtraAttacks
                if (string.IsNullOrEmpty(attackName) && file.HasMentionOf("Data", $"ExtraAttack{i}"))
                {
                    attackName = $"{name}BasicAttack{nameIndex}";
                }

                if (BasicAttacks.FirstOrDefault(x => x.Name == attackName) != null)
                {
                    nameIndex++;
                    continue;
                }
                float offsetPercent = AttackDelayCastOffsetPercent = file.GetValue("Data", $"ExtraAttack{i}_AttackDelayCastOffsetPercent", AttackDelayCastOffsetPercent);

                BasicAttacks[i] = new BasicAttackInfo(
                    AttackDelayOffsetPercent,
                    offsetPercent,
                    AttackDelayCastOffsetPercentAttackSpeedRatio,
                    name: attackName,
                    attackCastTime: file.GetValue("Data", $"ExtraAttack{i}_AttackCastTime", AttackCastTime),
                    attackTotalTime: file.GetValue("Data", $"ExtraAttack{i}_AttackTotalTime", AttackTotalTime),
                    probability: file.GetValue("Data", $"ExtraAttack{i}_Probability", BaseAttackProbability)
                );
                nameIndex++;
            }

            //Main Crit AutoAttack
            BasicAttacks[9] = new BasicAttackInfo(
                CritAttackDelayOffsetPercent,
                CritAttackDelayCastOffsetPercent,
                CritAttackDelayCastOffsetPercentAttackSpeedRatio,
                name: file.GetValue("Data", $"CritAttack", ""),
                attackCastTime: AttackCastTime,
                attackTotalTime: AttackTotalTime,
                probability: CritAttackProbability
            );

            //Secondary Crit AutoAttacks
            for (var i = 1; i < 9; i++)
            {
                var index = i + 9;
                var attackName = file.GetValue("Data", $"ExtraCritAttack{i}", "");
                float delayOffset = file.GetValue("Data", $"{attackName}_AttackDelayOffsetPercent", AttackDelayOffsetPercent);
                float delayCastOffsetPercent = file.GetValue("Data", $"{attackName}_AttackDelayCastOffsetPercent", CritAttackDelayCastOffsetPercent);

                BasicAttacks[index] = new BasicAttackInfo(
                    delayOffset,
                    delayCastOffsetPercent,
                    CritAttackDelayCastOffsetPercentAttackSpeedRatio,
                    name: attackName,
                    attackCastTime: AttackCastTime,
                    attackTotalTime: AttackTotalTime,
                    probability: CritAttackProbability
                );
            }
        }
    }
}

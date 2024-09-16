using System;
using GameServerCore.Enums;
using Chronobreak.GameServer.Content;

namespace Chronobreak.GameServer.GameObjects.StatsNS
{
    public class Stats : BaseStats
    {
        public ulong SpellsEnabled { get; private set; }
        public ulong SummonerSpellsEnabled { get; private set; }

        public bool IsDead
        {
            get
            {
                if (IsZombie) return false;
                return _isDead;
            }
            internal set => _isDead = value;
        }

        public bool IsZombie { get; internal set; }
        public bool IsMagicImmune { get; set; }
        public bool IsPhysicalImmune { get; set; }

        public float AttackSpeedFlat { get; set; }
        public float RespawnTimer { get; internal set; }
        public float HealthPerLevel { get; set; }
        public float ManaPerLevel { get; set; }
        public float ArmorPerLevel { get; set; }
        public float MagicResistPerLevel { get; set; }
        public float AttackDamagePerLevel { get; set; }
        public float HealthRegenerationPerLevel { get; set; }
        public float ManaRegenerationPerLevel { get; set; }
        public float GrowthAttackSpeed { get; set; }

        public ActionState ActionState { get; private set; }
        public PrimaryAbilityResourceType PrimaryAbilityResourceType { get; set; }
        public SpellDataFlags IsTargetableToTeam { get; set; }

        public bool RegenEnabled = true;

        private float _currentHealth;
        private float _currentMana;
        private bool _isDead;

        public float CurrentHealth
        {
            //get => _currentHealth = Math.Clamp(_currentHealth, 0.0f, HealthPoints.Total);
            //internal set => _currentHealth = Math.Clamp(value, 0.0f, HealthPoints.Total);
            internal set
            {
                _currentHealth = Math.Max(0, value);
                _oldStat.HP = _currentHealth;
            }
            get => _currentHealth;
        }

        public float CurrentMana
        {
            //get  =>  _currentMana = Math.Clamp(_currentMana, 0.0f, ManaPoints.Total);
            //internal set => _currentMana = Math.Clamp(value, 0.0f, ManaPoints.Total);
            set
            {
                _currentMana = Math.Max(0, value);
                _oldStat.PAR = _currentMana;
            }
            get => _currentMana;
        }

        private HPMP _oldStat;
        internal record struct HPMP(float HP, float TotalHP, float PAR, float TotalPAR);

        internal void SaveStatState()
        {
            _oldStat = GetHPMP();
        }

        internal void RestoreStatState()
        {
            CurrentHealth = Math.Clamp(_oldStat.HP, 0, HealthPoints.Total);
            CurrentMana = Math.Clamp(_oldStat.PAR, 0, ManaPoints.Total);
        }

        internal HPMP GetHPMP()
        {
            return new HPMP(CurrentHealth, HealthPoints.Total, CurrentMana, ManaPoints.Total);
        }
        internal void AddHPMPDiff(HPMP prev)
        {
            CurrentHealth = prev.HP + MathF.Max(0, HealthPoints.Total - prev.TotalHP);
            CurrentMana = prev.PAR + MathF.Max(0, ManaPoints.Total - prev.TotalPAR);
        }

        public Stats(CharData charData)
        {
            SaveStatState();
            var prev = GetHPMP();

            ActionState = ActionState.CAN_ATTACK | ActionState.CAN_CAST | ActionState.CAN_MOVE | ActionState.TARGETABLE;
            IsTargetableToTeam = SpellDataFlags.TargetableToAll;

            AcquisitionRange.BaseValue = charData.AcquisitionRange;
            AttackDamagePerLevel = charData.DamagePerLevel;
            Armor.BaseValue = charData.Armor;
            ArmorPerLevel = charData.ArmorPerLevel;
            AttackDamage.BaseValue = charData.BaseDamage;

            // AttackSpeedFlat = GlobalAttackSpeed / CharAttackDelay
            AttackSpeedFlat = (1.0f / GlobalData.GlobalCharacterDataConstants.AttackDelay) / (1.0f + charData.AttackDelayOffsetPercent);

            CriticalDamage.PercentBase = charData.CritDamageMultiplier;
            ExpGivenOnDeath.BaseValue = charData.ExpGivenOnDeath;
            GoldGivenOnDeath.BaseValue = charData.GoldGivenOnDeath;
            GrowthAttackSpeed = charData.AttackSpeedPerLevel;
            HealthPerLevel = charData.HpPerLevel;
            HealthPoints.BaseValue = charData.BaseHp;
            HealthRegeneration.BaseValue = charData.BaseStaticHpRegen;
            HealthRegenerationPerLevel = charData.HpRegenPerLevel;
            MagicResist.BaseValue = charData.SpellBlock;
            MagicResistPerLevel = charData.SpellBlockPerLevel;
            ManaPerLevel = charData.MpPerLevel;
            ManaPoints.BaseValue = charData.BaseMp;
            ManaRegeneration.BaseValue = charData.BaseStaticMpRegen;
            ManaRegenerationPerLevel = charData.MpRegenPerLevel;
            MoveSpeed.Flat = charData.BaseMoveSpeed;
            PerceptionRange.BaseValue = charData.PerceptionBubbleRadius <= 0 ? 1100 : charData.PerceptionBubbleRadius;
            PrimaryAbilityResourceType = charData.ParType;
            Range.BaseValue = charData.AttackRange;

            if (charData.ParType == PrimaryAbilityResourceType.None)
            {
                ManaPoints.BaseValue = 0;
            }

            /* Verify whether the following situations are truly required.
            Aatrox, Yasuo are two clear examples, and Tryndamere's CharData has MPPerLevel = 0
            so I think the following determination will be removed
            if (charData.ParType != PrimaryAbilityResourceType.MANA)
            {
                ManaPerLevel = 0;
            }*/

            AddHPMPDiff(prev);
        }

        internal void AddModifier(StatsModifier modifier)
        {
            var prev = GetHPMP();

            bool acceptMana = PrimaryAbilityResourceType is PrimaryAbilityResourceType.MANA;
            for (int i = 0; i < Stats.Length; i++)
            {
                var modified = Stats[i];
                if (acceptMana || (modified != ManaPoints && modified != ManaRegeneration))
                {
                    modified.ApplyStatModifier(modifier.Stats[i]);
                }
            }
            MoveSpeed.ApplyStatModifier(modifier.MoveSpeed);

            AddHPMPDiff(prev);
        }

        internal void RemoveModifier(StatsModifier modifier)
        {
            var prev = GetHPMP();

            bool acceptMana = PrimaryAbilityResourceType is PrimaryAbilityResourceType.MANA;
            for (int i = 0; i < Stats.Length; i++)
            {
                var modified = Stats[i];
                if (acceptMana || (modified != ManaPoints && modified != ManaRegeneration))
                {
                    modified.RemoveStatModifier(modifier.Stats[i]);
                }
            }
            MoveSpeed.RemoveStatModifier(modifier.MoveSpeed);

            AddHPMPDiff(prev);
        }

        internal void ResetToPermanent()
        {
            var prev = GetHPMP();

            for (int i = 0; i < Stats.Length; i++)
            {
                Stats[i].ResetToPermanent();
            }
            MoveSpeed.ResetToPermanent();

            AddHPMPDiff(prev);
        }

        internal float GetTotalAttackSpeed()
        {
            return AttackSpeedFlat * AttackSpeedMultiplier.Total;
        }

        internal virtual void Update(float diff)
        {
            if (!IsDead && RegenEnabled)
            {
                CurrentHealth += Math.Max(0, HealthRegeneration.Total) * diff / 1000.0f;
                if (
                    PrimaryAbilityResourceType
                    is PrimaryAbilityResourceType.MANA
                    or PrimaryAbilityResourceType.Energy //TODO: Verify
                )
                {
                    CurrentMana += Math.Max(0, ManaRegeneration.Total) * diff / 1000.0f;
                }
            }
            CurrentHealth = Math.Clamp(CurrentHealth, 0, HealthPoints.Total);
            CurrentMana = Math.Clamp(CurrentMana, 0, ManaPoints.Total);
        }

        internal void LevelUp(int currentLevel, int amount = 1)
        {
            if (amount <= 0) return;

            //This is a modifier used by league on level up. It is used so the stat increase isn't linear
            float levelUpModifier = (0.65f + 0.035f * 0.5f * (2 * currentLevel + amount + 1)) * amount;
            /*
            // Equivalent notation:
            float levelUpModifier = 0;
            int desiredLevel = currentLevel + amount;
            for (int i = currentLevel + 1; i <= desiredLevel; i++)
            {
                levelUpModifier += 0.65f + 0.035f * i;
            }
            */

            var prev = GetHPMP();

            HealthPoints.IncBaseValuePerm(HealthPerLevel * levelUpModifier);
            HealthRegeneration.IncBaseValuePerm(HealthRegenerationPerLevel * levelUpModifier);
            ManaPoints.IncBaseValuePerm(ManaPerLevel * levelUpModifier);
            ManaRegeneration.IncBaseValuePerm(ManaRegenerationPerLevel * levelUpModifier);
            AttackDamage.IncBaseValuePerm(AttackDamagePerLevel * levelUpModifier);
            AttackSpeedMultiplier.IncPercentBasePerm(GrowthAttackSpeed / 100 * levelUpModifier);
            Armor.IncBaseValuePerm(ArmorPerLevel * levelUpModifier);
            MagicResist.IncBaseValuePerm(MagicResistPerLevel * levelUpModifier);

            AddHPMPDiff(prev);
        }

        internal bool GetSpellEnabled(byte id)
        {
            return (SpellsEnabled & 1u << id) != 0;
        }

        internal void SetSpellEnabled(byte id, bool enabled)
        {
            if (enabled)
            {
                SpellsEnabled |= 1u << id;
            }
            else
            {
                SpellsEnabled &= ~(1u << id);
            }
        }

        internal bool GetSummonerSpellEnabled(byte id)
        {
            return (SummonerSpellsEnabled & 16u << id) != 0;
        }

        internal void SetSummonerSpellEnabled(byte id, bool enabled)
        {
            if (enabled)
            {
                SummonerSpellsEnabled |= 16u << id;
            }
            else
            {
                SummonerSpellsEnabled &= ~(16u << id);
            }
        }

        internal bool GetActionState(ActionState state)
        {
            return ActionState.HasFlag(state);
        }

        internal void SetActionState(ActionState state, bool enabled)
        {
            if (enabled)
            {
                ActionState |= state;
            }
            else
            {
                ActionState &= ~state;
            }
        }

        internal void HealthSetToMax()
        {
            CurrentHealth = HealthPoints.Total;
        }
    }
}

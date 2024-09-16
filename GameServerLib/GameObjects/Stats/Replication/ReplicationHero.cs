using GameServerCore.Enums;
using GameServerLib.GameObjects.Stats;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

namespace Chronobreak.GameServer.GameObjects.StatsNS
{
    public class ReplicationHero : Replication
    {
        new Champion Owner;
        public ReplicationHero(Champion owner) : base(owner)
        {
            Owner = owner;
        }

        public EvolutionState EvolutionState => Owner.EvolutionState;

        internal override void Update()
        {
            UpdateUint(EvolutionState.EvolvePoints, 0, 6); //mEvolvePoints
            UpdateUint(EvolutionState.EvolveFlags, 0, 7); //mEvolveFlag
            UpdateFloat(0, Owner.GoldOwner.Gold, 0, 0); //mGold
            UpdateFloat(0, Owner.GoldOwner.TotalGoldEarned, 0, 1); //mGoldTotal
            UpdateUint((uint)Stats.SpellsEnabled, 0, 2); //mReplicatedSpellCanCastBitsLower1
            UpdateUint((uint)(Stats.SpellsEnabled >> 32), 0, 3); //mReplicatedSpellCanCastBitsUpper1
            UpdateUint((uint)Stats.SummonerSpellsEnabled, 0, 4); //mReplicatedSpellCanCastBitsLower2
            UpdateUint((uint)(Stats.SummonerSpellsEnabled >> 32), 0, 5); //mReplicatedSpellCanCastBitsUpper2
            for (short i = 0; i < 4; i++)
            {
                //SpellSlots
                UpdateFloat(0, Owner.Spells[i].ManaCost, 0, 8 + i); //ManaCost_{i}
            }
            for (short i = 0; i < 16; i++)
            {
                UpdateFloat(0, Owner.Spells[(short)(SpellSlotType.ExtraSlots + i)].ManaCost, 0, 12 + i); //ManaCost_Ex{i}
            }
            UpdateUint((uint)(Stats.ActionState | ActionState.IS_GHOSTED), 1, 0); //ActionState
            UpdateBool(Stats.IsMagicImmune, 1, 1); //MagicImmune
            UpdateBool(Owner.IsInvulnerable, 1, 2); //IsInvulnerable
            UpdateBool(Stats.IsPhysicalImmune, 1, 3); //IsPhysicalImmune
            UpdateBool(Owner.IsLifestealImmune, 1, 4); //IsLifestealImmune
            UpdateFloat(0, Stats.AttackDamage.TotalBase, 1, 5); //mBaseAttackDamage
            UpdateFloat(0, Stats.AbilityPower.TotalBase, 1, 6); //mBaseAbilityDamage
            UpdateFloat(2, Stats.DodgeChance.Total, 1, 7); //mDodge
            UpdateFloat(2, Stats.CriticalChance.Total, 1, 8); //mCrit
            UpdateFloat(0, Stats.Armor.Total, 1, 9); //mArmor
            UpdateFloat(0, Stats.MagicResist.Total, 1, 10); //mSpellBlock
            UpdateFloat(1, Stats.HealthRegeneration.Total, 1, 11); //mHPRegenRate
            UpdateFloat(1, Stats.ManaRegeneration.Total, 1, 12); //mPARRegenRate
            UpdateFloat(0, Stats.Range.Total, 1, 13); //mAttackRange
            UpdateFloat(0, Stats.AttackDamage.FlatBonus, 1, 14); //mFlatPhysicalDamageMod
            UpdateFloat(2, Stats.AttackDamage.PercentBonus, 1, 15); //mPercentPhysicalDamageMod
            UpdateFloat(0, Stats.AbilityPower.Total, 1, 16); //mFlatMagicDamageMod
            UpdateFloat(0, Stats.MagicalReduction.TotalFlat, 1, 17); //mFlatMagicReduction
            UpdateFloat(2, Stats.MagicalReduction.TotalPercent, 1, 18); //mPercentMagicReduction
            UpdateFloat(2, Stats.AttackSpeedMultiplier.Total, 1, 19); //mAttackSpeedMod
            UpdateFloat(0, Stats.Range.TotalBonus, 1, 20); //mFlatCastRangeMod
            // TODO: Find out why a negative value is required for ability cooldowns to display properly.
            UpdateFloat(2, -Stats.CooldownReduction.Total, 1, 21); //mPercentCooldownMod
            if (Owner.Spells.Passive.CurrentCooldown > 0)
            {
                UpdateFloat(2, Game.Time.GameTime / 1000 + Owner.Spells.Passive.CurrentCooldown, 1, 22); //mPassiveCooldownEndTime
                UpdateFloat(2, Owner.Spells.Passive.Cooldown, 1, 23); //mPassiveCooldownTotalTime
            }
            else
            {
                UpdateFloat(0, 0, 1, 22); //mPassiveCooldownEndTime
                UpdateFloat(0, 0, 1, 23); //mPassiveCooldownTotalTime
            }
            UpdateFloat(2, Stats.ArmorPenetration.TotalFlat, 1, 24); //mFlatArmorPenetration
            UpdateFloat(2, Stats.ArmorPenetration.TotalPercent, 1, 25); //mPercentArmorPenetration
            UpdateFloat(2, Stats.MagicPenetration.TotalFlat, 1, 26); //mFlatMagicPenetration
            UpdateFloat(2, Stats.MagicPenetration.TotalPercent, 1, 27); //mPercentMagicPenetration
            UpdateFloat(2, Stats.LifeSteal.Total, 1, 28); //mPercentLifeStealMod
            UpdateFloat(2, Stats.SpellVamp.Total, 1, 29); //mPercentSpellVampMod
            UpdateFloat(2, Stats.Tenacity.Total, 1, 30); //mPercentCCReduction
            UpdateFloat(2, Stats.ArmorPenetration.PercentBonus, 2, 0); //mPercentBonusArmorPenetration
            UpdateFloat(2, Stats.MagicPenetration.PercentBonus, 2, 1); //mPercentBonusMagicPenetration
            UpdateFloat(1, Stats.HealthRegeneration.TotalBase, 2, 2); //mBaseHPRegenRate
            UpdateFloat(1, Stats.ManaRegeneration.TotalBase, 2, 3); //mBasePARRegenRate
            UpdateFloat(0, Stats.CurrentHealth, 3, 0); //mHP
            UpdateFloat(0, Stats.CurrentMana, 3, 1); //mMP
            UpdateFloat(0, Stats.HealthPoints.Total, 3, 2); //mMaxHP
            UpdateFloat(0, Stats.ManaPoints.Total, 3, 3); //mMaxMP
            UpdateFloat(0, Owner.Experience.Exp, 3, 4); //mExp
            // UpdateFloat(Stats.LifeTime, 3, 5); //mLifetime
            // UpdateFloat(Stats.MaxLifeTime, 3, 6); //mMaxLifetime
            // UpdateFloat(Stats.LifeTimeTicks, 3, 7); //mLifetimeTicks
            UpdateFloat(0, Stats.PerceptionRange.FlatBonus, 3, 8); //mFlatBubbleRadiusMod
            UpdateFloat(2, Stats.PerceptionRange.TotalPercent, 3, 9); //mPercentBubbleRadiusMod
            UpdateFloat(2, Stats.MoveSpeed.Total, 3, 10); //mMoveSpeed
            UpdateFloat(2, Stats.Size.Total, 3, 11); //mSkinScaleCoef(mistyped as mCrit)
            // UpdateFloat(Stats.FlatPathfindingRadiusMod, 3, 12); //mPathfindingRadiusMod
            UpdateInt(Owner.Experience.Level, 3, 13); //mLevelRef
            UpdateUint((uint)Owner.ChampionStatistics.NeutralMinionsKilled, 3, 14); //mNumNeutralMinionsKilled
            UpdateBool(Owner.IsTargetable, 3, 15); //mIsTargetable
            UpdateUint((uint)Stats.IsTargetableToTeam, 3, 16); //mIsTargetableToTeamFlags
        }
    }
}

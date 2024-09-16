using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

namespace Chronobreak.GameServer.GameObjects.StatsNS
{
    public class ReplicationLaneMinion : Replication
    {
        public ReplicationLaneMinion(LaneMinion owner) : base(owner)
        {
        }
        internal override void Update()
        {
            UpdateFloat(0, Stats.CurrentHealth, 1, 0); //mHP
            UpdateFloat(0, Stats.HealthPoints.Total, 1, 1); //mMaxHP
            // UpdateFloat(Stats.LifeTime, 1, 2); //mLifetime
            // UpdateFloat(Stats.MaxLifeTime, 1, 3); //mMaxLifetime
            // UpdateFloat(Stats.LifeTimeTicks, 1, 4); //mLifetimeTicks
            UpdateFloat(0, Stats.ManaPoints.Total, 1, 5); //mMaxMP
            UpdateFloat(0, Stats.CurrentMana, 1, 6); //mMP
            UpdateUint((uint)(Stats.ActionState | ActionState.IS_GHOSTED), 1, 7); //ActionState
            UpdateBool(Stats.IsMagicImmune, 1, 8); //MagicImmune
            UpdateBool(Owner.IsInvulnerable, 1, 9); //IsInvulnerable
            UpdateBool(Stats.IsPhysicalImmune, 1, 10); //IsPhysicalImmune
            UpdateBool(Owner.IsLifestealImmune, 1, 11); //IsLifestealImmune
            UpdateFloat(0, Stats.AttackDamage.TotalBase, 1, 12); //mBaseAttackDamage
            UpdateFloat(0, Stats.Armor.Total, 1, 13); //mArmor
            UpdateFloat(0, Stats.MagicResist.Total, 1, 14); //mSpellBlock
            UpdateFloat(2, Stats.AttackSpeedMultiplier.Total, 1, 15); //mAttackSpeedMod
            UpdateFloat(0, Stats.AttackDamage.FlatBonus, 1, 16); //mFlatPhysicalDamageMod
            UpdateFloat(2, Stats.AttackDamage.PercentBonus, 1, 17); //mPercentPhysicalDamageMod
            UpdateFloat(0, Stats.AbilityPower.Total, 1, 18); //mFlatMagicDamageMod
            UpdateFloat(1, Stats.HealthRegeneration.Total, 1, 19); //mHPRegenRate
            UpdateFloat(1, Stats.ManaRegeneration.Total, 1, 20); //mPARRegenRate
            UpdateFloat(0, Stats.MagicalReduction.TotalFlat, 1, 21); //mFlatMagicReduction
            UpdateFloat(2, Stats.MagicalReduction.TotalPercent, 1, 22); //mPercentMagicReduction
            UpdateFloat(0, Stats.PerceptionRange.FlatBonus, 3, 0); //mFlatBubbleRadiusMod
            UpdateFloat(2, Stats.PerceptionRange.TotalPercent, 3, 1); //mPercentBubbleRadiusMod
            UpdateFloat(2, Stats.MoveSpeed.Total, 3, 2); //mMoveSpeed
            UpdateFloat(2, Stats.Size.Total, 3, 3); //mSkinScaleCoef(mistyped as mCrit)
            UpdateBool(Owner.IsTargetable, 3, 4); //mIsTargetable
            UpdateUint((uint)Stats.IsTargetableToTeam, 3, 5); //mIsTargetableToTeamFlags
        }
    }
}

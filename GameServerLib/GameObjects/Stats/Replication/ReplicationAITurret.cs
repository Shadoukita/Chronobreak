using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

namespace Chronobreak.GameServer.GameObjects.StatsNS
{
    public class ReplicationAITurret : Replication
    {
        public ReplicationAITurret(BaseTurret owner) : base(owner)
        {
        }
        internal override void Update()
        {
            UpdateFloat(0, Stats.ManaPoints.Total, 1, 0); //mMaxMP
            UpdateFloat(0, Stats.CurrentMana, 1, 1); //mMP
            UpdateUint((uint)(Stats.ActionState | ActionState.IS_GHOSTED), 1, 2); //ActionState
            UpdateBool(Stats.IsMagicImmune, 1, 3); //MagicImmune
            UpdateBool(Owner.IsInvulnerable, 1, 4); //IsInvulnerable
            UpdateBool(Stats.IsPhysicalImmune, 1, 5); //IsPhysicalImmune
            UpdateBool(Owner.IsLifestealImmune, 1, 6); //IsLifestealImmune
            UpdateFloat(0, Stats.AttackDamage.TotalBase, 1, 7); //mBaseAttackDamage
            UpdateFloat(0, Stats.Armor.Total, 1, 8); //mArmor
            UpdateFloat(0, Stats.MagicResist.Total, 1, 9); //mSpellBlock
            UpdateFloat(2, Stats.AttackSpeedMultiplier.Total, 1, 10); //mAttackSpeedMod
            UpdateFloat(0, Stats.AttackDamage.FlatBonus, 1, 11); //mFlatPhysicalDamageMod
            UpdateFloat(2, Stats.AttackDamage.PercentBonus, 1, 12); //mPercentPhysicalDamageMod
            UpdateFloat(0, Stats.AbilityPower.Total, 1, 13); //mFlatMagicDamageMod
            UpdateFloat(1, Stats.HealthRegeneration.Total, 1, 14); //mHPRegenRate
            UpdateFloat(0, Stats.CurrentHealth, 3, 0); //mHP
            UpdateFloat(0, Stats.HealthPoints.Total, 3, 1); //mMaxHP
            UpdateFloat(0, Stats.PerceptionRange.FlatBonus, 3, 2); //mFlatBubbleRadiusMod
            UpdateFloat(2, Stats.PerceptionRange.TotalPercent, 3, 3); //mPercentBubbleRadiusMod
            UpdateFloat(0, Stats.MoveSpeed.Total, 3, 4); //mMoveSpeed
            UpdateFloat(2, Stats.Size.Total, 3, 5); //mSkinScaleCoef(mistyped as mCrit)
            UpdateBool(Owner.IsTargetable, 5, 0); //mIsTargetable
            UpdateUint((uint)Stats.IsTargetableToTeam, 5, 1); //mIsTargetableToTeamFlags
        }
    }
}

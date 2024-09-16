using System;

namespace Chronobreak.GameServer.Content
{
    //TODO: Check official default values
    public class BasicAttackInfo
    {
        public string Name { get; init; }
        public float AttackCastTime { get; init; }
        public float AttackDelayCastOffsetPercent { get; init; }
        public float AttackDelayCastOffsetPercentAttackSpeedRatio { get; init; }
        public float AttackDelayOffsetPercent { get; init; }
        public float AttackTotalTime { get; init; }
        public float Probability { get; init; } = 0;

        public BasicAttackInfo()
        {
        }

        public BasicAttackInfo(
            float delayCastOffset,
            float delayCastOffsetPercent,
            float delayCastOffsetPercentAttackSpeedRatio,
            string name,
            float attackCastTime,
            float attackTotalTime,
            float probability
        )
        {
            AttackDelayOffsetPercent = delayCastOffset;
            AttackDelayCastOffsetPercent = delayCastOffsetPercent;
            AttackDelayCastOffsetPercentAttackSpeedRatio = delayCastOffsetPercentAttackSpeedRatio;

            Name = name;
            AttackCastTime = attackCastTime;
            AttackTotalTime = attackTotalTime;
            Probability = probability;

            float atkCastTime = Math.Min(AttackTotalTime, AttackCastTime);
            float atkDelayCastOffsetPercent = AttackDelayCastOffsetPercent;
            if (AttackTotalTime > 0.0f && atkCastTime > 0.0f)
            {
                AttackDelayOffsetPercent = (AttackTotalTime / GlobalData.GlobalCharacterDataConstants.AttackDelay) - 1.0f;
                AttackDelayCastOffsetPercent = (atkCastTime / AttackTotalTime) - GlobalData.GlobalCharacterDataConstants.AttackDelayCastPercent;
                AttackDelayCastOffsetPercentAttackSpeedRatio = 1.0f;
            }
            else
            {
                AttackDelayCastOffsetPercent = Math.Max(atkDelayCastOffsetPercent, -GlobalData.GlobalCharacterDataConstants.AttackDelayCastPercent);
            }
        }
    }
}

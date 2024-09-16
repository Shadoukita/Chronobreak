namespace Buffs
{
    public class OdinGolemBombBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        Region bubbleID;
        Region bubbleID2;
        float lastTimeExecuted;
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                if (type == BuffType.SNARE)
                {
                    duration *= 0.7f;
                }
                if (type == BuffType.SLOW)
                {
                    duration *= 0.7f;
                }
                if (type == BuffType.FEAR)
                {
                    duration *= 0.7f;
                }
                if (type == BuffType.CHARM)
                {
                    duration *= 0.7f;
                }
                if (type == BuffType.SLEEP)
                {
                    duration *= 0.7f;
                }
                if (type == BuffType.STUN)
                {
                    duration *= 0.7f;
                }
                if (type == BuffType.TAUNT)
                {
                    duration *= 0.7f;
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            SetGhosted(owner, true);
            float nextBuffVars_HPPerLevel = 315;
            AddBuff((ObjAIBase)owner, owner, new Buffs.HPByPlayerLevel(nextBuffVars_HPPerLevel), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            TeamId orderTeam = TeamId.TEAM_ORDER;
            TeamId chaosTeam = TeamId.TEAM_CHAOS;
            bubbleID = AddUnitPerceptionBubble(orderTeam, 650, owner, 25000, default, default, false);
            bubbleID2 = AddUnitPerceptionBubble(chaosTeam, 650, owner, 25000, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
            RemovePerceptionBubble(bubbleID2);
        }
        public override void OnUpdateStats()
        {
            SetGhosted(owner, true);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, false))
            {
                bool run = false;
                if (run)
                {
                    if (!IsDead(owner))
                    {
                        bool killedGuardian = false;
                        foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 450, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, nameof(Buffs.OdinGuardianBuff), true))
                        {
                            killedGuardian = true;
                            ApplyDamage((ObjAIBase)owner, unit, 1000000000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, (ObjAIBase)owner);
                        }
                        if (killedGuardian)
                        {
                            ApplyDamage(attacker, owner, 1000000000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, attacker);
                        }
                    }
                }
            }
        }
    }
}
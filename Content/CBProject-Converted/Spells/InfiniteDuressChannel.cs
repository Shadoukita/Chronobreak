namespace Spells
{
    public class InfiniteDuressChannel : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            ChannelDuration = 2.1f,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void ChannelingSuccessStop()
        {
            SpellBuffRemove(attacker, nameof(Buffs.InfiniteDuressSound), attacker, 0);
        }
        public override void ChannelingCancelStop()
        {
            SpellBuffRemove(attacker, nameof(Buffs.InfiniteDuressChannel), attacker, 0);
            SpellBuffRemove(attacker, nameof(Buffs.InfiniteDuressSound), attacker, 0);
            SpellBuffRemove(target, nameof(Buffs.Suppression), attacker, 0);
        }
    }
}
namespace Buffs
{
    public class InfiniteDuressChannel : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Infinite Duress",
            BuffTextureName = "Wolfman_InfiniteDuress.dds",
        };
        float hitsRemaining;
        float damagePerTick;
        float lastTimeExecuted;
        public InfiniteDuressChannel(float hitsRemaining = default, float damagePerTick = default)
        {
            this.hitsRemaining = hitsRemaining;
            this.damagePerTick = damagePerTick;
        }
        public override void OnActivate()
        {
            //RequireVar(this.hitsRemaining);
            //RequireVar(this.damagePerTick);
            SpellEffectCreate(out _, out _, "InfiniteDuress_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            if (expired)
            {
                if (hitsRemaining > 0)
                {
                    while (hitsRemaining > 0)
                    {
                        SpellEffectCreate(out _, out _, "InfiniteDuress_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                        ApplyDamage(attacker, owner, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
                        hitsRemaining--;
                    }
                }
            }
            else
            {
                StopChanneling(attacker, ChannelingStopCondition.Cancel, ChannelingStopSource.LostTarget);
            }
            IssueOrder(owner, OrderType.AttackTo, default, target);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, true))
            {
                if (GetBuffCountFromCaster(owner, attacker, nameof(Buffs.Suppression)) == 0)
                {
                    StopChanneling(attacker, ChannelingStopCondition.Cancel, ChannelingStopSource.LostTarget);
                    SpellBuffRemoveCurrent(owner);
                }
                else if (IsDead(attacker))
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else if (hitsRemaining > 0)
                {
                    SpellEffectCreate(out _, out _, "InfiniteDuress_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                    ApplyDamage(attacker, owner, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
                    hitsRemaining--;
                }
            }
        }
    }
}
namespace Spells
{
    public class AlZaharNetherGrasp : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 2.5f,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        EffectEmitter particleID;
        int[] effect0 = { 50, 80, 110 };
        public override void ChannelingStart()
        {
            if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.IfHasBuffCheck)) == 0)
            {
                AddBuff(owner, owner, new Buffs.AlZaharVoidlingCount(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            BreakSpellShields(target);
            AddBuff(owner, owner, new Buffs.AlZaharNetherGraspSound(), 4, 1, 2.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            float nextBuffVars_DamageToDeal = effect0[level - 1];
            SpellEffectCreate(out particleID, out _, "AlzaharNetherGrasp_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, target, "root", default, false, false, false, false, false);
            AddBuff(owner, target, new Buffs.AlZaharNetherGrasp(nextBuffVars_DamageToDeal), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            AddBuff(owner, target, new Buffs.Suppression(), 100, 1, 2.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SUPPRESSION, 0, true, false, false);
        }
        public override void ChannelingSuccessStop()
        {
            SpellBuffRemove(target, nameof(Buffs.AlZaharNetherGrasp), attacker, 0);
            SpellBuffRemove(owner, nameof(Buffs.AlZaharNetherGraspSound), owner, 0);
            SpellEffectRemove(particleID);
        }
        public override void ChannelingCancelStop()
        {
            SpellBuffRemove(target, nameof(Buffs.Suppression), owner, 0);
            SpellBuffRemove(target, nameof(Buffs.AlZaharNetherGrasp), attacker, 0);
            SpellBuffRemove(owner, nameof(Buffs.AlZaharNetherGraspSound), owner, 0);
            SpellEffectRemove(particleID);
        }
    }
}
namespace Buffs
{
    public class AlZaharNetherGrasp : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "root", },
            AutoBuffActivateEffect = new[] { "", "AlZaharNetherGrasp_tar.troy", },
            BuffName = "AlZaharNetherGrasp",
            BuffTextureName = "AlZahar_NetherGrasp.dds",
        };
        float damageToDeal;
        float ticksRemaining;
        float lastTimeExecuted;
        public AlZaharNetherGrasp(float damageToDeal = default)
        {
            this.damageToDeal = damageToDeal;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageToDeal);
            ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.26f, 0, false, false, attacker);
            ticksRemaining = 4;
        }
        public override void OnUpdateActions()
        {
            float distance = DistanceBetweenObjects(attacker, owner);
            if (GetBuffCountFromCaster(owner, attacker, nameof(Buffs.Suppression)) == 0)
            {
                StopChanneling(attacker, ChannelingStopCondition.Cancel, ChannelingStopSource.LostTarget);
                SpellBuffRemoveCurrent(owner);
            }
            else if (distance >= 1500)
            {
                StopChanneling(attacker, ChannelingStopCondition.Cancel, ChannelingStopSource.LostTarget);
                SpellBuffRemoveCurrent(owner);
            }
            else if (IsDead(attacker))
            {
                StopChanneling(attacker, ChannelingStopCondition.Cancel, ChannelingStopSource.LostTarget);
                SpellBuffRemoveCurrent(owner);
            }
            else if (ticksRemaining > 0)
            {
                if (ExecutePeriodically(0.5f, ref lastTimeExecuted, false))
                {
                    ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.26f, 0, false, false, attacker);
                    ticksRemaining--;
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if (!expired)
            {
                StopChanneling(attacker, ChannelingStopCondition.Cancel, ChannelingStopSource.LostTarget);
            }
        }
    }
}
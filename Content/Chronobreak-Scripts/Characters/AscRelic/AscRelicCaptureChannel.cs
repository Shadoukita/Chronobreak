/*namespace Spells
{
    public class AscRelicCaptureChannel : SpellScript
    {
        public SpellScriptMetadata ScriptMetadata { get; } = new()
        {
            NotSingleTargetSpell = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            CastingBreaksStealth = true,
            IsDamagingSpell = true,
            ChannelDuration = 7.0f
        };

        AttackableUnit Target;

        public override void OnSpellPreCast(AttackableUnit target, Vector2 start, Vector2 end)
        {
            Target = target;
        }

        public override void OnSpellCast()
        {
            AddBuff("AscRelicCaptureChannel", 30.0f, 1, Spell, Owner, Owner);

            PlayAnimation(Owner, "channel_windup", flags: (AnimationFlags)2);
            ApiEventManager.OnTakeDamage.AddListener(this, Owner, OnTakeDamage, true);
        }

        public override void OnSpellChannelCancel(ChannelingStopSource reason)
        {
            var target = Spell.CurrentCastInfo.Target.Unit;
            if (target.HasBuff("AscRelicSuppression"))
            {
                var buffs = Spell.CurrentCastInfo.Target.Unit.GetBuffsWithName("AscRelicSuppression");
                var ownerBuff = buffs.Find(x => x.SourceUnit == Spell.Caster);

                if (ownerBuff != null)
                {
                    RemoveBuff(ownerBuff);
                }
            }

            AddParticleLink(Owner, "OdinCaptureCancel", Owner, Owner, 1, 1, bindBone: "spine", targetBone: "spine");
            AddParticleLink(Owner, "ezreal_essenceflux_tar", Owner, Owner, 1, 1, bindBone: "ROOT");

            StopChannel();
        }

        public override void OnSpellPostChannel()
        {
            if (Spell.CurrentCastInfo.Target.Unit != null)
            {
                var crystal = Spell.CurrentCastInfo.Target.Unit;
                //I Suspect that the condition that actually kills the crystal is it running out of mana, have to investigate further.
                crystal.Die(CreateDeathData(false, 0, crystal, crystal, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 0.0f));

                if (Spell.Caster is Champion ch)
                {
                    ch.IncrementScore(3.0f, ScoreCategory.Objective, ScoreEvent.MajorRelicPickup, true, true);
                }
            }

            StopChannel();
        }

        public void StopChannel()
        {
            List<Buff> buffs = new List<Buff>
            {
                Target.GetBuffWithName("AscRelicSuppression"),
                Target.GetBuffWithName("OdinBombSuppressionOrder"),
                Target.GetBuffWithName("OdinBombSuppressionChaos"),
                Spell.Caster.GetBuffWithName("AscRelicCaptureChannel"),
                Spell.Caster.GetBuffWithName("OdinChannelVision")
            };

            foreach (var buff in buffs)
            {
                if (buff != null)
                {
                    RemoveBuff(buff);
                }
            }

            Owner.StopAnimation("", true, true);
            ApiEventManager.OnTakeDamage.RemoveListener(this);
            Spell.SetCooldown(4.0f);
        }

        public void OnTakeDamage(DamageData damageData)
        {
            Spell.StopChanneling(ChannelingStopCondition.Cancel, ChannelingStopSource.Attack);
        }
    }
}*/
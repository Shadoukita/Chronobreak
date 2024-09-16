namespace Spells
{
    public class BlindMonkWOneShield : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class BlindMonkWOneShield : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "blindMonk_W_shield_self.troy", "", },
            BuffName = "BlindMonkSafeguard",
            BuffTextureName = "BlindMonkWOne.dds",
            OnPreDamagePriority = 3,
        };
        float shieldAbsorb;
        bool willRemove;
        float oldArmorAmount;
        public BlindMonkWOneShield(float shieldAbsorb = default, bool willRemove = default)
        {
            this.shieldAbsorb = shieldAbsorb;
            this.willRemove = willRemove;
        }
        public override void OnActivate()
        {
            //RequireVar(this.shieldAbsorb);
            SetBuffToolTipVar(1, shieldAbsorb);
            ApplyAssistMarker(attacker, owner, 10);
            IncreaseShield(owner, shieldAbsorb, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            if (!willRemove)
            {
                SpellEffectCreate(out _, out _, "blindMonk_W_shield_self_deactivate.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
            }
            if (shieldAbsorb > 0)
            {
                RemoveShield(owner, shieldAbsorb, true, true);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            oldArmorAmount = shieldAbsorb;
            if (shieldAbsorb >= damageAmount)
            {
                shieldAbsorb -= damageAmount;
                damageAmount = 0;
                oldArmorAmount -= shieldAbsorb;
                ReduceShield(owner, oldArmorAmount, true, true);
            }
            else
            {
                ObjAIBase caster = GetBuffCasterUnit(); // UNUSED
                TeamId teamID = GetTeamID_CS(owner);
                damageAmount -= shieldAbsorb;
                shieldAbsorb = 0;
                ReduceShield(owner, oldArmorAmount, true, true);
                SpellEffectCreate(out _, out _, "blindMonk_W_shield_block.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}
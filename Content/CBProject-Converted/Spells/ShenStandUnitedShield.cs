namespace Spells
{
    public class ShenStandUnitedShield : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class ShenStandUnitedShield : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            AutoBuffActivateEvent = "DeathsCaress_buf.prt",
            BuffName = "Shen Stand United Shield",
            BuffTextureName = "Shen_StandUnited.dds",
            OnPreDamagePriority = 3,
            DoOnPreDamageInExpirationOrder = true,
        };
        float shieldHealth;
        EffectEmitter shieldz;
        float oldArmorAmount;
        public ShenStandUnitedShield(float shieldHealth = default)
        {
            this.shieldHealth = shieldHealth;
        }
        public override void OnActivate()
        {
            //RequireVar(this.shieldHealth);
            SetBuffToolTipVar(1, shieldHealth);
            ApplyAssistMarker(attacker, owner, 10);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out shieldz, out _, "Shen_StandUnited_shield_v2.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            IncreaseShield(owner, shieldHealth, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(shieldz);
            if (shieldHealth > 0)
            {
                RemoveShield(owner, shieldHealth, true, true);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            oldArmorAmount = shieldHealth;
            if (shieldHealth >= damageAmount)
            {
                shieldHealth -= damageAmount;
                damageAmount = 0;
                oldArmorAmount -= shieldHealth;
                ReduceShield(owner, oldArmorAmount, true, true);
                SetBuffToolTipVar(1, shieldHealth);
            }
            else
            {
                damageAmount -= shieldHealth;
                shieldHealth = 0;
                ReduceShield(owner, oldArmorAmount, true, true);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}
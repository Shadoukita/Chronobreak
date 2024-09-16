namespace Buffs
{
    public class Hexdrinker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "hexTech_dmg_shield_duration.troy", "", "", },
            BuffName = "HexdrunkEmpowered",
            BuffTextureName = "3155_Hexdrinker.dds",
            OnPreDamagePriority = 2,
            DoOnPreDamageInExpirationOrder = true,
        };
        float shieldHealth;
        float oldArmorAmount;
        public Hexdrinker(float shieldHealth = default)
        {
            this.shieldHealth = shieldHealth;
        }
        public override void OnActivate()
        {
            //RequireVar(this.shieldHealth);
            IncreaseShield(owner, shieldHealth, true, false);
        }
        public override void OnDeactivate(bool expired)
        {
            float duration = 60 - lifeTime;
            AddBuff((ObjAIBase)owner, owner, new Buffs.HexdrinkerTimerCD(), 1, 1, duration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            if (shieldHealth > 0)
            {
                RemoveShield(owner, shieldHealth, true, false);
            }
        }
        public override void OnUpdateActions()
        {
            if (shieldHealth <= 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            oldArmorAmount = shieldHealth;
            if (damageAmount > 0 && damageType == DamageType.DAMAGE_TYPE_MAGICAL)
            {
                SpellEffectCreate(out _, out _, "hexTech_dmg_shield_onHit_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                SpellEffectCreate(out _, out _, "hexTech_dmg_shield_onHit_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                if (shieldHealth >= damageAmount)
                {
                    shieldHealth -= damageAmount;
                    damageAmount = 0;
                    oldArmorAmount -= shieldHealth;
                    ReduceShield(owner, oldArmorAmount, true, false);
                }
                else
                {
                    damageAmount -= shieldHealth;
                    shieldHealth = 0;
                    ReduceShield(owner, oldArmorAmount, true, false);
                }
            }
        }
    }
}
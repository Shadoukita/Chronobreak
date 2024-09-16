namespace ItemPassives
{
    public class ItemID_3155 : ItemScript
    {
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            TeamId teamID = GetTeamID_CS(owner);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.HexdrinkerTimer)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.Hexdrinker)) == 0 && damageType == DamageType.DAMAGE_TYPE_MAGICAL && damageAmount > 0)
            {
                float hP = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                float projectedHP = hP - damageAmount;
                float maxHP = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                float newPercentHP = projectedHP / maxHP;
                if (newPercentHP < 0.3f)
                {
                    float nextBuffVars_ShieldHealth;
                    SpellEffectCreate(out _, out _, "hexTech_dmg_shield_birth.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
                    SpellEffectCreate(out _, out _, "hexTech_dmg_shield_onHit_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                    SpellEffectCreate(out _, out _, "hexTech_dmg_shield_onHit_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                    float shieldHealth = 300;
                    if (shieldHealth >= damageAmount)
                    {
                        shieldHealth -= damageAmount;
                        nextBuffVars_ShieldHealth = shieldHealth;
                        damageAmount = 0;
                        AddBuff(owner, owner, new Buffs.Hexdrinker(nextBuffVars_ShieldHealth), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                        AddBuff(owner, owner, new Buffs.HexdrinkerTimer(), 1, 1, 60, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                    }
                    else
                    {
                        damageAmount -= shieldHealth;
                        nextBuffVars_ShieldHealth = 0;
                        AddBuff(owner, owner, new Buffs.Hexdrinker(nextBuffVars_ShieldHealth), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                        AddBuff(owner, owner, new Buffs.HexdrinkerTimer(), 1, 1, 60, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                    }
                }
            }
        }
    }
}
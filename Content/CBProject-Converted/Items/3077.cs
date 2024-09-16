namespace ItemPassives
{
    public class ItemID_3077 : ItemScript
    {
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not BaseTurret)
            {
                float tempTable1_ThirdDA;
                SpellEffectCreate(out _, out _, "TiamatMelee_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false);
                if (IsRanged(owner))
                {
                    tempTable1_ThirdDA = 0.35f * damageAmount;
                }
                else
                {
                    if (GetBuffCountFromCaster(owner, default, nameof(Buffs.JudicatorRighteousFury)) > 0)
                    {
                        tempTable1_ThirdDA = 0.35f * damageAmount;
                    }
                    else
                    {
                        tempTable1_ThirdDA = 0.5f * damageAmount;
                    }
                }
                foreach (AttackableUnit unit in GetUnitsInArea(owner, target.Position3D, 210, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    if (target != unit)
                    {
                        if (damageType == DamageType.DAMAGE_TYPE_MAGICAL)
                        {
                            ApplyDamage(owner, unit, tempTable1_ThirdDA, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, true, true, attacker);
                        }
                        else if (damageType == DamageType.DAMAGE_TYPE_PHYSICAL)
                        {
                            ApplyDamage(owner, unit, tempTable1_ThirdDA, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, true, true, attacker);
                        }
                        else
                        {
                            ApplyDamage(owner, unit, tempTable1_ThirdDA, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, true, true, attacker);
                        }
                    }
                }
            }
        }
    }
}
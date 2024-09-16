namespace Spells
{
    public class KogMawCritAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            SpellFXOverrideSkins = new[] { "NewYearDragonKogMaw", },
            SpellVOOverrideSkins = new[] { "NewYearDragonKogMaw", },
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            hitResult = HitResult.HIT_Critical;
            float baseAttackDamage = GetBaseAttackDamage(owner);
            int kMSkinID = GetSkinID(attacker);
            if (target is ObjAIBase)
            {
                if (kMSkinID == 5)
                {
                    SpellEffectCreate(out _, out _, "KogMawChineseBasicAttack_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                }
                else
                {
                    SpellEffectCreate(out _, out _, "KogMawSpatter.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                }
            }
            ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
        }
    }
}
namespace Spells
{
    public class BrandCritAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            SpellFXOverrideSkins = new[] { "FrostFireBrand", },
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float baseAttackDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
            if (target is ObjAIBase)
            {
                int brandSkinID = GetSkinID(owner);
                TeamId teamID = GetTeamID_CS(owner);
                if (brandSkinID == 3)
                {
                    SpellEffectCreate(out _, out _, "BrandCritAttack_Frost_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out _, out _, "BrandCritAttack_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "Spine", default, target, default, default, true, false, false, false, false);
                }
            }
        }
    }
}
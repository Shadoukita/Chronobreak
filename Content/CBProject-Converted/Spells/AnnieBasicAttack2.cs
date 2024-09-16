namespace Spells
{
    public class AnnieBasicAttack2 : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            SpellFXOverrideSkins = new[] { "GangsterTwitch", "PunkTwitch", },
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float baseAttackDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
            if (target is ObjAIBase)
            {
                int annieSkinID = GetSkinID(owner);
                TeamId teamID = GetTeamID_CS(owner);
                if (annieSkinID == 5)
                {
                    SpellEffectCreate(out _, out _, "AnnieBasicAttack_tar_frost.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                }
                else
                {
                    SpellEffectCreate(out _, out _, "AnnieBasicAttack_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                }
            }
        }
    }
}
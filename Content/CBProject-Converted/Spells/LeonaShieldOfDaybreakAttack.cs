namespace Spells
{
    public class LeonaShieldOfDaybreakAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { -0.2f, -0.25f, -0.3f, -0.35f, -0.4f };
        int[] effect1 = { 40, 70, 100, 130, 160 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(attacker);
            float slowPercent = effect0[level - 1]; // UNUSED
            float bonusDamage = effect1[level - 1];
            float supremeDmg = GetTotalAttackDamage(owner);
            float dealtDamage = supremeDmg * 1;
            hitResult = HitResult.HIT_Normal;
            if (target is ObjAIBase)
            {
                Vector3 targetPos = GetUnitPosition(target);
                SpellEffectCreate(out _, out _, "Leona_ShieldOfDaybreak_nova.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true, default, default, false, false);
                SpellEffectCreate(out _, out _, "Leona_ShieldOfDaybreak_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                ApplyDamage(attacker, target, dealtDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, true, attacker);
                BreakSpellShields(target);
                ApplyDamage(attacker, target, bonusDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.3f, 0, false, true, attacker);
                if (target is not BaseTurret)
                {
                    AddBuff(attacker, target, new Buffs.LeonaSunlight(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    ApplyStun(attacker, target, 1);
                }
            }
            else
            {
                ApplyDamage(attacker, target, bonusDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.3f, 0, false, true, attacker);
                ApplyDamage(attacker, target, dealtDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, true, attacker);
            }
        }
    }
}
namespace Buffs
{
    public class LeonaShieldOfDaybreakAttack : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "GarenSlash",
            BuffTextureName = "17.dds",
        };
    }
}
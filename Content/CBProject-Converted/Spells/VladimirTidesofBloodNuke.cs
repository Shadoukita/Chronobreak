namespace Spells
{
    public class VladimirTidesofBloodNuke : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
            SpellFXOverrideSkins = new[] { "BloodkingVladimir", },
        };
        int[] effect0 = { 60, 90, 120, 150, 180 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float multiplier = charVars.NumTideStacks * 0.25f;
            multiplier++;
            float baseDamage = effect0[level - 1];
            float finalDamage = baseDamage * multiplier;
            BreakSpellShields(target);
            ApplyDamage(owner, target, finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.45f, 1, false, false, attacker);
            TeamId teamID = GetTeamID_CS(owner);
            int vladimirSkinID = GetSkinID(owner);
            if (vladimirSkinID == 5)
            {
                SpellEffectCreate(out _, out _, "VladTidesofBlood_BloodKing_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out _, out _, "VladTidesofBlood_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class VladimirTidesofBloodNuke : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            IsDeathRecapSource = true,
        };
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            if (health >= 0)
            {
                int count = GetBuffCountFromAll(owner, nameof(Buffs.VladimirTidesofBloodCost));
                float bonusHealPercent = count * 0.08f;
                float healRatio = bonusHealPercent + 1;
                float effectiveHeal = healRatio * health;
                returnValue = effectiveHeal;
            }
            return returnValue;
        }
    }
}
namespace Spells
{
    public class LuxIlluminationPassive : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
        };
    }
}
namespace Buffs
{
    public class LuxIlluminationPassive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LuxIlluminationPassive",
            BuffTextureName = "LuxIlluminatingFraulein.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(target, attacker, nameof(Buffs.LuxIlluminatingFraulein)) > 0)
            {
                TeamId teamID = GetTeamID_CS(target);
                ApplyDamage(attacker, target, charVars.IlluminateDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                SpellEffectCreate(out _, out _, "LuxPassive_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                SpellBuffRemove(target, nameof(Buffs.LuxIlluminatingFraulein), attacker, 0);
            }
        }
        public override void OnUpdateActions()
        {
            SetBuffToolTipVar(1, charVars.IlluminateDamage);
        }
    }
}
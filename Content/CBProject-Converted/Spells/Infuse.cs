namespace Spells
{
    public class Infuse : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 25, 50, 75, 100, 125 };
        int[] effect2 = { 50, 100, 150, 200, 250 };
        float[] effect3 = { 1.5f, 1.75f, 2, 2.25f, 2.5f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(attacker);
            if (target.Team == owner.Team)
            {
                ApplyAssistMarker(owner, target, 10);
                if (target != owner)
                {
                    IncPAR(target, effect0[level - 1], PrimaryAbilityResourceType.MANA);
                }
                IncPAR(owner, effect0[level - 1], PrimaryAbilityResourceType.MANA);
                SpellEffectCreate(out _, out _, "soraka_infuse_ally_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                if (target != owner)
                {
                    SpellEffectCreate(out _, out _, "soraka_infuse_ally_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
                }
            }
            else
            {
                ApplyDamage(attacker, target, effect2[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.75f, 1, false, false, attacker);
                ApplySilence(attacker, target, effect3[level - 1]);
                SpellEffectCreate(out _, out _, "soraka_infuse_enemy_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class Infuse : BuffScript
    {
    }
}
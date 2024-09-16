namespace Spells
{
    public class MonsterRegenSpell : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 0f, 0f, 0f, 0f, 0f, },
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(target);
            SpellEffectCreate(out _, out _, "VampHeal.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out _, out _, "Meditate_eff.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
            float healthPercent = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
            float missingHealthPercent = 1 - healthPercent;
            float healthToRestore = 60 * missingHealthPercent;
            healthToRestore = Math.Max(10, healthToRestore);
            IncHealth(target, healthToRestore, target);
        }
    }
}
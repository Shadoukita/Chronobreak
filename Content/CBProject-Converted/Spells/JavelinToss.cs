namespace Spells
{
    public class JavelinToss : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 55, 95, 140, 185, 230 };
        public override void SelfExecute()
        {
            float baseDamage = effect0[level - 1];
            float aP = GetFlatMagicDamageMod(owner);
            float aPDamage = aP * 0.65f;
            float startingDamage = baseDamage + aPDamage;
            charVars.StartingDamage = startingDamage;
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID;
            float distance;
            float multiplicant;
            float finalDamage;
            bool isStealthed = GetStealthed(target);
            if (!isStealthed)
            {
                teamID = GetTeamID_CS(owner);
                SpellEffectCreate(out _, out _, "nidalee_javelinToss_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                BreakSpellShields(target);
                distance = DistanceBetweenObjects(target, owner);
                multiplicant = distance / 1000;
                multiplicant++;
                multiplicant = Math.Min(multiplicant, 2.5f);
                finalDamage = multiplicant * charVars.StartingDamage;
                ApplyDamage(attacker, target, finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, attacker);
                DestroyMissile(missileNetworkID);
            }
            else
            {
                if (target is Champion)
                {
                    teamID = GetTeamID_CS(owner);
                    SpellEffectCreate(out _, out _, "nidalee_javelinToss_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                    BreakSpellShields(target);
                    distance = DistanceBetweenObjects(target, owner);
                    multiplicant = distance / 1000;
                    multiplicant++;
                    multiplicant = Math.Min(multiplicant, 2.5f);
                    finalDamage = multiplicant * charVars.StartingDamage;
                    ApplyDamage(attacker, target, finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, attacker);
                    DestroyMissile(missileNetworkID);
                }
                else
                {
                    bool canSee = CanSeeTarget(owner, target);
                    if (canSee)
                    {
                        teamID = GetTeamID_CS(owner);
                        SpellEffectCreate(out _, out _, "nidalee_javelinToss_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                        BreakSpellShields(target);
                        distance = DistanceBetweenObjects(target, owner);
                        multiplicant = distance / 1000;
                        multiplicant++;
                        multiplicant = Math.Min(multiplicant, 2.5f);
                        finalDamage = multiplicant * charVars.StartingDamage;
                        ApplyDamage(attacker, target, finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, attacker);
                        DestroyMissile(missileNetworkID);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class JavelinToss : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
    }
}
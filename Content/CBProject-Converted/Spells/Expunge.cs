namespace Spells
{
    public class TwitchExpunge : Expunge { }
    public class Expunge : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 20, 30, 40, 50, 60 };
        int[] effect1 = { 30, 60, 90, 120, 150 };
        public override void SelfExecute()
        {
            TeamId teamID = GetTeamID_CS(owner);
            float explosionDamage = effect0[level - 1];
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.DeadlyVenom)) > 0)
                {
                    BreakSpellShields(unit);
                    int count = GetBuffCountFromAll(unit, nameof(Buffs.DeadlyVenom));
                    float baseDamage = effect1[level - 1];
                    float bonusDamage = count * explosionDamage;
                    float totalDamage = baseDamage + bonusDamage;
                    ApplyDamage(attacker, unit, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 1, false, false, attacker);
                    SpellEffectCreate(out _, out _, "Expunge_tar_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, true);
                    SpellBuffRemoveStacks(unit, owner, nameof(Buffs.DeadlyVenom), 0);
                }
            }
        }
    }
}
namespace Spells
{
    public class Overload : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 16f, 14f, 12f, 10f, 8f, },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 40, 65, 90, 115, 140 };
        float[] effect1 = { 0.5f, 0.5f, 0.5f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = base.level;
            SpellEffectCreate(out _, out _, "overload_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "root", default, target, "root", default, false, default, default, false, false);
            TeamId teamID = GetTeamID_CS(attacker);
            float pAR = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
            float baseDamage = effect0[level - 1];
            level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float bonusDamage = pAR * 0.08f;
            float totalDamage = bonusDamage + baseDamage;
            float aoEDamage = effect1[level - 1];
            aoEDamage *= totalDamage;
            ApplyDamage(attacker, target, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.2f, 1, false, false, attacker);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.DesperatePower)) > 0)
            {
                SpellEffectCreate(out _, out _, "DesperatePower_aoe.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                foreach (AttackableUnit unit in GetUnitsInArea(owner, target.Position3D, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    if (target != unit)
                    {
                        SpellEffectCreate(out _, out _, "ManaLeach_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                        ApplyDamage(attacker, unit, aoEDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.1f, 1, false, false, attacker);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class Overload : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float cooldownBonus;
        float[] effect0 = { -0.02f, -0.04f, -0.06f, -0.08f, -0.1f };
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            cooldownBonus = effect0[level - 1];
        }
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, cooldownBonus);
        }
    }
}
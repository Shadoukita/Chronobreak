namespace Spells
{
    public class SpellFlux : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 150f, 120f, 90f, },
            CastingBreaksStealth = true,
            ChainMissileParameters = new()
            {
                CanHitCaster = true,
                CanHitEnemies = true,
                CanHitFriends = false,
                CanHitSameTarget = true,
                CanHitSameTargetConsecutively = false,
                MaximumHits = 5,
            },
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { -12, -15, -18, -21, -24 };
        int[] effect1 = { 50, 70, 90, 110, 130 };
        float[] effect2 = { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            TeamId teamID = GetTeamID_CS(attacker);
            float nextBuffVars_ResistanceMod = effect0[level - 1];
            float damage = effect1[level - 1];
            float aoEDamage = effect2[level - 1];
            float ultDamage = damage * aoEDamage;
            SpellEffectCreate(out _, out _, "SpellFlux_tar2.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            if (target != owner)
            {
                AddBuff(attacker, target, new Buffs.SpellFlux(nextBuffVars_ResistanceMod), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.SHRED, 0, true, false, false);
                ApplyDamage(attacker, target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.35f, 1, false, false, attacker);
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.DesperatePower)) > 0)
                {
                    SpellEffectCreate(out _, out _, "DesperatePower_aoe.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                    foreach (AttackableUnit unit in GetUnitsInArea(owner, target.Position3D, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                    {
                        if (target != unit)
                        {
                            SpellEffectCreate(out _, out _, "ManaLeach_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                            ApplyDamage(attacker, unit, ultDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.175f, 1, false, false, attacker);
                        }
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class SpellFlux : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Spell Flux",
            BuffTextureName = "Ryze_LightningFlux.dds",
        };
        float resistanceMod;
        public SpellFlux(float resistanceMod = default)
        {
            this.resistanceMod = resistanceMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.resistanceMod);
            IncFlatSpellBlockMod(owner, resistanceMod);
        }
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, resistanceMod);
        }
    }
}
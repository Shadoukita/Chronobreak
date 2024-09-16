namespace Spells
{
    public class CH1ConcussionGrenadeUpgrade : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 80, 135, 190, 245, 300 };
        float[] effect1 = { 1, 1.5f, 2, 2.5f, 3 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "heimerdinger_CH1_grenade_tar.troy", default, teamID, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, target.Position3D, target, default, default, true);
            foreach (AttackableUnit unit in GetUnitsInArea(owner, target.Position3D, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets, default, true))
            {
                BreakSpellShields(unit);
                if (unit is not BaseTurret)
                {
                    ApplyDamage(attacker, unit, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
                }
                SpellEffectCreate(out _, out _, "heimerdinger_CH1_grenade_unit_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, unit.Position3D, unit, default, default, false);
            }
            foreach (AttackableUnit unit in GetUnitsInArea(owner, target.Position3D, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff(attacker, unit, new Buffs.BlindingDart(), 100, 1, effect1[level - 1], BuffAddType.STACKS_AND_OVERLAPS, BuffType.BLIND, 0, true, false);
            }
            foreach (AttackableUnit unit in GetUnitsInArea(owner, target.Position3D, 125, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyStun(attacker, unit, 1.5f);
            }
        }
    }
}
namespace Buffs
{
    public class CH1ConcussionGrenadeUpgrade : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "Cryophoenix_FrigidOrb.dds",
        };
    }
}
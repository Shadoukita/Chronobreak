namespace Spells
{
    public class AhriOrbMissile : SpellScript
    {
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            missileEndPosition.Y -= 50;
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, missileEndPosition, 100, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                int nextBuffVars_OrbofDeceptionIsActive = charVars.OrbofDeceptionIsActive;
                AddBuff(attacker, unit, new Buffs.AhriOrbDamage(nextBuffVars_OrbofDeceptionIsActive), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            foreach (AttackableUnit unit in GetUnitsInArea(owner, missileEndPosition, 100, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                int nextBuffVars_OrbofDeceptionIsActive = charVars.OrbofDeceptionIsActive;
                AddBuff(attacker, unit, new Buffs.AhriOrbDamageSilence(nextBuffVars_OrbofDeceptionIsActive), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            SpellCast(owner, owner, default, default, 1, SpellSlotType.ExtraSlots, level, true, true, false, true, false, true, missileEndPosition);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int nextBuffVars_OrbofDeceptionIsActive = charVars.OrbofDeceptionIsActive;
            AddBuff(owner, target, new Buffs.AhriOrbDamage(nextBuffVars_OrbofDeceptionIsActive), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class AhriOrbMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "root", },
            AutoBuffActivateEffect = new[] { "Ahri_OrbofDeception.troy", },
        };
    }
}
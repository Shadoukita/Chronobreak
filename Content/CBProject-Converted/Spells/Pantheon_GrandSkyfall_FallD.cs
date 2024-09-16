namespace Buffs
{
    public class Pantheon_GrandSkyfall_FallD : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Pantheon_GrandSkyfall_FallDamage",
        };
        float damageRank;
        int[] effect0 = { 400, 700, 1000 };
        public override void OnDeactivate(bool expired)
        {
            Vector3 targetPos = charVars.TargetPos;
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            damageRank = effect0[level - 1];
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)target, targetPos, 700, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                float percentDamage;
                Vector3 unitPos = GetUnitPosition(unit);
                float distance = DistanceBetweenPoints(targetPos, unitPos);
                if (distance <= 250)
                {
                    percentDamage = 1;
                }
                else
                {
                    float percentNotDamage = distance - 200;
                    percentNotDamage = distance / 500;
                    percentDamage = 1 - percentNotDamage;
                    percentDamage = Math.Min(percentDamage, 1);
                    percentDamage = Math.Max(percentDamage, 0.5f);
                }
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, damageRank, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, percentDamage, 1, 1, false, false, attacker);
                float nextBuffVars_MoveSpeedMod = -0.35f;
                AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 1, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                SpellEffectCreate(out _, out _, "Globalhit_physical.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "head", default, target, default, default, false, default, default, false, false);
            }
        }
    }
}
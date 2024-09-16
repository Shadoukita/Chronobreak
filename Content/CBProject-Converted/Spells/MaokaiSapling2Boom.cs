namespace Spells
{
    public class MaokaiSapling2Boom : SpellScript
    {
        int[] effect0 = { 5, 5, 5, 5, 5 };
        int[] effect1 = { 40, 75, 110, 145, 180 };
        int[] effect2 = { 80, 130, 180, 230, 280 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            Vector3 targetPos = GetUnitPosition(target);
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            TeamId teamID = GetTeamID_CS(owner);
            int buffDuration = effect0[level - 1]; // UNUSED
            SpellEffectCreate(out _, out _, "maoki_sapling_unit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, default, default, targetPos, true, default, default, false, false);
            float damageAmount = effect1[level - 1];
            int mineDamageAmount = effect2[level - 1];
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 240, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
            }
            Minion other1 = SpawnMinion("DoABarrelRoll", "MaokaiSproutling", "idle.lua", targetPos, teamID, false, false, false, false, false, false, 0, false, false, (Champion)attacker);
            SetCanMove(other1, false);
            SetCanAttack(other1, false);
            float nextBuffVars_MineDamageAmount = mineDamageAmount;
            bool nextBuffVars_Sprung = false;
            AddBuff(attacker, other1, new Buffs.MaokaiSaplingMine(nextBuffVars_MineDamageAmount, nextBuffVars_Sprung), 1, 1, 35, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
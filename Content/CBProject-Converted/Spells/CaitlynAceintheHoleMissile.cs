namespace Spells
{
    public class CaitlynAceintheHoleMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 250, 475, 700 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(attacker);
            Vector3 targetPos = GetUnitPosition(target);
            int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            BreakSpellShields(target);
            float baseDamage = effect0[level - 1];
            float totalDmg = GetTotalAttackDamage(owner);
            float baseDmg = GetBaseAttackDamage(owner);
            float bonusDmg = totalDmg - baseDmg;
            float physPreMod = 2 * bonusDmg;
            float damageToDeal = physPreMod + baseDamage;
            ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, true, true, attacker);
            SpellEffectCreate(out _, out _, "caitlyn_ace_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, owner, default, default, true);
            SpellBuffRemove(attacker, nameof(Buffs.IfHasBuffCheck), attacker);
            DestroyMissile(missileNetworkID);
            if (GetBuffCountFromCaster(target, attacker, nameof(Buffs.CaitlynAceintheHole)) > 0)
            {
                SpellBuffRemove(target, nameof(Buffs.CaitlynAceintheHole), attacker);
            }
            else
            {
                foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, nameof(Buffs.CaitlynAceintheHole), true))
                {
                    SpellBuffRemove(unit, nameof(Buffs.CaitlynAceintheHole), attacker);
                }
            }
        }
    }
}
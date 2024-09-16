namespace Spells
{
    public class GragasExplosiveCaskBoom : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
        };
        int[] effect0 = { 200, 325, 450 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamofOwner = GetTeamID_CS(owner);
            int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            int gragasSkinID = GetSkinID(attacker);
            if (gragasSkinID == 4)
            {
                SpellEffectCreate(out _, out _, "gragas_caskboom_classy.troy", default, teamofOwner, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, target.Position3D, owner, default, target.Position3D, true, default, default, false);
            }
            else
            {
                SpellEffectCreate(out _, out _, "gragas_caskboom.troy", default, teamofOwner, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, target.Position3D, owner, default, target.Position3D, true, default, default, false);
            }
            foreach (AttackableUnit unit in GetUnitsInArea(owner, target.Position3D, 430, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                Vector3 center = GetSpellTargetPos(spell);
                int nextBuffVars_Speed = 900;
                int nextBuffVars_Gravity = 5;
                Vector3 nextBuffVars_Center = center;
                int nextBuffVars_Distance = 900;
                int nextBuffVars_IdealDistance = 900;
                ApplyDamage(attacker, unit, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 1, false, false, attacker);
                AddBuff(attacker, unit, new Buffs.MoveAwayCollision(nextBuffVars_Speed, nextBuffVars_Gravity, nextBuffVars_Center, nextBuffVars_Distance, nextBuffVars_IdealDistance), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                SpellEffectCreate(out _, out _, "gragas_caskwine_tar.troy", default, teamofOwner, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, unit.Position3D, unit, default, default, true, default, default, false);
            }
        }
    }
}
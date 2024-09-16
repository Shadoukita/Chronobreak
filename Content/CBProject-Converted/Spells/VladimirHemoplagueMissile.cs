namespace Spells
{
    public class VladimirHemoplagueMissile : SpellScript
    {
        int[] effect0 = { 150, 250, 350 };
        float[] effect1 = { -0.14f, -0.14f, -0.14f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            Vector3 targetPos; // UNITIALIZED
            AddBuff(owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            PlayAnimation("Spell4", 0.5f, owner, false, true, true);
            TeamId teamofOwner = GetTeamID_CS(owner);
            int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            int vladSkinID = GetSkinID(owner);
            if (vladSkinID == 5)
            {
                SpellEffectCreate(out _, out _, "VladHemoplague_BloodKing_nova.troy", default, teamofOwner, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, target.Position3D, owner, default, target.Position3D, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out _, out _, "VladHemoplague_nova.troy", default, teamofOwner, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, target.Position3D, owner, default, target.Position3D, true, false, false, false, false);
            }
            float nextBuffVars_DamagePerLevel = effect0[level - 1];
            float nextBuffVars_DamageIncrease = effect1[level - 1];
            //Vector3 nextBuffVars_TargetPos = targetPos;
            foreach (AttackableUnit unit in GetUnitsInArea(owner, target.Position3D, 375, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                AddBuff(attacker, unit, new Buffs.VladimirHemoplagueDebuff(nextBuffVars_DamageIncrease, nextBuffVars_DamagePerLevel), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            }
        }
    }
}
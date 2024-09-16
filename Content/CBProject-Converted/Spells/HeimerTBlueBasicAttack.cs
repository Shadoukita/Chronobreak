namespace Spells
{
    public class HeimerTBlueBasicAttack : SpellScript
    {
        float[] effect0 = { -0.2f, -0.25f, -0.3f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(owner);
            ObjAIBase attacker = GetChampionBySkinName("Heimerdinger", teamID);
            float dmg = GetTotalAttackDamage(owner);
            if (target is BaseTurret)
            {
                dmg /= 2;
            }
            ApplyDamage(attacker, target, dmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, owner);
            if (target is ObjAIBase && target is not BaseTurret)
            {
                int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float nextBuffVars_MovementSpeedMod = effect0[level - 1];
                float nextBuffVars_AttackSpeedMod = 0;
                AddBuff(attacker, target, new Buffs.Chilled(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
        }
    }
}
namespace Spells
{
    public class ShacoBoxBasicAttack : SpellScript
    {
        int[] effect0 = { 35, 55, 75, 95, 115 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            ObjAIBase attacker = GetChampionBySkinName("Shaco", TeamId.TEAM_ORDER);
            int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float dmg = effect0[level - 1];
            ApplyDamage(attacker, target, dmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0.25f, 1, false, false, owner);
        }
    }
}
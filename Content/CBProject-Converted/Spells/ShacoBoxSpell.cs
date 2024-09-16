namespace Spells
{
    public class ShacoBoxSpell : SpellScript
    {
        int[] effect0 = { 35, 50, 65, 80, 95 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(owner);
            ObjAIBase attacker = GetChampionBySkinName("Shaco", teamID);
            int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float dmg = effect0[level - 1];
            ApplyDamage(attacker, target, dmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0.2f, 1, false, false, owner);
        }
    }
}
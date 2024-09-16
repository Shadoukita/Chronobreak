namespace Spells
{
    public class Red_Minion_MechRangeBasicAttack : SpellScript
    {
        int[] effect0 = { 40, 55, 70, 85, 100 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(owner);
            ObjAIBase attacker = GetChampionBySkinName("Jester", teamID);
            int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float dmg = effect0[level - 1];
            float abilityPower = GetFlatMagicDamageMod(attacker);
            float abilityPowerBonus = abilityPower * 0.35f;
            float totalDmg = dmg + abilityPowerBonus;
            if (target is not Champion)
            {
                totalDmg *= 0.5f;
            }
            ApplyDamage(attacker, target, totalDmg, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0);
        }
    }
}
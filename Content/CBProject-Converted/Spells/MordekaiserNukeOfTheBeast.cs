namespace Spells
{
    public class MordekaiserNukeOfTheBeast : SpellScript
    {
        int[] effect0 = { 80, 110, 140, 170, 200 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "mordakaiser_maceOfSpades_tar2.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseDamage = effect0[level - 1];
            baseDamage = GetBaseAttackDamage(owner);
            float totalDamage = GetTotalAttackDamage(owner);
            float bonusDamage = totalDamage - baseDamage;
            baseDamage += bonusDamage;
            float abilityPower = GetFlatMagicDamageMod(owner);
            float bonusAPDamage = abilityPower * 0.4f;
            baseDamage += bonusAPDamage;
            float nextBuffVars_BaseDamage = baseDamage;
            AddBuff((ObjAIBase)target, owner, new Buffs.MordekaiserNukeOfTheBeastDmg(nextBuffVars_BaseDamage), 5, 1, 0.001f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
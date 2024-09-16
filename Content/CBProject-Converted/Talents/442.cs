namespace Talents
{
    public class Talent_442 : TalentScript
    {
        int[] effect0 = { 20, 40 };
        public override void OnUpdateActions()
        {
            int level = talentLevel;
            int nextBuffVars_GoldAmount = effect0[level - 1];
            AddBuff(owner, owner, new Buffs.MasteryHoardBuff(nextBuffVars_GoldAmount), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
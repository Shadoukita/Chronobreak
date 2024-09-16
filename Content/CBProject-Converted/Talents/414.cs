namespace Talents
{
    public class Talent_414 : TalentScript
    {
        public override void OnUpdateActions()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.MasteryImprovedRecallBuff)) == 0)
            {
                AddBuff(owner, owner, new Buffs.MasteryImprovedRecallBuff(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
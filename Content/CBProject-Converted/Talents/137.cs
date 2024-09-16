namespace Talents
{
    public class Talent_137 : TalentScript
    {
        public override void OnUpdateActions()
        {
            int nextBuffVars_Level = talentLevel;
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OffensiveMasteryBuff)) == 0)
            {
                AddBuff(owner, owner, new Buffs.OffensiveMasteryBuff(nextBuffVars_Level), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            }
        }
    }
}
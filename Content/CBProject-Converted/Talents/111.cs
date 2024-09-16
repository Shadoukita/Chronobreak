namespace Talents
{
    public class Talent_111 : TalentScript
    {
        float[] effect0 = { 0.01333f, 0.02667f, 0.04f };
        public override void OnUpdateActions()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Ardor)) == 0)
            {
                int level = talentLevel;
                float nextBuffVars_PercentMod = effect0[level - 1];
                AddBuff(owner, owner, new Buffs.Ardor(nextBuffVars_PercentMod), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
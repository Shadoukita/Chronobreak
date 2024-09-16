namespace Talents
{
    public class Talent_253 : TalentScript
    {
        float[] effect0 = { 0.0125f, 0.025f, 0.0375f, 0.05f };
        public override void OnUpdateActions()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.MasteryBlastBuff)) == 0)
            {
                int level = talentLevel;
                float nextBuffVars_PercentMod = effect0[level - 1];
                AddBuff(owner, owner, new Buffs.MasteryBlastBuff(nextBuffVars_PercentMod), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
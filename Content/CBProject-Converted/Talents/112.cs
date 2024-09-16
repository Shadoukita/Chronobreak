namespace Talents
{
    public class Talent_112 : TalentScript
    {
        float[] effect0 = { 0.00066f, 0.00133f, 0.002f };
        public override void OnUpdateStats()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.StrengthOfSpirit)) == 0)
            {
                int level = talentLevel;
                float nextBuffVars_multiplier = effect0[level - 1];
                AddBuff(owner, owner, new Buffs.StrengthOfSpirit(nextBuffVars_multiplier), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace Talents
{
    public class TestScript : TalentScript
    {
        float[] effect0 = { 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.16f, 0.16f, 0.16f, 0.16f, 0.16f, 0.22f, 0.22f, 0.22f, 0.22f, 0.22f, 0.28f, 0.28f, 0.28f, 0.28f };
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.8f, ref charVars.LastTimeExecuted))
            {
                foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 400, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes))
                {
                    float nextBuffVars_AttackSpeedIncrease = charVars.AttackSpeedIncrease; // UNUSED
                    AddBuff(owner, unit, new Buffs.DivineBlessingAura(), 1, default, 1, BuffAddType.RENEW_EXISTING, BuffType.AURA);
                }
            }
            avatarVars.Test = 10;
        }
        public override void SetVarsByLevel()
        {
            int level = talentLevel;
            charVars.AttackSpeedIncrease = effect0[level - 1];
        }
    }
}
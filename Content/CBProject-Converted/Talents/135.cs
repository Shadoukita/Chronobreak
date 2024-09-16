namespace Talents
{
    public class Talent_135 : TalentScript
    {
        public override void OnUpdateActions()
        {
            if (talentLevel == 2)
            {
                AddBuff(owner, owner, new Buffs.Monsterbuffs2(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            }
            else
            {
                AddBuff(owner, owner, new Buffs.MonsterBuffs(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            }
        }
    }
}
namespace Talents
{
    public class Talent_434 : TalentScript
    {
        public override void OnUpdateActions()
        {
            AddBuff(owner, owner, new Buffs.MonsterBuffs(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Talents
{
    public class PreAttackTest : TalentScript
    {
        public override void OnPreAttack(AttackableUnit target)
        {
            DebugSay(owner, "Avatar PreAttack event.");
        }
    }
}
namespace CharScripts
{
    public class CharScriptOdinBlueSuperminion : CharScript
    {
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.OdinSuperMinion(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace CharScripts
{
    public class CharScriptOdin_Blue_Minion_Caster : CharScript
    {
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.OdinMinion(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
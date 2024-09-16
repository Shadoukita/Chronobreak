namespace CharScripts
{
    public class CharScriptCrystal_platform : CharScript
    {
        public override void OnActivate()
        {
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetGhosted(owner, true);
            SetCanMove(owner, false);
            AddBuff(owner, owner, new Buffs.OdinDisintegrate(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}
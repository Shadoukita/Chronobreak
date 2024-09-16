namespace CharScripts
{
    public class CharScriptOdinShieldRelic : CharScript
    {
        public override void OnActivate()
        {
            SetCanMove(owner, false);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            AddBuff(owner, owner, new Buffs.OdinShieldRelicAura(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
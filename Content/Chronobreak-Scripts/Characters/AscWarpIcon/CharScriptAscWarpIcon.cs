/*namespace CharScripts
{
    internal class CharScriptAscWarpIcon : CharScript
    {
        public override void OnActivate()
        {
            SetStatus(Owner, StatusFlags.Targetable, false);
            SetStatus(Owner, StatusFlags.Stunned, true);
            SetStatus(Owner, StatusFlags.IgnoreCallForHelp, true);
            SetStatus(Owner, StatusFlags.Ghosted, true);
            SetStatus(Owner, StatusFlags.Invulnerable, true);
            SetStatus(Owner, StatusFlags.CanMoveEver, false);
        }
    }
}*/
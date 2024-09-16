namespace CharScripts
{
    public class CharScriptNidalee_Spear : CharScript
    {
        public override void OnActivate()
        {
            SetTargetable(owner, false);
        }
    }
}
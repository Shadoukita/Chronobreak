namespace CharScripts
{
    public class CharScriptTestCube : CharScript
    {
        public override void OnActivate()
        {
            SetNoRender(owner, true);
        }
    }
}
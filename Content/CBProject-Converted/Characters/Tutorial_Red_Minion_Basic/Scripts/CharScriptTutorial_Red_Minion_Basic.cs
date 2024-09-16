namespace CharScripts
{
    public class CharScriptTutorial_Red_Minion_Basic : CharScript
    {
        public override void OnActivate()
        {
            SpellEffectCreate(out _, out _, "HallucinatePoof.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, owner.Position3D, false);
        }
    }
}
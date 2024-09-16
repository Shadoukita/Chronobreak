using Syndra;

namespace CharScripts;

public class CharScriptSyndra: CharScript
{
    private SyndraFollower SyndraFollower;
    
    public override void OnActivate()
    {
        if (SyndraFollower is null)
        { 
            SyndraFollower = new SyndraFollower(owner); 
            SyndraFollower.OnActivate();
        }
    }

    public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
    {
        SyndraFollower.OnDeath();
    }

    public override void OnResurrect()
    {
        owner.PlayAnimation("Respawn",0,0,1, (AnimationFlags) 230);
        SyndraFollower.OnResurrect();
    }
}
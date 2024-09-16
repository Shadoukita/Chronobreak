namespace Syndra;

public class SyndraFollower(ObjAIBase syndra)
{
    private ObjAIBase Syndra = syndra;
    public FollowerObject? FollowerObject { get; set; }
    public EffectEmitter Orb1 { get; set; }
    public EffectEmitter Orb2 { get; set; }
    public EffectEmitter Orb3  { get; set; }


    public void CreateFollowerObject()
    {
        var follower = new FollowerObject(Syndra,"SyndraOrbs", "SyndraOrbs", Syndra.SkinID, syndra.NetId);
        ApiHandlers.AddGameObject(follower);
        
        //TODO: Handle GameObjects that exist client & server side but do not interact with majority of game systems
        //HACK: Need to check for non-packet ways to handle Follower objects
        {
            follower.SetCollisionRadius(0);
            follower.SetIsTargetableToTeam(TeamId.TEAM_CHAOS, false);
            follower.SetIsTargetableToTeam(TeamId.TEAM_ORDER, false);
            follower.SetIsTargetableToTeam(TeamId.TEAM_NEUTRAL, false);
        }
        
        follower.UpdateMoveOrder(OrderType.OrderNone);
        follower.PlayAnimation("Orbs",0,0,1, (AnimationFlags) 132);
        follower.PlayAnimation("Backup_Idle",0,0,1, (AnimationFlags) 132);
        FollowerObject = follower;
    }

    public void OnActivate()
    {
        CreateFollowerObject();
        CreateOrbitingParticles(); 
        //NOTE: Seems follower objects get updated ever so often to set their face dirctions to some value, maybe their ownder diections
        FollowerObject.FaceDirection(syndra.Direction,true,1.75f);
    }
    
    public void OnResurrect()
    {
        FollowerObject.Reattach(0);
        FollowerObject.SetToRemove();
        CreateFollowerObject();
        CreateOrbitingParticles(); 
    }
    
    public void OnDeath()
    {
        RemoveOrbs();
    }

    /// <summary>
    /// ISSUE: Works fine onces but after used again it makes unremovable particles
    /// </summary>
    public void CreateOrbs()
    {
        Orb1 =  CreateOrb(1);
        Orb2 =  CreateOrb(2);
        Orb3 =  CreateOrb(3);
    }

    public void RemoveOrbs()
    {
        RemoveParticle(Orb1);
        RemoveParticle(Orb2);
        RemoveParticle(Orb3);
    }

    public void CreateOrbitingParticles()
    {
        //Seems like the server queues some FX effects during start up and then sends them in bundles with the FXGroups
        CreateOrbs();
    }
    
    private EffectEmitter CreateOrb(int orbNumber)
    {
        //var skinFlag = SyndraFunctions.SkinFlag(((Champion)Syndra).SkinID);
        
        //var orb = AddParticleLink(
        //    Syndra, 
        //    "Syndra_base_idle_spheres.troy", 
        //    FollowerObject, 
        //    FollowerObject, 
        //    2500f, 
        //    1f,
        //    Syndra.Position3D, 
        //    $"Buffbone_CSTM_Orb_{orbNumber}", 
        //    "Bird_Head", 
        //    "Syndra_base_idle_spheres_enemy.troy", 
        //    false, 
        //    (FXFlags)32, 
        //    Syndra.Team);
        
        return null;
    }
}
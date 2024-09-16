using System.Numerics;
using GameServerCore.Enums;

namespace Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

/// <summary>
/// Object that follows an object with a given NetId client-side
/// Server-side: Exists for networking reasons such as attaching and reattaching to objects
/// Client-Side: Object will update positions with the object it is attached to
/// TOCONFIRM:
/// In Syndra replay packets this object type only received: Follower object, MovementDataNone, animations, particles, and vision packets.
/// Seems to be an object that  exists but doesn't have any tangible existence in the game state other being viewable
/// NOTE: In replays this object will get a face direction packet ever so often but its unknown atm if its time, script, or event triggered.
/// ISSUE: Due to some oddities with the server and spawning objects, the spawn facing direction of the object is wrong even fi you try to correct it
/// </summary>
/// <param name="masterObject">GameObject that is being followed</param>
/// <param name="characterName">Name of the CharacterModel to use as the base for the FollowerObject</param>
/// <param name="internalName">Name of this GameObject</param>
/// <param name="skinId">Id number of the skin to use</param>
/// <param name="senderNetId">NetId of who created the follower object</param>
public class FollowerObject(GameObject masterObject, string internalName, string characterName, int skinId, uint senderNetId = 0)
    : ObjAIBase(characterName, internalName, default, default, skinId, default, default)
{

    /// <summary>
    /// GameObject that is being followed
    /// </summary>
    public GameObject MasterObject { get; } = masterObject;

    public float RotationEnds { get; set; }
    public float LastUpdateTime { get; set; }

    protected override void OnSpawn(int userId, TeamId team, bool doVision)
    {
        LastUpdateTime = Game.Time.GameTime;
        Game.PacketNotifier.NotifyCreateFollowerObject(Model, Name, SkinID, NetId, senderNetId);
    }

    /// <summary>
    /// Used to attach a follower object to a new GameObject or simply the object it was originally attached to.
    /// </summary>
    /// <param name="newOwnerNetId">NetId of the GameObject that FollowerObject will be attached to</param>
    /// <param name="senderNetId">NetId of the GameObject that triggered this packet, usually the owner of the FollowerObject</param>
    public void Reattach(uint newOwnerNetId, uint senderNetId = default)
    {
        if (senderNetId == default)
        {
            senderNetId = NetId;
        }
        LastUpdateTime = Game.Time.GameTime;
        Game.PacketNotifier.NotifyReattachFollowerObject(newOwnerNetId, senderNetId);
    }

    //NOTE: (4.17) Looks to have a custom implementation of FaceDirection(?) 
    public void DoFaceDirection(Vector3 direction, float forceLerpTime, bool doLerp)
    {
    }
}
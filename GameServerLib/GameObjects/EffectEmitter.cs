using System.Numerics;
using GameServerCore.Content;
using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.Logging;
using log4net;
using static Chronobreak.GameServer.Content.ContentManager;

namespace Chronobreak.GameServer.GameObjects;

/// <summary>
/// Class used for all in-game special effects meant to be explicitly networked by the server.
/// </summary>
public class EffectEmitter : GameObject
{
    private static ILog _logger = LoggerProvider.GetLogger();

    //NOTE: Since some hash values haven't been parsed yet, giving option to set the manually

    internal uint PackageHash { get; }
    internal uint EffectNameHash { get; }
    internal uint TargetBoneNameHash { get; }
    internal uint BoneNameHash { get; }
    internal GameObject? Caster { get; }
    internal GameObject? BindObject { get; }
    internal GameObject? TargetObject { get; }
    internal string Package { get; }
    internal string TargetBoneName { get; }
    internal string BoneName { get; }
    internal float ScriptScale { get; }
    internal float TimeSpent { get; private set; }
    internal FXFlags Flags { get; }
    internal Vector2? TargetPosition { get; }
    internal Vector3 Orientation { get; }
    internal float Lifetime { get; }
    internal uint KeywordNetID { get; } = 0;
    internal Champion VisibilityOwner { get; }
    public override bool IsAffectedByFoW => true;
    public override bool SpawnShouldBeHidden => true;


    /// <summary>
    /// Constructor for EffectEmitter where package is not the same as the caster's model (Character).
    /// </summary>
    /// <param name="package">Character package this FX belongs to</param>
    /// <param name="effectName">Name of the FX to spawn</param>
    /// <param name="caster">GameObject that is triggering the FX spawn</param>
    /// <param name="targetObj">GameObject that the FX will target</param>
    /// <param name="bindObj">GameObject that the FX will attach to</param>
    /// <param name="targetBoneName">Rig bone to target</param>
    /// <param name="boneName">Name of a specific rig bone to target</param>
    /// <param name="position">Position of the FX</param>
    /// <param name="targetPosition"></param>
    /// <param name="orientation">Direction the FX will face</param>
    /// <param name="lifetime">Amount of time the EffectEmitter will exist server-side</param>
    /// <param name="scriptScale">How fast the FX script should run at</param>
    /// <param name="flags">Flags that change how a FX will behave</param>
    /// <param name="team">Team the FX belongs to</param>
    /// <param name="visibilityOwner"></param>
    /// <param name="visibilityRadius"></param>
    public EffectEmitter
    (
        string package,
        string effectName,
        AttackableUnit caster,
        GameObject? targetObj = null,
        GameObject? bindObj = null,
        string targetBoneName = "",
        string boneName = "",
        Vector2 position = new(),
        Vector3 orientation = new(),
        Vector2? targetPosition = null,
        float lifetime = 0f,
        float scriptScale = 1.0f,
        FXFlags flags = FXFlags.BindDirection,
        TeamId team = TeamId.TEAM_UNKNOWN,
        Champion? visibilityOwner = null,
        float visibilityRadius = 0
    ) : base(position, "FX", 0, 0, visibilityRadius, 0, team)
    {
        effectName = effectName.ToLower();

        if (effectName.EndsWith(".prt"))
        {
            effectName = $"{effectName[..^4]}.troy";
        }
        else if (!effectName.EndsWith(".troy"))
        {
            effectName = $"{effectName}.troy";
        }

        Package = package;
        Name = effectName;
        Caster = caster;
        TargetObject = targetObj;
        BindObject = bindObj;
        //NOTE: Replays show that Target Position is not always the position of a Target, it can sometimes be the Caster position for some FX.
        //As there was a case in a replay where TargetObj was not set and the Caster position was present in both owner-position, target-position section
        //Also possible this could be set to a map position but this needs to be confirmed. 
        //However for most instances in replays it seems to be mostly the case that it is.
        //So TargetPosition will default to null and just fallback to TargetObj-Position if its null or new Vector2 if that is also null or a target isn't provided
        if (targetPosition is null)
        {
            TargetPosition = targetObj?.Position ?? new Vector2();
        }
        else
        {
            TargetPosition = targetPosition;
        }
        // NOTE: In theory this shouldn't matter with a BindObjct but needs to be confirmed and checked in replays for instances of BindObj providing a target-position
        // This will require checking previous packets for a position as there is no bindobj position in FX packets, just owner-position, position, and target-position.

        Orientation = Direction = orientation;
        TargetBoneName = targetBoneName;
        BoneName = boneName;
        Flags = flags;
        ScriptScale = scriptScale;
        VisibilityOwner = visibilityOwner;

        if (team != TeamId.TEAM_UNKNOWN)
        {
            Team = team;
        }
        else if (caster is not null)
        {
            Team = Caster.Team;
        }
        else
        {
            Team = TeamId.TEAM_ALL;
        }

        var pd = GetParticleData(effectName, caster, bindObj, targetObj);

        if (lifetime <= 0)
        {
            Lifetime = pd?.LifeTime ?? 0;
        }
        else
        {
            Lifetime = lifetime;
        }

        var skinId = (caster as ObjAIBase)?.SkinID ?? 0;
        var szSkin = skinId < 10 ? $"{skinId:D2}" : skinId.ToString();
        package = $"[Character]{package}{szSkin}";

        PackageHash = HashFunctions.HashStringNorm(package);
        EffectNameHash = HashFunctions.HashString(effectName);
        TargetBoneNameHash = HashFunctions.HashString(targetBoneName);
        BoneNameHash = HashFunctions.HashString(boneName);
    }

    /// <summary>
    /// Constructor for EffectEmitter where package matches the caster Character (model)
    /// </summary>
    /// <param name="effectName">Name of the FX to spawn</param>
    /// <param name="caster">GameObject that is triggering the FX spawn</param>
    /// <param name="targetObj">GameObject that the FX will target</param>
    /// <param name="bindObj">GameObject that the FX will attach to</param>
    /// <param name="targetBoneName">Rig bone to target</param>
    /// <param name="boneName">Name of a specific rig bone to target</param>
    /// <param name="position">Position of the FX</param>
    /// <param name="orientation">Direction the FX will face</param>
    /// <param name="lifetime">Amount of time the EffectEmitter will exist server-side</param>
    /// <param name="scriptScale">How fast the FX script should run at</param>
    /// <param name="flags">Flags that change how a FX will behave</param>
    /// <param name="team">Team the FX belongs to</param>
    /// <param name="visibilityOwner"></param>
    /// <param name="visibilityRadius"></param>
    public EffectEmitter
    (
        string effectName,
        GameObject? caster = null,
        GameObject? targetObj = null,
        GameObject? bindObj = null,

        string targetBoneName = "",
        string boneName = "",

        Vector2 position = new(),
        Vector3 orientation = new(),

        Vector2? targetPosition = null,
        float lifetime = 0f,
        float scriptScale = 1.0f,

        FXFlags flags = FXFlags.BindDirection,
        TeamId team = TeamId.TEAM_UNKNOWN,
        Champion visibilityOwner = null,
        float visibilityRadius = 0
    ) : base(position, "FX", 0, 0, visibilityRadius, 0, team)
    {
        effectName = effectName.ToLower();
        effectName = effectName.EndsWith(".prt") ? $"{effectName[..^4]}.troy" : effectName.EndsWith(".troy") ? effectName : $"{effectName}.troy";

        Name = effectName;
        Caster = caster;
        TargetObject = targetObj;
        BindObject = bindObj;
        Orientation = Direction = orientation;
        TargetBoneName = targetBoneName;
        BoneName = boneName;
        Flags = flags;
        ScriptScale = scriptScale;
        VisibilityOwner = visibilityOwner;
        Lifetime = lifetime;


        // Check the other constructors for note on TargetPosition
        if (targetPosition is null)
        {
            TargetPosition = targetObj?.Position ?? new Vector2();
        }
        else
        {
            TargetPosition = targetPosition;
        }

        if (team is not TeamId.TEAM_UNKNOWN && team != caster?.Team)
        {
            Team = team;
        }
        else if (caster is not null)
        {
            Team = Caster.Team;
        }
        else
        {
            Team = TeamId.TEAM_UNKNOWN;
        }

        var pd = GetParticleData(effectName, caster, bindObj, targetObj);

        if (pd is null)
        {
            return;
        }

        if (lifetime <= 0f)
        {
            Lifetime = pd.LifeTime;
        }

        //var skinId = ((pd.SkinId >= 0) ? pd.SkinId.ToString().PadLeft(2, '0') : "");
        PackageHash = (caster as AttackableUnit)?.GetObjHash() ?? 0;//HashFunctions.HashStringNorm($"[character]{pd.Model}{skinId}");
        EffectNameHash = HashFunctions.HashString(effectName);
        TargetBoneNameHash = HashFunctions.HashString(targetBoneName);
        BoneNameHash = HashFunctions.HashString(boneName);
    }

    //NOTE: Team based FX packets are only sent to the correct teams they belong to in replays
    public static void CreateTeamEffects(
        out EffectEmitter allyEffect,
        out EffectEmitter enemyEffect,

        string package,
        string allyEffectName,
        string enemyEffectName,

        TeamId allyTeam,
        TeamId enemyTeam,

        AttackableUnit caster,
        GameObject? targetObj = null,
        GameObject? bindObj = null,

        string targetBoneName = "",
        string boneName = "",

        Vector2 position = new(),
        Vector3 orientation = new(),

        Vector2? targetPosition = null,
        float lifetime = 0f,
        float scriptScale = 1.0f,

        FXFlags flags = FXFlags.BindDirection,
        Champion visibilityOwner = null,
        float visibilityRadius = 0)
    {
        allyEffect = new EffectEmitter(package, allyEffectName, caster, targetObj, bindObj, targetBoneName, boneName, position, orientation, targetPosition, lifetime, scriptScale, flags, allyTeam, visibilityOwner, visibilityRadius);
        enemyEffect = new EffectEmitter(package, enemyEffectName, caster, targetObj, bindObj, targetBoneName, boneName, position, orientation, targetPosition, lifetime, scriptScale, flags, enemyTeam, visibilityOwner, visibilityRadius);
    }

    //NOTE: Team based FX packets are only sent to the correct teams they belong to in replays
    public static void CreateTeamEffects(
        out EffectEmitter allyEffect,
        out EffectEmitter enemyEffect,

        string allyEffectName,
        string enemyEffectName,

        TeamId allyTeam,
        TeamId enemyTeam,

        GameObject caster,
        GameObject? targetObj = null,
        GameObject? bindObj = null,

        string targetBoneName = "",
        string boneName = "",

        Vector2 position = new(),
        Vector3 orientation = new(),

        Vector2? targetPosition = null,
        float lifetime = 0f,
        float scriptScale = 1.0f,

        FXFlags flags = FXFlags.BindDirection,
        Champion visibilityOwner = null,
        float visibilityRadius = 0)
    {
        allyEffect = new EffectEmitter(allyEffectName, caster, targetObj, bindObj, targetBoneName, boneName, position, orientation, targetPosition, lifetime, scriptScale, flags, allyTeam, visibilityOwner, visibilityRadius);
        enemyEffect = new EffectEmitter(enemyEffectName, caster, targetObj, bindObj, targetBoneName, boneName, position, orientation, targetPosition, lifetime, scriptScale, flags, enemyTeam, visibilityOwner, visibilityRadius);
    }

    protected override void OnEnterVision(int userId, TeamId team)
    {
        Game.PacketNotifier.NotifyFXEnterTeamVisibility(NetId, NetId, team, userId);
    }

    protected override void OnLeaveVision(int userId, TeamId team)
    {
        Game.PacketNotifier.NotifyFXLeaveTeamVisibility(NetId, NetId, team, userId);
    }

    internal override void Update()
    {
        TimeSpent += Game.Time.DeltaTimeSeconds;

        if (Lifetime >= 0 && TimeSpent >= Lifetime)
        {
            SetToRemove();
        }
    }

    public override void SetToRemove()
    {
        if (IsToRemove())
        {
            return;
        }
        base.SetToRemove();
        Game.PacketNotifier.NotifyFXKill(NetId, NetId);
    }
}
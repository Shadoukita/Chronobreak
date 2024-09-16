using System;
using System.Numerics;
using GameServerCore;
using GameServerCore.Enums;
using Chronobreak.GameServer.API;
using Chronobreak.GameServer.GameObjects.AttackableUnits;

namespace Chronobreak.GameServer.GameObjects.SpellNS.Missile;

public abstract class SpellMissile : GameObject
{
    public virtual MissileType Type => MissileType.None;
    public CastInfo CastInfo { get; }
    public AttackableUnit? TargetUnit { get; protected set; }
    public Spell SpellOrigin { get; }
    public Vector3 StartPoint { get; protected set; }
    public Vector3 EndPoint { get; protected set; }
    protected bool IsServerOnly { get; }
    protected SpellDataFlags Flags { get; } = 0;

    public override bool IsAffectedByFoW => true;
    public override bool SpawnShouldBeHidden => true;

    public float CreationTime { get; internal set; }
    public float Lifetime => SpellOrigin.Data.MissileLifetime;
    public bool UseFixedTravelTime => SpellOrigin.Data.MissileFixedTravelTime > 0 && (SpellOrigin.Data.MissileGravity > 0 || SpellOrigin.Data.MissileUnblockable);
    public float TimeSinceCreation => Game.Time.GameTime / 1000.0f - CreationTime;
    // This parameter paves the way for the implementation of LuaOnMissileUpdateDistanceInterval in SpellData
    public float DistanceSinceCreation = 0;
    public float Speed;
    private Vector2 TargetPosition;
    public Vector2 Destination
    {
        get => TargetUnit?.Position ?? TargetPosition;
        set
        {
            Game.PacketNotifier.NotifyS2C_ChangeMissileTarget(this, value.ToVector3(0));
            TargetPosition = value;
        }
    }

    public float Physics
    {
        get =>
        SpellOrigin.Data.MissileAccel == 0 ? Speed :
        Math.Clamp(
            Speed + SpellOrigin.Data.MissileAccel * TimeSinceCreation,
            SpellOrigin.Data.MissileMinSpeed,
            SpellOrigin.Data.MissileMaxSpeed
        );
        set
        {
            Game.PacketNotifier.NotifyS2C_ChangeMissileSpeed(this, value - Speed, 0);
            Speed = value;
        }
    }
    public SpellMissile(
        Spell spell,
        CastInfo castInfo,
        SpellDataFlags flags
    ) : base(
        position: castInfo.SpellCastLaunchPosition.ToVector2(),
        name: spell.SpellData.MissileEffect,
        collisionRadius: spell.SpellData.LineWidth,
        pathingRadius: 0,
        //TODO: spell.Data.MissilePerceptionBubbleRevealsStealth
        visionRadius: spell.Data.MissilePerceptionBubbleRadius,
        netId: 0
    )
    {
        Team = castInfo.Caster.Team;
        IsServerOnly = spell.Data.MissileEffect != "";
        SpellOrigin = spell;
        CastInfo = castInfo;
        Flags = flags;

        TargetUnit = castInfo.Target?.Unit;
        StartPoint = castInfo.SpellCastLaunchPosition;
        EndPoint = castInfo.TargetPosition;
        TargetPosition = EndPoint.ToVector2();
        Speed = UseFixedTravelTime ? Vector3.Distance(StartPoint, EndPoint) / spell.Data.MissileFixedTravelTime : spell.Data.MissileSpeed;
    }

    protected override void OnSpawn(int userId, TeamId team, bool doVision)
    {
        CreationTime = Game.Time.GameTime / 1000;
        //if(!IsServerOnly)
        {
            Game.PacketNotifier.NotifyMissileReplication(this, userId);
        }
    }

    internal override void Update()
    {
        SpellOrigin.OnMissileUpdate(this);
        if (Lifetime > 0 && TimeSinceCreation > Lifetime)
        {
            SetToRemove();
        }
    }

    public override Vector3 GetPosition3D()
    {
        return Position.ToVector3(GetHeight() + SpellOrigin.Data.MissileTargetHeightAugment);
    }

    internal override float GetHeight()
    {
        return SpellOrigin.Data.MissileFollowsTerrainHeight ? base.GetHeight() : StartPoint.Y;
    }

    protected bool LinearMoveTo(Vector2 dest, float distance = 0)
    {
        var dir = (dest - Position).Normalized();
        Direction = dir.ToVector3(0);

        //TODO: FixedTravelTime + MinTravelTime
        float dist = Physics * Game.Time.DeltaTime / 1000;
        if (dist * dist < Position.DistanceSquared(dest))
        {
            Position += dir * dist;
            DistanceSinceCreation += dist;
            return false;
        }
        Position = dest;
        return true;
    }

    public void SetToRemoveBlocked()
    {
        if (!IsToRemove())
        {
            base.SetToRemove();
            ApiEventManager.OnSpellMissileRemoved.Publish(this);
            //if(!IsServerOnly)
            {
                Game.PacketNotifier.NotifyDestroyClientMissile(this);
            }
        }
    }

    public override void SetToRemove()
    {
        if (!IsToRemove())
        {
            base.SetToRemove();
            SpellOrigin.OnMissileEnd(this);
            ApiEventManager.OnSpellMissileEnd.Publish(this);
            ApiEventManager.OnSpellMissileRemoved.Publish(this);
            //if(!IsServerOnly)
            {
                Game.PacketNotifier.NotifyDestroyClientMissile(this);
            }
        }
    }

    public override void OnCollision(GameObject collider, bool isTerrain = false)
    {
        if (isTerrain)
        {
            SpellOrigin.OnSpellMissileHitTerrain(this);
            ApiEventManager.OnSpellMissileHitTerrain.Publish(this);
        }
    }
}

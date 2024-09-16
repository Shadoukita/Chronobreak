using GameServerCore;
using GameServerCore.Enums;
using E = GameServerCore.Extensions;

namespace Chronobreak.GameServer.GameObjects.SpellNS.Missile;

public class SpellCircleMissile : SpellMissile
{
    float radius;
    float initialAngularAngle;
    float angularSpeed => SpellOrigin.Data.CircleMissileAngularVelocity * E.RAD2DEG;
    float currentAngularAngle => (initialAngularAngle + angularSpeed * TimeSinceCreation) % 360;
    float initialRadialAngle;
    float radialSpeed => SpellOrigin.Data.CircleMissileRadialVelocity * E.RAD2DEG;
    float currentRadialAngle => (initialRadialAngle + radialSpeed * TimeSinceCreation) % 360;
    public override MissileType Type => MissileType.Circle;
    public SpellCircleMissile(Spell spell, CastInfo castInfo, SpellDataFlags flags) : base(spell, castInfo, flags)
    {
        var dir = Position - Destination;
        radius = dir.Length();
        initialAngularAngle = dir.Angle();
    }
    internal override void Update()
    {
        base.Update();
        Position = Destination + E.FromAngle(currentAngularAngle) * radius;
        Direction = E.FromAngle(currentRadialAngle).ToVector3(0);
    }
}
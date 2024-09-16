using System;
using System.Numerics;
using System.Collections.Generic;
using GameServerCore;
using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using F = Chronobreak.GameServer.Scripting.CSharp.Converted.Functions_CS;

namespace Chronobreak.GameServer.GameObjects.SpellNS.Missile;

public class SpellLineMissile : SpellMissile
{
    private List<AttackableUnit> _unitsHit = [];
    private List<AttackableUnit> _newUnitsHit = [];
    public bool Bounced { get; private set; } = false;
    public override MissileType Type => MissileType.Arc;
    public SpellLineMissile(Spell spell, CastInfo castInfo, SpellDataFlags flags) : base(spell, castInfo, flags)
    {
        Direction = (EndPoint - StartPoint).Normalized();
        EndPoint = StartPoint + Direction * MathF.Min(castInfo.Spell.CastRange, Vector2.Distance(StartPoint.ToVector2(), EndPoint.ToVector2()));
    }
    internal override float GetHeight()
    {
        return SpellOrigin.Data.LineMissileFollowsTerrainHeight ? base.GetHeight() : StartPoint.Y;
    }
    internal override void Update()
    {
        base.Update();

        bool reached = LinearMoveTo(Destination);

        //TODO: Consider it as a collision of two stadiums?
        // Currently a collision is expected between a fast-moving missile and a static unit.
        //TODO: Consider the possibility of a missile going through an entity during a lag-spike/low tick-rate
        var units = F.GetUnitsInArea(SpellOrigin.Caster, Position.ToVector3(GetHeight()), SpellOrigin.Data.LineWidth, Flags);
        foreach (var unit in units)
        {
            if (!_unitsHit.Contains(unit))
            {
                _newUnitsHit.Add(unit);
            }
        }

        if (_newUnitsHit.Count > 0)
        {
            Game.PacketNotifier.NotifyS2C_LineMissileHitList(this, _newUnitsHit);
            foreach (var unit in _newUnitsHit)
            {
                SpellOrigin.ApplyEffects(unit, this, Flags);
                if (IsToRemove())
                {
                    // To avoid unnecessary multiple triggering
                    return;
                }
            }
            _unitsHit.AddRange(_newUnitsHit);
            _newUnitsHit.Clear();
        }

        if (reached) //TODO: SpellOrigin.Data.LineMissileEndsAtTargetPoint
        {
            if (!Bounced && SpellOrigin.Data.LineMissileBounces > 0)
            {
                Bounced = true;
                StartPoint = Position3D;
                TargetUnit = SpellOrigin.Caster;
                EndPoint = TargetUnit.Position3D;
            }
            else
                SetToRemove();
        }
    }
}

using System.Diagnostics;
using System.Collections.Generic;
using GameServerCore;
using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using F = Chronobreak.GameServer.Scripting.CSharp.Converted.Functions_CS;
using Chronobreak.GameServer.Scripting.CSharp;

namespace Chronobreak.GameServer.GameObjects.SpellNS.Missile;

public class SpellChainMissile : SpellMissile
{
    private int Hits = 0;
    private HashSet<AttackableUnit> _unitsHit = [];
    private MissileParameters mp;
    public override MissileType Type => MissileType.Chained;
    public SpellChainMissile(Spell spell, CastInfo castInfo, SpellDataFlags flags) : base(spell, castInfo, flags)
    {
        mp = SpellOrigin.Script.MetaData.MissileParameters!;
        Debug.Assert(TargetUnit != null);
        Debug.Assert(mp != null);
    }

    internal override void Update()
    {
        base.Update();

        Game.PacketNotifier.NotifyS2C_ChainMissileSync(this);

        if (LinearMoveTo(TargetUnit!.Position))
        {
            SpellOrigin.ApplyEffects(TargetUnit!, this, Flags);
            _unitsHit.Add(TargetUnit!);
            Hits++;
            if (Hits < mp.MaximumHits) //TODO: MaximumHitsByLevel
            {
                var units = F.GetClosestUnitsInArea(
                    SpellOrigin.Caster,
                    Position.ToVector3(0),
                    SpellOrigin.Data.BounceRadius,
                    Flags, //TODO: CanHitEnemies and CanHitFriends
                    int.MaxValue
                );
                if (FoundTarget()) return;
                bool FoundTarget()
                {
                    foreach (var unit in units)
                        if (!_unitsHit.Contains(unit) && (unit != SpellOrigin.Caster || mp.CanHitCaster))
                        {
                            TargetUnit = unit;

                            // Does not work
                            //Game.PacketNotifier.NotifyS2C_ChainMissileSync(this);
                            //Game.PacketNotifier.NotifyS2C_UpdateBounceMissile(this);

                            //HACK: (Inherited from the previous implementation)
                            StartPoint = Position3D;
                            EndPoint = TargetUnit.Position3D;
                            CastInfo.Targets.Clear();
                            CastInfo.Targets.Add(new CastTarget(TargetUnit, HitResult.HIT_Normal));
                            CastInfo.TargetPosition = CastInfo.TargetPositionEnd = EndPoint;
                            CastInfo.SpellCastLaunchPosition = StartPoint;
                            Game.PacketNotifier.NotifyMissileReplication(this);

                            return true;
                        }
                    return false;
                }
                if (mp.CanHitSameTarget)
                {
                    _unitsHit.Clear();
                    _unitsHit.Add(TargetUnit!);
                    if (FoundTarget()) return;
                }
                //TODO: CanHitSameTargetConsecutively
            }
            SetToRemove();
        }
    }
}
using System.Diagnostics;
using GameServerCore.Enums;
using Chronobreak.GameServer.Logging;
using log4net;

namespace Chronobreak.GameServer.GameObjects.SpellNS.Missile;

public class SpellTargetMissile : SpellMissile
{
    private readonly ILog _logger = LoggerProvider.GetLogger();
    public override MissileType Type => MissileType.Target;
    public SpellTargetMissile(Spell spell, CastInfo castInfo, SpellDataFlags flags) : base(spell, castInfo, flags)
    {
        Debug.Assert(TargetUnit != null);
        EndPoint = TargetUnit.Position3D;
    }
    internal override void Update()
    {
        base.Update();
        if (TargetUnit is null)
        {
            _logger.Warn("TargetUnit can't be null!");
            SetToRemove();
            return;
        }
        if (LinearMoveTo(TargetUnit.Position))
        {
            SpellOrigin.ApplyEffects(TargetUnit, this, Flags);
            SetToRemove();
        }
    }
}

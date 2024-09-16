using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.SpellNS.Missile;

namespace Chronobreak.GameServer.GameObjects.SpellNS;

public class CastInfo
{
    public uint NetId { get; init; }
    public Spell Spell { get; init; }
    public int SpellLevel { get; init; }
    public float AttackSpeedModifier = 1.0f;
    public ObjAIBase Caster { get; init; }
    public ObjAIBase SpellChainOwner { get; init; }
    public SpellMissile? Missile;
    public Vector3 TargetPosition { get; set; } //HACK: Modified by chain missile
    public Vector3 TargetPositionEnd { get; set; } //HACK: Modified by chain missile

    public CastTarget? Target => Targets.FirstOrDefault();
    public List<CastTarget> Targets; //HACK: Modified by chain missile

    public float DesignerCastTime { get; init; }
    public float ExtraCastTime { get; init; }
    public float DesignerTotalTime { get; init; }
    public float Cooldown { get; init; }
    public float StartCastTime { get; init; }

    public bool IsBasicAttack { get; init; } = false;
    public bool IsSecondAutoAttack { get; init; } = false;
    public bool IsForceCastingOrChannel { get; init; } = false;
    public bool IsOverrideCastPosition { get; init; } = false;
    public bool IsClickCasted { get; init; } = false;

    public float ManaCost { get; init; }
    public Vector3 SpellCastLaunchPosition { get; set; } //HACK: Modified by chain missile
    public int AmmoUsed { get; init; }
    public float AmmoRechargeTime { get; init; }
}

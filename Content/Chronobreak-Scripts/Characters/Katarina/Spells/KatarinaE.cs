using GameServerCore;
using GameServerLib.Services;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

namespace Spells;

public class KatarinaE : SpellScript
{
    public override SpellScriptMetadata MetaData { get; } = new() { };

    public override void SelfExecute()
    {
        owner.FaceDirection(spell.CurrentCastInfo!.TargetPosition, true, 0);

        caster.Spells.Extra[0].TryCast(
            default,
            spell.CurrentCastInfo!.TargetPosition,
            spell.CurrentCastInfo!.TargetPosition,
            0,
            false,
            false,
            false,
            false,
            false,
            false,
            default
        );

        var targetPos = spell.CurrentCastInfo!.TargetPosition;
        var indicator = new EffectEmitter(
            "Katarina",
            "Katarina_Base_E_EndIndicator.troy",
            owner,
            default,
            default,
            "",
            "",
            targetPos.ToVector2(),
            owner.Direction,
            targetPos.ToVector2(),
            flags: (FXFlags)48
        );
        SpecialEffectService.SpawnFx([indicator], owner.NetId);
    }
}


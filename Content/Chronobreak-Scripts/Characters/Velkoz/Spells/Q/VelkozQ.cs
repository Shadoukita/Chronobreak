using System;
using GameServerCore;
using GameServerLib.Services;

namespace Spells;

public class VelkozQ: SpellScript
{
    public override SpellScriptMetadata MetaData { get; } = new() { };
    
    public override void SelfExecute()
    {
        
        owner.FaceDirection(spell.CurrentCastInfo!.TargetPosition,true, 0);
        
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

        var x = GetPointByUnitFacingOffset(owner, 1000, 0);
        var pos = x.ToVector2();
        var indicator = new EffectEmitter(
            "Velkoz", 
            "Velkoz_Base_Q_EndIndicator.troy", 
            owner, 
            default, 
            default,
            "", 
            "",
            pos,
            owner.Direction,
            pos,
            flags: (FXFlags) 48);
        SpecialEffectService.SpawnFx([indicator], owner.NetId);
    }
}
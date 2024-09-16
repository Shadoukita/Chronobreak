using System;
using GameServerCore;
using GameServerLib.Services;
using Chronobreak.GameServer;
using Chronobreak.GameServer.Chatbox;
using VelKoz;

namespace Spells;

public class VelkozQMissile: SpellScript
{
    private bool _hitTarget = false;
    
    public override SpellScriptMetadata MetaData { get; } = new()
    {
        MissileParameters = new MissileParameters()
        {
            MaximumHits = 1,
            CanHitSameTargetConsecutively = false,
            CanHitSameTarget = false
        },
        TriggersSpellCasts = false,
        IsDamagingSpell = true,
        NotSingleTargetSpell = false,
        SpellFXOverrideSkins = new[] { "CyberEzreal", },
    };


    private bool splitEarly = false;
    public override void OnMissileEnd(SpellMissile missile)
    {
        if (splitEarly || missile.TargetUnit is not null)
        {
            splitEarly = false;
            return;
        }
        
        VelkozPackage.CastQSplitMissiles(caster, missile, missile.EndPoint); 
    }
    
    
    
    public override void TargetExecute(AttackableUnit target, SpellMissile? missileNetworkID, ref HitResult hitResult)
    {
        ApiHandlers.PacketNotifier.NotifyDestroyClientMissile(missileNetworkID);
        if (target is not null)
        { 
            var splitPos = target.GetPosition3D();
            target.TakeDamage(owner, 1, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELL);
            splitEarly = true;
            var implosion = new EffectEmitter(
                "Velkoz", 
                "Velkoz_Base_Q_SplitImplosion.troy", 
                owner, 
                default, 
                default,
                "", 
                "",
                splitPos.ToVector2(),
                missileNetworkID.Direction,
                splitPos.ToVector2(),
                flags: (FXFlags) 48);
            
            var tar = new EffectEmitter(
                "Velkoz", 
                "Velkoz_Base_Q_Missile_tar.troy", 
                owner, 
                target, 
                target,
                "", 
                "",
                target.Position,
                default,
                target.Position,
                flags: (FXFlags) 48);

            SpecialEffectService.SpawnFx([[implosion],[tar]], owner.NetId);
            VelkozPackage.CastQSplitMissiles(caster, missileNetworkID, splitPos);
        }
    }
}
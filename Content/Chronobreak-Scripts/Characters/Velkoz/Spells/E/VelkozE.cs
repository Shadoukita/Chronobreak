using GameServerCore;
using GameServerLib.Services;
using Chronobreak.GameServer.Chatbox;
using Chronobreak.GameServer.GameObjects.SpellNS;

namespace Spells;

public class VelkozE: SpellScript
{
    public override SpellScriptMetadata MetaData { get; } = new()
    {
        TriggersSpellCasts = true,
        ChannelDuration = 0.4f,
    };
    
    public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, ref HitResult hitResult)
    {
        base.TargetExecute(target, missileNetworkID, ref hitResult);
    }

    public override void ChannelingStart()
    {
        base.ChannelingStart();
    }

    public override void ChannelingSuccessStop()
    {
        var targetPos = GetCastSpellTargetPos(spell);
        EffectEmitter.CreateTeamEffects(
            out var allyEffect,
            out var enemyEffect,
            "Velkoz",
            "Velkoz_Base_E_AOE_green.troy",
            "Velkoz_Base_E_AOE_red.troy",
            caster.Team,
            caster.Team.GetEnemyTeam(),
            owner,
            default,
            default,
            "",
            "",
            targetPos.ToVector2(),
            owner.Direction,
            targetPos.ToVector2());
        
        SpecialEffectService.SpawnTeamFx(caster.Team, caster.Team.GetEnemyTeam(), [[allyEffect]], [[enemyEffect]]);
        ChatManager.Send($"{owner.Position}",0);
    }
}
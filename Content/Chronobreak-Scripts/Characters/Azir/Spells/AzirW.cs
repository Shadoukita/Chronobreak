using CharScripts;
using GameServerCore;
using Chronobreak.GameServer;
using Chronobreak.GameServer.GameObjects.SpellNS;
using ValkyrieScripts.Characters.Azir;

namespace Spells;

public class AzirW: SpellScript
{
    //private AzirMinionManager _manager;// =>  ((CharScriptAzir)owner.CharScript).AzirMinionManager;

    public override SpellScriptMetadata MetaData { get; } = new()
    {
        TriggersSpellCasts = true,
        IsDamagingSpell = false,
        //SpellFXOverrideSkins = new[] { "", },
        AmmoPerCharge = 2,
        //idk if this should be set as the OverrideCastTime from the ini but the only way to get channel methods to trigger is to make this above 0 
        SpellDamageRatio = 0.6f,
        OverrideFlags = (SpellDataFlags) 262144
    };

    public override void SelfExecute()
    {
        var castPos = GetCastSpellTargetPos(spell).ToVector2();

        var soldier  = AddMinion(
            owner,
            "AzirSoldier",
            "AzirSoldier",
            castPos,
            owner.Team,
            owner.SkinID,
            true,
            false, // packets shows this as being set to true
            false,
            true,
            "AzirSoldier",
            true,
            1,
            0,
            null,
            (SpellDataFlags)33554432);
        
        soldier.UpdateMoveOrder(OrderType.Stop);
        AzirFunctions.SoldierSpawnFX(owner, soldier);
    }
}
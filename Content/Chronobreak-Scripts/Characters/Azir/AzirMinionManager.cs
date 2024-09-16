using GameServerCore;
using GameServerLib.Services;
using LeaguePackets.Game.Common;

namespace CharScripts;

public class AzirMinionManager(ObjAIBase azir)
{
    private ObjAIBase _azir = azir;
    public List<Minion> Soldiers { get; } = [];
    public List<Minion> SoldiersUlt { get; } = [];

    
    public Minion CreateSandSoldier(Vector2 position)
    {
        var soldier = AddMinion(
            _azir,
            "AzirSoldier",
            "AzirSoldier",
            position,
            _azir.Team,
            _azir.SkinID,
            true,
            false, // packets shows this as being set to true(?)
            false,
            true,
            //"AzirSoldier",
            "",
            true,
            1,
            0,
            null,
            (SpellDataFlags)33554432);
        Soldiers.Add(soldier);
        
        return soldier;
    }
    
    public Minion CreateUltSandSoldier(Vector2 position)
    {
        var soldier = AddMinion(
            _azir,
            "AzirUltSoldier",
            "AzirSoldier",
            position,
            _azir.Team,
            _azir.SkinID,
            true,
            false, // packets shows this as being set to true
            false,
            true,
            "",
            true,
            1,
            0,
            null,
            (SpellDataFlags)33554432);
        
        SoldiersUlt.Add(soldier);
        //ApplyParticles(_azir, soldier);
        
        return soldier;
    }

    public List<Minion> CreateUltSandSoldierWall(Vector3 position, float endPosition, int wallLength, int soldierCount)
    {
        var wallSoldiers = new List<Minion>();
        
        var positions = Functions_CS.GetPointsOnLine(
            _azir.GetPosition3D(), 
            position, 
            wallLength, //TODO: Implemented SpellTargeter from ini files
            endPosition, 
            soldierCount).ToArray();

        for (int i = 0; i < soldierCount; i++)
        {
            wallSoldiers.Add(CreateUltSandSoldier(positions[i].ToVector2()));
        }

        return wallSoldiers;
    }

    public Minion CreatTowerClicker(Vector2 position)
    {
        var towerClicker = AddMinion(
            _azir,
            "TowerClicker",
            "AzirTowerClicker",
            position,
            _azir.Team,
            _azir.SkinID,
            true,
            true,
            false,
            true,
            "",
            true,
            1,
            0,
            null,
            (SpellDataFlags)33554432,
            _azir);
        
        var passiveIdle = new EffectEmitter(package: "Azir", effectName: "Azir_Base_P_Idle.troy", caster: towerClicker, targetObj: towerClicker, bindObj: towerClicker, targetBoneName: "C_Buffbone_Glb_Chest_Loc", boneName: "Root", position: towerClicker.Position, orientation: towerClicker.Direction, team: towerClicker.Team);
        
        SpecialEffectService.SpawnFx([passiveIdle]);
        
        return towerClicker;
    }
    
    public void RemoveSandSoldier(ObjAIBase soldier)
    {
        Soldiers.Remove(((Minion)soldier));
    }
}
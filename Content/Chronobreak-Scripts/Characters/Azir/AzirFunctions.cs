using GameServerCore;
using GameServerLib.Services;

namespace ValkyrieScripts.Characters.Azir;

public class AzirFunctions
{
    public static void SoldierSpawnFX(ObjAIBase azir, Minion soldier)
    {
        EffectEmitter[] weapon =
        [
            new EffectEmitter("Azir", "Azir_Base_W_WeaponGlow.troy", azir, soldier, soldier,
                "", "Cstm_Buffbone_R_Weapon_Tip", soldier.Position, team: azir.Team),
            /*
            new EffectEmitter("Azir", "Azir_Base_W_WeaponGlow2.troy", azir, soldier, soldier,
                "", "Cstm_Buffbone_R_Weapon_Tip", soldier.Position, team: azir.Team.GetEnemyTeam())
            */
        ];

        EffectEmitter[] spawnIn =
        [
            new EffectEmitter("Azir", "Azir_Base_W_SpawnIn.troy", azir, default, default,
                "", "", soldier.Position, flags: (FXFlags) 160, team: azir.Team)
        ];

        EffectEmitter[] bib =
        [
            new EffectEmitter("Azir", "Azir_base_W_Sandbib.troy", azir, soldier, soldier,
                "", "C_Buffbone_Glb_Chest_Loc", soldier.Position, team: azir.Team),
            
            /*
             new EffectEmitter("Azir", "Azir_base_W_Sandbib_Enemy.troy", azir, soldier, soldier,
                "", "C_Buffbone_Glb_Chest_Loc", soldier.Position, team: azir.Team.GetEnemyTeam()),
            */
        ];
        
        EffectEmitter[] cape = 
        [
            new EffectEmitter("Azir", "Azir_Base_W_SoldierCape.troy", azir, soldier, soldier,
                "", "", azir.Position, team: azir.Team),
            /*
            new EffectEmitter("Azir", "Azir_Base_W_SoldierCape_Enemy.troy", azir, soldier, soldier,
                "", "", azir.Position, team: azir.Team.GetEnemyTeam()),
            */
        ];
        
        EffectEmitter[] soldierIndicator = 
        [
            new EffectEmitter("Azir", "Azir_Base_W_SoldierIndicator.troy", soldier, soldier, azir,
                "C_Buffbone_Glb_Chest_Loc", "Pelvis", azir.Position, flags: (FXFlags) 160, team: azir.Team),
            new EffectEmitter("Azir", "Azir_Base_W_SoldierIndicator.troy", soldier, azir, soldier,
                "Pelvis", "C_Buffbone_Glb_Chest_Loc", soldier.Position, flags: (FXFlags) 160, team: azir.Team),
        ];
        SpecialEffectService.SpawnFx([soldierIndicator]);
        SpecialEffectService.SpawnFx([weapon, spawnIn, bib, cape], azir.NetId);
    }

    public static void SoldierDeathFX(ObjAIBase azir, Minion soldier)
    {
        SpecialEffectService.SpawnFx(
        [
            new EffectEmitter("Azir", "Azir_Base_W_Soldier_Dissipate.troy", azir, soldier, azir,
            "C_Buffbone_Glb_Chest_Loc", "Pelvis", soldier.Position, team: azir.Team)
        ]);
    }

    public static void SoldierMoveEnd(Minion soldier)
    {
        soldier.StopAnimation("Run",false,true);
        soldier.PlayAnimation("Run_Exit", flags:(AnimationFlags) 128);
    }
}
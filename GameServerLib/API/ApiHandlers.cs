using System;
using System.Collections.Generic;
using System.Linq;
using GameServerCore.Enums;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.Content.Navigation;
using Chronobreak.GameServer.GameObjects;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using MapScripts.GameModes;
using PacketDefinitions420;

namespace Chronobreak.GameServer.API;
/// <summary>
/// Api class that handles passing parts of core system managers/handlers to scripts
/// rather than them having full access to the server internals.
/// TODO: Work on how this class should be structured
/// </summary>
public class ApiHandlers
{
    public static bool CheatsEnabled => Game.Config.ChatCheatsEnabled;

    public static CharData? GetCharData(string characterName) => Content.ContentManager.GetCharData(characterName);
    public static PacketNotifier PacketNotifier => Game.PacketNotifier;
    public static PlayerManager PlayerManager => Game.PlayerManager;

    public static List<TeamId> Teams = Teams = Enum.GetValues(typeof(TeamId)).Cast<TeamId>().ToList();

    // ObjectManager
    public static List<Champion> GetAllChampions() => Game.ObjectManager.GetAllChampions();
    public static Dictionary<uint, GameObject> GetGameObjects() => Game.ObjectManager.GetObjects();
    public static GameObject GetGameObjectByNetId(uint netId) => Game.ObjectManager.GetObjectById(netId);
    public static void AddGameObject(GameObject obj) => Game.ObjectManager.AddObject(obj);
    public static void RemoveGameObject(GameObject obj) => Game.ObjectManager.RemoveObject(obj);

    // MapHandler
    public static NavigationGrid MapNavGrid => Game.Map.NavigationGrid;
    public static MapData MapData => Game.Map.MapData;
    public static IGameModeScript MapGameMode => Game.Map.GameMode;
}
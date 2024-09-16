using GameServerCore.Enums;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.Scripting.CSharp;
using System.Collections.Generic;

namespace MapScripts.GameModes;

public interface IGameModeScript
{
    MapScriptMetadata MapScriptMetadata { get; }
    public Dictionary<ObjectType, List<MapObject>> MapObjects { get; set; }

    void OnLevelLoad()
    {
    }

    void OnMatchPreStart()
    {
    }

    void OnMatchStart()
    {
    }

    void OnUpdate()
    {
    }

    //Other callbacks?
}
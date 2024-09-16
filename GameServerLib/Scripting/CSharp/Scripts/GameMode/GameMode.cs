using GameServerCore.Enums;
using Chronobreak.GameServer.API;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.Scripting.CSharp;
using System.Collections.Generic;

namespace MapScripts.GameModes;

public class DefaultGamemode : IGameModeScript
{
    public Dictionary<ObjectType, List<MapObject>> MapObjects { get; set; } = [];
    public virtual MapScriptMetadata MapScriptMetadata { get; } = new();

    public ILevelScript? LevelScript;
    public virtual void OnLevelLoad()
    {
        LevelScript = ApiMapFunctionManager.GetLevelScript();
    }

    public virtual void OnMatchPreStart()
    {
    }

    public virtual void OnMatchStart()
    {
    }

    public virtual void OnUpdate()
    {
    }

    //Other callbacks?
}
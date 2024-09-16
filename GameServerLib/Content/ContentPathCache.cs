using System.Collections.Generic;

namespace GameServerLib.Content;

public class ContentPathCache
{
    public Dictionary<string, string> INIData { get; } = [];
    public Dictionary<string, string> LuaScripts { get; } = [];
    public Dictionary<string, string> PreloadScripts { get; } = [];
}
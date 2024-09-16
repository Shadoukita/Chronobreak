using System;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.Scripting.CSharp;
using static GameServerCore.Content.HashFunctions;

namespace Chronobreak.GameServer.GameObjects;

public class Talent
{
    public string Id { get; }
    public byte Rank { get; }
    public ITalentScript Script { get; } = null!;
    public TalentData TalentData { get; } = null!;
    public uint ScriptNameHash { get; private set; }
    public IEventSource? ParentScript => null;

    public static Talent EmptyTalent => new();

    private Talent()
    {
        Id = "";
        Rank = 0;
    }

    public Talent(TalentData data, byte level)
    {
        Id = data.Id;
        Rank = Math.Min(level, data.MaxLevel);
        Script = Game.ScriptEngine.CreateObject<ITalentScript>("Talents", $"Talent_{Id}", Game.Config.SupressScriptNotFound) ?? new TalentScript();
        ScriptNameHash = HashString(Id.ToString());
    }
}
using MoonSharp.Interpreter;

namespace Chronobreak.GameServer.Scripting.Lua;

public interface IBBMetadata
{
    public void Parse(Table dynValue);
}
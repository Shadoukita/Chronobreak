
using Chronobreak.GameServer.Scripting.CSharp;

namespace Chronobreak.GameServer.Scripting.Lua;

public class BBBuffScriptEmpty : BuffScript
{
    private readonly BuffScriptMetaData _metadata;
    public override BuffScriptMetaData BuffMetaData => _metadata;

    public BBBuffScriptEmpty()
    {
        _metadata = Functions.NextBBBuffScriptCtrArgs ?? base.BuffMetaData;
    }
}
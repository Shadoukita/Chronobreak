
using System;

namespace Chronobreak.GameServer.Scripting.Lua;

[AttributeUsage(AttributeTargets.Parameter)]
public class BBParamAttribute : Attribute
{
    public string? VarPostfix = "Var";
    public string? VarTablePostfix = "VarTable";
    public string? ValuePostfix = "";
    public string? ValueByLevelPostfix = "ByLevel";
    public BBParamAttribute()
    {
    }
    public BBParamAttribute(
        string? varPostfix,
        string? varTablePostfix,
        string? valuePostfix,
        string? valueByLevelPostfix
    )
    {
        VarPostfix = varPostfix;
        VarTablePostfix = varTablePostfix;
        ValuePostfix = valuePostfix;
        ValueByLevelPostfix = valueByLevelPostfix;
    }
}

[AttributeUsage(AttributeTargets.Parameter)]
public class BBSubBlocksAttribute : Attribute
{
    public string[] ParamNames;
    public BBSubBlocksAttribute(params string[] paramNames)
    {
        ParamNames = paramNames;
    }
}

[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
public class BBSpellNameAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
public class BBBuffNameAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Method)]
public class BBFuncAttribute : Attribute
{
    public string Dest = "Dest";
}

[AttributeUsage(AttributeTargets.Method)]
public class BBCallAttribute : Attribute
{
    public string Name;
    public object? DefaultReturnValue = null;
    public BBCallAttribute(string name, object? defaultReturn = null)
    {
        Name = name;
        DefaultReturnValue = defaultReturn;
    }
}

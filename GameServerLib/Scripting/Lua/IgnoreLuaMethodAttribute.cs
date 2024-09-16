using System;

namespace GameServerLib.Scripting.Lua;

/// <summary>
/// Specifies that a certain method should not be taken in account for the Lua context table globals
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
internal class IgnoreLuaMethodAttribute : Attribute
{
}
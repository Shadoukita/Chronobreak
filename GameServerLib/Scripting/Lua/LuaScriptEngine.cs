
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using GameServerCore.Enums;
using GameServerLib.Scripting.Lua;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects;
using Chronobreak.GameServer.Logging;
using log4net;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;

namespace Chronobreak.GameServer.Scripting.Lua;

public static class ScriptExtensions
{
    public static DynValue MakeClosure(this Script script, int address, Table? envTable = null)
    {
        return (DynValue)
            script.GetType()
                .GetMethod("MakeClosure", BindingFlags.NonPublic | BindingFlags.Instance)!
                .Invoke(script, new object?[] { address, envTable })!;
    }
}

[MoonSharpModule]
internal static class LuaScriptEngine
{
    private const string _BB_LUA = ".bb.lua";
    private const string _PRELOAD_BB_LUA = ".preload.bb.lua";
    private static readonly ILog _logger = LoggerProvider.GetLogger();

    private static readonly Script Script = new(CoreModules.Preset_Default);
    private static readonly Table MetaTableReferringToGlobal;
    private static readonly BBParamAttribute _defaultBBParamAttribute = new();

    private static readonly Dictionary<string, string> Files = [];
    private static readonly Dictionary<string, ScriptCacheInfo> Cache = [];

    private static readonly Dictionary<string, BBCacheEntry> BBCache = [];

    private static readonly Closure ExecuteBuildingBlocks;

    static LuaScriptEngine()
    {
        FindScriptFiles(Path.Combine(ContentManager.MapPath, "Scripts"));
        FindScriptFiles(Path.Combine(ContentManager.ContentPath, "DATA/Scripts"));
        FindScriptFiles(Path.Combine(ContentManager.ContentPath, "DATA/Shared/Scripts"));
        FindScriptFiles(ContentManager.CharactersPath);
        FindScriptFiles(ContentManager.TalentsPath);
        FindScriptFiles(ContentManager.ItemsPath);
        FindScriptFiles(ContentManager.SpellsPath);
        FindScriptFiles(ContentManager.SpellsSharedPath);

        var globals = Script.Globals.RegisterModuleType(typeof(LuaScriptEngine));
        RegisterGlobals(globals);
        RegisterHelperMethods(globals);

        var knownUnknowns = new HashSet<string>();
        globals.MetaTable = new Table(Script)
        {
            ["__index"] = (Table table, string key) =>
            {
                if (!knownUnknowns.Contains(key))
                {
                    knownUnknowns.Add(key);
                    _logger.Error($"An attempt was made to access an uninitialized global variable \"{key}\"");
                }
            }
        };

        MetaTableReferringToGlobal = new Table(Script)
        {
            ["__index"] = globals
        };

        UserData.RegistrationPolicy = InteropRegistrationPolicy.Automatic;
        UserData.DefaultAccessMode = InteropAccessMode.HideMembers;

        //TODO: move to LVector3
        UserData.RegisterType<LVector3>(InteropAccessMode.Preoptimized);
        Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion(
            typeof(Vector3),
            (script, vec) => UserData.Create(
                new LVector3((Vector3)vec)
            )
        );
        Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Number, typeof(bool), value =>
        {
            var num = value.CastToNumber();
            return num is 1;
        });
        Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(
            DataType.UserData, typeof(Vector3), DynValToVector3
        );
        //HACK: MoonSharp does not support nullables very well
        Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(
            DataType.UserData, typeof(Vector3?), DynValToVector3
        );

        //These two below are for converting river regions for Map11 RiverCrab from LVector3 to Vector2
        Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(
            DataType.UserData, typeof(Vector2), DynValToVector2
        );
        Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(
            DataType.UserData, typeof(Vector2?), DynValToVector2
        );

        object? DynValToVector3(DynValue dynval)
        {
            if (dynval.UserData.Object is LVector3 lv) return (Vector3)lv;
            //HACK: Scripts often pass objects where position is implied.
            //HACK: Union<T1, T2> is used for the same purposes.
            if (dynval.UserData.Object is GameObject o) return o.Position3D;
            return null;
        }

        object? DynValToVector2(DynValue dynval)
        {
            if (dynval.UserData.Object is LVector3 lv) return new Vector2(lv.x, lv.z);
            if (dynval.UserData.Object is GameObject o) return o.Position;
            if (dynval.UserData.Object is Vector2 v) return v;
            return Vector2.Zero;
        }

        Constants.RegisterConstants(globals);

        DoScript("BuildingBlocksBase.opt.lua", globals);
        ExecuteBuildingBlocks = globals.Get("ExecuteBuildingBlocks").Function;
    }

    [MoonSharpModuleMethod]
    public static DynValue LoadScript(ScriptExecutionContext executionContext, CallbackArguments args)
    {
        return DoLua(nameof(LoadScript), executionContext, args);
    }

    [MoonSharpModuleMethod]
    public static DynValue LoadLevelScriptIntoScript(ScriptExecutionContext executionContext, CallbackArguments args)
    {
        return DoLua(nameof(LoadLevelScriptIntoScript), executionContext, args);
    }

    [MoonSharpModuleMethod]
    public static DynValue DoLuaLevel(ScriptExecutionContext executionContext, CallbackArguments args)
    {
        return DoLua(nameof(DoLuaLevel), executionContext, args);
    }

    [MoonSharpModuleMethod]
    public static DynValue DoLuaShared(ScriptExecutionContext executionContext, CallbackArguments args)
    {
        return DoLua(nameof(DoLuaShared), executionContext, args);
    }

    [MoonSharpModuleMethod]
    public static DynValue InitTimer(ScriptExecutionContext executionContext, CallbackArguments args)
    {
        var funcname = nameof(InitTimer);
        var callback = args.AsType(0, funcname, DataType.String).String;
        var duration = (float)args.AsType(1, funcname, DataType.Number).Number;
        var repeat = args.AsType(2, funcname, DataType.Boolean).Boolean;
        var callbackFunction = executionContext.CurrentGlobalEnv.Get(callback).Function;

        Functions.LevelTimers.Add(new()
        {
            Name = callback,
            Delay = duration,
            Elapsed = 0.0f,
            Repeat = repeat,
            Enabled = true,
            Callback = () => callbackFunction.Call()
        });

        return DynValue.Nil;
    }

    [MoonSharpModuleMethod]
    public static DynValue LuaForEachChampion(ScriptExecutionContext executionContext, CallbackArguments args)
    {
        var functionName = nameof(LuaForEachChampion);
        var team = (TeamId)args.AsInt(0, functionName);
        var callback = args.AsType(1, functionName, DataType.String).String;
        var callBackFunction = executionContext.CurrentGlobalEnv.Get(callback).Function;

        foreach (var champ in Game.ObjectManager.GetAllChampionsFromTeam(team)) callBackFunction.Call(champ);

        return DynValue.Nil;
    }

    private static DynValue DoLua(string funcname, ScriptExecutionContext executionContext, CallbackArguments args)
    {
        var filename = args.AsType(0, funcname, DataType.String).String;
        DoScript(filename, executionContext.CurrentGlobalEnv);
        return DynValue.Nil;
    }

    private static string? ConcatWithNull(string first, string? second)
    {
        return second != null ? first + second : null;
    }

    /// <summary>
    /// Some scripts use the ipairs function which should be a internal Lua function but not available in MoonSharp, so we add it.
    /// </summary>
    private static void RegisterHelperMethods(Table globals)
    {
        Script.DoString(@"function ipairs(t)
                            local function iter(tbl, i)
                            i = i + 1
                            if tbl[i] ~= nil then
                                return i, tbl[i]
                            end
                        end

                        return iter, t, 0
                       end", globals);
    }

    private static void RegisterGlobals(Table globals)
    {
        foreach (var mInfo in typeof(FunctionsLua).GetMethods(BindingFlags.Public | BindingFlags.Static))
        {
            globals[mInfo.Name] = mInfo;
            var fAttr = mInfo.GetCustomAttribute<BBFuncAttribute>();
            if (fAttr != null)
            {
                var parameters = mInfo.GetParameters();
                globals["BB" + mInfo.Name] = (Table pt, Table pb, Table? sub) =>
                {
                    Unnamed(pt, pb, sub, mInfo, fAttr, parameters);
                };
            }
        }

        var proxyMethods = FunctionsLua.GenerateDynamicProxies();
        foreach (var mInfo in proxyMethods)
        {
            globals[mInfo.Name] = mInfo;
        }

        foreach (var mInfo in typeof(Functions).GetMethods(BindingFlags.Public | BindingFlags.Static).Concat(typeof(Functions_BBB_and_CS).GetMethods(BindingFlags.Public | BindingFlags.Static)))
            //We prioritize the proxy lua functions first
            if (globals[mInfo.Name] == null)
            {
                globals[mInfo.Name] = mInfo;
                var fAttr = mInfo.GetCustomAttribute<BBFuncAttribute>();
                if (fAttr != null)
                {
                    var parameters = mInfo.GetParameters();
                    globals["BB" + mInfo.Name] = (Table pt, Table pb, Table? sub) =>
                    {
                        Unnamed(pt, pb, sub, mInfo, fAttr, parameters);
                    };
                }
            }
    }

    private static string UCFirst(string str)
    {
        return char.ToUpperInvariant(str[0]) + str[1..];
    }

    private static bool IsNullable(ParameterInfo pInfo)
    {
        return
            // Attributes are not assigned to objects and they are not wrapped in Nullable...
            !pInfo.ParameterType.IsValueType || // ...so it is not possible to know for sure at runtime
            Nullable.GetUnderlyingType(pInfo.ParameterType) != null ||
            pInfo.CustomAttributes.Any(x => x.AttributeType.Name == "NullableAttribute");
    }

    private static object WrapInAction(int paramCount, Action<object?[]> action)
    {
        return paramCount switch
        {
            0 => () => action(Array.Empty<object?>()),
            1 => (object? a) => action(new[] { a }),
            2 => (object? a, object? b) => action(new[] { a, b }),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    //TODO: *reactive* ref & out?
    public static void Unnamed(Table pt, Table pb, Table? sub, MethodInfo mInfo, BBFuncAttribute fAttr,
        ParameterInfo[] parameters)
    {
        var args = new object?[parameters.Length];
        var prevArgs = args; //HACK: to bypass the unassigned error 

        for (var i = 0; i < parameters.Length; i++)
        {
            var pInfo = parameters[i];
            if (pInfo.IsOut)
                // We don't need its initial value.
                continue;

            var sAttr = parameters[i].GetCustomAttribute<BBSubBlocksAttribute>();
            if (sAttr != null)
            {
                if (sub != null)
                    args[i] = WrapInAction(sAttr.ParamNames.Length, p =>
                    {
                        for (var i = 0; i < sAttr.ParamNames.Length; i++)
                        {
                            SetVar(pt, pb, sAttr.ParamNames[i], p[i]);
                            ExecBB(sub, pt, $"{mInfo.Name}({pInfo.Name})");
                        }
                    });
                else
                    args[i] = WrapInAction(sAttr.ParamNames.Length, p => { });
                continue;
            }

            var pAttr = pInfo.GetCustomAttribute<BBParamAttribute>() ?? _defaultBBParamAttribute;
            var pName = UCFirst(pInfo.Name!);
            var dynValue = Resolve
            (
                pt, pb,
                ConcatWithNull(pName, pAttr.VarPostfix),
                ConcatWithNull(pName, pAttr.VarTablePostfix),
                ConcatWithNull(pName, pAttr.ValuePostfix),
                ConcatWithNull(pName, pAttr.ValueByLevelPostfix)
            );
            args[i] = DynValueToObject(
                dynValue, pInfo.ParameterType,
                pName, mInfo.Name,
                pInfo.DefaultValue, IsNullable(pInfo)
            );
        }

        // Might be useful for debugging
        /*
        Console.WriteLine(mInfo.Name);
        Console.WriteLine("(");
        for (int i = 0; i < parameters.Length; i++)
        {
            var pInfo = parameters[i];
            var a = args[i];
            string? v;
            if(a == null)
            {
                v = "null";
            }
            else
            {
                v = a.ToString();
                if(pInfo.ParameterType == typeof(string))
                {
                    v = '"' + v + '"';
                }
            }
            Console.WriteLine($"    {pInfo.Name} = {v}");
        }
        Console.WriteLine(")");
        */

        prevArgs = (object?[])args.Clone();
        var ret = mInfo.Invoke(null, args);
        if (ret != null) SetVar(pt, pb, fAttr.Dest, ret);
        for (var i = 0; i < parameters.Length; i++)
        {
            ret = args[i];
            if (prevArgs[i] != ret)
            {
                prevArgs[i] = ret;
                var pInfo = parameters[i];
                var pName = UCFirst(pInfo.Name!);
                SetVar(pt, pb, pName, ret);
            }
        }
    }

    public static object? DynValueToObject(
        DynValue? dynValue, Type pType,
        string pName, string mName,
        object? defaultValue, bool isNullable
    )
    {
        object? value;
        if (dynValue == null || dynValue.IsNil())
        {
            if (defaultValue != DBNull.Value)
                value = defaultValue;
            else if (isNullable)
                value = null;
            else
                throw new ArgumentException($"{pName} of {mName} is not defined");
        }
        else
        {
            try
            {
                //TODO: Optimize double Nullable.GetUnderlyingType call
                pType = Nullable.GetUnderlyingType(pType) ?? pType;
                if (pType.IsEnum)
                {
                    //TODO: Custom MoonSharp converter?
                    //TODO: out EnumFlags support?
                    if (dynValue.Type == DataType.String)
                        value = Enum.Parse(pType, dynValue.String.Trim().Replace(' ', ','));
                    else if (dynValue.Type == DataType.Number)
                        value = Enum.ToObject(pType, (long)dynValue.Number);
                    else if (dynValue.Type == DataType.UserData)
                        value = Enum.ToObject(pType, dynValue.UserData.Object);
                    else
                        throw null!;
                }
                else if (Union.IsUnion(pType))
                {
                    value = Union.ToObject(pType, dynValue);
                }
                else
                {
                    value = dynValue.ToObject(pType);
                }
            }
            catch (Exception? e)
            {
                throw new ArgumentException
                (
                    $"{pName} of {mName} cannot be converted from {dynValue.Type} to {pType}",
                    e
                );
            }
        }

        return value;
    }

    private static void SetVar(Table pt, Table pb, string dest, object? ret)
    {
        var destVarNameName = dest + "Var";
        var destVarName = pb.RawGet(destVarNameName)?.String;
        if (destVarName != null)
        {
            var destTable = pt;
            var destTableNameName = dest + "VarTable";
            var destTableName = pb.RawGet(destTableNameName)?.String;
            if (destTableName != null) destTable = pt.RawGet(destTableName)?.Table ?? destTable;

            //Console.WriteLine($"{destTableName}[{destVarName}] = {ret}");

            destTable[destVarName] = ret;
        }
    }

    private static void SumOrOverride(ref DynValue? var, DynValue? with)
    {
        if (
            var != null && var.Type == DataType.Number &&
            with != null && with.Type == DataType.Number
        )
            var = DynValue.NewNumber(var.Number + with.Number);
        else
            var = with;
    }

    public static DynValue? Resolve(Table vars, Table args, string? varNameName = null, string? varTableNameName = null,
        string? valueName = null, string? valueByLevelName = null)
    {
        DynValue? value = null;
        if (valueName != null) value = args.RawGet(valueName);
        if (valueByLevelName != null)
        {
            var level = vars.RawGet("Level")?.Number;
            var valueByLevel = args.RawGet(valueByLevelName)?.Table;
            if (level != null && valueByLevel != null)
                SumOrOverride(ref value, valueByLevel.RawGet((int)level));
        }

        if (varNameName != null)
        {
            var varName = args.RawGet(varNameName)?.String;
            if (varName != null)
            {
                var varTable = vars;
                if (varTableNameName != null)
                {
                    var varTableName = args.RawGet(varTableNameName)?.String;
                    if (varTableName != null) varTable = vars.RawGet(varTableName)?.Table; // ?? vars;
                }

                SumOrOverride(ref value, varTable?.RawGet(varName));
            }
        }

        return value;
    }

    private static void FindScriptFiles(string folder)
    {
        if (!Directory.Exists(folder))
        {
            return;
        }
        foreach (var filepath in Directory.EnumerateFiles(folder, "*", SearchOption.AllDirectories))
        {
            if (filepath.EndsWith(".lua") || filepath.EndsWith(".preload"))
            {
                var filename = Path.GetFileName(filepath).ToLowerInvariant();
                if (Files.TryGetValue(filename, out var existing) && !Game.Config.SupressScriptNotFound)
                {
                    _logger.Warn($"Filename Collision: {Path.GetFileName(existing)}");
                }
                Files[filename] = filepath;
            }
        }
    }

    internal static DynValue DoScript(string luaName, Table globals)
    {
        luaName = luaName.ToLowerInvariant();
        var found = false;
        var result = DynValue.Nil;

        lock (Cache)
        {
            if (Cache.TryGetValue(luaName, out ScriptCacheInfo? s))
            {
                if (s.PreloadAddress != -1)
                {
                    found = true;
                    result = Script.Call(Script.MakeClosure(s.PreloadAddress, globals));
                }

                if (s.LuaAddress != -1)
                {
                    found = true;
                    result = Script.Call(Script.MakeClosure(s.LuaAddress, globals));
                }

                if (!found)
                {
                    _logger.Error($"Lua script {luaName} could not be found");
                }
            }
            else
            {
                Cache[luaName] = s = new ScriptCacheInfo();

                if (luaName.EndsWith(".preload"))
                {
                    var preloadName = luaName.Substring(0, luaName.Length) + _PRELOAD_BB_LUA;
                    if (Files.TryGetValue(luaName, out var preloadPath))
                    {
                        //Console.WriteLine($"{nameof(Script.LoadFile)}(\"{preloadPath}\", {nameof(globals)})");

                        found = true;
                        var c = Script.LoadFile(preloadPath, globals);
                        s.PreloadAddress = c.Function.EntryPointByteCodeLocation;
                        return Script.Call(c);
                    }
                }

                if (Files.TryGetValue(luaName, out var luaPath))
                {
                    //Console.WriteLine($"{nameof(Script.LoadFile)}(\"{luaPath}\", {nameof(globals)})");

                    found = true;
                    DynValue? c;
                    try
                    {
                        c = Script.LoadFile(luaPath, globals);
                    }
                    catch (SyntaxErrorException error)
                    {
                        _logger.Error(error.DecoratedMessage);
                        throw error;
                    }

                    s.LuaAddress = c.Function.EntryPointByteCodeLocation;
                    result = Script.Call(c);
                }

                if (!found && !Game.Config.SupressScriptNotFound)
                {
                    _logger.Warn($"Lua script {luaName} could not be found");
                }
            }
        }

        return result;
    }

    internal static DynValue DoLuaString(string luaCode, Table globals)
    {
        var result = Script.DoString(luaCode, globals);
        return result;
    }

    public static BBCacheEntry GetBBCacheEntry(string bbname)
    {
        bbname = bbname.ToLowerInvariant();

        if (!BBCache.TryGetValue(bbname, out var bbEntry))
        {
            var bbGlobals = new Table(Script);
            bbGlobals.MetaTable = MetaTableReferringToGlobal;
            BBCache[bbname] = bbEntry = new BBCacheEntry(bbGlobals);

            _logger.Info($"{nameof(DoScript)}(\"{bbname + _BB_LUA}\", {nameof(bbGlobals)})");

            DoScript(bbname + _BB_LUA, bbGlobals);
        }

        return bbEntry;
    }

    public static DynValue? ExecBB(Table blocks, Table passThrough, string? funcname)
    {
        try
        {
            ExecuteBuildingBlocks.Call(blocks, passThrough);
            var ret = passThrough.RawGet("ReturnValue");
            passThrough.Remove("ReturnValue");
            return ret;
        }
        catch (Exception ex)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Script: ");
            stringBuilder.AppendLine(BBScript.CurrentlyExecuted?.Args.Name);
            stringBuilder.Append("Block: ");
            stringBuilder.AppendLine(Script.Globals.RawGet("gCurrentBuildingBlockString")?.String);
            if (ex is ScriptRuntimeException sre)
            {
                stringBuilder.Append("Message: ");
                stringBuilder.AppendLine(sre.DecoratedMessage);
                stringBuilder.AppendLine("Stack trace:");
                foreach (var watchItem in sre.CallStack) stringBuilder.AppendLine(watchItem.ToString());
            }

            _logger.Error(stringBuilder, ex);

            return null;
        }
    }

    public static Table NewTable()
    {
        return new Table(Script);
    }

    public static Table CreateTableReferringGlobal()
    {
        return new Table(Script)
        {
            MetaTable = MetaTableReferringToGlobal
        };
    }

    public static bool HasBBScript(string name)
    {
        return HasLuaScript(name + _BB_LUA);
    }

    public static bool HasLuaScript(string name)
    {
        name = name.ToLowerInvariant();
        return Files.ContainsKey(name);
    }
    private class ScriptCacheInfo
    {
        public int LuaAddress = -1;
        public int PreloadAddress = -1;
    }
}
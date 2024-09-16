
using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;

namespace Chronobreak.GameServer.Scripting.Lua;
public class BBCacheEntry
{
    public Table Globals; //TODO: Readonly Table
    public bool Preloaded = false;
    private Dictionary<Type, object> Metadata = [];
    public BBCacheEntry(Table globals)
    {
        Globals = globals;
    }
    public T GetMetadata<T>() where T : IBBMetadata
    {
        var type = typeof(T);
        var metadata = (IBBMetadata?)Metadata.GetValueOrDefault(type);
        if (metadata == null)
        {
            metadata = Activator.CreateInstance<T>()!;
            metadata.Parse(Globals);

            Metadata[type] = metadata;
        }
        return (T)metadata;
    }
    //TODO: Use RegExes?
    //TODO: Move to a helper class?
    public static T?[] ReadArray<T>(Table globals, string name, T? defaultValue)
    {
        var list = new List<T?>();
        foreach (var global in globals.Pairs)
        {
            var key = global.Key.String!;
            var value = global.Value;
            if (key.StartsWith(name))
            {
                int i = 0;
                if (key.Length > name.Length)
                {
                    i = int.Parse(key.Substring(name.Length));
                }
                while (list.Count <= i)
                {
                    list.Add(defaultValue);
                }
                try
                {
                    list[i] = value.ToObject<T>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        return list.ToArray();
    }
    /*
    public static object?[] ReadArray(Table globals, Type type, string name)
    {
        var list = new List<object?>();
        foreach(var global in globals.Pairs)
        {
            var key = global.Key.String!;
            var value = global.Value;
            if(key.StartsWith(name))
            {
                int i = 0;
                if(key.Length > name.Length)
                {
                    i = int.Parse(key.Substring(name.Length));
                }
                while(list.Count <= i)
                {
                    list.Add(null);
                }
                list[i] = value.ToObject(type);
            }
        }
        return list.ToArray();
    }
    */
}
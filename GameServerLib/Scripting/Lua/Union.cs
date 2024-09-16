
using System;
using MoonSharp.Interpreter;

namespace Chronobreak.GameServer.Scripting.Lua;

public class Union
{
    public static bool IsUnion(Type type)
    {
        return type.IsGenericType && type.IsSubclassOf(typeof(Union));
    }
    public static object? ToObject(Type type, object value)
    {
        return Activator.CreateInstance(type, value);
    }
}

public class Union<T1, T2> : Union
{
    private T1? item1;
    private T2? item2;
    public T1? Item1 => item1;
    public T2? Item2 => item2;
    public T? Get<T>() where T : T1, T2
    {
        if (typeof(T) == typeof(T1))
        {
            return (T?)Item1;
        }
        else //if(typeof(T) == typeof(T2))
        {
            return (T?)Item2;
        }
    }
    public Union(T1 item) { item1 = item; }
    public Union(T2 item) { item2 = item; }
    public Union(DynValue dynValue)
    {
        if (TryCast<T1>(dynValue, out var item1))
        {
            this.item1 = item1;
        }
        else if (TryCast<T2>(dynValue, out var item2))
        {
            this.item2 = item2;
        }
        else
        {
            throw new ArgumentException();
        }
    }
    public static implicit operator Union<T1, T2>(T1 item) => new(item);
    public static implicit operator Union<T1, T2>(T2 item) => new(item);
    public static explicit operator T1?(Union<T1, T2> union) => union.Item1;
    public static explicit operator T2?(Union<T1, T2> union) => union.Item2;
    private static bool TryCast<T>(DynValue dynValue, out T? result)
    {
        try
        {
            result = dynValue.ToObject<T>();
            return true;
        }
        catch (ScriptRuntimeException)
        {
            result = default;
            return false;
        }
    }
    public override string? ToString()
    {
        return Item1?.ToString() ?? Item2?.ToString();
    }
}
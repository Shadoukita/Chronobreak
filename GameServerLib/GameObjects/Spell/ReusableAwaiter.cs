
using System;
using System.Runtime.CompilerServices;

namespace Chronobreak.GameServer.GameObjects.SpellNS;

// Based on https://stackoverflow.com/a/40689207
class ReusableAwaiter<T> : INotifyCompletion
{
    private T? _result;
    private Action? _continuation;
    public bool IsCompleted => false;

    public ReusableAwaiter()
    {
        Reset();
    }
    private ReusableAwaiter<T> Reset()
    {
        _result = default(T);
        _continuation = null;
        return this;
    }
    public ReusableAwaiter<T> GetAwaiter()
    {
        return this;
    }
    public void OnCompleted(Action continuation)
    {
        if (_continuation != null)
            throw new Exception();
        _continuation = continuation;
    }
    public void SetResult(T result)
    {
        _result = result;
        var c = _continuation;
        if (c != null)
            c();
    }
    public T? GetResult()
    {
        var r = _result;
        Reset();
        return r;
    }
}
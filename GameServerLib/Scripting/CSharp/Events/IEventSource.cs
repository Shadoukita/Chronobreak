namespace Chronobreak.GameServer.Scripting.CSharp
{
    public interface IEventSource
    {
        uint ScriptNameHash { get; }
        IEventSource? ParentScript { get; }
    }
}
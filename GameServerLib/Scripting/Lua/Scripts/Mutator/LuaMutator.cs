using MoonSharp.Interpreter;

namespace MapScripts.Mutators;

public class LuaMutator : IMutatorScript
{
    internal Table? Script;

    public void OnInitClient()
    {
        Script?.Get("OnInitClient")?.Function?.Call();
    }
    public void OnInitServer()
    {
        Script?.Get("OnInitServer")?.Function?.Call();
    }
    public void IntroBotStarterBuffsOnInit()
    {
        Script?.Get("IntroBotStarterBuffsOnInit")?.Function?.Call();
    }
    public void OnInit()
    {
        Script?.Get("OnInit")?.Function?.Call();
    }
}
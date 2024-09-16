namespace MapScripts.Mutators;

public interface IMutatorScript
{
    void OnInitClient() { }
    void OnInitServer() { }
    void IntroBotStarterBuffsOnInit() { }
    void OnInit() { }
}
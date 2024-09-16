namespace AIScripts;

using GameServerCore.Enums;

using Chronobreak.GameServer.Scripting.CSharp.Converted;

//Status: 100% Identical to Lua script
public class IdleAI: CAIScript
{
    public override bool OnInit()
    {
        SetState(AIState.AI_HARDIDLE);
        return false;
    }
}
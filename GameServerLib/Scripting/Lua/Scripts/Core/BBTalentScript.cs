
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.Scripting.CSharp;

namespace Chronobreak.GameServer.Scripting.Lua;

public class BBTalentScript : BBScriptInstance, ITalentScript
{
    //Talent scripts are just char scripts in disguise
    protected override BBScriptType ScriptType => BBScriptType.Char;

    public BBTalentScript(BBScriptCtrReqArgs args) : base(args)
    {
        _bb = new BBScript(this, args);
    }
    protected override void SetTypeDependentArgs()
    {
        _bb.PassThrough["Attacker"] = attacker ?? owner;
        _bb.PassThrough["Target"] = target ?? owner;
        _bb.PassThrough["Owner"] = target ?? owner;
        _bb.PassThrough["Caster"] = target ?? owner;
    }

    public void PreLoad()
    {
        SetTypeDependentArgs();
        _bb.Call("PreLoad");
    }

    public void OnActivate(ObjAIBase owner, byte rank)
    {
        _bb.PassThrough["TalentLevel"] = rank;
        base.Activate();
    }
    //TODO:
    public void Deactivate()
    {
        base.OnDeactivate(true);
    }

    public override void OnUpdate()
    {
        //TODO: Talent script tickrate?
        UpdateSelfBuffStats();
        UpdateSelfBuffActions();
    }
    public void UpdateSelfBuffActions()
    {
        SetTypeDependentArgs();
        _bb.Call("UpdateSelfBuffActions");
    }
    public void UpdateSelfBuffStats()
    {
        SetTypeDependentArgs();
        _bb.Call("UpdateSelfBuffStats");
    }

    // At that moment, I realized that the talent is CharScript, judging by the callbacks
}
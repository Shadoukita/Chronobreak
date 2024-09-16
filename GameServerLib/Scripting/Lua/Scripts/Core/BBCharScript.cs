
using Chronobreak.GameServer.API;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.Scripting.CSharp;

namespace Chronobreak.GameServer.Scripting.Lua;

public class BBCharScript : BBScriptInstance, ICharScript
{
    public BBCharScript(BBScriptCtrReqArgs args) : base(args)
    {
        _bb = new BBScript(this, args);
    }

    protected override BBScriptType ScriptType => BBScriptType.Char;

    public void Init(ObjAIBase owner, Spell spell)
    {
        base.Init(owner, owner, spell);
    }

    public void OnActivate()
    {
        Activate();
    }

    public void OnDeactivate()
    {
        base.OnDeactivate(true);
        ApiEventManager.RemoveAllListenersForOwner(this);
    }

    public override void OnUpdate()
    {
        UpdateSelfBuffStats();
        UpdateSelfBuffActions();
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
        _bb.SetArgs();
        _bb.Call("PreLoad");
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

    public override void OnDodge(AttackableUnit attacker)
    {
        _bb.PassThrough["Attacker"] = attacker;
        _bb.Call("CharOnDodge");
    }

    public override void OnNearbyDeath(ObjAIBase attacker, AttackableUnit target)
    {
        _bb.PassThrough["Attacker"] = attacker;
        _bb.PassThrough["Target"] = target;
        _bb.Call("CharOnNearbyDeath");
    }

    public void SetVarsByLevel()
    {
        SetTypeDependentArgs();
        _bb.Call("SetVarsByLevel");
    }
}
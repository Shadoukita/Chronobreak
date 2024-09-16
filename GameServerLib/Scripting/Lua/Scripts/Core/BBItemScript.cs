
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.GameObjects.SpellNS.Missile;
using Chronobreak.GameServer.Scripting.CSharp;

namespace Chronobreak.GameServer.Scripting.Lua;

public class BBItemScript : BBScriptInstance, IItemScript
{
    public BBItemScript(BBScriptCtrReqArgs args) : base(args)
    {
        _bb = new BBScript(this, args);
    }

    protected override BBScriptType ScriptType => BBScriptType.Item;

    public void OnActivate(ObjAIBase owner)
    {
        Activate();
    }

    public void OnDeactivate(ObjAIBase owner)
    {
        base.OnDeactivate(true);
    }

    public override void OnUpdate()
    {
        UpdateSelfBuffStats();
        UpdateSelfBuffActions();
    }

    protected override void SetTypeDependentArgs()
    {
        SetArgs();
    }

    private void SetArgs(
        DamageData? damageData = null,
        DeathData? deathData = null,
        Spell? spell = null,
        SpellMissile? missile = null,
        AttackableUnit? attacker = null,
        AttackableUnit? target = null
    )
    {
        _bb.PassThrough["Slot"] = 0; //TODO:
        _bb.PassThrough["Attacker"] = attacker;
        _bb.PassThrough["Target"] = target;

        _bb.SetArgs(damageData, deathData, spell, missile);
    }

    public void PreLoad()
    {
        SetArgs();
        _bb.Call("PreLoad");
    }

    public void UpdateSelfBuffActions()
    {
        SetArgs();
        _bb.Call("UpdateSelfBuffActions");
    }

    public void UpdateSelfBuffStats()
    {
        SetArgs();
        _bb.Call("UpdateSelfBuffStats");
    }
}
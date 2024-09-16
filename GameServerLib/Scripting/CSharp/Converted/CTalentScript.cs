
using Chronobreak.GameServer.API;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

namespace Chronobreak.GameServer.Scripting.CSharp.Converted;

public class CTalentScript : CScript, ITalentScript
{
    protected int talentLevel => _level;
    protected new ObjAIBase owner => _owner; //TODO:

    private int _level;
    private ObjAIBase _owner = null!; //TODO: Champion?
    public void OnActivate(ObjAIBase owner, byte rank)
    {
        base.Init(owner, owner, null); //TODO: Move to Init
        base.Activate();
        _owner = owner;
        _level = rank;
        ApiEventManager.OnLevelUp.AddListener(this, owner, unit => SetVarsByLevel());
    }

    public virtual void SetVarsByLevel() { }
}
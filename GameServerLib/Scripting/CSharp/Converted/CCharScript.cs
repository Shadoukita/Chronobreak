
using Chronobreak.GameServer.API;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.Scripting.Lua;

namespace Chronobreak.GameServer.Scripting.CSharp.Converted;

public class CCharScript : CScript, ICharScript
{
    protected int level
    {
        //TODO: Make a normal way to get the level
        get
        {
            return Functions.GetLevel(owner);
        }
    }

    protected new ObjAIBase owner => _owner;

    private ObjAIBase _owner = null!;
    public void Init(ObjAIBase owner, Spell spell)
    {
        base.Init(owner, owner, null);
        base.Activate(); //TODO: Move to Activate
        _owner = owner;
        //_spell = spell;
        ApiEventManager.OnLevelUp.AddListener(this, owner, unit => SetVarsByLevel()); //TODO: Move to Activate
    }

    public virtual void SetVarsByLevel() { }

    public virtual void OnActivate()
    {
    }

    public virtual void OnDeactivate()
    {
    }
}
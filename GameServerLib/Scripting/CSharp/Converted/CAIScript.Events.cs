
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

namespace Chronobreak.GameServer.Scripting.CSharp.Converted;

public partial class CAIScript
{
    public virtual bool OnInit() => false;
    public virtual void OnActivate(ObjAIBase owner) { }
    public virtual void OnStopMove() { }
    public virtual void OnPathToTargetBlocked() { }
    public virtual void OnFearBegin() { }
    public virtual void OnFearEnd() { }
    public virtual void OnFleeBegin() { }
    public virtual void OnFleeEnd() { }
    public virtual void OnTauntBegin() { }
    public virtual void OnTauntEnd() { }
    public virtual void OnCanMove() { }
    public virtual void OnCanAttack() { }
    public virtual void OnCharmBegin() { }
    public virtual void OnCharmEnd() { }
    public virtual void OnTakeDamage(AttackableUnit attacker) { }
    public virtual bool OnCollisionEnemy(AttackableUnit y) => false;
    public virtual bool OnCollisionOther(AttackableUnit y) => false;
    public virtual void OnReachedDestinationForGoingToLastLocation() { }
    public virtual void HaltAI() { }
    public virtual void OnUpdate() { }
}
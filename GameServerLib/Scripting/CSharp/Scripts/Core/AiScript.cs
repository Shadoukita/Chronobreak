using System.Numerics;
using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

namespace Chronobreak.GameServer.Scripting.CSharp
{
    public interface IAIScript
    {
        public AIScriptMetaData AIScriptMetaData { get; }

        internal void Init(ObjAIBase owner);
        internal void Activate();
        internal void HaltAI();
        internal void Deactivate(bool expired);
        internal void Update();
        internal bool OnOrder(OrderType orderType, AttackableUnit target, Vector2 position);
        internal void TargetLost(LostTargetEvent eventType, AttackableUnit target);
        internal void OnAICommand();
        internal void OnCollision(AttackableUnit target);
        internal void OnCollisionTerrain();
        internal void OnStoppedMoving();
        internal void OnCallForHelp(ObjAIBase attacker, ObjAIBase victim);
        internal void OnLeashedCallForHelp(ObjAIBase attacker, ObjAIBase victim);
        internal void OnReachedDestinationForGoingToLastLocation();
        internal void OnCanAttack();
        internal void OnCanMove();
        internal void OnStopMove();
        internal AttackableUnit? FindTargetInAcR();
    }

    public class AiScript : IAIScript
    {
        public ObjAIBase Owner { get; internal set; }

        public virtual AIScriptMetaData AIScriptMetaData { get; } = new();

        public virtual void Init(ObjAIBase owner)
        {
        }

        public virtual void Activate()
        {
        }

        public void HaltAI()
        {

        }

        public virtual void Deactivate(bool expired)
        {
        }

        public virtual void Update()
        {
        }

        public virtual bool OnOrder(OrderType orderType, AttackableUnit target, Vector2 position)
        {
            return false;
        }

        public virtual void TargetLost(LostTargetEvent eventType, AttackableUnit target)
        {

        }

        public virtual void OnAICommand()
        {

        }

        public virtual void OnCollision(AttackableUnit target)
        {

        }

        public virtual void OnCollisionTerrain()
        {

        }

        public virtual void OnStoppedMoving()
        {

        }

        public virtual void OnCallForHelp(ObjAIBase attacker, ObjAIBase victim)
        {

        }

        public virtual void OnLeashedCallForHelp(ObjAIBase attacker, ObjAIBase victim)
        {

        }

        public virtual void OnReachedDestinationForGoingToLastLocation()
        {

        }

        public virtual void OnCanAttack()
        {

        }

        public virtual void OnCanMove()
        {

        }

        public virtual void OnStopMove()
        {

        }

        public AttackableUnit? FindTargetInAcR()
        {
            return Owner.TargetAcquisition(GetPosition(), Owner.Stats.AcquisitionRange.Total + Owner.CollisionRadius);
        }


        public Vector2 GetPosition()
        {
            return Owner.Position;
        }
    }
}
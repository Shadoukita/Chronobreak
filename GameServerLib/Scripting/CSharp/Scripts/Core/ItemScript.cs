using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

namespace Chronobreak.GameServer.Scripting.CSharp
{
    internal interface IItemScript
    {
        internal void OnActivate(ObjAIBase owner) { }
        internal void OnDeactivate(ObjAIBase owner) { }
        internal void OnUpdate();
        internal void OnUpdateStats();
    }

    public class ItemScript : IItemScript
    {
        public virtual void OnActivate(ObjAIBase owner)
        {
        }
        public virtual void OnDeactivate(ObjAIBase owner)
        {
        }
        public virtual void OnUpdateStats()
        {
        }
        public virtual void OnUpdate()
        {
        }
    }
}

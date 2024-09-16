using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.SpellNS;

namespace Chronobreak.GameServer.Scripting.CSharp
{
    internal interface ICharScript
    {
        internal void Init(ObjAIBase owner, Spell spell);
        internal void OnActivate() { }
        internal void OnDeactivate() { }
        internal void OnUpdate() { }
        internal void OnUpdateStats() { }
    }
    public class CharScript : ICharScript
    {
        public ObjAIBase Owner { get; private set; }
        public Spell Spell { get; private set; }

        public void Init(ObjAIBase owner, Spell spell)
        {
            Owner = owner;
            Spell = spell;
        }

        public virtual void OnActivate()
        {
        }
        public virtual void OnDeactivate()
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

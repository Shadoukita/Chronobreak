using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

namespace Chronobreak.GameServer.Scripting.CSharp
{
    public interface ITalentScript
    {
        internal void OnActivate(ObjAIBase owner, byte rank) { }
        internal void OnUpdateStats() { }
    }
    public class TalentScript : ITalentScript
    {
        public virtual void OnActivate(ObjAIBase owner, byte rank)
        {
        }
        public virtual void OnUpdateStats()
        {
        }
    }
}
using System;
using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.SpellNS;

namespace Chronobreak.GameServer.Scripting.CSharp
{
    public interface IBuffScript : ICloneable
    {
        public BuffScriptMetadataUnmutable MetaData { get; }
        public BuffScriptMetaData BuffMetaData { get; }
        public string Name { get; }

        internal void Init(Buff buff);
        internal void Activate();
        internal void Deactivate(bool expired);
        internal void OnStackUpdate(int prevStack, int newStack);
        internal void OnUpdate();
        internal void OnUpdateStats();
        internal void OnActivate();
        internal void OnDeactivate();
        internal void OnDeactivate(bool expired);
        internal void UpdateStats();
        internal bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration);
    }
    public class BuffScript : IBuffScript
    {
        public Buff Buff { get; private set; }
        public AttackableUnit Target => Buff.TargetUnit;
        public ObjAIBase Owner => Buff.SourceUnit;
        public Spell Spell => Buff.OriginSpell;

        public virtual BuffScriptMetadataUnmutable MetaData { get; } = new();
        public virtual BuffScriptMetaData BuffMetaData { get; } = new();
        //TODO: Change?
        public string Name => this.GetType().Name;

        public void Init(Buff buff)
        {
            Buff = buff;
        }

        public void Activate()
        {
            OnActivate();
        }

        public virtual void OnUpdateStats() { }

        public virtual void OnActivate()
        {
        }

        public void Deactivate(bool expired)
        {
            OnDeactivate();
            OnDeactivate(expired);
        }
        public virtual void OnDeactivate()
        {
        }
        public virtual void OnDeactivate(bool expired)
        {
        }
        public virtual void OnStackUpdate(int prevStack, int newStack)
        {
        }
        public virtual void UpdateStats()
        {
        }

        public bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            return true;
        }

        public virtual void OnUpdate()
        {
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}

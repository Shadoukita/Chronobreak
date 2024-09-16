using System.Numerics;
using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.GameObjects.SpellNS.Missile;

namespace Chronobreak.GameServer.Scripting.CSharp
{
    public interface ISpellScript
    {
        public SpellScriptMetadata MetaData { get; }

        internal void Init(Spell spell, ObjAIBase owner);
        /// <summary>
        /// Happens when the spell is assigned to the Owner
        /// </summary>
        internal void OnActivate() { }
        /// <summary>
        /// Happens when the spell is replaced
        /// </summary>
        internal void OnDeactivate() { }
        /// <summary>
        /// Happens when a Missile, Sector, Melee Attack hits an unit
        /// </summary>
        /// <param name="target"></param>
        /// <param name="missile"></param>
        /// <param name="sector"></param>
        internal void OnSpellHit(AttackableUnit target, SpellMissile? missile) { }
        /// <summary>
        /// Happens after the spell is requested to cast, before cheking mana or energy
        /// </summary>
        /// <param name="target"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        internal void OnSpellPreCast(AttackableUnit? target, Vector2 start, Vector2 end) { }
        /// <summary>
        /// Happens after starting the casting delay time, before creating the missile
        /// </summary>
        internal void OnSpellCast() { }
        /// <summary>
        /// Happens after the casting delay time, after creating the missile
        /// </summary>
        internal void OnSpellPostCast() { }
        /// <summary>
        /// Happens after the cast is completed
        /// </summary>
        internal void OnSpellChannel() { }
        /// <summary>
        /// Happens if the channeling is canceled
        /// </summary>
        internal void OnSpellChannelCancel(ChannelingStopSource reason) { }
        /// <summary>
        /// Happens after the channeling is completed, or when the player cast to finishe early
        /// </summary>
        internal void OnSpellPostChannel() { }
        internal void OnUpdate() { }
        internal void OnUpdateStats() { }
        internal void OnMissileUpdate(SpellMissile missile) { }
        internal void OnMissileEnd(SpellMissile missile) { }
        internal void OnSpellMissileHitTerrain(SpellMissile missile) { }
        internal bool CanCast() { return true; }
        internal void SelfExecute() { }
        internal void TargetExecute(AttackableUnit target, SpellMissile? missile, ref HitResult hitResult) { }
        internal void AdjustCastInfo() { }
        internal float AdjustCooldown()
        {
            return float.NaN;
        }
        internal void ChannelingStart() { }
        internal void ChannelingStop() { }
        internal void ChannelingCancelStop() { }
        internal void ChannelingSuccessStop() { }
    }
    public class SpellScript : ISpellScript
    {
        public static readonly SpellDataFlags Minions = SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.NotAffectSelf | SpellDataFlags.AffectMinions | SpellDataFlags.AffectMinions;
        public static readonly SpellDataFlags Champions = SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.NotAffectSelf | SpellDataFlags.AffectHeroes;
        public static readonly SpellDataFlags AllUnits = Minions | Champions;
        public static readonly SpellDataFlags AllNonTurrets = AllUnits | SpellDataFlags.AffectWards;
        public static readonly SpellDataFlags AllNonBuilding = AllNonTurrets | SpellDataFlags.AffectTurrets;
        public static readonly SpellDataFlags All = AllNonBuilding | SpellDataFlags.AffectBuildings;

        public Spell Spell { get; private set; } = null!;
        public ObjAIBase Owner { get; private set; } = null!;
        public int SpellLevel => Spell.Level;
        public int SpellLevelMinusOne => Spell.LevelMinusOne;

        public virtual SpellScriptMetadata MetaData { get; } = new();

        public void Init(Spell spell, ObjAIBase owner)
        {
            Spell = spell;
            Owner = owner;
        }

        public virtual void OnActivate()
        {
        }
        public virtual void OnDeactivate()
        {
        }
        public virtual void OnSpellHit(AttackableUnit target, SpellMissile? missile)
        {
        }
        public virtual void OnSpellPreCast(AttackableUnit? target, Vector2 start, Vector2 end)
        {
        }
        public virtual void OnSpellCast()
        {
        }
        public virtual void OnSpellPostCast()
        {
        }
        public virtual void OnSpellChannel()
        {
        }
        public virtual void OnSpellChannelCancel(ChannelingStopSource reason)
        {
        }
        public virtual void OnSpellPostChannel()
        {
        }
        public virtual void OnUpdateStats()
        {
        }
        public virtual void OnUpdate()
        {
        }
        public virtual void OnMissileUpdate(SpellMissile missile)
        {
        }
        public virtual void OnMissileEnd(SpellMissile missile)
        {
        }
        public virtual void OnSpellMissileHitTerrain(SpellMissile missile)
        {
        }
        public virtual bool CanCast()
        {
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Numerics;
using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.GameObjects.SpellNS.Missile;
using Chronobreak.GameServer.Logging;
using log4net;

/*
 * Possible Events:
 * Events that are always performed are accounted for in-script, no event handling needed. EX: Spell calls spellscript.OnActivate()
[OnActivate] - buffs and spells (always performed)
[OnAddPAR]
[OnAllowAdd(Buff)]
[OnAssist]
[OnAssistUnit]
[OnBeingDodged]
[OnBeingHit]
[OnBeingSpellHit]
[OnCanCast]
[OnCollision]
[OnCollisionTerrain]
[OnDeactivate] - buffs and spells (always performed)
[OnDealDamage]
[OnDeath]
[OnDisconnect]
[OnDodge]
[OnHeal]
[OnHitUnit]
[OnKill]
[OnKillUnit]
[OnLaunchAttack]
[OnLaunchMissile]
[OnLevelUp]
[OnLevelUpSpell]
[OnMiss]
[OnMissileEnd]
[OnMissileUpdate]
[OnMoveEnd]
[OnMoveFailure]
[OnMoveSuccess]
[OnNearbyDeath]
[OnPreAttack]
[OnPreDamage]
[OnPreDealDamage]
[OnPreMitigationDamage]
[OnPreTakeDamage]
[OnReconnect]
[OnResurrect]
[OnSpellCast] - start casting
[OnSpellChannel] - start channeling
[OnSpellChannelCancel] - abrupt stop channeling
[OnSpellPostCast] - finish casting
[OnSpellPostChannel] - finish channeling
[OnSpellPreCast] - setup cast info before casting (always performed)
[OnSpellHit] - "ApplyEffects" function in Spell.
[OnTakeDamage]
[OnUpdateActions] - move order probably
[OnUpdateAmmo]
[OnUpdateStats]
[OnZombie]
 */

namespace Chronobreak.GameServer.API
{
    public static class ApiEventManager
    {
        private static ILog _logger = LoggerProvider.GetLogger();
        private static List<DispatcherBase> _dispatchers = [];

        public static void RemoveAllListenersForOwner(object owner)
        {
            foreach (var dispatcher in _dispatchers)
            {
                dispatcher.RemoveListener(owner);
            }
        }

        public static Dispatcher<AttackableUnit, DamageData> OnBeingHit = new();
        public static Dispatcher<AttackableUnit, Spell, SpellMissile> OnBeingSpellHit = new();
        public static Dispatcher<Buff> OnBuffActivated = new();
        public static Dispatcher<Buff> OnBuffDeactivated = new();
        public static Dispatcher<GameObject, GameObject> OnCollision = new();
        public static Dispatcher<GameObject> OnCollisionTerrain = new();
        public static DataOnlyDispatcher<AttackableUnit, DeathData> OnDeath = new();
        public static DataOnlyDispatcher<AttackableUnit, DeathData> OnZombie = new();
        public static DataOnlyDispatcher<ObjAIBase, DamageData> OnHitUnit = new();
        public static DataOnlyDispatcher<Champion, ScoreData> OnIncrementChampionScore = new();
        public static DataOnlyDispatcher<AttackableUnit, DeathData> OnKill = new();
        public static Dispatcher<ObjAIBase, Spell, AttackableUnit> OnLaunchAttack = new();
        /// <summary>
        /// Called immediately after the rocket is added to the scene. *NOTE*: At the time of the call, the rocket has not yet been spawned for players.
        /// <summary>
        public static Dispatcher<Spell, SpellMissile> OnLaunchMissile = new();
        public static Dispatcher<AttackableUnit> OnLevelUp = new();
        public static Dispatcher<Spell> OnLevelUpSpell = new();
        public static DataOnlyDispatcher<ObjAIBase, Spell> OnUnitLevelUpSpell = new();
        public static Dispatcher<AttackableUnit> OnMoveEnd = new();
        public static Dispatcher<AttackableUnit> OnMoveFailure = new();
        public static Dispatcher<AttackableUnit> OnMoveSuccess = new();
        public static Dispatcher<ObjAIBase, Spell, AttackableUnit> OnPreAttack = new();
        public static Dispatcher<ObjAIBase> OnResurrect = new();
        public static Dispatcher<Champion> OnReconnect = new();
        public static Dispatcher<Spell, AttackableUnit, Vector2, Vector2> OnSpellPreCast = new();
        public static Dispatcher<Spell> OnSpellCast = new();
        public static DataOnlyDispatcher<ObjAIBase, Spell> OnUnitSpellCast = new();
        public static Dispatcher<Spell> OnSpellChannel = new();
        public static Dispatcher<Spell, AttackableUnit, SpellMissile> OnSpellHit = new();
        public static Dispatcher<ObjAIBase, Spell, AttackableUnit, SpellMissile> OnUnitSpellHit = new();
        public static Dispatcher<SpellMissile> OnSpellMissileEnd = new();
        public static Dispatcher<SpellMissile> OnSpellMissileRemoved = new();
        public static Dispatcher<SpellMissile, AttackableUnit> OnSpellMissileHit = new();
        public static Dispatcher<SpellMissile> OnSpellMissileHitTerrain = new();
        public static Dispatcher<Spell> OnSpellPostCast = new();

        public static DataOnlyDispatcher<AttackableUnit, HealData> OnHeal = new();
        public static Dispatcher<AttackableUnit, AttackableUnit> OnDodge = new();
        public static Dispatcher<AttackableUnit, AttackableUnit> OnBeingDodged = new();
        public static Dispatcher<AttackableUnit, AttackableUnit> OnMiss = new();
        #region Damage
        public static DataOnlyDispatcher<AttackableUnit, DamageData> OnPreDealDamage = new();
        public static DataOnlyDispatcher<AttackableUnit, DamageData> OnPreMitigationDamage = new();
        public static DataOnlyDispatcher<AttackableUnit, DamageData> OnPreTakeDamage = new();
        public static DataOnlyDispatcher<AttackableUnit, DamageData> OnTakeDamage = new();
        public static DataOnlyDispatcher<AttackableUnit, DamageData> OnDealDamage = new();
        #endregion

        public static DataOnlyDispatcher<ObjAIBase, AttackableUnit> OnTargetLost = new();
        public static Dispatcher<AttackableUnit, Buff> OnUnitBuffDeactivated = new();
        public static Dispatcher<AttackableUnit, Buff> OnUnitBuffActivated = new();
        public static Dispatcher<Shield> OnShieldBreak = new();
        // TODO: Change to OnMoveSuccess and change where Publish is called internally to reflect the name.
        public static ConditionDispatcher<ObjAIBase, OrderType> OnUnitUpdateMoveOrder = new();
        public static Dispatcher<Champion> OnDisconnect = new();

        public abstract class DispatcherBase
        {
            public DispatcherBase()
            {
                _dispatchers.Add(this);
            }
            public abstract void RemoveListener(object owner);
        }

        public abstract class DispatcherBase<Source, CBType> : DispatcherBase
        {
            protected class Listener
            {
                public object Owner;
                public Source Source;
                public CBType Callback;
                public bool SingleInstance;
                public Listener(object owner, Source source, CBType callback, bool singleInstance = false)
                {
                    Owner = owner;
                    Source = source;
                    Callback = callback;
                    SingleInstance = singleInstance;
                }
            }
            protected readonly List<Listener> _listeners = [];
            // Storage for Publish functions counters.
            protected List<int> _stack = [-1, -1, -1, -1, -1, -1, -1, -1];
            // The index of the last Publish function currently executing.
            protected int _nestingLevel = -1;
            protected void IncrementNestingLevel()
            {
                _nestingLevel++;
                if (_nestingLevel >= _stack.Count)
                {
                    _stack.Add(-1);
                }
            }
            // Removes the element and adjusts the counters of all currently executing Publish functions, if necessary.
            protected void CarefulRemoval(int index)
            {
                _listeners.RemoveAt(index);
                for (int l = 0; l < _nestingLevel + 1; l++)
                {
                    if (index < _stack[l])
                    {
                        _stack[l]--;
                    }
                }
            }
            private void CarefulRemoval(Predicate<Listener> match)
            {
                for (int j = _listeners.Count - 1; j >= 0; j--)
                {
                    var listener = _listeners[j];
                    if (match(listener))
                    {
                        CarefulRemoval(j);
                    }
                }
            }
            public void AddListener(object owner, Source source, CBType callback, bool singleInstance = false)
            {
                if (owner == null || source == null || callback == null)
                {
                    return;
                }

                _listeners.Add(
                    new Listener(owner, source, callback, singleInstance)
                );
            }
            public override void RemoveListener(object owner)
            {
                CarefulRemoval(listener => listener.Owner == owner);
            }
            public void RemoveListener(object owner, Source source)
            {
                CarefulRemoval(listener => listener.Owner == owner && listener.Source.Equals(source));
            }
            public void RemoveListener(object owner, Source source, CBType callback)
            {
                CarefulRemoval(listener => listener.Owner == owner && listener.Source.Equals(source) && listener.Callback.Equals(callback));
            }
        }

        public abstract class VariableDispatcherBase<Source, CBType> : DispatcherBase<Source, CBType>
        {
            protected Source _source;
            protected abstract void Call(CBType callback);
            protected void Publish(Source source)
            {
                IncrementNestingLevel();
                _source = source;

                int i;
                for (
                    _stack[_nestingLevel] = _listeners.Count - 1;
                    (i = _stack[_nestingLevel]) >= 0;
                    _stack[_nestingLevel]--
                )
                {
                    var listener = _listeners[i];
                    if (listener.Source.Equals(source))
                    {
                        if (listener.SingleInstance)
                        {
                            CarefulRemoval(i);
                        }

                        try
                        {
                            Call(listener.Callback);
                        }
                        catch (Exception e)
                        {
                            _logger.Error(e);
                        }
                    }
                }
                _nestingLevel--;
            }
        }

        public abstract class VariableDispatcherBase<Source, Data, CBType> : VariableDispatcherBase<Source, CBType>
        {
            protected Data _data;
            public void Publish(Source source, Data data)
            {
                _data = data;
                base.Publish(source);
            }
        }

        public abstract class ConditionDispatcherBase<Source, Data, CBType> : DispatcherBase<Source, CBType>
        {
            protected Source _source;
            protected Data _data;
            protected abstract bool Call(CBType callback);
            public bool Publish(Source source, Data data)
            {
                IncrementNestingLevel();
                _source = source;
                _data = data;

                bool returnVal = true;
                int i;
                for (
                    _stack[_nestingLevel] = _listeners.Count - 1;
                    (i = _stack[_nestingLevel]) >= 0;
                    _stack[_nestingLevel]--
                )
                {
                    var listener = _listeners[i];
                    if (listener.Source.Equals(source))
                    {
                        if (listener.SingleInstance)
                        {
                            CarefulRemoval(i);
                        }

                        try
                        {
                            returnVal = returnVal && Call(listener.Callback);
                        }
                        catch (Exception e)
                        {
                            _logger.Error(e);
                        }
                    }
                }
                _nestingLevel--;
                return returnVal;
            }
        }

        public class Dispatcher<Source> : VariableDispatcherBase<Source, Action<Source>>
        {
            public new void Publish(Source source)
            {
                base.Publish(source);
            }
            protected override void Call(Action<Source> callback)
            {
                callback(_source);
            }
        }

        public class Dispatcher<Source, Data> : VariableDispatcherBase<Source, Data, Action<Source, Data>>
        {
            protected override void Call(Action<Source, Data> callback)
            {
                callback(_source, _data);
            }
        }

        public class DataOnlyDispatcher<Source, Data> : VariableDispatcherBase<Source, Data, Action<Data>>
        {
            protected override void Call(Action<Data> callback)
            {
                callback(_data);
            }
        }

        public class Dispatcher<Source, D1, D2> : VariableDispatcherBase<Source, (D1, D2), Action<Source, D1, D2>>
        {
            protected override void Call(Action<Source, D1, D2> callback)
            {
                callback(_source, _data.Item1, _data.Item2);
            }
        }

        public class Dispatcher<Source, D1, D2, D3> : VariableDispatcherBase<Source, (D1, D2, D3), Action<Source, D1, D2, D3>>
        {
            protected override void Call(Action<Source, D1, D2, D3> callback)
            {
                callback(_source, _data.Item1, _data.Item2, _data.Item3);
            }
        }

        public class Dispatcher<Source, D1, D2, D3, D4> : VariableDispatcherBase<Source, (D1, D2, D3, D4), Action<Source, D1, D2, D3, D4>>
        {
            protected override void Call(Action<Source, D1, D2, D3, D4> callback)
            {
                callback(_source, _data.Item1, _data.Item2, _data.Item3, _data.Item4);
            }
        }

        public class ConditionDispatcher<Source, Data> : ConditionDispatcherBase<Source, Data, Func<Source, Data, bool>>
        {
            protected override bool Call(Func<Source, Data, bool> callback)
            {
                return callback(_source, _data);
            }
        }

        public class DataOnlyConditionDispatcher<Source, Data> : ConditionDispatcherBase<Source, Data, Func<Data, bool>>
        {
            protected override bool Call(Func<Data, bool> callback)
            {
                return callback(_data);
            }
        }

        public class ConditionDispatcher<Source, D1, D2> : ConditionDispatcherBase<Source, (D1, D2), Func<Source, D1, D2, bool>>
        {
            protected override bool Call(Func<Source, D1, D2, bool> callback)
            {
                return callback(_source, _data.Item1, _data.Item2);
            }
        }
    }
}
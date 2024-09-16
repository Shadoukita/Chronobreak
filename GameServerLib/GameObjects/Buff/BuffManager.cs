#nullable enable

using GameServerCore.Enums;
using GameServerLib.Scripting.Lua.Scripts;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.Logging;
using Chronobreak.GameServer.Scripting.CSharp;
using Chronobreak.GameServer.Scripting.Lua;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Chronobreak.GameServer.GameObjects;

public class BuffManager(AttackableUnit owner)
{
    private static readonly ILog _logger = LoggerProvider.GetLogger();
    private readonly AttackableUnit _owner = owner;
    private readonly List<Slot> _slots = new(255);
    private class Slot(AttackableUnit owner, int slotIndex, string name, ObjAIBase? attacker)
    {
        private AttackableUnit owner = owner;
        public int Index = slotIndex;
        public string Name = name;
        public ObjAIBase? Attacker = attacker;
        public List<Buff> Stacks = [];
        private bool HasBeenNotified = false;
        public bool PersistsThroughDeath => Stacks.Any(buff => buff.Script?.MetaData.PersistsThroughDeath ?? false);
        public void RemoveStack(bool notify)
        {
            Stacks.LastOrDefault()?.SetToRemove(notify);
        }

        public void RemoveStacks(int count, bool notify)
        {
            for (int i = Stacks.Count - 1; i >= 0 && count > 0; i--)
            {
                if (!Stacks[i].IsToRemove)
                {
                    Stacks[i].SetToRemove(notify);
                    count--;
                }
            }
        }

        public void RemoveAllStacks(bool notify)
        {
            RemoveStacks(Stacks.Count, notify);
        }

        public void Renew(float duration, bool notify)
        {
            foreach (var buff in Stacks)
            {
                buff.Duration = duration;
                buff.ResetTimeElapsed();
            }
            if (notify)
            {
                Notify();
            }
        }

        public bool Has(BuffType type)
        {
            foreach (var buff in Stacks)
            {
                if (buff.BuffType == type)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasNegative()
        {
            foreach (var buff in Stacks)
            {
                if (
                    false
                    || buff.BuffType == BuffType.INTERNAL
                    //|| buff.BuffType == BuffType.AURA
                    //|| buff.BuffType == BuffType.COMBAT_ENCHANCER
                    || buff.BuffType == BuffType.COMBAT_DEHANCER
                    //|| buff.BuffType == BuffType.SPELL_SHIELD
                    || buff.BuffType == BuffType.STUN
                    //|| buff.BuffType == BuffType.INVISIBILITY
                    || buff.BuffType == BuffType.SILENCE
                    || buff.BuffType == BuffType.TAUNT
                    || buff.BuffType == BuffType.POLYMORPH
                    || buff.BuffType == BuffType.SLOW
                    || buff.BuffType == BuffType.SNARE
                    || buff.BuffType == BuffType.DAMAGE
                    //|| buff.BuffType == BuffType.HEAL
                    //|| buff.BuffType == BuffType.HASTE
                    //|| buff.BuffType == BuffType.SPELL_IMMUNITY
                    //|| buff.BuffType == BuffType.PHYSICAL_IMMUNITY
                    //|| buff.BuffType == BuffType.INVULNERABILITY
                    || buff.BuffType == BuffType.SLEEP
                    || buff.BuffType == BuffType.NEAR_SIGHT
                    || buff.BuffType == BuffType.FRENZY
                    || buff.BuffType == BuffType.FEAR
                    || buff.BuffType == BuffType.CHARM
                    || buff.BuffType == BuffType.POISON
                    || buff.BuffType == BuffType.SUPPRESSION
                    || buff.BuffType == BuffType.BLIND
                    //|| buff.BuffType == BuffType.COUNTER
                    || buff.BuffType == BuffType.SHRED
                    || buff.BuffType == BuffType.FLEE
                    //|| buff.BuffType == BuffType.KNOCKUP
                    //|| buff.BuffType == BuffType.KNOCKBACK
                    || buff.BuffType == BuffType.DISARM
                )
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsActive()
        {
            return GetStartTime() > Game.Time.GameTime / 1000f;
        }

        public float GetRemainingDuration()
        {
            if (Stacks.Count > 0)
                return Stacks.Max(buff => buff.RemainingDuration);
            return -1; //TODO: Verify
        }

        public float GetStartTime()
        {
            return Stacks.Min(buff => buff.StartTime);
        }

        public void Notify()
        {
            Buff[] runningBuffs = Stacks.Where(x => !x.IsToRemove).ToArray();
            if (runningBuffs.Any())
            {
                Buff expiringFirst = runningBuffs.MinBy(buff => buff.RemainingDuration)!;
                if (!HasBeenNotified)
                {
                    bool isInternal = Stacks.Any(buff => buff.IsInternal);
                    if (!isInternal)
                    {
                        HasBeenNotified = true;
                        Game.PacketNotifier.NotifyNPC_BuffAdd2(expiringFirst, runningBuffs.Length);
                    }
                }
                else
                {
                    Game.PacketNotifier.NotifyNPC_BuffUpdateCount(expiringFirst, runningBuffs.Length);
                }
            }
            else if (HasBeenNotified)
            {
                HasBeenNotified = false;
                Game.PacketNotifier.NotifyNPC_BuffRemove2(owner, Index, Name);
            }
        }
    }

    public void RemoveStack(Buff buff, bool expired = false)
    {
        foreach (var slot in _slots)
        {
            if (slot.Index == buff.Slot)
            {
                buff.SetToRemove(true);
                return;
            }
        }
    }

    /// <returns>
    /// Returns the slot with the requested name and attacker.
    /// The returned slot may not contain stacks.
    /// </returns>
    private Slot? GetSlot(string name, ObjAIBase? attacker = null)
    {
        return _slots.Find(slot => slot.Name == name && slot.Attacker == attacker);
    }

    /// <returns>
    /// Returns non-empty slot with the specified attacker
    /// or non-empty slots with any attackers if null is passed.
    /// </returns>
    private List<Slot> GetSlots(string name, ObjAIBase? attacker = null)
    {
        return _slots.FindAll(slot =>
        slot.Name == name &&
        (slot.Attacker == null || attacker == null || slot.Attacker == attacker) &&
        slot.Stacks.Count > 0);
    }

    private Slot? GetSlot(int slotIndex, ObjAIBase? attacker = null)
    {
        return _slots.Find(slot =>
        slot.Index == slotIndex && (
        slot.Attacker == null || attacker == null || slot.Attacker == attacker) &&
        slot.Stacks.Count > 0);
    }

    private Slot? GetSingleSlot(string name, ObjAIBase? attacker = null)
    {
        var slots = GetSlots(name, attacker);
        Debug.Assert(slots.Count <= 1);
        return slots.FirstOrDefault();
    }

    public void Add(
        string name,
        float duration,
        int stacks,
        Spell? originspell,
        AttackableUnit onto,
        ObjAIBase from,
        IEventSource? parent = null
    )
    {
        IBuffScript script = Game.ScriptEngine.CreateObject<IBuffScript>("Buffs", name, Game.Config.SupressScriptNotFound);

        if (script == null)
        {
            if (LuaScriptEngine.HasBBScript(name))
            {
                script = new BBBuffScript
                (
                    new BBScriptCtrReqArgs
                    (
                        name,
                        onto,
                        (onto as Minion)?.Owner as Champion
                    ),
                    from
                );
            }
            else
            {
                script = new BuffScript();
            }
        }
        BuffScriptMetaData m = script.BuffMetaData ?? new();

        foreach (var buff in All())
        {
            if (!buff.Elapsed && (!buff.Script?.OnAllowAdd(from, m.BuffType, name, m.MaxStacks, ref duration) ?? false))
            {
                return;
            }
        }

        Add(
            from,
            script,
            m.MaxStacks, stacks,
            duration,
            m.BuffAddType, m.BuffType,
            m.TickRate,
            m.StacksExclusive, m.CanMitigateDuration, m.IsHidden,
            originspell
        );
    }

    public void Add
    (
        ObjAIBase attacker,
        IBuffScript script,
        int maxStack = 0,
        int stacks = 1,
        float duration = 0,

        BuffAddType addType = BuffAddType.REPLACE_EXISTING,
        BuffType type = BuffType.INTERNAL,
        float tickRate = 0,
        bool stacksExclusive = false,
        bool canMitigateDuration = false, //TODO:
        bool isHiddenOnClient = false,

        Spell? spell = null
    )
    {
        if (_owner.Stats.IsDead)
        {
            return;
        }
        string name = script.Name;
        ObjAIBase? exclusiveAttacker = stacksExclusive ? attacker : null;

        Slot? slot = GetSlot(name, exclusiveAttacker);
        bool hasBuffBefore = true;
        int slotIndex = 0;
        if (slot is null)
        {
            for (; slotIndex < 256; slotIndex++)
            {
                //Very inneficient way to find an unused index
                if (_slots.Find(slot => slot.Index == slotIndex) == null)
                {
                    break;
                }
            }
            if (slotIndex == 256)
            {
                throw new Exception();
            }
            slot = new Slot(_owner, slotIndex, name, exclusiveAttacker);
            _slots.Add(slot);
            hasBuffBefore = false;
        }
        switch (addType)
        {
            case BuffAddType.REPLACE_EXISTING:
                {
                    //TODO: Check exactly the packet calls and see if this is correct
                    stacks = Math.Min(stacks, maxStack);
                    slot.RemoveAllStacks(false);
                    AddStacks();
                }
                break;
            case BuffAddType.RENEW_EXISTING:
                {
                    stacks = Math.Min(stacks, maxStack);
                    if (hasBuffBefore)
                    {
                        slot.Renew(duration, false);
                    }
                    else
                    {
                        AddStacks();
                    }
                }
                break;
            case BuffAddType.STACKS_AND_RENEWS:
                {
                    Debug.Assert(maxStack >= slot.Stacks.Count);
                    stacks = Math.Min(stacks, maxStack - slot.Stacks.Count);
                    slot.Renew(duration, false);
                    AddStacks();
                }
                break;
            case BuffAddType.STACKS_AND_CONTINUE:
                {
                    Debug.Assert(maxStack >= slot.Stacks.Count);
                    stacks = Math.Min(stacks, maxStack - slot.Stacks.Count);
                    AddContinuingStacks(duration);
                }
                break;
            case BuffAddType.STACKS_AND_OVERLAPS:
                {
                    Debug.Assert(maxStack >= slot.Stacks.Count);
                    stacks = Math.Min(stacks, maxStack - slot.Stacks.Count);
                    AddStacks();
                }
                break;
        }

        slot.Notify();

        void AddStack(int index, float duration)
        {
            if (index > 0)
            {
                script = (IBuffScript)script.Clone();
            }
            Buff buff = new(slot.Index, name, duration, 1, spell, _owner, attacker, null, script, type, isHiddenOnClient, tickRate);
            slot.Stacks.Add(buff);
            buff.Activate();
        }

        void AddStacks()
        {
            for (int i = 0; i < stacks; i++)
            {
                AddStack(i, duration);
            }
        }

        void AddContinuingStacks(float duration)
        {
            float prevStacksDuration = Math.Max(0, slot.GetRemainingDuration());
            for (int i = 0; i < stacks; i++)
            {
                var durationToAdd = prevStacksDuration + i * duration;
                if (durationToAdd == 0)
                {
                    durationToAdd = duration;
                }
                AddStack(i, durationToAdd);
            }
        }
    }

    public int Count(string name, ObjAIBase? attacker = null)
    {
        /*
        if(attacker != null)
        {
            return
                (GetSlot(name, attacker)?.stacks.Count ?? 0) +
                (GetSlot(name, null)?.stacks.Count(buff => buff.SourceUnit == attacker) ?? 0);
        }
        else
        */
        return GetSlots(name, attacker).Sum(slot => slot.Stacks.Count);
    }

    public float GetRemainingDuration(string name, ObjAIBase? attacker = null)
    {
        var slots = GetSlots(name, attacker);
        if (slots.Count > 0)
            return slots.Max(slot => slot.GetRemainingDuration());
        return -1; //TODO: Verify
    }

    public float GetStartTime(string name, ObjAIBase? attacker = null)
    {
        return GetSlots(name, attacker).Min(slot => slot.GetStartTime());
    }

    public bool Has(string name, ObjAIBase? attacker = null)
    {
        return Count(name, attacker) > 0;
    }

    public bool Has(BuffType type)
    {
        foreach (var slot in _slots)
        {
            if (slot.Has(type))
            {
                return true;
            }
        }
        return false;
    }

    #region
    // Functions that strictly require the presence or absence of an attacker
    // depending on whether the buff stacks exclusively or not.
    public void RemoveStack(string name, ObjAIBase? attacker = null)
    {
        GetSingleSlot(name, attacker)?.RemoveStack(true);
    }
    public void RemoveStack(int buffSlot, ObjAIBase? attacker = null)
    {
        GetSlot(buffSlot, attacker)?.RemoveStack(true);
    }
    public void RemoveStacks(string name, ObjAIBase? attacker = null, int count = 0)
    {
        GetSingleSlot(name, attacker)?.RemoveStacks(count, true);
    }
    public void RemoveAllStacks(string name, ObjAIBase? attacker = null)
    {
        GetSingleSlot(name, attacker)?.RemoveAllStacks(true);
    }
    public void Renew(string name, ObjAIBase? attacker = null, float duration = 0)
    {
        GetSingleSlot(name, attacker)?.Renew(duration, true);
    }

    // For compatibility with the legacy CB-Scripts
    public List<Buff>? GetStacks(string name, ObjAIBase? attacker = null)
    {
        return GetSingleSlot(name, attacker)?.Stacks;
    }
    #endregion

    public void RemoveType(BuffType type)
    {
        foreach (var slot in new List<Slot>(_slots))
        {
            if (slot.Has(type) && slot.IsActive())
            {
                slot.RemoveAllStacks(true);
            }
        }
    }

    public void DispellNegative()
    {
        //TODO: Maybe it should not remove the entire slot,
        //TODO: but only matching stacks?
        foreach (var slot in new List<Slot>(_slots))
        {
            if (slot.HasNegative() && slot.IsActive())
            {
                slot.RemoveAllStacks(true);
            }
        }
    }

    public void RemoveNotLastingThroughDeath()
    {
        foreach (var slot in new List<Slot>(_slots))
        {
            if (!slot.PersistsThroughDeath)
            {
                slot.RemoveAllStacks(true);
            }
        }
    }

    public IEnumerable<Buff> All()
    {
        foreach (var slot in _slots)
        {
            foreach (var buff in slot.Stacks)
            {
                yield return buff;
            }
        }
    }

    internal void ForEach(Action<Buff> action)
    {
        foreach (var slot in _slots.ToArray())
        {
            foreach (var buff in slot.Stacks.ToArray())
            {
                action(buff);
            }
        }
    }

    internal void UpdateStats()
    {
        ForEach(buff => buff.UpdateStats());
    }

    internal void Update()
    {
        foreach (var slot in _slots.ToList())
        {
            foreach (var buff in slot.Stacks.ToList())
            {
                if (buff.IsToRemove)
                {
                    buff.Deactivate(Game.Time.GameTime >= buff.EndTime);

                    slot.Stacks.Remove(buff);
                    if (slot.Stacks.Count <= 0)
                    {
                        _slots.Remove(slot);
                    }

                    if (buff.NotifyRemoval)
                    {
                        slot.Notify();
                    }
                }
                else
                {
                    buff.Update();
                }
            }
        }
    }

    internal void ReloadScripts()
    {
        ForEach(buff => buff.LoadScript());
    }
}
using System;
using GameServerCore.Enums;
using GameServerLib.Scripting.Lua.Scripts;
using Chronobreak.GameServer.API;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.GameObjects.StatsNS;
using Chronobreak.GameServer.Logging;
using Chronobreak.GameServer.Scripting.CSharp;
using Chronobreak.GameServer.Scripting.Lua;
using log4net;
using static GameServerCore.Content.HashFunctions;

namespace Chronobreak.GameServer.GameObjects
{
    public class Buff : IEventSource
    {
        private static ILog _logger = LoggerProvider.GetLogger();

        public int Slot { get; }
        public string Name { get; }
        public BuffType BuffType { get; }
        public float StartTime { get; private set; }
        public float Duration { get; internal set; }
        public float EndTime => StartTime + Duration;
        public float TimeElapsed => Game.Time.GameTime / 1000f - StartTime;
        public float RemainingDuration => Duration - TimeElapsed;
        public bool IsHidden { get; }
        public Spell OriginSpell { get; }
        public ObjAIBase SourceUnit { get; }
        public AttackableUnit TargetUnit { get; }
        internal bool IsToRemove { get; private set; }
        internal bool NotifyRemoval { get; private set; }

        internal void SetToRemove(bool notify)
        {
            IsToRemove = true;
            NotifyRemoval = notify;
        }

        /// <summary>
        /// InfiniteDuration buffs does not show a timer (Attention to the BuffType, not all of them are compatible)
        /// </summary>
        public bool IsInfiniteDuration
        {
            get => Duration >= 20000;
        }

        /// <summary>
        /// Internal Buffs are not synchronized to the client
        /// </summary>
        public bool IsInternal
        {
            get => BuffType == BuffType.INTERNAL;
        }

        /// <summary>
        /// Script instance for this buff. Casting to a specific buff class gives access its functions and variables.
        /// </summary>
        internal IBuffScript? Script { get; private set; }

        /// <summary>
        /// IEventSource variable for Packets.
        /// </summary>
        public uint ScriptNameHash { get; }

        /// <summary>
        /// IEventSource variable for Packets.
        /// </summary>
        public IEventSource? ParentScript { get; }

        /// <summary>
        /// Status that will be applied Positively to the Target
        /// </summary>
        public StatusFlags StatusEffectsToEnable { get; private set; }

        /// <summary>
        /// Status that will be applied Negatively to the Target
        /// </summary>
        public StatusFlags StatusEffectsToDisable { get; private set; }

        /// <summary>
        /// Used to update player buff tool tip values.
        /// </summary>
        public ToolTipData ToolTipData { get; }

        /// <summary>
        /// Check if the buff has elapsed the timer
        /// </summary>
        public bool Elapsed => !IsInfiniteDuration && Game.Time.GameTime / 1000f >= EndTime;

        public float TickRate { get; }

        internal Buff
        (
            int slot,
            string name,
            float duration,
            int stacks,
            Spell originSpell,
            AttackableUnit onto,
            ObjAIBase from,
            IEventSource? parent = null,
            IBuffScript? script = null,
            BuffType buffType = BuffType.INTERNAL,
            bool isHidden = false,
            float tickRate = 0
        )
        {
            if (duration < 0)
            {
                throw new ArgumentException("Error: Duration was set to under 0.");
            }

            Slot = slot;
            Name = name;
            Duration = duration;
            OriginSpell = originSpell;
            SourceUnit = from;
            TargetUnit = onto;

            BuffType = buffType;
            IsHidden = isHidden;
            TickRate = tickRate;

            ParentScript = parent;
            ScriptNameHash = HashString(Name);
            ToolTipData = new ToolTipData(this);

            if (script is IBuffScript buffScriptInternal)
            {
                LoadScript(buffScriptInternal);
            }
        }

        internal void LoadScript(IBuffScript? script = null)
        {
            // Debug Only
            bool isReload = Script != null;
            if (isReload)
            {
                Deactivate(false);
            }

            if (script == null)
            {
                script = Game.ScriptEngine.CreateObject<IBuffScript>("Buffs", Name, Game.Config.SupressScriptNotFound, Script);
            }
            if (script == null)
            {
                if (LuaScriptEngine.HasBBScript(Name))
                {
                    script = new BBBuffScript
                    (
                        new BBScriptCtrReqArgs
                        (
                            Name,
                            TargetUnit,
                            (TargetUnit as Minion)?.Owner as Champion
                        ),
                        SourceUnit
                    );
                }
                else
                {
                    script = new BBBuffScriptEmpty();
                    //HasEmptyScript = true;
                }
            }
            Script = script;
            Script.Init(this);

            //Debug Only
            if (isReload)
            {
                Activate(true);
            }

            //TODO:
            //IsHidden = BuffScript.BuffMetaData.IsHidden;
            //BuffType = BuffScript.BuffMetaData.BuffType;
        }

        internal void Activate(bool isReload = false)
        {
            if (!isReload)
            {
                StartTime = Game.Time.GameTime / 1000f;
            }
            try
            {
                Script?.Activate();
                ApiEventManager.OnBuffActivated.Publish(this);
                ApiEventManager.OnUnitBuffActivated.Publish(TargetUnit, this);
            }
            catch (Exception e)
            {
                _logger.Error(null, e);
            }
        }

        internal void Deactivate(bool expired)
        {
            try
            {
                Script?.Deactivate(expired);
            }
            catch (Exception e)
            {
                _logger.Error(null, e);
            }

            ApiEventManager.OnBuffDeactivated.Publish(this);
            ApiEventManager.OnUnitBuffDeactivated.Publish(TargetUnit, this);
            if (Script is not null)
            {
                ApiEventManager.RemoveAllListenersForOwner(Script);
            }
        }

        public void SetStatusEffect(StatusFlags flag, bool enabled)
        {
            if (enabled)
            {
                StatusEffectsToEnable |= flag;
                StatusEffectsToDisable &= ~flag;
            }
            else
            {
                StatusEffectsToDisable |= flag;
                StatusEffectsToEnable &= ~flag;
            }

            //HACK: To apply changes immideately (very inefficient)
            //TODO: Removing this will become possible after
            //TODO: reworking the status flags into refcounted
            TargetUnit.UpdateBuffEffects();
        }

        public void SetToolTipVar<T>(int tipIndex, T value) where T : struct
        {
            ToolTipData.Update(tipIndex, value);
        }

        internal void ResetTimeElapsed()
        {
            StartTime = Game.Time.GameTime / 1000f;
        }

        internal void UpdateStats()
        {
            if (!Elapsed)
            {
                try
                {
                    Script?.UpdateStats();
                }
                catch (Exception e)
                {
                    _logger.Error(null, e);
                }
            }
        }

        internal void Update()
        {
            if (!Elapsed)
            {
                try
                {
                    Script?.OnUpdate();
                }
                catch (Exception e)
                {
                    _logger.Error(null, e);
                }
            }
            else
            {
                TargetUnit.Buffs.RemoveStack(this, true);
            }
        }
    }
}

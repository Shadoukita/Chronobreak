using System;
using System.Activities.Presentation.View;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using GameServerCore;
using GameServerCore.Enums;
using Chronobreak.GameServer.Chatbox;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.GameObjects.StatsNS;
using Chronobreak.GameServer.Inventory;
using Chronobreak.GameServer.Logging;
using Chronobreak.GameServer.Scripting.CSharp;
using log4net;

namespace Chronobreak.GameServer.API
{
    /// <summary>
    /// Class housing functions most commonly used by scripts.
    /// </summary>
    public static class ApiFunctionManager
    {
        // Required variables.
        private static ILog _logger = LoggerProvider.GetLogger();
        private static readonly Random rng = new();

        /// <summary>
        /// Converts the given string of hex values into an array of bytes.
        /// </summary>
        /// <param name="hex">String of hex values.</param>
        /// <returns>Array of bytes.</returns>
        public static byte[] StringToByteArray(string hex)
        {
            hex = hex.Replace(" ", string.Empty);
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        /// <summary>
        /// Sets the visibility of the specified GameObject.
        /// </summary>
        /// <param name="gameObject">GameObject to set.</param>
        /// <param name="visibility">Whether or not the GameObject should be visible.</param>
        public static void SetGameObjectVisibility(GameObject gameObject, bool visibility)
        {
            var teams = GetTeams();
            foreach (var id in teams)
            {
                gameObject.SetVisibleByTeam(id, visibility);
            }
        }

        /// <summary>
        /// Gets the possible teams.
        /// </summary>
        /// <returns>Usually BLUE/PURPLE/NEUTRAL.</returns>
        public static List<TeamId> GetTeams()
        {
            return Game.ObjectManager.Teams;
        }

        internal static int ConvertAPISlot(SpellSlotType slotType, int slot)
        {
            if ((slotType == SpellSlotType.SpellSlots && (slot < 0 || slot > 3))
                || (slotType == SpellSlotType.InventorySlots && (slot < 0 || slot > 6))
                || (slotType == SpellSlotType.ExtraSlots && (slot < 0 || slot > 15)))
            {
                return -1;
            }

            if (slotType is SpellSlotType.SummonerSpellSlots
                         or SpellSlotType.InventorySlots
                         or SpellSlotType.TempItemSlot
                         or SpellSlotType.ExtraSlots)
            {
                slot += (int)slotType;
            }

            return slot;
        }

        internal static int ConvertAPISlot(SpellbookType spellbookType, SpellSlotType slotType, int slot)
        {
            if
            (
                spellbookType == SpellbookType.SPELLBOOK_UNKNOWN ||
                spellbookType == SpellbookType.SPELLBOOK_SUMMONER &&
                slotType != SpellSlotType.SummonerSpellSlots ||
                (
                    spellbookType == SpellbookType.SPELLBOOK_CHAMPION &&
                    (
                        (
                            slotType == SpellSlotType.SpellSlots &&
                            (
                                slot < 0 ||
                                slot > 3
                            )
                        ) ||
                        (
                            slotType == SpellSlotType.InventorySlots &&
                            (
                                slot < 0 ||
                                slot > 6
                            )
                        ) ||
                        (
                            slotType == SpellSlotType.ExtraSlots &&
                            (
                                slot < 0 ||
                                slot > 15
                            )
                        )
                    )
                )
            )
            {
                return -1;
            }

            if
            (
                (
                    spellbookType == SpellbookType.SPELLBOOK_CHAMPION &&
                    slotType is SpellSlotType.InventorySlots or SpellSlotType.TempItemSlot or SpellSlotType.ExtraSlots
                ) ||
                (
                    spellbookType == SpellbookType.SPELLBOOK_SUMMONER &&
                    slotType is SpellSlotType.SummonerSpellSlots
                )
            )
            {
                slot += (int)slotType;
            }

            return slot;
        }

        /// <summary>
        /// Teleports an AI unit to the specified coordinates.
        /// Instant.
        /// </summary>
        /// <param name="unit">Unit to be moved</param>
        /// <param name="position">The end position</param>
        /// <param name="clearMovement">Clear the unit pathfinding movement</param>
        public static void TeleportTo(AttackableUnit unit, Vector2 position, bool clearMovement = true)
        {
            if (unit.MovementParameters != null)
            {
                CancelDash(unit);
            }
            unit.TeleportTo(position, !clearMovement);
        }

        public static void FaceDirection(Vector2 location, GameObject target, bool isInstant = false, float turnTime = 0.08333f)
        {
            if (location == target.Position)
            {
                return;
            }

            var goingTo = location - target.Position;
            var direction = Vector2.Normalize(goingTo);

            target.FaceDirection(direction.ToVector3(0), isInstant, turnTime);
        }

        /// <summary>
        /// Gets a point that is in the direction the specified unit is facing, given it is a specified distance away from the unit.
        /// </summary>
        /// <param name="obj">Unit to base the point off of.</param>
        /// <param name="distance">Distance</param>
        /// <param name="offsetAngle">Offset angle from the unit's facing angle (in degrees, clockwise). Must be > 0 to have an effect.</param>
        /// <returns>Vector2 point.</returns>
        public static Vector2 GetPointFromUnit(GameObject obj, float distance, float offsetAngle = 0)
        {
            Vector2 pos = obj.Position;
            Vector2 dir = obj.Direction.ToVector2();
            if (offsetAngle != 0)
            {
                dir = Extensions.Rotate(dir, offsetAngle);
            }
            return pos + (dir * distance);
        }

        /// <summary>
        /// Reports whether or not the specified coordinates are walkable.
        /// </summary>
        /// <param name="x">X coordinaate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="checkRadius">Radius around the given point to check for walkability.</param>
        /// <returns>True/False</returns>
        public static bool IsWalkable(float x, float y, float checkRadius = 0)
        {
            return Game.Map.PathingHandler.IsWalkable(new Vector2(x, y), checkRadius);
        }

        /// <summary>
        /// Adds a named buff with the given duration, stacks, and origin spell to a unit.
        /// From = owner of the spell (usually caster).
        /// </summary>
        /// <param name="buffName">Internally named buff to add.</param>
        /// <param name="duration">Time in seconds the buff should last.</param>
        /// <param name="stacks">Stacks of the buff to add.</param>
        /// <param name="originspell">Spell which called this function.</param>
        /// <param name="onto">Target of the buff.</param>
        /// <param name="from">Owner of the buff.</param>
        /// <param name="parent">Custom hash name Source</param>
        /// <returns>New buff instance.</returns>
        public static Buff AddBuff
        (
            string buffName,
            float duration,
            byte stacks,
            Spell originspell,
            AttackableUnit onto,
            AttackableUnit from,

            IEventSource parent = null
        )
        {
            onto.Buffs.Add(buffName, duration, stacks, originspell, onto, from as ObjAIBase, parent);

            // For compatibility with the legacy CB-Scripts
            return onto.Buffs.GetStacks(buffName, from as ObjAIBase)?.Last();
        }

        /// <summary>
        /// Whether or not the specified unit has a buff with the given name.
        /// </summary>
        /// <param name="unit">Unit to check.</param>
        /// <param name="b">Buff name to check for.</param>
        /// <param name="owner">Buff Owner filter</param>
        /// <returns>True/False</returns>
        public static bool HasBuff(AttackableUnit unit, string b, ObjAIBase owner = null)
        {
            return unit.Buffs.Has(b, owner);
        }

        /// <summary>
        /// Returns the current total buff stack with the given name.
        /// </summary>
        /// <param name="unit">Unit to check.</param>
        /// <param name="b">Buff name to check for.</param>
        /// <returns>Buff Stack Count</returns>
        public static int GetBuffStackCount(AttackableUnit unit, string b)
        {
            return unit.Buffs.Count(b);
        }

        /// <summary>
        /// Removes the specified buff from any AI units it is applied to and runs OnDeactivate callback for the buff's script.
        /// If the buff's BuffAddType is STACKS_AND_OVERLAPS, each stack is individually instanced, so only one stack is removed.
        /// </summary>
        /// <param name="buff">Buff instance to remove.</param>
        public static void RemoveBuff(Buff buff)
        {
            buff.TargetUnit.Buffs.RemoveAllStacks(buff.Name);
        }

        /// <summary>
        /// Removes all buffs of the given name from the specified AI unit and runs OnDeactivate callback for the buff's script.
        /// Even if the buff's BuffAddType is STACKS_AND_OVERLAPS, it will still remove all buff instances.
        /// </summary>
        /// <param name="target">AI unit to check.</param>
        /// <param name="buff">Buff name to remove.</param>
        public static void RemoveBuff(AttackableUnit target, string buff)
        {
            target.Buffs.RemoveAllStacks(buff, null);
        }

        /// <summary>
        /// Adds a shield to a AttackableUnit, with specified values
        /// </summary>
        /// <param name="source">SourceUnit of the shield</param>
        /// <param name="target">AI unit to shield.</param>
        /// <param name="amount">The amout of Health protection.</param>
        /// <param name="physical">If the shield blocks Physical damage.</param>
        /// <param name="magical">If the shield blocks Magical damage.</param>
        public static Shield AddShield
        (
            ObjAIBase source,
            AttackableUnit target,
            float amount,
            bool physical = true,
            bool magical = true
        )
        {
            var shield = new Shield(source, target, physical, magical, amount);
            target.AddShield(shield);
            return shield;
        }

        /// <summary>
        /// Remove a shield from a AttackableUnit immediately 
        /// </summary>
        /// <param name="target">AI unit to check.</param>
        /// <param name="shield">Shield to remove.</param>
        public static void RemoveShield(AttackableUnit target, Shield shield)
        {
            target.RemoveShield(shield);
        }

        /// <summary>
        /// Check if an AttackableUnit has any Shield 
        /// </summary>
        /// <param name="target">AI unit to check.</param>
        /// <param name="shield">Shield to look for.</param>
        public static bool HasShield(AttackableUnit target, Shield shield = null)
        {
            return target.HasShield(shield);
        }

        /// <summary>
        /// Removes the specified particle from ObjectManager and networks the removal to players.
        /// </summary>
        /// <param name="particle">Particle to remove.</param>
        public static void RemoveParticle(EffectEmitter particle)
        {
            if (particle != null)
            {
                particle.SetToRemove();
            }
        }

        /// <summary>
        /// Creates a new Minion with the specified parameters.
        /// </summary>
        /// <param name="owner">AI unit that owns this minion.</param>
        /// <param name="model">Internal name of the model of this minion.</param>
        /// <param name="name">Internal name of the minion.</param>
        /// <param name="position">Position to spawn at.</param>
        /// <param name="team">Minion team</param>
        /// <param name="skinId">ID of the skin the minion should use for its model.</param>
        /// <param name="ignoreCollision">Whether or not the minion should ignore collisions.</param>
        /// <param name="targetable">Whether or not the minion should be targetable.</param>
        /// <param name="isWard">If the minion is a Ward</param>
        /// <param name="isVisible">Whether or not this minion should be visible.</param>
        /// <param name="aiPaused">Whether or not this minion's AI is inactive.</param>
        /// <param name="aiScript">Minion AI script</param>
        /// <param name="initialLevel">Minion initial level</param>
        /// <param name="collisionRadius">Minion colision radius</param>
        /// <param name="visionRadius">Minion vision radius</param>
        /// <param name="stats">Minion inicial stat</param>
        /// <param name="minionStats">Minion inicial minionStats</param>
        /// <param name="targetingFlags">Flags determining targetability.</param>
        /// <param name="visibilityOwner">Specifies the only unit which should be able to see the minion.</param>
        /// <returns>New Minion instance.</returns>
        public static Minion AddMinion
        (
            ObjAIBase owner,
            string model,
            string name,
            Vector2 position,

            TeamId team = TeamId.TEAM_NEUTRAL,
            int skinId = 0,
            bool ignoreCollision = false,
            bool targetable = true,
            bool isWard = false,
            bool isVisible = true,
            string aiScript = "",
            bool aiPaused = true,
            int initialLevel = 1,
            int visionRadius = 1100,
            Stats? stats = null,
            SpellDataFlags targetingFlags = 0,
            ObjAIBase? visibilityOwner = null
        )
        {
            var m = new Minion(owner, position, model, name, team, skinId, ignoreCollision, targetable, isWard,
                visibilityOwner, stats, aiScript, initialLevel, visionRadius);

            m.Stats.IsTargetableToTeam = targetingFlags;
            Game.ObjectManager.AddObject(m);
            if (owner != null)
            {
                m.SetVisibleByTeam(owner.Team, isVisible);
            }
            m.PauseAI(aiPaused);
            return m;
        }

        /// <summary>
        /// Creates a stationary perception bubble at the given location.
        /// </summary>
        /// <param name="position">Position to spawn the perception bubble at.</param>
        /// <param name="radius">Size of the perception bubble.</param>
        /// <param name="duration">Number of seconds the perception bubble should exist.</param>
        /// <param name="team">Team the perception bubble should be owned by.</param>
        /// <param name="revealStealthed">Whether or not the perception bubble should reveal stealthed units while they are in range.</param>
        /// <param name="revealSpecificUnitOnly">Specific unit to reveal. Perception bubble will not reveal any other units when used. *NOTE* Currently does nothing.</param>
        /// <param name="collisionArea">Area around the perception bubble where units are not allowed to move into.</param>
        /// <param name="regionType">Unkown data. Use Default</param>
        /// <returns>New Region instance.</returns>
        public static Region AddPosPerceptionBubble
        (
            Vector2 position,
            float radius,
            float duration,
            TeamId team = TeamId.TEAM_NEUTRAL,
            bool revealStealthed = false,
            AttackableUnit revealSpecificUnitOnly = null,
            float collisionArea = 0f,
            RegionType regionType = RegionType.Default
        )
        {
            return new Region
            (
                team, position, regionType,
                visionTarget: revealSpecificUnitOnly,
                visionRadius: radius,
                revealStealth: revealStealthed,
                collisionRadius: collisionArea,
                lifetime: duration
            );
        }

        /// <summary>
        /// Creates a perception bubble which is attached to a specific unit.
        /// </summary>
        /// <param name="target">Unit to attach the perception bubble to.</param>
        /// <param name="radius">Size of the perception bubble.</param>
        /// <param name="duration">Number of seconds the perception bubble should exist.</param>
        /// <param name="team">Team the perception bubble should be owned by.</param>
        /// <param name="revealStealthed">Whether or not the perception bubble should reveal stealthed units while they are in range.</param>
        /// <param name="revealSpecificUnitOnly">Specific unit to reveal. Perception bubble will not reveal any other units when used. *NOTE* Currently does nothing.</param>
        /// <param name="collisionArea">Area around the perception bubble where units are not allowed to move into.</param>
        /// <param name="regionType">Unkown data. Use Default</param>
        /// <returns>New Region instance.</returns>
        public static Region AddUnitPerceptionBubble
        (
            AttackableUnit target,
            float radius,
            float duration,
            TeamId team = TeamId.TEAM_NEUTRAL,
            bool revealStealthed = false,
            AttackableUnit revealSpecificUnitOnly = null,
            float collisionArea = 0f,
            RegionType regionType = RegionType.Default
        )
        {
            return new Region
            (
                team, target.Position, regionType,
                collisionUnit: target,
                visionTarget: revealSpecificUnitOnly,
                visionRadius: radius,
                revealStealth: revealStealthed,
                collisionRadius: collisionArea,
                lifetime: duration
            );
        }

        /// <summary>
        /// Prints the specified string to the in-game chat.
        /// </summary>
        /// <param name="msg">String to print.</param>
        public static void PrintChat(string msg)
        {
            ChatManager.Send(msg);
        }

        /// <summary>
        /// Acquires all units in range filtered by filterFlags
        /// </summary>
        /// <param name="targetPos">Origin of the range to check.</param>
        /// <param name="range">Range to check from the target position.</param>
        /// <param name="sourceTeam">Source team to validate the targets</param>
        /// <param name="filterFlags">Flags to filters units types and teams</param>
        /// <returns>List of AttackableUnits.</returns>
        public static List<AttackableUnit> FilterUnitsInRange
        (
            AttackableUnit self,
            Vector2 targetPos,
            float range,
            SpellDataFlags filterFlags
        )
        {
            List<AttackableUnit> returnList = [];
            foreach (var obj in Game.Map.CollisionHandler.GetNearestObjects(new Circle(targetPos, range)))
            {
                if (obj is AttackableUnit u && Spell.IsValidTarget(self, u, filterFlags))
                {
                    returnList.Add(u);
                }
            }
            return [.. returnList.OrderBy(unit => Vector2.DistanceSquared(unit.Position, targetPos))];
        }


        /// <summary>
        /// Acquires all dead or alive AttackableUnits within the specified range of a target position.
        /// </summary>
        /// <param name="targetPos">Origin of the range to check.</param>
        /// <param name="range">Range to check from the target position.</param>
        /// <param name="isAlive">Whether or not to return alive AttackableUnits.</param>
        /// <returns>List of AttackableUnits.</returns>
        public static IEnumerable<AttackableUnit> EnumerateUnitsInRange
        (
            Vector2 targetPos,
            float range,
            bool isAlive = true
        )
        {
            foreach (var obj in Game.Map.CollisionHandler.GetNearestObjects(new Circle(targetPos, range)))
            {
                if (obj is AttackableUnit u && (!isAlive || !u.Stats.IsDead))
                {
                    yield return u;
                }
            }
        }

        /// <summary>
        /// Acquires all dead or alive AttackableUnits within the specified range of a unit.
        /// </summary>
        /// <param name="unit">Unit to check around.</param>
        /// <param name="range">Range to check from the target.</param>
        /// <param name="isAlive">Whether or not to return alive AttackableUnits.</param>
        /// <param name="selfIncluded">Include the unit itself to the list</param>
        /// <param name="excludeTeam">Exclude all units from this Team</param>
        /// <returns>List of AttackableUnits.</returns>
        public static List<AttackableUnit> GetUnitsInRange
        (
            AttackableUnit unit,
            float range,
            bool isAlive,

            bool selfIncluded = false,
            TeamId excludeTeam = TeamId.TEAM_UNKNOWN
        )
        {

            List<AttackableUnit> returnList = [.. EnumerateUnitsInRange(unit.Position, range, isAlive)];
            if (excludeTeam != TeamId.TEAM_UNKNOWN)
            {
                returnList = returnList.Where(x => x.Team != excludeTeam).ToList();
            }
            returnList = [.. returnList.OrderBy(x => Vector2.DistanceSquared(x.Position, unit.Position))];

            if (!selfIncluded)
            {
                returnList.Remove(unit);
            }

            return returnList;
        }

        /// <summary>
        /// Returns a new list of all players in the game.
        /// Players are designated as clients, this includes bot champions.
        /// Currently only a single champion is designated to each player.
        /// </summary>
        /// <returns></returns>
        public static List<Champion> GetAllPlayers()
        {
            var toreturn = new List<Champion>();
            foreach (var player in Game.PlayerManager.GetPlayers(true))
            {
                toreturn.Add(player.Champion);
            }
            return toreturn;
        }

        //Consider changing this to take bots into account too
        public static List<Champion> GetAllPlayersFromTeam(TeamId team)
        {
            return Game.ObjectManager.GetAllChampionsFromTeam(team);
        }

        /// <summary>
        /// Returns a new list of all champions in the game.
        /// </summary>
        /// <returns></returns>
        public static List<Champion> GetAllChampions()
        {
            return Game.ObjectManager.GetAllChampions();
        }

        /// <summary>
        /// Instantly cancels any dashes the specified unit is performing.
        /// </summary>
        /// <param name="unit">Unit to stop dashing.</param>
        public static void CancelDash(AttackableUnit unit)
        {
            // Allow the user to move the champion
            unit.SetDashingState(false);
        }

        /// <summary>
        /// Cast a Spell to a given positon or to a given target list
        /// </summary>
        /// <param name="spell">The spell to cast</param>
        /// <param name="pos">Spell target position</param>
        /// <param name="endPos">Spell target end position for line spells</param>
        /// <param name="fireWithoutCasting">Does not play animations and cast delays</param>
        /// <param name="overrideCastPos">Override the spell start position to fire</param>
        /// <param name="targets">Target for target spells</param>
        /// <param name="isForceCastingOrChanneling">Some spells need to be forced</param>
        /// <param name="overrideForceLevel">Set the spell level in case of ExtraSlots</param>
        /// <param name="updateAutoAttackTimer">The spell work as an autoattack</param>
        /// <param name="useAutoAttackSpell">The spell should use the current autoattack spell</param>
        public static void SpellCast
        (
            Spell spell,
            Vector2 pos,
            Vector2 endPos,
            bool fireWithoutCasting,
            Vector2 overrideCastPos = default,
            List<CastTarget> targets = null,
            bool isForceCastingOrChanneling = false,
            int overrideForceLevel = -1,
            bool updateAutoAttackTimer = false,
            bool useAutoAttackSpell = false
        )
        {
            var target = targets == null || targets.Count == 0 ? null : targets[0].Unit;
            spell.TryCast(
                target, pos.ToVector3(0), endPos.ToVector3(0),
                overrideForceLevel,
                false,
                fireWithoutCasting,
                useAutoAttackSpell,
                isForceCastingOrChanneling,
                updateAutoAttackTimer,
                overrideCastPos != default,
                overrideCastPos.ToVector3(0)
            );
        }

        /// <summary>
        /// Creates a DeathData variable for use with the AttackableUnit.Die() function.
        /// </summary>
        /// <param name="zombify">Whether or not the unit should become a zombie after death.</param>
        /// <param name="deathType">Type of death. Values unknown.</param>
        /// <param name="unit">Unit that died.</param>
        /// <param name="killer">Killer of the unit.</param>
        /// <param name="dmgType">Type of damage that caused the death.</param>
        /// <param name="dmgSource">Source of the damage that caused the death.</param>
        /// <param name="duration">Time until the death completes (fade-out?).</param>
        /// <returns></returns>
        public static DeathData CreateDeathData
        (
            bool zombify,
            byte deathType,
            AttackableUnit unit,
            AttackableUnit killer,
            DamageType dmgType,
            DamageSource dmgSource,
            float duration
        )
        {
            return new DeathData
            {
                BecomeZombie = zombify,
                DieType = (DieType)deathType,
                Unit = unit,
                Killer = killer,
                DamageType = dmgType,
                DamageSource = dmgSource,
                DeathDuration = duration
            };
        }

        public static ItemData GetItemData(int Id)
        {
            return ContentManager.GetItemData(Id);
        }

        public static void PlaySound(string soundName, AttackableUnit soundOwner)
        {
            Game.PacketNotifier.NotifyS2C_PlaySound(soundName, soundOwner);
        }

        public static void Heal(AttackableUnit target, float ammount, AttackableUnit source = null, AddHealthType type = AddHealthType.HEAL, IEventSource sourceScript = null)
        {
            target.TakeHeal(new HealData(target, ammount, source, type, sourceScript));
        }

        public static void SetFullHealth(AttackableUnit unit)
        {
            Heal(unit, unit.Stats.HealthPoints.Total, type: AddHealthType.RGEN);
        }

        public static void RestoreMana(AttackableUnit unit, float ammount)
        {
            unit.RestorePAR(ammount);
        }

        public static void RemoveMana(AttackableUnit unit, float ammount)
        {
            unit.SpendPAR(ammount);
        }

        /// <summary>
        /// Creates a new instance of a GameScriptTimer with the specified arguments.
        /// </summary>
        /// <param name="duration">Time till the timer ends.</param>
        /// <param name="callback">Action to perform when the timer ends.</param>
        /// <returns>New GameScriptTimer instance.</returns>
        [Obsolete]
        public static GameScriptTimer CreateTimer(float duration, Action callback, bool executeImmediately = true, bool repeat = false)
        {
            var newTimer = new GameScriptTimer(duration, callback, executeImmediately, !repeat);
            Game.AddGameScriptTimer(newTimer);

            return newTimer;
        }
    }
}

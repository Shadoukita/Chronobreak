using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using GameServerCore;
using GameServerCore.Enums;
using GameServerCore.NetInfo;
using GameServerLib.GameObjects;
using GameServerLib.Services;
using Chronobreak.GameServer.GameObjects;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;
using Chronobreak.GameServer.Logging;
using log4net;

namespace Chronobreak.GameServer
{
    // TODO: refactor this class

    /// <summary>
    /// Class that manages addition, removal, and updating of all GameObjects, their visibility, and buffs.
    /// </summary>
    public class ObjectManager
    {
        private static ILog _logger = LoggerProvider.GetLogger();

        private List<GameObject> _objectsToAdd = [];
        private List<GameObject> _objectsToRemove = [];
        private SortedDictionary<uint, GameObject> _objects = [];
        private SortedDictionary<uint, Champion> _champions = [];
        private SortedDictionary<uint, BaseTurret> _turrets = [];
        private SortedDictionary<uint, Inhibitor> _inhibitors = [];


        private bool _currentlyInUpdate = false;

        /// <summary>
        /// List of all possible teams in League of Legends. Normally there are only three.
        /// </summary>
        public List<TeamId> Teams { get; private set; }

        /// <summary>
        /// Instantiates all GameObject Dictionaries in ObjectManager.
        /// </summary>
        /// <param name="game">Game instance.</param>
        public ObjectManager()
        {
            Teams = Enum.GetValues(typeof(TeamId)).Cast<TeamId>().ToList();
        }

        private void UpdateStats()
        {
            foreach (var obj in _objects.Values)
            {
                obj.UpdateStats();
            }
        }

        private void UpdateActions()
        {
            foreach (var obj in _objects.Values)
            {
                try
                {
                    obj.Update();
                }
                catch (Exception e)
                {
                    _logger.Error(null, e);
                }
            }
        }

        /// <summary>
        /// Function called every tick of the game.
        /// </summary>
        /// <param name="diff">Number of milliseconds since this tick occurred.</param>
        internal void Update()
        {
            _currentlyInUpdate = true;

            UpdateStats();
            UpdateActions();

            // It is now safe to call RemoveObject at any time,
            // but compatibility with the older remove method remains.
            foreach (var obj in _objects.Values)
            {
                if (obj.IsToRemove())
                {
                    RemoveObject(obj);
                }
            }

            foreach (var obj in _objectsToRemove)
            {
                _objects.Remove(obj.NetId);
            }
            _objectsToRemove.Clear();

            int oldObjectsCount = _objects.Count;

            foreach (var obj in _objectsToAdd)
            {
                _objects.Add(obj.NetId, obj);
            }
            _objectsToAdd.Clear();

            var players = Game.PlayerManager.GetPlayers(includeBots: false);

            int i = 0;
            foreach (GameObject obj in _objects.Values)
            {
                VisionService.UpdateTeamsVision(obj);
                if (i++ < oldObjectsCount)
                {
                    // Don't call on newly created objects
                    // because Update has not yet been called on them
                    obj.LateUpdate();
                }

                foreach (var kv in players)
                {
                    SyncObject(obj, kv);
                }

                obj.OnAfterSync();
            }

            Game.PacketNotifier.NotifyOnReplication();
            Game.PacketNotifier.NotifyWaypointGroup();

            _currentlyInUpdate = false;
        }

        /// <summary>
        /// Normally, objects will spawn at the end of the frame, but calling this function will force the teams' and players' vision of that object to update and send out a spawn notification.
        /// </summary>
        /// <param name="obj">Object to spawn.</param>
        public void SpawnObject(GameObject obj)
        {
            VisionService.UpdateTeamsVision(obj);

            var players = Game.PlayerManager.GetPlayers(includeBots: false);
            foreach (var kv in players)
            {
                SyncObject(obj, kv, forceSpawn: true);
            }

            obj.OnAfterSync();
        }

        // Called in response to a SpawnRequest
        public void OnReconnect(int userId, TeamId team, bool hard)
        {
            foreach (GameObject obj in _objects.Values)
            {
                obj.OnReconnect(userId, team, hard);
            }
        }

        public void SpawnObjects(ClientInfo clientInfo)
        {
            foreach (GameObject obj in _objects.Values)
            {
                SyncObject(obj, clientInfo, forceSpawn: true);
            }
        }

        /// <summary>
        /// Updates the player's vision, which may not be tied to the team's vision, sends a spawn notification or updates if the object is already spawned.
        /// </summary>
        public void SyncObject(GameObject obj, ClientInfo clientInfo, bool forceSpawn = false)
        {
            int cid = clientInfo.ClientId;
            TeamId team = clientInfo.Team;
            Champion champion = clientInfo.Champion;

            bool nearSighted = champion.Status.HasFlag(StatusFlags.NearSighted);
            bool shouldBeVisibleForPlayer;

            if (obj is EffectEmitter particle && particle.VisibilityOwner != null)
            {
                shouldBeVisibleForPlayer = particle.VisibilityOwner == champion;
            }
            else if (obj is EffectEmitter emitter && emitter.VisibilityOwner != null)
            {
                shouldBeVisibleForPlayer = emitter.VisibilityOwner == champion;
            }
            else
            {
                var hasVision = VisionService.UnitHasVisionOn(champion, obj, nearSighted);
                shouldBeVisibleForPlayer = !obj.IsAffectedByFoW || (nearSighted ? hasVision : obj.IsVisibleByTeam(champion.Team));
            }

            obj.Sync(cid, team, shouldBeVisibleForPlayer, forceSpawn);
        }

        /// <summary>
        /// Adds a GameObject to the dictionary of GameObjects in ObjectManager.
        /// </summary>
        /// <param name="o">GameObject to add.</param>
        public void AddObject(GameObject o)
        {
            if (o != null)
            {
                _objectsToRemove.Remove(o);

                if (_currentlyInUpdate)
                {
                    _objectsToAdd.Add(o);
                }
                else
                {
                    if (!_objects.TryAdd(o.NetId, o))
                    {
                        _logger.Error($"Can't add object \"{o.Name}\"(ID: {o.NetId}) to ObjectManager!");
                    }
                }
                o.OnAdded();
            }
        }

        /// <summary>
        /// Removes a GameObject from the dictionary of GameObjects in ObjectManager.
        /// </summary>
        /// <param name="o">GameObject to remove.</param>
        public void RemoveObject(GameObject o)
        {
            if (o != null)
            {
                _objectsToAdd.Remove(o);

                if (_currentlyInUpdate)
                {
                    _objectsToRemove.Add(o);
                }
                else
                {
                    _objects.Remove(o.NetId);
                }

                VisionService.ObjectRemoved(o);
                o.OnRemoved();
            }
        }

        /// <summary>
        /// Gets a new Dictionary of all NetID,GameObject pairs present in the dictionary of objects in ObjectManager.
        /// </summary>
        /// <returns>Dictionary of NetIDs and the GameObjects that they refer to.</returns>
        public Dictionary<uint, GameObject> GetObjects()
        {
            var ret = new Dictionary<uint, GameObject>();
            foreach (var obj in _objects)
            {
                ret.Add(obj.Key, obj.Value);
            }

            return ret;
        }

        /// <summary>
        /// Gets a GameObject from the list of objects in ObjectManager that is identified by the specified NetID.
        /// </summary>
        /// <param name="id">NetID to check.</param>
        /// <returns>GameObject instance that has the specified NetID. Null otherwise.</returns>
        public GameObject GetObjectById(uint id)
        {
            GameObject obj = _objectsToAdd.Find(o => o.NetId == id);

            if (obj == null)
            {
                obj = _objects.GetValueOrDefault(id, null);
            }

            return obj;
        }

        /// <summary>
        /// Gets a list of all GameObjects of type AttackableUnit that are within a certain distance from a specified position.
        /// </summary>
        /// <param name="checkPos">Vector2 position to check.</param>
        /// <param name="range">Distance to check.</param>
        /// <param name="onlyAlive">Whether dead units should be excluded or not.</param>
        /// <returns>List of all AttackableUnits within the specified range and of the specified alive status.</returns>
        public List<AttackableUnit> GetUnitsInRange(Vector2 checkPos, float range, bool onlyAlive = false)
        {
            var units = new List<AttackableUnit>();
            foreach (var kv in _objects)
            {
                if (kv.Value is AttackableUnit u && Vector2.DistanceSquared(checkPos, u.Position) <= range * range && (onlyAlive && !u.Stats.IsDead || !onlyAlive))
                {
                    units.Add(u);
                }
            }

            return units;
        }

        /// <summary>
        /// Counts the number of units attacking a specified GameObject of type AttackableUnit.
        /// </summary>
        /// <param name="target">AttackableUnit potentially being attacked.</param>
        /// <returns>Number of units attacking target.</returns>
        public int CountUnitsAttackingUnit(AttackableUnit target)
        {
            return GetObjects().Count(x =>
                x.Value is ObjAIBase aiBase &&
                aiBase.Team == target.Team.GetEnemyTeam() &&
                !aiBase.Stats.IsDead &&
                aiBase.TargetUnit != null &&
                aiBase.TargetUnit == target
            );
        }

        /// <summary>
        /// Forces all GameObjects of type ObjAIBase to stop targeting the specified AttackableUnit.
        /// </summary>
        /// <param name="target">AttackableUnit that should be untargeted.</param>
        public void StopTargeting(AttackableUnit target)
        {
            foreach (var kv in _objects)
            {
                if (kv.Value is ObjAIBase ai)
                {
                    ai.Untarget(target);
                }
            }
        }

        /// <summary>
        /// Adds a GameObject of type Champion to the list of Champions in ObjectManager.
        /// </summary>
        /// <param name="champion">Champion to add.</param>
        public void AddChampion(Champion champion)
        {
            _champions.Add(champion.NetId, champion);
        }

        /// <summary>
        /// Removes a GameObject of type Champion from the list of Champions in ObjectManager.
        /// </summary>
        /// <param name="champion">Champion to remove.</param>
        public void RemoveChampion(Champion champion)
        {
            _champions.Remove(champion.NetId);
        }

        /// <summary>
        /// Gets a new list of all Champions found in the list of Champions in ObjectManager.
        /// </summary>
        /// <returns>List of all valid Champions.</returns>
        public List<Champion> GetAllChampions()
        {
            var champs = new List<Champion>();
            foreach (var kv in _champions)
            {
                var c = kv.Value;
                if (c != null)
                {
                    champs.Add(c);
                }
            }

            return champs;
        }

        /// <summary>
        /// Gets a new list of all Champions of the specified team found in the list of Champios in ObjectManager.
        /// </summary>
        /// <param name="team">TeamId.BLUE/PURPLE/NEUTRAL</param>
        /// <returns>List of valid Champions of the specified team.</returns>
        public List<Champion> GetAllChampionsFromTeam(TeamId team)
        {
            if (team is TeamId.TEAM_UNKNOWN)
            {
                return _champions.Select(x => x.Value).ToList();
            }

            var champs = new List<Champion>();
            foreach (var kv in _champions)
            {
                var c = kv.Value;
                if (c.Team == team)
                {
                    champs.Add(c);
                }
            }

            return champs;
        }

        /// <summary>
        /// Gets a list of all GameObjects of type Champion that are within a certain distance from a specified position.
        /// </summary>
        /// <param name="checkPos">Vector2 position to check.</param>
        /// <param name="range">Distance to check.</param>
        /// <param name="onlyAlive">Whether dead Champions should be excluded or not.</param>
        /// <returns>List of all Champions within the specified range of the position and of the specified alive status.</returns>
        public List<Champion> GetChampionsInRange(Vector2 checkPos, float range, bool onlyAlive = false)
        {
            var champs = new List<Champion>();
            foreach (var kv in _champions)
            {
                var c = kv.Value;
                if (Vector2.DistanceSquared(checkPos, c.Position) <= range * range)
                {
                    if (onlyAlive && !c.Stats.IsDead || !onlyAlive)
                    {
                        champs.Add(c);
                    }
                }
            }

            return champs;
        }

        /// <summary>
        /// Gets a list of all GameObjects of type Champion that are within a certain distance from a specified position.
        /// </summary>
        /// <param name="checkPos">Vector2 position to check.</param>
        /// <param name="range">Distance to check.</param>
        /// <param name="onlyAlive">Whether dead Champions should be excluded or not.</param>
        /// <returns>List of all Champions within the specified range of the position and of the specified alive status.</returns>
        public List<Champion> GetChampionsInRangeFromTeam(Vector2 checkPos, float range, TeamId team, bool onlyAlive = false)
        {
            var champs = new List<Champion>();
            foreach (var kv in _champions)
            {
                var c = kv.Value;
                if (Vector2.DistanceSquared(checkPos, c.Position) <= range * range)
                {
                    if (c.Team == team && (onlyAlive && !c.Stats.IsDead || !onlyAlive))
                    {
                        champs.Add(c);
                    }
                }
            }

            return champs;
        }

        public List<IExperienceOwner> GetExperienceOwnersInRangeFromTeam(Vector2 checkPos, float range, TeamId team, bool onlyAlive = false)
        {
            List<IExperienceOwner> experienceOwners = [];

            GetChampionsInRangeFromTeam(checkPos, range, team, onlyAlive).ForEach(x => experienceOwners.Add(x));

            foreach (var kv in _objects)
            {
                if (kv.Value is IExperienceOwner expOwner && Vector2.DistanceSquared(checkPos, expOwner.Experience.Owner.Position) <= range * range)
                {
                    if (expOwner.Experience.Owner.Team == team && (onlyAlive && !expOwner.Experience.Owner.Stats.IsDead || !onlyAlive))
                    {
                        experienceOwners.Add(expOwner);
                    }
                }
            }

            return experienceOwners;
        }

        internal void LoadScripts()
        {
            _currentlyInUpdate = true;
            foreach (var unit in _objects.Values)
            {
                if (unit is ObjAIBase obj)
                {
                    obj.LoadCharScript(obj.Spells.Passive);
                    obj.Buffs.ReloadScripts();
                    obj.ReloadSpellsScripts();
                }
            }
            _currentlyInUpdate = false;
        }
    }
}

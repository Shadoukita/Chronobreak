namespace Commands;

public class DebugModeCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    private static ILog _logger = LoggerProvider.GetLogger();
    private float lastDrawTime = Game.Time.GameTime;
    private int _userId;
    private Champion _userChampion;
    private static readonly Dictionary<uint, EffectEmitter> _circleParticles = [];
    private static readonly Dictionary<uint, List<EffectEmitter>> _arrowParticlesList = [];
    [Flags]
    private enum DebugMode : int
    {
        None = 1 << 0,
        Self = 1 << 1,
        Champions = 1 << 2,
        Minions = 1 << 3,
        Projectiles = 1 << 4,
        /*Sectors,*/
        PathFinding = 1 << 6,
        All = 1 << 7
    }

    private DebugMode Toggle(DebugMode flagToToggle)
    {
        return _debugMode ^= flagToToggle;
    }

    private DebugMode _debugMode = DebugMode.None;

    public override string Command => "debugmode";
    private string[] _modes = Enum.GetNames(typeof(DebugMode)).Select(x => x.ToLowerInvariant()).ToArray();
    public override string Syntax => $"{Command} {string.Join('/', _modes)}";

    private const float _debugCircleScale = 0.01f;

    private EffectEmitter AddParticle(GameObject bindObj, Vector2 startPos, string particleName, float scale, Vector3 direction)
    {
        return null;//ApiFunctionManager.AddParticleTarget(bindObj, particleName, bindObj, startPos, 0.200f, scale, direction);
    }

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        _userId = clientId;
        _userChampion = champion;

        var split = arguments.ToLower().Split(' ');

        if (split.Length <= 1)
        {
            ChatManager.Send($"Currently debugging {_debugMode & ~DebugMode.None}.");
            return;
        }

        if (Enum.TryParse(typeof(DebugMode), split[1], true, out var requestDbgMode))
        {
            var requestedDebugMode = (DebugMode)requestDbgMode;
            if (requestedDebugMode != DebugMode.None)
            {
                if (_debugMode.HasFlag(requestedDebugMode))
                {
                    var message = $"Stopped debugging {requestedDebugMode}.";
                    ChatManager.Send(message);
                    _logger.Debug(message);
                }
                else
                {
                    // Arbitrary ratio is required for the DebugCircle particle to look accurate
                    var circlesize = _debugCircleScale * _userChampion.PathfindingRadius;

                    var startdebugmsg = $"Started debugging {requestedDebugMode}. Your Debug Circle Radius: {_debugCircleScale} * {_userChampion.PathfindingRadius} = {circlesize}";
                    _logger.Debug(startdebugmsg);
                    ChatManager.Send(startdebugmsg);
                    ApiHandlers.PacketNotifier.NotifyCreateUnitHighlight(clientId, _userChampion);
                    if (_debugMode == DebugMode.None)
                    {
                        ApiHandlers.PacketNotifier.NotifyCreateUnitHighlight(clientId, _userChampion);
                    }
                }
                _debugMode = Toggle(requestedDebugMode);
            }
            else
            {
                _debugMode = DebugMode.None;
            }

            if (requestedDebugMode == 0 || _debugMode == DebugMode.None)
            {
                _debugMode = DebugMode.None;
                var message = "Stopped all debugging";
                ChatManager.Send(message);
                _logger.Debug(message);
                ApiHandlers.PacketNotifier.NotifyRemoveUnitHighlight(clientId, _userChampion);

                if (_circleParticles.Count != 0)
                {
                    foreach (var particle in _circleParticles)
                    {
                        particle.Value.SetToRemove();
                    }
                    _circleParticles.Clear();
                }
                if (_arrowParticlesList.Count != 0)
                {
                    foreach (var particleList in _arrowParticlesList)
                    {
                        foreach (var arrowparticle in particleList.Value)
                        {
                            arrowparticle.SetToRemove();
                        }
                        particleList.Value.Clear();
                    }
                    _arrowParticlesList.Clear();
                }
            }
        }
        else
        {
            SyntaxError();
            ShowSyntax();
        }
    }

    public override void Update()
    {
        if (Game.Time.GameTime - lastDrawTime > 100.0f)
        {
            if (_debugMode.HasFlag(DebugMode.All))
            {
                DrawAll(_userId);
            }
            else
            {
                if (_debugMode.HasFlag(DebugMode.Self))
                {
                    DrawSelf(_userId);
                }
                if (_debugMode.HasFlag(DebugMode.Champions))
                {
                    DrawChampions(_userId);
                }
                if (_debugMode.HasFlag(DebugMode.Minions))
                {
                    DrawMinions(_userId);
                }
                if (_debugMode.HasFlag(DebugMode.Projectiles))
                {
                    DrawProjectiles(_userId);
                }
                /*
                else if (_debugMode == DebugMode.Sectors)
                {
                    DrawSectors(_userId);
                }
                */
                if (_debugMode.HasFlag(DebugMode.PathFinding))
                {
                    DrawPathFinding(_userId);
                }
            }

            lastDrawTime = Game.Time.GameTime;
        }
    }

    // Draws your unit's collision radius and waypoints
    public void DrawSelf(int userId)
    {
        DrawAttackableUnit(_userChampion, userId);
    }

    void DrawAttackableUnit(AttackableUnit u, int userId = -1)
    {
        // Arbitrary ratio is required for the DebugCircle particle to look accurate
        var circlesize = _debugCircleScale * u.PathfindingRadius;
        if (u.PathfindingRadius < 5)
        {
            circlesize = _debugCircleScale * 35;
        }

        // Clear circle particles every draw in case the unit changes its position
        if (_circleParticles.ContainsKey(u.NetId))
        {
            if (_circleParticles[u.NetId] != null)
            {
                _circleParticles.Remove(u.NetId);
            }
        }

        var circleparticle = AddParticle(null, u.Position, "DebugCircle_green.troy", circlesize, default);
        _circleParticles.Add(u.NetId, circleparticle);
        //Game.PacketNotifier.NotifyFXCreateGroup(circleparticle, userId);

        if (u.Waypoints.Count > 0)
        {
            // Clear arrow particles every draw in case the unit changes its waypoints
            if (_arrowParticlesList.ContainsKey(u.NetId))
            {
                if (_arrowParticlesList[u.NetId].Count != 0)
                {
                    _arrowParticlesList[u.NetId].Clear();
                    _arrowParticlesList.Remove(u.NetId);
                }
            }

            for (int waypoint = u.CurrentWaypointKey; waypoint < u.Waypoints.Count; waypoint++)
            {
                var current = u.Waypoints[waypoint - 1];

                var wpTarget = u.Waypoints[waypoint];

                // Makes the arrow point to the next waypoint
                var to = Vector2.Normalize(wpTarget - current);
                if (u.Waypoints.Count - 1 > waypoint)
                {
                    var nextTargetWp = u.Waypoints[waypoint + 1];
                    to = Vector2.Normalize(nextTargetWp - u.Waypoints[waypoint]);
                }
                var direction = to.ToVector3(0);

                if (!_arrowParticlesList.ContainsKey(u.NetId))
                {
                    _arrowParticlesList.Add(u.NetId, []);
                }

                var arrowparticle = AddParticle(null, wpTarget, "DebugArrow_green.troy", 0.5f, direction);
                _arrowParticlesList[u.NetId].Add(arrowparticle);

                //Game.PacketNotifier.NotifyFXCreateGroup(arrowparticle, userId);

                if (waypoint >= u.Waypoints.Count)
                {
                    _logger.Debug("Waypoints Drawn: " + waypoint);
                }
            }
        }
    }

    // Draws the collision radius and waypoints of all champions
    public void DrawChampions(int userId)
    {
        // Same method as DebugSelf just for every champion
        foreach (var champion in ApiHandlers.GetAllChampions())
        {
            DrawAttackableUnit(champion, userId);
        }
    }

    // Draws the collision radius and waypoints of all Minions
    public void DrawMinions(int userId)
    {
        // Same method as DebugSelf just for every minion
        foreach (GameObject obj in ApiHandlers.GetGameObjects().Values)
        {
            if (obj is Minion minion)
            {
                DrawAttackableUnit(minion, userId);
            }
        }
    }

    // Draws the collision radius and waypoints of all projectiles
    public void DrawProjectiles(int userId)
    {
        var tempObjects = ApiHandlers.GetGameObjects();

        foreach (KeyValuePair<uint, GameObject> obj in tempObjects)
        {
            if (obj.Value is SpellMissile missile)
            {
                // Arbitrary ratio is required for the DebugCircle particle to look accurate
                var circlesize = _debugCircleScale * missile.CollisionRadius;
                if (missile.CollisionRadius < 5)
                {
                    circlesize = _debugCircleScale * 35;
                }

                // Clear circle particles every draw in case the unit changes its position
                if (_circleParticles.ContainsKey(missile.NetId))
                {
                    if (_circleParticles[missile.NetId] != null)
                    {
                        _circleParticles.Remove(missile.NetId);
                    }
                }

                var circleparticle = AddParticle(null, missile.Position, "DebugCircle_green.troy", circlesize, default);
                _circleParticles.Add(missile.NetId, circleparticle);
                //Game.PacketNotifier.NotifyFXCreateGroup(circleparticle, userId);

                if (missile.CastInfo.Targets.Any() || (missile.CastInfo.TargetPosition != Vector3.Zero && missile.CastInfo.TargetPositionEnd != Vector3.Zero))
                {
                    // Clear arrow particles every draw in case the unit changes its waypoints
                    if (_arrowParticlesList.ContainsKey(missile.NetId))
                    {
                        if (_arrowParticlesList[missile.NetId].Count != 0)
                        {
                            _arrowParticlesList[missile.NetId].Clear();
                            _arrowParticlesList.Remove(missile.NetId);
                        }
                    }

                    if (missile.CastInfo.Targets.Any())
                    {
                        if (!_arrowParticlesList.ContainsKey(missile.NetId))
                        {
                            _arrowParticlesList.Add(missile.NetId, []);
                        }

                        var current = missile.Position;

                        var wpTarget = missile.Destination;

                        // Makes the arrow point to the target
                        var to = Vector2.Normalize(wpTarget - current);

                        var direction = to.ToVector3(0);
                        var arrowparticle = AddParticle(null, wpTarget, "DebugArrow_green.troy", 0.5f, direction);
                        _arrowParticlesList[missile.NetId].Add(arrowparticle);

                        //Game.PacketNotifier.NotifyFXCreateGroup(arrowparticle, userId);
                    }
                    else if (missile is SpellCircleMissile skillshot)
                    {
                        if (!_arrowParticlesList.ContainsKey(missile.NetId))
                        {
                            _arrowParticlesList.Add(missile.NetId, []);
                        }

                        var current = missile.CastInfo.SpellCastLaunchPosition.ToVector2();

                        var wpTarget = skillshot.Destination;

                        // Points the arrow towards the target
                        var dirTangent = Extensions.Rotate(missile.Direction.ToVector2(), 90.0f) * missile.CollisionRadius;
                        var dirTangent2 = Extensions.Rotate(missile.Direction.ToVector2(), 270.0f) * missile.CollisionRadius;

                        var arrowParticleStart = AddParticle(null, current, "DebugArrow_green.troy", 0.5f, missile.Direction);
                        _arrowParticlesList[missile.NetId].Add(arrowParticleStart);
                        //Game.PacketNotifier.NotifyFXCreateGroup(arrowParticleStart, userId);

                        var arrowParticleEnd = AddParticle(null, wpTarget, "DebugArrow_green.troy", 0.5f, missile.Direction);
                        _arrowParticlesList[missile.NetId].Add(arrowParticleEnd);
                        //Game.PacketNotifier.NotifyFXCreateGroup(arrowParticleEnd, userId);

                        var arrowParticleEnd2Temp = AddParticle(null, wpTarget + dirTangent, "Global_Indicator_Line_Beam.troy", 0.0f, missile.Direction);
                        _arrowParticlesList[missile.NetId].Add(arrowParticleEnd2Temp);
                        //Game.PacketNotifier.NotifyFXCreateGroup(arrowParticleEnd2Temp, userId);

                        var arrowParticleStart2 = AddParticle(arrowParticleEnd2Temp, current + dirTangent, "Global_Indicator_Line_Beam.troy", 1.0f, missile.Direction);
                        _arrowParticlesList[missile.NetId].Add(arrowParticleStart2);
                        //Game.PacketNotifier.NotifyFXCreateGroup(arrowParticleStart2, userId);

                        var arrowParticleEnd3Temp = AddParticle(null, wpTarget + dirTangent2, "Global_Indicator_Line_Beam.troy", 0.0f, missile.Direction);
                        _arrowParticlesList[missile.NetId].Add(arrowParticleEnd3Temp);
                        //Game.PacketNotifier.NotifyFXCreateGroup(arrowParticleEnd3Temp, userId);

                        var arrowParticleStart3 = AddParticle(arrowParticleEnd3Temp, current + dirTangent2, "Global_Indicator_Line_Beam.troy", 1.0f, missile.Direction);
                        _arrowParticlesList[missile.NetId].Add(arrowParticleStart3);
                        //Game.PacketNotifier.NotifyFXCreateGroup(arrowParticleStart3, userId);
                    }
                }
            }
        }
    }
    /*
    // Draws the effected area of all sectors
    public void DrawSectors(int userId)
    {
        var tempObjects = Game.ObjectManager.GetObjects();

        foreach (KeyValuePair<uint, GameObject> obj in tempObjects)
        {
            if (obj.Value is SpellSector sector)
            {
                // Arbitrary ratio is required for the DebugCircle particle to look accurate
                var circlesize = _debugCircleScale * sector.CollisionRadius;
                if (sector.Parameters.Width < 5)
                {
                    circlesize = _debugCircleScale * 35;
                }

                // Clear circle particles every draw in case the unit changes its position
                if (_circleParticles.ContainsKey(sector.NetId))
                {
                    if (_circleParticles[sector.NetId] != null)
                    {
                        _circleParticles.Remove(sector.NetId);
                    }
                }

                if (sector.CurrentCastInfo.Target != null || (sector.CastInfo.TargetPosition != Vector3.Zero && sector.CastInfo.TargetPositionEnd != Vector3.Zero))
                {
                    // Clear arrow particles every draw in case the unit changes its waypoints
                    if (_arrowParticlesList.ContainsKey(sector.NetId))
                    {
                        if (_arrowParticlesList[sector.NetId].Count != 0)
                        {
                            _arrowParticlesList[sector.NetId].Clear();
                            _arrowParticlesList.Remove(sector.NetId);
                        }
                    }

                    if (sector is SpellSectorPolygon polygon)
                    {
                        if (!_arrowParticlesList.ContainsKey(polygon.NetId))
                        {
                            _arrowParticlesList.Add(polygon.NetId, new List<Particle>());
                        }

                        var current = polygon.Position;
                        var bindObj = polygon.Parameters.BindObject;
                        var wpTarget = polygon.CastInfo.TargetPositionEnd;

                        if (bindObj == null)
                        {
                            return;
                        }

                        var circleparticle = AddParticle(null, polygon.Position, "DebugCircle_green.troy", circlesize, default);
                        _circleParticles.Add(polygon.NetId, circleparticle);
                        //Game.PacketNotifier.NotifyFXCreateGroup(circleparticle, userId);

                        foreach (Vector2 vert in polygon.GetPolygonVertices())
                        {
                            var truePos = bindObj.Position + Extensions.Rotate(vert, -Extensions.UnitVectorToAngle(bindObj.Direction.ToVector2()) + 90f);
                            var arrowParticleVert = AddParticle(null, truePos, "DebugArrow_green.troy", 0.5f, bindObj.Direction);
                            _arrowParticlesList[polygon.NetId].Add(arrowParticleVert);
                            //Game.PacketNotifier.NotifyFXCreateGroup(arrowParticleVert, userId);
                        }
                    }
                }
            }
        }
    }
    */

    // Draws your unit's collision radius and waypoints
    public void DrawPathFinding(int userId)
    {
        DrawSelf(userId);
        foreach (var cell in ApiHandlers.MapNavGrid.GetAllCellsInRange(_userChampion.Position, 500))
        {
            var cp = AddParticle(_userChampion, ApiHandlers.MapNavGrid.TranslateFromNavGrid(cell.GetCenter()),
                cell.HasFlag(NavigationGridCellFlags.NOT_PASSABLE) ? "DebugCircle_green.troy" : "DebugCircle_red.troy", 0.25f, default);
        }

        /*foreach (var unit in ApiFunctionManager.GetUnitsInRange(_userChampion, _userChampion.VisionRadius, true))
        {
            var dir = Vector2.Normalize(unit.Position - _userChampion.Position);
            AddParticle(_userChampion, unit.Position, "Global_Indicator_Line_Beam.troy", 1.0f, dir.ToVector3(0));
            AddParticle(_userChampion, unit.Position + new Vector2(dir.Y, -dir.X) * unit.CollisionRadius, "Global_Indicator_Line_Beam.troy", 1.0f, dir.ToVector3(0));
            AddParticle(_userChampion, unit.Position + new Vector2(-dir.Y, dir.X) * unit.CollisionRadius, "Global_Indicator_Line_Beam.troy", 1.0f, dir.ToVector3(0));
            AddParticle(_userChampion, unit.Position, "DebugCircle_green.troy", _debugCircleScale * unit.CollisionRadius, default);
        }*/
    }

    // Draws the effected area of all game objects
    public void DrawAll(int userId)
    {
        var tempObjects = ApiHandlers.GetGameObjects();

        foreach (GameObject obj in tempObjects.Values)
        {
            // Arbitrary ratio is required for the DebugCircle particle to look accurate
            var circlesize = _debugCircleScale * obj.PathfindingRadius;
            if (obj.PathfindingRadius < 5)
            {
                circlesize = _debugCircleScale * 35;
            }

            // Clear circle particles every draw in case the unit changes its position
            if (_circleParticles.ContainsKey(obj.NetId))
            {
                if (_circleParticles[obj.NetId] != null)
                {
                    _circleParticles.Remove(obj.NetId);
                }
            }

            var circleparticle = AddParticle(null, obj.Position, "DebugCircle_green.troy", circlesize, default);
            _circleParticles.Add(obj.NetId, circleparticle);
            //Game.PacketNotifier.NotifyFXCreateGroup(circleparticle, userId);

            // TODO: Add check for AttackableUnit and draw waypoints.
        }
    }
}
using System.Collections.Generic;

namespace MapScripts.Map8
{
    public static class LevelScriptObjects
    {
        private static Dictionary<ObjectType, List<MapObject>> _mapObjects;
        static readonly List<CapturePoint> CapturePoints = new();
        static readonly List<MapObject> MinionGraveyards = new();
        static readonly List<MapObject> MinionSpawns = new();
        static readonly List<MapObject> HealthRelics = new();
        static readonly List<MapObject> LesserRelics = new();
        static readonly List<MapObject> SpeedShrines = new();
        static readonly List<MapObject> LargeRelics = new();
        static readonly List<MapObject> ExitPortals = new();
        static readonly List<MapObject> SpawnInfo = new();
        static readonly List<MapObject> Neutrals = new();
        static readonly List<MapObject> Sigils = new();

        public static void LoadObjects(Dictionary<ObjectType, List<MapObject>> mapObjects)
        {
            _mapObjects = mapObjects;

            LoadInfoPoints();
        }

        public static void OnMatchStart()
        {
            foreach (var infoPoint in CapturePoints)
            {
                NotifyHandleCapturePointUpdate(infoPoint.Index, infoPoint.Parent.NetId, 0, 0, CapturePointUpdateCommand.AttachToObject);
            }
        }

        static void LoadInfoPoints()
        {
            byte pointIndex = 0;
            foreach (var infoPoint in _mapObjects[ObjectType.InfoPoint])
            {
                string name = infoPoint.Name.Replace("Info_", string.Empty);
                if (name.StartsWith("HealthRelic"))
                {
                    HealthRelics.Add(infoPoint);
                }
                else if (name.StartsWith("LargeRelic"))
                {
                    LargeRelics.Add(infoPoint);
                }
                else if (name.StartsWith("LesserRelic"))
                {
                    LesserRelics.Add(infoPoint);
                }
                else if (name.StartsWith("MinionGraveyard"))
                {
                    MinionGraveyards.Add(infoPoint);
                }
                else if (name.StartsWith("Spawn"))
                {
                    SpawnInfo.Add(infoPoint);
                }
                else if (name.StartsWith("MinionSpawn"))
                {
                    MinionSpawns.Add(infoPoint);
                }
                else if (name.StartsWith("Neutral"))
                {
                    Neutrals.Add(infoPoint);
                }
                else if (name.StartsWith("Point"))
                {
                    CapturePoints.Add(new CapturePoint(infoPoint.Position, pointIndex, infoPoint.Name[^1]));
                    pointIndex++;
                }
                else if (name.StartsWith("Sigil"))
                {
                    Sigils.Add(infoPoint);
                }
                else if (name.StartsWith("SpeedShrine"))
                {
                    SpeedShrines.Add(infoPoint);
                }
                else if (name.Contains("PortalExit"))
                {
                    ExitPortals.Add(infoPoint);
                }
            }
        }
    }

    class CapturePoint
    {
        public int Index;
        public Minion Parent;
        public char Id;
        public CapturePoint(Vector2 position, int index, char id)
        {
            Parent = CreateMinion("OdinNeutralGuardian", "OdinNeutralGuardian", position, ignoreCollision: true);
            Parent.PauseAI(true);
            AddUnitPerceptionBubble(Parent, 800.0f, 25000.0f, TeamId.TEAM_ORDER, true, collisionArea: 120.0f);
            AddUnitPerceptionBubble(Parent, 800.0f, 25000.0f, TeamId.TEAM_CHAOS, true, collisionArea: 120.0f);

            Id = id;
            Index = index;
        }
    }
}

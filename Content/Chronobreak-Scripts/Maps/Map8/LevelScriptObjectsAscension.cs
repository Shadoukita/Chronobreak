namespace MapScripts.Map8;

public static class LevelScriptObjectsAscension
{
    private static Dictionary<ObjectType, List<MapObject>> _mapObjects;
    static List<Minion> TeleportPlates = new();

    public static Dictionary<TeamId, string> TowerModels { get; set; } = new()
    {
        {TeamId.TEAM_ORDER, "OdinOrderTurretShrine" },

        {TeamId.TEAM_CHAOS, "OdinChaosTurretShrine" }
    };

    public static void LoadObjects(Dictionary<ObjectType, List<MapObject>> mapObjects)
    {
        _mapObjects = mapObjects;
    }

    public static void OnMatchStart()
    {
        LoadTeleportPlates();
    }

    static void LoadTeleportPlates()
    {
        foreach (var infoPoint in _mapObjects[ObjectType.InfoPoint].FindAll(x => x.Name.StartsWith("Info_Point")))
        {
            var position = infoPoint.Position;
            AddPosPerceptionBubble(position, 500.0f, 25000.0f, TeamId.TEAM_ORDER, false);
            AddPosPerceptionBubble(position, 500.0f, 25000.0f, TeamId.TEAM_CHAOS, false);

            if (infoPoint.Name == "Info_PointA" || infoPoint.Name == "Info_PointB")
            {
                CreateTeleportPoint(position, TeamId.TEAM_ORDER, "CapturePoint");
            }
            else if (infoPoint.Name == "Info_PointD" || infoPoint.Name == "Info_PointE")
            {
                CreateTeleportPoint(position, TeamId.TEAM_CHAOS, "CapturePoint");
            }
            else
            {
                CreateTeleportPoint(position, TeamId.TEAM_ORDER, "NeutralPointOrder");
                CreateTeleportPoint(position, TeamId.TEAM_CHAOS, "NeutralPointChaos");
            }
        }
    }

    public static void CreateTeleportPoint(Vector2 position, TeamId team, string mapIcon)
    {
        var point = CreateMinion("AscWarpIcon", "AscWarpIcon", position, team: team, ignoreCollision: false, isTargetable: false);
        point.IconInfo.ChangeIcon(mapIcon);
        TeleportPlates.Add(point);
        point.PauseAI(true);
    }
}

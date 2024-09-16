using System.Collections.Generic;
using System.Numerics;
using Force.Crc32;
using System.Text;
using GameServerCore.Enums;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects.StatsNS;

namespace Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;

public class Nexus : ObjAnimatedBuilding
{
    static List<Nexus> NexusList = [];
    Region VisionRegion { get; set; }
    public Nexus(
        string name,
        TeamId team,
        int collisionRadius = 40,
        Vector2 position = new(),
        int visionRadius = 0,
        Stats stats = null
    ) : base(name, collisionRadius, position, visionRadius, Crc32Algorithm.Compute(Encoding.UTF8.GetBytes(name)) | 0xFF000000, team, stats)
    {
        //OnCreate
        Stats.HealthPoints.BaseValue = ContentManager.MapConfig.GetFloat(Name, "mMaxHP", 3010.0f);
        Stats.ManaPoints.BaseValue = ContentManager.MapConfig.GetFloat(Name, "mMaxMP", 0.0f);
        Armor = ContentManager.MapConfig.GetFloat(Name, "mArmor", 0.0f);
        Stats.HealthRegeneration.BaseValue = ContentManager.MapConfig.GetFloat(Name, "mBaseStaticHPRegen", 0.0f);
        SelectionHeight = ContentManager.MapConfig.GetFloat(Name, "SelectionHeight", 0.0f);
        SelectionRadius = ContentManager.MapConfig.GetFloat(Name, "SelectionRadius", 0.0f);
        PathfindingRadius = ContentManager.MapConfig.GetFloat(Name, "PathfindingCollisionRadius", -1.0f);
        float radius = ContentManager.MapConfig.GetFloat(Name, "PerceptionBubbleRadius", 1350);

        if (SelectionRadius != -1.0f && PathfindingRadius <= 0)
        {
            PathfindingRadius = SelectionRadius * 0.95f;
        }

        CollisionRadius = PathfindingRadius;

        Stats.CurrentHealth = Stats.HealthPoints.Total;
        VisionRegion = new(Team, Position, visionRadius: radius);

        SetStatus(StatusFlags.Targetable, false);
        Stats.IsTargetableToTeam = SpellDataFlags.NonTargetableAll;
        SetStatus(StatusFlags.Invulnerable, true);

        if (!Name.EndsWith("11") || !Name.EndsWith("21"))
        {
            NexusList.Add(this);
        }
    }

    public override void Die(DeathData data)
    {
        Game.Map.LevelScript.HandleDestroyedObject(this);
        base.Die(data);
    }

    internal static Nexus? GetNexus(TeamId team)
    {
        return NexusList.Find(x => x.Team == team);
    }

    internal static float GetEoGPanTime()
    {
        return GlobalData.NexusVariables.EoGPanTime;
    }

    internal static bool GetEoGUseDeathAnimation()
    {
        return GlobalData.NexusVariables.EoGUseNexusDeathAnimation;
    }

    internal static float GetEoGNexusChangeSkinTime()
    {
        return GlobalData.NexusVariables.EoGNexusChangeSkinTime;
    }
}

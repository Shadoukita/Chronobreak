using System.Numerics;
using GameServerCore.Enums;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

namespace Chronobreak.GameServer.GameObjects;

//Requires further research
public class LevelProp : ObjAIBase
{
    public byte NetNodeID { get; set; }
    public float Height { get; set; }
    public Vector3 PositionOffset { get; set; }
    public Vector3 Scale { get; set; }
    public byte SkillLevel { get; set; }
    public byte Rank { get; set; }
    public byte Type { get; set; }
    string CurrentAnimationName { get; set; }
    string SkinName { get; set; }
    public override bool IsAffectedByFoW => false;

    //?
    uint CurrentAnimationFlags { get; set; }
    string IdleAnimation;
    string ActiveAnimation;
    bool ShouldRandomizeIdleAnimationPhase;
    bool DoubleSided;

    public LevelProp(
        byte netNodeId,
        string name,
        string model,
        Vector3 position,
        Vector3 direction,
        Vector3 posOffset,
        Vector3 scale,
        int skinId = 0,
        byte skillLevel = 0,
        byte rank = 0,
        byte type = 2,
        uint netId = 64
    ) : base(model, name, new Vector2(position.X, position.Y), 0, skinId, netId, TeamId.TEAM_NEUTRAL)
    {
        NetNodeID = netNodeId;
        SkinID = skinId;
        Height = position.Y;
        Direction = direction;
        PositionOffset = posOffset;
        Scale = scale;
        SkillLevel = skillLevel;
        Rank = rank;
        Type = type;
        Model = model;
        Game.ObjectManager.AddObject(this);
    }

    protected override void OnSpawn(int userId, TeamId team, bool doVision)
    {
        Game.PacketNotifier.NotifySpawnLevelProp(this, userId, team);
    }

    internal override void OnAfterSync()
    {
    }

    protected override void OnSync(int userId, TeamId team)
    {
    }

    internal override void LateUpdate()
    {
    }

    internal override float GetAttackRatioWhenAttackingTurret()
    {
        return GlobalData.DamageRatios.BuildingToBuilding;
    }
    internal override float GetAttackRatioWhenAttackingMinion()
    {
        return GlobalData.DamageRatios.BuildingToUnit;
    }
    internal override float GetAttackRatioWhenAttackingChampion()
    {
        return GlobalData.DamageRatios.BuildingToHero;
    }
    internal override float GetAttackRatioWhenAttackingBuilding()
    {
        return GlobalData.DamageRatios.BuildingToBuilding;
    }
    internal override float GetAttackRatio(AttackableUnit attackingUnit)
    {
        return attackingUnit.GetAttackRatioWhenAttackingTurret();
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using GameServerCore.Enums;
using GameServerCore.Packets.Enums;
using LeaguePackets.Game;
using LeaguePackets.Game.Common;
using Chronobreak.GameServer;
using Chronobreak.GameServer.Content.Navigation;
using Chronobreak.GameServer.GameObjects;

namespace GameServerLib.Services;
//TODO: I am leaning towards Service classes being internal only but might just depends on the service and APIs
//NOTE: There are probably better ways of handling Emitter groups but atm all i can think of his arrays of arrays.
/// <summary>
/// Class that handles notifying spawning and killing FX
/// </summary>
public static class SpecialEffectService
{
    //TODO: Packet sending needs to be refactored for better packet handling so cases of single/team packets can be handled better
    public static void SpawnTeamFx(TeamId allyTeam, TeamId enemyTeam, EffectEmitter[][] allyEffects, EffectEmitter[][] enemyEffects, uint senderNetId = 0)
    {
        var allyFXPacket = CreateGroupData(out var objects1, allyEffects, senderNetId);
        var enemyFXPacket = CreateGroupData(out var objects2, enemyEffects, senderNetId);
        TrackEmitters(objects1);
        TrackEmitters(objects2);

        Game.PacketServer.PacketHandlerManager.BroadcastPacketTeam(allyTeam, allyFXPacket, Channel.CHL_S2C);
        Game.PacketServer.PacketHandlerManager.BroadcastPacketTeam(enemyTeam, enemyFXPacket, Channel.CHL_S2C);
    }


    private static FX_Create_Group CreateGroupData(out List<EffectEmitter> objects, EffectEmitter[][] emitterGroups, uint senderNetId = 0)
    {
        List<FXCreateGroupData> fxGroups = new(emitterGroups.Length);
        objects = new(emitterGroups.First().Length);

        foreach (EffectEmitter[] emitterGroup in emitterGroups)
        {
            List<FXCreateData> fxData = new(emitterGroup.Length);
            EffectEmitter refEmitter = emitterGroup.First();

            foreach (EffectEmitter emitter in emitterGroup)
            {
                fxData.Add(CreateFxData(emitter));
                objects.Add(emitter);
            }

            fxGroups.Add(CreateGroupData(refEmitter.PackageHash, refEmitter.EffectNameHash, (ushort)refEmitter.Flags, refEmitter.TargetBoneNameHash, refEmitter.BoneNameHash, fxData));
        }

        FX_Create_Group fxPacket = new()
        {
            FXCreateGroup = fxGroups,
            SenderNetID = senderNetId
        };

        return fxPacket;
    }


    public static void SpawnFx(EffectEmitter[][] emitterGroups, uint senderNetId = 0)
    {
        List<FXCreateGroupData> fxGroups = new(emitterGroups.Length);
        List<EffectEmitter> objects = new(emitterGroups.First().Length);

        foreach (EffectEmitter[] emitterGroup in emitterGroups)
        {
            List<FXCreateData> fxData = new(emitterGroup.Length);
            EffectEmitter refEmitter = emitterGroup.First();

            foreach (EffectEmitter emitter in emitterGroup)
            {
                fxData.Add(CreateFxData(emitter));
                objects.Add(emitter);
            }

            fxGroups.Add(CreateGroupData(refEmitter.PackageHash, refEmitter.EffectNameHash, (ushort)refEmitter.Flags, refEmitter.TargetBoneNameHash, refEmitter.BoneNameHash, fxData));
        }
        TrackEmitters(objects);

        FX_Create_Group fxPacket = new()
        {
            FXCreateGroup = fxGroups,
            SenderNetID = senderNetId
        };

        Game.PacketServer.PacketHandlerManager.BroadcastPacket(fxPacket, Channel.CHL_S2C);
    }

    public static void SpawnFx(EffectEmitter[] emitters, uint senderNetId = 0)
    {
        EffectEmitter refEmitter = emitters.First();
        List<FXCreateData> fxData = [.. emitters.Select(CreateFxData)];
        List<FXCreateGroupData> fxGroups = [CreateGroupData(refEmitter.PackageHash, refEmitter.EffectNameHash, (ushort)refEmitter.Flags, refEmitter.TargetBoneNameHash, refEmitter.BoneNameHash, fxData)];

        FX_Create_Group fxPacket = new()
        {
            FXCreateGroup = fxGroups,
            SenderNetID = senderNetId
        };

        TrackEmitters(emitters);

        Game.PacketServer.PacketHandlerManager.BroadcastPacket(fxPacket, Channel.CHL_S2C);
    }

    public static void KillFx(params EffectEmitter[] emitters)
    {
        foreach (EffectEmitter emitter in emitters)
        {
            emitter?.SetToRemove();
        }
    }

    private static void TrackEmitters(IEnumerable<EffectEmitter> emitters)
    {
        foreach (EffectEmitter emitter in emitters)
        {
            Game.ObjectManager.AddObject(emitter);
        }
    }

    private static FXCreateGroupData CreateGroupData
    (
        uint packageHash,
        uint effectNameHash,
        ushort flags,
        uint targetBoneNameHash,
        uint boneNameHash,
        List<FXCreateData> fxData
    )
    {
        return new FXCreateGroupData()
        {
            PackageHash = packageHash,
            EffectNameHash = effectNameHash,
            Flags = flags,
            TargetBoneNameHash = targetBoneNameHash,
            BoneNameHash = boneNameHash,
            FXCreateData = fxData
        };
    }
    private static FXCreateData CreateFxData(EffectEmitter effectEmitter)
    {
        NavigationGrid navGrid = Game.Map.NavigationGrid;

        Vector2 ownerPos = effectEmitter.Caster?.Position ?? Vector2.Zero;
        Vector2 targetPos = effectEmitter.TargetPosition ?? Vector2.Zero;
        Vector2 position = effectEmitter.Position;

        return new FXCreateData()
        {
            NetAssignedNetID = effectEmitter.NetId,
            KeywordNetID = effectEmitter.KeywordNetID, //TODO: Find out what it is.

            CasterNetID = effectEmitter.Caster?.NetId ?? 0,
            OwnerPositionX = (short)((ownerPos.X - navGrid.MapWidth / 2) / 2),
            OwnerPositionY = navGrid.GetHeightAtLocation(ownerPos),
            OwnerPositionZ = (short)((ownerPos.Y - navGrid.MapHeight / 2) / 2),

            BindNetID = effectEmitter.BindObject?.NetId ?? 0,
            PositionX = (short)((position.X - navGrid.MapWidth / 2) / 2),
            PositionY = navGrid.GetHeightAtLocation(position),
            PositionZ = (short)((position.Y - navGrid.MapHeight / 2) / 2),

            TargetNetID = effectEmitter.TargetObject?.NetId ?? 0,
            TargetPositionX = (short)((targetPos.X - navGrid.MapWidth / 2) / 2),
            TargetPositionY = navGrid.GetHeightAtLocation(targetPos),
            TargetPositionZ = (short)((targetPos.Y - navGrid.MapHeight / 2) / 2),

            TimeSpent = effectEmitter.TimeSpent,
            ScriptScale = effectEmitter.ScriptScale,
            OrientationVector = effectEmitter.Orientation
        };
    }
}
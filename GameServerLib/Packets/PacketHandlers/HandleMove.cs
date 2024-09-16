using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using GameServerCore.Enums;
using GameServerCore.NetInfo;
using GameServerLib.Packets;
using LeaguePackets.Game;
using Chronobreak.GameServer.Content.Navigation;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using PacketDefinitions420;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    public class HandleMove : IPacketHandler<NPC_IssueOrderReq>
    {
        public void ValidateAndFixWaypoints(List<Vector2> waypoints, ObjAIBase unit)
        {
            //TODO: Don't directly access NavigationGrid
            NavigationGrid nav = Game.Map.NavigationGrid;

            //TODO: Find the nearest point on the path and discard everything before it
            for (int i = 0; i < waypoints.Count - 1; i++)
            {
                if (nav.CastCircle(waypoints[i], waypoints[i + 1], unit.PathfindingRadius, true))
                {
                    Vector2 ithWaypoint = waypoints[i];
                    Vector2 lastWaypoint = waypoints.Last();
                    List<Vector2>? path = nav.GetPath(ithWaypoint, lastWaypoint, unit.PathfindingRadius);
                    waypoints.RemoveRange(i, waypoints.Count - i);
                    if (path != null)
                    {
                        waypoints.AddRange(path);
                    }
                    else
                    {
                        waypoints.Add(ithWaypoint);
                    }
                    break;
                }
            }
        }

        public bool HandlePacket(int userId, NPC_IssueOrderReq req)
        {
            ClientInfo peerInfo = Game.PlayerManager.GetPeerInfo(userId);
            if (peerInfo == null || req == null)
            {
                return true;
            }

            Champion champion = peerInfo.Champion;
            //TODO: Don't directly access NavigationGrid
            NavigationGrid nav = Game.Map.NavigationGrid;
            AttackableUnit target = Game.ObjectManager.GetObjectById(req.TargetNetID) as AttackableUnit;
            Pet pet = champion.GetPet();
            List<Vector2> waypoints = null;
            OrderType order = (OrderType)req.OrderType;

            ObjAIBase unit = champion;
            switch (order)
            {
                case OrderType.PetHardMove:
                case OrderType.PetHardAttack:
                case OrderType.PetHardReturn:
                    unit = pet;
                    goto case OrderType.MoveTo;
                case OrderType.MoveTo:
                case OrderType.AttackTo:
                case OrderType.AttackMove:
                case OrderType.Use:
                    if (unit.MovementParameters == null && req.MovementData.Waypoints != null && req.MovementData.Waypoints.Count > 0)
                    {
                        waypoints = req.MovementData.Waypoints.ConvertAll(PacketExtensions.WaypointToVector2);
                        ValidateAndFixWaypoints(waypoints, unit);
                    }
                    unit.IssueOrDelayOrder(order, target, req.Position, waypoints);
                    break;
                case OrderType.Taunt:
                case OrderType.Stop:
                    champion.IssueOrDelayOrder(order, null, Vector2.Zero);
                    break;
                case OrderType.PetHardStop:
                    if (pet != null)
                    {
                        pet.UpdateMoveOrder(order, true);
                    }
                    break;
            }

            return true;
        }
    }
}

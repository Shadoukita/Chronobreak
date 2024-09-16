using System.Collections.Generic;
using System.Numerics;
using GameServerCore.Enums;
using LeaguePackets;
using LeaguePackets.Game;
using LeaguePackets.Game.Common;
using Chronobreak.GameServer;
using Chronobreak.GameServer.API;
using Chronobreak.GameServer.GameObjects;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using LENet;
using PacketDefinitions420;
using Channel = GameServerCore.Packets.Enums.Channel;

namespace GameServerLib.Services;

public static class VisionService
{
    private static readonly Dictionary<TeamId, List<GameObject>> VisionProviders = [];

    static VisionService()
    {
        foreach (var team in ApiHandlers.Teams)
        {
            VisionProviders.Add(team, []);
        }
    }

    /// <summary>
    /// Adds a GameObject to the list of Vision Providers in ObjectManager.
    /// </summary>
    /// <param name="obj">GameObject to add.</param>
    /// <param name="team">The team that GameObject can provide vision to.</param>
    internal static void AddVisionProvider(GameObject obj, TeamId team)
    {
        //lock (_visionLock)
        {
            VisionProviders[team].Add(obj);
        }
    }

    /// <summary>
    /// Removes a GameObject from the list of Vision Providers in ObjectManager.
    /// </summary>
    /// <param name="obj">GameObject to remove.</param>
    /// <param name="team">The team that GameObject provided vision to.</param>
    internal static void RemoveVisionProvider(GameObject obj, TeamId team)
    {
        //lock (_visionLock)
        {
            VisionProviders[team].Remove(obj);
        }
    }

    /// <summary>
    /// Updates the vision of the teams on the object.
    /// </summary>
    internal static void UpdateTeamsVision(GameObject obj)
    {
        foreach (var team in ApiHandlers.Teams)
        {
            obj.SetVisibleByTeam(team, !obj.IsAffectedByFoW || TeamHasVisionOn(team, obj));
        }
    }

    /// <summary>
    /// Whether or not a specified GameObject is being networked to the specified team.
    /// </summary>
    /// <param name="team">TeamId.BLUE/PURPLE/NEUTRAL</param>
    /// <param name="o">GameObject to check.</param>
    /// <returns>true/false; networked or not.</returns>
    internal static bool TeamHasVisionOn(TeamId team, GameObject o)
    {
        if (o != null)
        {
            if (!o.IsAffectedByFoW)
            {
                return true;
            }

            foreach (var p in VisionProviders[team])
            {
                if (UnitHasVisionOn(p, o))
                {
                    return true;
                }
            }
        }

        return false;
    }

    internal static bool UnitHasVisionOn(GameObject observer, GameObject tested, bool nearSighted = false)
    {
        if (!tested.IsAffectedByFoW)
        {
            return true;
        }

        bool stealthed = false;

        if (tested is AttackableUnit testedUnit)
        {
            if (testedUnit.Status.HasFlag(StatusFlags.RevealSpecificUnit))
            {
                return true;
            }

            if (testedUnit.Status.HasFlag(StatusFlags.Stealthed) && testedUnit.Team != observer.Team)
            {
                stealthed = true;
            }
        }

        if (observer is Region region)
        {
            if (region.VisionTarget != null && region.VisionTarget != tested)
            {
                return false;
            }

            if (stealthed)
            {
                if (region.RevealsStealth)
                {
                    return Vector2.DistanceSquared(observer.Position, tested.Position) <
                           observer.VisionRadius * observer.VisionRadius;
                }
            }
            else
            {
                return Vector2.DistanceSquared(observer.Position, tested.Position) <
                       observer.VisionRadius * observer.VisionRadius;
            }
        }


        //TODO: Force everyone to use HasFlag?
        if (tested.Team.HasFlag(observer.Team) && !nearSighted)
        {
            return true;
        }

        if (
            !(observer is AttackableUnit u && u.Stats.IsDead)
            && Vector2.DistanceSquared(observer.Position, tested.Position) <
            (observer.VisionRadius + observer.CollisionRadius) * (observer.VisionRadius + observer.CollisionRadius)
            && !Game.Map.NavigationGrid.IsAnythingBetween(observer, tested, true)
        )
        {
            return true;
        }

        return false;
    }

    internal static void ObjectRemoved(GameObject o)
    {
        //This Removes all regions attached to the entity being removed
        //TODO: Maybe keep track of which regions are attached to the unit within the unit itself? As Looping like this may become expensive at some point
        foreach (var teamProviders in VisionProviders)
        {
            foreach (var provider in teamProviders.Value)
            {
                if (provider is Region reg && reg.CollisionUnit == o)
                {
                    provider.SetToRemove();
                }
            }
        }
    }

    internal static void EnterVision(GameObject o, int clientId)
    {
        Game.PacketNotifier.NotifyEnterVisibilityClient(o, clientId);
    }

    internal static void EnterTeamVision(AttackableUnit unit, int clientId)
    {
        Game.PacketNotifier.NotifyEnterTeamVision(unit, clientId);
    }

    internal static void EnterTeamVisibility(GameObject o, TeamId team, int clientId)
    {
        Game.PacketNotifier.NotifyOnEnterTeamVisibility(o, team, clientId);
    }

    internal static void LeaveVision(AttackableUnit unit, TeamId team, int clientId)
    {
        Game.PacketNotifier.NotifyLeaveVisibilityClient(unit, team, clientId);
    }

    internal static OnEnterVisibilityClient ConstructEnterVisibilityClientPacket(GameObject o, List<GamePacket>? packets = null)
    {
        var itemDataList = new List<ItemData>();
        var shields = new ShieldValues();

        var charStackDataList = new List<CharacterStackData>();
        var charStackData = new CharacterStackData
        {
            SkinID = 0,
            OverrideSpells = false,
            ModelOnly = false,
            ReplaceCharacterPackage = false,
            ID = 0
        };

        var buffCountList = new List<KeyValuePair<byte, int>>();

        if (o is AttackableUnit a)
        {
            foreach (var shield in a.Shields)
            {
                if (shield.Magical && shield.Physical)
                {
                    shields.MagicalAndPhysical += shield.Amount;
                }
                else if (shield.Magical)
                {
                    shields.Magical += shield.Amount;
                }
                else if (shield.Physical)
                {
                    shields.Phyisical += shield.Amount;
                }
            }

            if (shields.Magical + shields.Phyisical + shields.MagicalAndPhysical <= 0)
            {
                shields = null;
            }

            charStackData.SkinName = a.Model;

            if (a is ObjAIBase obj)
            {
                charStackData.SkinID = (uint)obj.SkinID;
                if (obj.ItemInventory != null)
                {
                    foreach (var item in obj.ItemInventory.GetItems(true, true))
                    {
                        var itemData = item.ItemData;
                        itemDataList.Add(new ItemData
                        {
                            ItemID = (uint)itemData.Id,
                            ItemsInSlot = (byte)item.StackCount,
                            Slot = obj.ItemInventory.GetItemSlot(item),
                            //Unhardcode this when spell ammo gets introduced
                            SpellCharges = 0
                        });
                    }
                }
            }
            /*
            TODO:
            buffCountList = new List<KeyValuePair<byte, int>>();
            foreach (var buff in a.Buffs.All())
            {
                buffCountList.Add(new KeyValuePair<byte, int>(buff.Slot, buff.TotalStackCount));
            }
            */

            // TODO: if (a.IsDashing), requires SpeedParams, add it to AttackableUnit so it can be accessed outside of initialization
        }

        //TODO: Looks like it's not being used
        //charStackDataList.Add(charStackData);

        MovementData md;

        if (o is AttackableUnit u && u.Waypoints.Count > 1)
        {
            md = PacketExtensions.CreateMovementDataWithSpeedIfPossible(u, Game.Map.NavigationGrid,
                useTeleportID: true);
        }
        else
        {
            md = PacketExtensions.CreateMovementDataStop(o);
        }

        var enterVis = new OnEnterVisibilityClient
        {
            SenderNetID = o.NetId,
            Items = itemDataList,
            ShieldValues = shields,
            CharacterDataStack = charStackDataList,
            BuffCount = buffCountList,
            LookAtPosition = new Vector3(1, 0, 0),
            // TODO: Verify
            IsHero = o is Champion,
            MovementData = md
        };

        if (packets != null)
        {
            enterVis.Packets = packets;
        }

        return enterVis;
    }

    internal static void BroadCastVisionPacket(
        GameObject obj,
        BasePacket packet,
        Channel channel = Channel.CHL_S2C,
        PacketFlags flag = PacketFlags.RELIABLE)
    {
        foreach (var cid in obj.VisibleForPlayers)
        {
            Game.PacketServer.PacketHandlerManager.SendPacket(cid, packet, channel, flag);
        }
    }
}
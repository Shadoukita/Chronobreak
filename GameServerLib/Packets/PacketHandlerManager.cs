using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using GameServerCore.Enums;
using LeaguePackets;
using LeaguePackets.Game.Events;
using Chronobreak.GameServer;
using Chronobreak.GameServer.Logging;
using LENet;
using log4net;
using Newtonsoft.Json;
using Channel = GameServerCore.Packets.Enums.Channel;

namespace PacketDefinitions420
{
    /// <summary>
    /// Class containing all functions related to sending and receiving packets.
    /// TODO: refactor this class (may be able to replace it with LeaguePackets' implementation), get rid of IGame, use generic API requests+responses also for disconnect and unpause
    /// </summary>
    public class PacketHandlerManager
    {
        #region StaticConstructor
        private static readonly ILog _logger;
        private static readonly Dictionary<LoadScreenPacketID, Type> LoadScreenPackets;
        private static readonly Dictionary<GamePacketID, Type> GamePackets;
        private static readonly JsonSerializerSettings SerializerSettings;
        static PacketHandlerManager()
        {
            _logger = LoggerProvider.GetLogger();
            SerializerSettings = new() { TypeNameHandling = TypeNameHandling.Auto };
            LoadScreenPackets = [];
            GamePackets = [];

            Assembly? assembly = AppDomain.CurrentDomain.GetAssemblies().ToList().Find(x => x.GetName().Name is "LeaguePackets");
            if (assembly is not null)
            {
                List<TypeInfo>? list = [.. assembly?.DefinedTypes.Where(x => x.BaseType?.Name is nameof(GamePacket) or nameof(LoadScreenPacket))];
                foreach (var item in list ?? [])
                {
                    Type type = item.AsType();
                    BasePacket? packet = Activator.CreateInstance(type) as BasePacket;
                    if (packet is LoadScreenPacket loadScreenPacket)
                    {
                        LoadScreenPackets.TryAdd(loadScreenPacket.ID, type);
                    }
                    else
                    {
                        GamePackets.TryAdd((packet as GamePacket)!.ID, type);
                    }
                }
            }
        }
        #endregion

        #region InstanceConstructor
        // should be one-to-one, no two users for the same Peer
        private readonly Peer[] _peers;
        private readonly BlowFish[] _blowfishes;
        private readonly Host _server;
        private readonly StreamWriter? OutPacketStream;
        private readonly StreamWriter? InPacketStream;
        public PacketHandlerManager(BlowFish[] blowfishes, Host server, bool logOutPackets, bool logInPackets)
        {
            _blowfishes = blowfishes;
            _server = server;
            _peers = new Peer[blowfishes.Length];

            if (logOutPackets)
            {
                Directory.CreateDirectory("OutPackets");
                string fileName = Path.Join("OutPackets", "Out_PacketLog.json");
                for (int i = 1; File.Exists(fileName); i++)
                {
                    fileName = Path.Join("OutPackets", $"Out_PacketLog_{i}.json");
                }
                OutPacketStream = new StreamWriter($"{fileName}") { AutoFlush = true };
            }

            if (logInPackets)
            {
                Directory.CreateDirectory("InPackets");
                string fileName = Path.Join("InPackets", "In_PacketLog.json");
                for (int i = 1; File.Exists(fileName); i++)
                {
                    fileName = Path.Join("InPackets", $"In_PacketLog_{i}.json");
                }
                InPacketStream = new StreamWriter($"{fileName}") { AutoFlush = true };
            }
        }
        #endregion

        private class LogObject(BasePacket packet)
        {
            public float Time = Game.Time.GameTime;
            public BasePacket Packet = packet;
        }

        private static void Log(string str, object packet)
        {
            //Console.WriteLine(str + " "  + packet.GetType().Name + " " + Newtonsoft.Json.JsonConvert.SerializeObject(packet, Newtonsoft.Json.Formatting.Indented));
        }

        private void PrintPacket(byte[] buffer, string str)
        {
            // FIXME: currently lock disabled, not needed?
            Console.Write(str);
            foreach (var b in buffer)
            {
                Console.Write(b.ToString("X2") + " ");
            }

            Console.WriteLine("");
            Console.WriteLine("--------");
        }

        public bool SendPacket(int userId, BasePacket packet, Channel channelNo, PacketFlags flag = PacketFlags.RELIABLE)
        {
            OutPacketStream?.WriteLine(JsonConvert.SerializeObject(new LogObject(packet), Formatting.Indented, SerializerSettings));
            return SendPacket(userId, packet.GetBytes(), channelNo, flag);
        }
        public bool SendPacket(int userId, byte[] source, Channel channelNo, PacketFlags flag = PacketFlags.RELIABLE)
        {
            // Sometimes we try to send packets to a user that doesn't exist (like in broadcast when not all players are connected).
            if (userId >= 0 && userId < _peers.Length && _peers[userId] != null)
            {
                byte[] temp;
                if (source.Length >= 8)
                {
                    // _peers.Length == _blowfishes.Length
                    temp = _blowfishes[userId].Encrypt(source);
                }
                else
                {
                    temp = source;
                }
                return _peers[userId].Send((byte)channelNo, new Packet(temp, flag)) == 0;
            }
            return false;
        }

        public bool BroadcastPacket(BasePacket packet, Channel channelNo, PacketFlags flag = PacketFlags.RELIABLE)
        {
            Log("BROADCAST", packet);
            return BroadcastPacket(packet.GetBytes(), channelNo, flag);
        }
        public bool BroadcastPacket(byte[] data, Channel channelNo, PacketFlags flag = PacketFlags.RELIABLE)
        {
            if (data.Length >= 8)
            {
                // send packet to all peers and save failed ones
                int failedPeers = 0;
                for (int i = 0; i < _peers.Length; i++)
                {
                    if (_peers[i] != null && _peers[i].Send((byte)channelNo, new Packet(_blowfishes[i].Encrypt(data), flag)) < 0)
                    {
                        failedPeers++;
                    }
                }

                if (failedPeers > 0)
                {
                    Debug.WriteLine($"Broadcasting packet failed for {failedPeers} peers.");
                    return false;
                }
                return true;
            }
            else
            {
                var packet = new Packet(data, flag);
                _server.Broadcast((byte)channelNo, packet);
                return true;
            }
        }

        public bool BroadcastPacketTeam(TeamId team, BasePacket packet, Channel channelNo, PacketFlags flag = PacketFlags.RELIABLE)
        {
            Log("BROADCAST TEAM", packet);
            return BroadcastPacketTeam(team, packet.GetBytes(), channelNo, flag);
        }

        // TODO: find a way with no need of player manager
        public bool BroadcastPacketTeam(TeamId team, byte[] data, Channel channelNo, PacketFlags flag = PacketFlags.RELIABLE)
        {
            foreach (var ci in Game.PlayerManager.GetPlayers(false))
            {
                if (ci.Team == team)
                {
                    SendPacket(ci.ClientId, data, channelNo, flag);
                }
            }

            return true;
        }

        public bool HandlePacket(Peer peer, byte[] data, Channel channelId)
        {
            if (peer.UserData is null)
            {
#if DEBUG
                PrintPacket(data, "Error: ");
#endif
                return false;
            }

            BasePacket packet;
            using (BinaryReader sla = new(new MemoryStream(data)))
            {
                byte opCode = sla.ReadByte();
                if (channelId == Channel.CHL_COMMUNICATION || channelId == Channel.CHL_LOADING_SCREEN)
                {
                    packet = (Activator.CreateInstance(LoadScreenPackets[(LoadScreenPacketID)opCode]) as BasePacket)!;
                }
                else
                {
                    var gamePacketId = (GamePacketID)opCode;
                    //???
                    if (gamePacketId == GamePacketID.ExtendedPacket)
                    {
                        gamePacketId = (GamePacketID)(data[5] | (ushort)(data[6] << 8));
                    }
                    packet = (Activator.CreateInstance(GamePackets[gamePacketId]) as BasePacket)!;
                }
            }

            packet.Read(data);

            InPacketStream?.WriteLine(JsonConvert.SerializeObject(new LogObject(packet), Formatting.Indented, SerializerSettings));

            try
            {
                Game.RequestHandler.OnMessage((int)peer.UserData, packet);
            }
            catch (Exception e)
            {
                _logger.Error("Handle Packet Error", e);
                return false;
            }

            return true;
        }

        public bool HandleDisconnect(Peer peer)
        {
            if (peer == null || peer.UserData == null)
            {
                // Didn't receive an ID by initiating a handshake.
                return true;
            }
            int clientId = (int)peer.UserData;
            return HandleDisconnect(clientId);
        }

        public bool HandleDisconnect(int clientId)
        {
            var peerInfo = Game.PlayerManager.GetPeerInfo(clientId);
            if (peerInfo.IsDisconnected)
            {
                Debug.WriteLine($"Prevented double disconnect of {peerInfo.PlayerId}");
                return true;
            }

            Debug.WriteLine($"Player {peerInfo.PlayerId} disconnected!");

            var annoucement = new OnLeave { OtherNetID = peerInfo.Champion.NetId };
            Game.PacketNotifier.NotifyS2C_OnEventWorld(annoucement, peerInfo.Champion);
            peerInfo.IsDisconnected = true;
            peerInfo.IsStartedClient = false;
            _peers[clientId] = null;

            return Game.PlayerManager.CheckIfAllPlayersLeft() || peerInfo.Champion.OnDisconnect();
        }

        public bool HandlePacket(Peer peer, Packet packet, Channel channelId)
        {
            var data = packet.Data;

            // if channel id is HANDSHAKE we should initialize blowfish key and return
            if (channelId == Channel.CHL_HANDSHAKE)
            {
                return HandleHandshake(peer, data);
            }

            // every packet that is not blowfish go here
            if (data.Length >= 8)
            {
                //TODO: An unhandled exception of type 'System.NullReferenceException' occurred
                if (peer.UserData == null)
                {
#if DEBUG
                    if (_blowfishes.Length == 1)
                    {
                        HandleReconnect(peer, 0);
                    }
                    else
#endif
                        return false; // Unauthorized peer sends garbage
                }
                int clientId = (int)peer.UserData;
                data = _blowfishes[clientId].Decrypt(data);
            }

            return HandlePacket(peer, data, channelId);
        }

#if DEBUG
        private bool HandleReconnect(Peer peer, int userId)
        {
            var peerInfo = Game.PlayerManager.GetPeerInfo(userId);
            peerInfo.IsStartedClient = true;
            peerInfo.IsDisconnected = false;
            peer.UserData = peerInfo.ClientId;
            _peers[peerInfo.ClientId] = peer;
            return true;
        }
#endif

        private bool HandleHandshake(Peer peer, byte[] data)
        {
            var request = new KeyCheckPacket();
            request.Read(data);

            _logger.Info("A peer is attempting a server Handshake");
            Log("HANDSHAKE", request);

            //TODO: Handle version matching

            //TODO: Figure out how KeyCheckPacket.Action plays into HandShakes

            var client = Game.PlayerManager.GetClientInfoByPlayerId(request.PlayerID);

            if (client is null)
            {
                _logger.Warn($"Peer Rejected: Invalid PlayerId - {request.PlayerID}");
                return false;
            }

            if (client.ClientId.Equals(-1))
            {
                client.ClientId = NetworkId.GetClientId();
                _logger.Info($"Peer validated and assigned a new ClientId: {client.ClientId}");
            }
            else
            {
                //TODO: Implement connection status tracking
                if (_peers[client.ClientId] != null && !client.IsDisconnected)
                {
                    //Debug.WriteLine($"Player {request.PlayerID} is already connected. Request from {peer.Address.IPEndPoint.Address.ToString()}.");
                    Debug.WriteLine($"Player {request.PlayerID} is reconnecting.");
                    _peers[client.ClientId].DisconnectNow(0); //TODO: Verify
                    HandleDisconnect(client.ClientId);
                    //return false;
                }
            }

            //TODO: Move away from hard setting peer and blowfish arrays in the same order listed in GameInfo.json
            //Works fine for now since since we use the same blowfish key for each player
            //However it allows for a new peer to overwrite an already existing one by just using the right PID
            //this check should happen sooner but impossible since we need the cid to verify it in the array
            var playerID = _blowfishes[client.ClientId].Decrypt(request.CheckSum); //Maybe move BlowfishKey to reside in ClientInfo?
            if (playerID != request.PlayerID)
            {
                _logger.Warn($"Peer Rejected: BlowfishKey did not validate against the provided CheckSum - {request.CheckSum}");
                return false;
            }


            client.IsStartedClient = true;

            peer.UserData = client.ClientId;
            _peers[client.ClientId] = peer;

            bool result = true;
            // inform players about their player numbers
            foreach (var player in Game.PlayerManager.GetPlayers(false))
            {
                var response = new KeyCheckPacket
                {
                    ClientID = player.ClientId,
                    PlayerID = player.PlayerId,
                    VersionNumber = request.VersionNumber,
                    Action = 0,
                    CheckSum = request.CheckSum
                };
                result = result && SendPacket(client.ClientId, response, Channel.CHL_HANDSHAKE);
            }

            //TODO: Move.
            /*
            foreach(var player in Game.PlayerManager.GetPlayers(true))
            {
                //TODO: Take into account already loaded players when reconnecting.
                if(player.Champion.IsBot)
                {
                    var response = new LeaguePackets.Game.S2C_Ping_Load_Info()
                    {
                        ConnectionInfo = new LeaguePackets.Game.Common.ConnectionInfo
                        {
                            ClientID = player.ClientId,
                            PlayerID = player.PlayerId, // -1
                            Ping = 0,
                            Ready = true,
                            Percentage = 100,
                            ETA = 0,
                            Count = 0
                        }
                    };
                    result = result && SendPacket(peerInfo.ClientId, response, Channel.CHL_S2C);
                }
            }
            */

            // only if all packets were sent successfully return true
            return result;
        }
    }
}

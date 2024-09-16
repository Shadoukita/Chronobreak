using System;
using System.Collections.Generic;
using LeaguePackets;
using Chronobreak.GameServer.Logging;
using log4net;

namespace GameServerLib.Packets
{
    // the global generic network handler between the bridge and the server
    public class PacketRouter
    {
        private static ILog _logger = LoggerProvider.GetLogger();
        private readonly Dictionary<Type, Delegate> _handlers = [];

        // server & scripts will register to events, this allows scripts more than the current state
        public void Register<T>(IPacketHandler<T>? handler) where T : BasePacket
        {
            if (handler is null)
            {
                _logger.Warn($"Provided handler for {typeof(T)} packets is null");
                return;
            }
            _handlers[typeof(T)] = handler.HandlePacket;
        }

        // every message (bridge->server or server->bridge) pass should pass here
        public void OnMessage<T>(int userId, T req) where T : BasePacket
        {
            _handlers[req.GetType()].DynamicInvoke(userId, req);
        }
    }
}
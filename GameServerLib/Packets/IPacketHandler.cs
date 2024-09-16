using LeaguePackets;

namespace GameServerLib.Packets;

public interface IPacketHandler<T> where T : BasePacket
{
    bool HandlePacket(int userId, T req);
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetUnpack.ReplayParser
{

    [Flags]
    public enum ENetPacketFlags
    {
        Reliable = (1 << 7),
        Unsequenced = (1 << 6),
        ReliableUnsequenced = Reliable | Unsequenced,
        None = 0,
    }
    public class ENetPacket
    {
        public float Time { get; set; }
        public byte[] Bytes { get; set; }
        public byte Channel { get; set; }
        public ENetPacketFlags Flags { get; set; }
    }

    //TODO: figure out what exact versions are to brake at 
    public enum ENetLeagueVersion
    {
        Seasson12 = 0x01,
        Seasson34 = 0x03,
        Patch420 = 0x05,
    }
    public class ENetProtocolHeader
    {
        public uint? CheckSum { get; set; } = null;
        public uint SessionID { get; set; }
        public ushort? PeerID { get; set; } = null;
        public ushort? TimeSent { get; set; } = null;
        public float TimeRecieved { get; set; }
        public ENetLeagueVersion ENetLeagueVersion { get; set; }

        public static readonly Dictionary<ENetLeagueVersion, int> ProtocolHeaderSizes = new Dictionary<ENetLeagueVersion, int>
        {
            [ENetLeagueVersion.Seasson12] = 8,
            [ENetLeagueVersion.Seasson34] = 4,
            [ENetLeagueVersion.Patch420] = 8,
        };

        public ENetProtocolHeader(BinaryReader reader, float timeRecieved, ENetLeagueVersion enetLeagueVersion)
        {
            switch (enetLeagueVersion)
            {
                case ENetLeagueVersion.Seasson12:
                    {
                        SessionID = reader.ReadUInt32(true);
                        ushort peerID = reader.ReadUInt16(true);
                        if((peerID & 0x7FFF) != 0x7FFF)
                        {
                            PeerID = peerID;
                        }
                        if ((peerID & 0x8000) > 0)
                        {
                            TimeSent = reader.ReadUInt16();
                        }
                    }
                    break;
                case ENetLeagueVersion.Seasson34:
                    {
                        SessionID = reader.ReadByte();
                        byte peerID = reader.ReadByte();
                        if ((peerID & 0x7F) != 0x7F)
                        {
                            PeerID = peerID;
                        }
                        if ((peerID & 0x80) > 0)
                        {
                            TimeSent = reader.ReadUInt16();
                        }
                    }
                    break;
                case ENetLeagueVersion.Patch420:
                    {
                        CheckSum = reader.ReadUInt32(true);
                        SessionID = reader.ReadByte();
                        byte peerID = reader.ReadByte();
                        if ((peerID & 0x7F) != 0x7F)
                        {
                            PeerID = peerID;
                        }
                        if ((peerID & 0x80) > 0)
                        {
                            TimeSent = reader.ReadUInt16();
                        }
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
            TimeRecieved = timeRecieved;
            ENetLeagueVersion = enetLeagueVersion;
        }
    }
    [Flags]
    public enum ENetCommandFlag : byte
    {
        NONE = 0,
        ACKNOWLEDGE = (1 << 7),
        UNSEQUENCED = (1 << 6),
        ACKNOWLEDGE_UNSEQUENCED = ACKNOWLEDGE | UNSEQUENCED,
    }
    public enum ENetProtocolCommand : byte
    {
        NONE = 0x00,
        ACKNOWLEDGE = 0x01,
        CONNECT = 0x02,
        VERIFY_CONNECT = 0x03,
        DISCONNECT = 0x04,
        PING = 0x05,
        SEND_RELIABLE = 0x06,
        SEND_UNRELIABLE = 0x07,
        SEND_FRAGMENT = 0x08,
        SEND_UNSEQUENCED = 0x09,
        BANDWIDTH_LIMIT = 0x0A,
        THROTTLE_CONFIGURE = 0x0B,
    }
    public class ENetProtocolCommandHeader
    {
        public ENetCommandFlag Flags { get; set; }
        public ENetProtocolCommand Command { get; set; }
        public byte Channel { get; set; }
        public ushort ReliableSequenceNumber { get; set; }

        public const int CommandHeaderSize = 4;
        public ENetProtocolCommandHeader(BinaryReader reader)
        {
            byte command_flags = reader.ReadByte();
            Flags = (ENetCommandFlag)(byte)(command_flags & 0xF0);
            Command = (ENetProtocolCommand)(byte)(command_flags & 0x0F);
            Channel = reader.ReadByte();
            ReliableSequenceNumber = reader.ReadUInt16(true);
        }
    }

    
    public abstract class ENetProtocol
    {
        public static readonly Dictionary<ENetProtocolCommand, int> CommandFullSize
            = new Dictionary<ENetProtocolCommand, int>
        {
            [ENetProtocolCommand.NONE] = 0,
            [ENetProtocolCommand.ACKNOWLEDGE] = 8,
            [ENetProtocolCommand.CONNECT] = 40,
            [ENetProtocolCommand.VERIFY_CONNECT] = 36,
            [ENetProtocolCommand.DISCONNECT] = 8,
            [ENetProtocolCommand.PING] = 4,
            [ENetProtocolCommand.SEND_RELIABLE] = 6,
            [ENetProtocolCommand.SEND_UNRELIABLE] = 8,
            [ENetProtocolCommand.SEND_FRAGMENT] = 24,
            [ENetProtocolCommand.SEND_UNSEQUENCED] = 8,
            [ENetProtocolCommand.BANDWIDTH_LIMIT] = 12,
            [ENetProtocolCommand.THROTTLE_CONFIGURE] = 16,
        };
        public static readonly Dictionary<ENetProtocolCommand, Func<ENetProtocolHeader, ENetProtocolCommandHeader, BinaryReader, ENetProtocol>> CommandConstructors
            = new Dictionary<ENetProtocolCommand, Func<ENetProtocolHeader, ENetProtocolCommandHeader, BinaryReader, ENetProtocol>>
        {
                [ENetProtocolCommand.ACKNOWLEDGE] = (p, c, r) =>  new ENetProtocolAcknowledge(p, c, r),
                [ENetProtocolCommand.CONNECT] = (p, c, r) => new ENetProtocolConnect(p, c, r),
                [ENetProtocolCommand.VERIFY_CONNECT] = (p, c, r) => new ENetProtocolVerifyConnect(p, c, r),
                [ENetProtocolCommand.DISCONNECT] = (p, c, r) => new ENetProtocolDisconnect(p, c, r),
                [ENetProtocolCommand.PING] = (p, c, r) => new ENetProtocolPing(p, c, r),
                [ENetProtocolCommand.SEND_FRAGMENT] = (p, c, r) => new ENetProtocolSendFragment(p, c, r),
                [ENetProtocolCommand.SEND_RELIABLE] = (p, c, r) => new ENetProtocolSendReliable(p, c, r),
                [ENetProtocolCommand.SEND_UNRELIABLE] = (p, c, r) => new ENetProtocolSendUnreliable(p, c, r),
                [ENetProtocolCommand.SEND_UNSEQUENCED] = (p, c, r) => new ENetProtocolSendUnsequenced(p, c, r),
                [ENetProtocolCommand.BANDWIDTH_LIMIT] = (p, c, r) => new ENetProtocolBandwidthLimit(p, c, r),
                [ENetProtocolCommand.THROTTLE_CONFIGURE] = (p, c, r) => new ENetProtocolThrottleConfigure(p, c, r),
       };
    }
    public class ENetProtocolAcknowledge : ENetProtocol
    {
        public ushort ReceivedReliableSequenceNumber { get; set; }
        public ushort ReceivedSentTime { get; set; }
        public ENetProtocolAcknowledge(ENetProtocolHeader protocolHeader, ENetProtocolCommandHeader protocolCommandHeader, BinaryReader reader)
        {
            ReceivedReliableSequenceNumber = reader.ReadUInt16(true);
            ReceivedSentTime = reader.ReadUInt16(true);
        }
    }
    public class ENetProtocolConnect : ENetProtocol
    {
        public ushort OutgoingPeerID { get; set; }
        public ushort MTU { get; set; }
        public uint WindowSize { get; set; }
        public uint ChannelCount { get; set; }
        public uint IncomingBandwidth { get; set; }
        public uint OutgoingBandwidth { get; set; }
        public uint PacketThrottleInterval { get; set; }
        public uint PacketThrottleAcceleration { get; set; }
        public uint PacketThrottleDeceleration { get; set; }
        public uint SessionID { get; set; }
        public ENetProtocolConnect(ENetProtocolHeader protocolHeader, ENetProtocolCommandHeader protocolCommandHeader, BinaryReader reader)
        {
            switch(protocolHeader.ENetLeagueVersion)
            {
                case ENetLeagueVersion.Seasson12:
                    OutgoingPeerID = reader.ReadUInt16(true);
                    break;
                case ENetLeagueVersion.Seasson34:
                case ENetLeagueVersion.Patch420:
                    OutgoingPeerID = reader.ReadByte();
                    reader.ReadByte();
                    break;
            }
            MTU = reader.ReadUInt16(true);
            WindowSize = reader.ReadUInt32(true);
            ChannelCount = reader.ReadUInt32(true);
            IncomingBandwidth = reader.ReadUInt32(true);
            OutgoingBandwidth = reader.ReadUInt32(true);
            PacketThrottleInterval = reader.ReadUInt32(true);
            PacketThrottleAcceleration = reader.ReadUInt32(true);
            PacketThrottleDeceleration = reader.ReadUInt32(true);
            switch (protocolHeader.ENetLeagueVersion)
            {
                case ENetLeagueVersion.Seasson12:
                    SessionID = reader.ReadUInt32(true);
                    break;
                case ENetLeagueVersion.Seasson34:
                case ENetLeagueVersion.Patch420:
                    SessionID = reader.ReadByte();
                    reader.ReadExactBytes(3);
                    break;
            }

        }
    }
    public class ENetProtocolVerifyConnect : ENetProtocol
    {
        public ushort OutgoingPeerID { get; set; }
        public ushort MTU { get; set; }
        public uint WindowSize { get; set; }
        public uint ChannelCount { get; set; }
        public uint IncomingBandwidth { get; set; }
        public uint OutgoingBandwidth { get; set; }
        public uint PacketThrottleInterval { get; set; }
        public uint PacketThrottleAcceleration { get; set; }
        public uint PacketThrottleDeceleration { get; set; }

        public ENetProtocolVerifyConnect(ENetProtocolHeader protocolHeader, ENetProtocolCommandHeader protocolCommandHeader, BinaryReader reader)
        {
            switch (protocolHeader.ENetLeagueVersion)
            {
                case ENetLeagueVersion.Seasson12:
                    OutgoingPeerID = reader.ReadUInt16(true);
                    break;
                case ENetLeagueVersion.Seasson34:
                case ENetLeagueVersion.Patch420:
                    OutgoingPeerID = reader.ReadByte();
                    reader.ReadByte();
                    break;
            }
            MTU = reader.ReadUInt16(true);
            WindowSize = reader.ReadUInt32(true);
            ChannelCount = reader.ReadUInt32(true);
            IncomingBandwidth = reader.ReadUInt32(true);
            OutgoingBandwidth = reader.ReadUInt32(true);
            PacketThrottleInterval = reader.ReadUInt32(true);
            PacketThrottleAcceleration = reader.ReadUInt32(true);
            PacketThrottleDeceleration = reader.ReadUInt32(true);
        }
    }
    public class ENetProtocolBandwidthLimit : ENetProtocol
    {
        public uint IncomingBandwidth { get; set; }
        public uint OutgoingBandwidth { get; set; }

        public ENetProtocolBandwidthLimit(ENetProtocolHeader protocolHeader, ENetProtocolCommandHeader protocolCommandHeader, BinaryReader reader)
        {
            IncomingBandwidth = reader.ReadUInt32(true);
            OutgoingBandwidth = reader.ReadUInt32(true);
        }
    }
    public class ENetProtocolThrottleConfigure : ENetProtocol
    {
        public uint PacketThrottleInterval { get; set; }
        public uint PacketThrottleAcceleration { get; set; }
        public uint PacketThrottleDeceleration { get; set; }

        public ENetProtocolThrottleConfigure(ENetProtocolHeader protocolHeader, ENetProtocolCommandHeader protocolCommandHeader, BinaryReader reader)
        {
            PacketThrottleInterval = reader.ReadUInt32(true);
            PacketThrottleAcceleration = reader.ReadUInt32(true);
            PacketThrottleDeceleration = reader.ReadUInt32(true);
        }
    }
    public class ENetProtocolDisconnect : ENetProtocol
    {
        public uint Data { get; set; }

        public ENetProtocolDisconnect(ENetProtocolHeader protocolHeader, ENetProtocolCommandHeader protocolCommandHeader, BinaryReader reader)
        {
            Data = reader.ReadUInt32(true);
        }
    }
    public class ENetProtocolPing : ENetProtocol
    {
        public ENetProtocolPing(ENetProtocolHeader protocolHeader, ENetProtocolCommandHeader protocolCommandHeader, BinaryReader reader)
        {
            
        }
    }
    public class ENetProtocolSendReliable : ENetProtocol
    {
        public byte[] Data { get; set; }

        public ENetProtocolSendReliable(ENetProtocolHeader protocolHeader, ENetProtocolCommandHeader protocolCommandHeader, BinaryReader reader)
        {
            ushort dataLength = reader.ReadUInt16(true);
            Data = reader.ReadExactBytes(dataLength);
        }
    }
    public class ENetProtocolSendUnreliable : ENetProtocol
    {
        public ushort UnreliableSequenceNumber { get; set; }
        public byte[] Data { get; set; }

        public ENetProtocolSendUnreliable(ENetProtocolHeader protocolHeader, ENetProtocolCommandHeader protocolCommandHeader, BinaryReader reader)
        {
            UnreliableSequenceNumber = reader.ReadUInt16(true);
            ushort dataLength = reader.ReadUInt16(true);
            Data = reader.ReadExactBytes(dataLength);
        }
    }
    public class ENetProtocolSendUnsequenced : ENetProtocol
    {
        public ushort UnsequencedGroup { get; set; }
        public byte[] Data { get; set; }

        public ENetProtocolSendUnsequenced(ENetProtocolHeader protocolHeader, ENetProtocolCommandHeader protocolCommandHeader, BinaryReader reader)
        {
            UnsequencedGroup = reader.ReadUInt16(true);
            ushort dataLength = reader.ReadUInt16(true);
            Data = reader.ReadExactBytes(dataLength);
        }
    }
    public class ENetProtocolSendFragment : ENetProtocol
    {
        public ushort StartSequenceNumber { get; set; }
        public uint FragmentCount { get; set; }
        public uint FragmentNumber { get; set; }
        public uint TotalLength { get; set; }
        public uint FragmentOffset { get; set; }
        public byte[] Data { get; set; }

        public ENetProtocolSendFragment(ENetProtocolHeader protocolHeader, ENetProtocolCommandHeader protocolCommandHeader, BinaryReader reader)
        {
            StartSequenceNumber = reader.ReadUInt16(true);
            ushort dataLength = reader.ReadUInt16(true);
            FragmentCount = reader.ReadUInt32(true);
            FragmentNumber = reader.ReadUInt32(true);
            TotalLength = reader.ReadUInt32(true);
            FragmentOffset = reader.ReadUInt32(true);
            Data = reader.ReadExactBytes(dataLength);
        }
    }

    public abstract class ENetProtocolHandler
    {
        public virtual bool HandleProtocolHeader(ENetProtocolHeader protocolHeader)
        {
            return true;
        }
        public virtual bool HandleProtocolCommandHeader(ENetProtocolHeader protocolHeader, ENetProtocolCommandHeader protocolCommandHeader)
        {
            return true;
        }
        public virtual bool HandleProtocol(ENetProtocolHeader protocolHeader, ENetProtocolCommandHeader protocolCommandHeader, ENetProtocol protocol)
        {
            return true;
        }
        public void Read(BinaryReader reader, float timeRecieved, ENetLeagueVersion enetLeagueVersion)
        {
            if (reader.BytesLeft() < ENetProtocolHeader.ProtocolHeaderSizes[enetLeagueVersion])
            {
                return;
            }
            var protocolHeader = new ENetProtocolHeader(reader, timeRecieved, enetLeagueVersion);
            if (!HandleProtocolHeader(protocolHeader))
            {
                return;
            }
            while (reader.BytesLeft() > 0)
            {
                if (reader.BytesLeft() < ENetProtocolCommandHeader.CommandHeaderSize)
                {
                    break;
                }
                var protocolCommandHeader = new ENetProtocolCommandHeader(reader);
                if (!ENetProtocol.CommandFullSize.ContainsKey(protocolCommandHeader.Command))
                {
                    break;
                }
                var fullSize = ENetProtocol.CommandFullSize[protocolCommandHeader.Command];
                if (fullSize == 0 || reader.BytesLeft() < (fullSize - ENetProtocolCommandHeader.CommandHeaderSize))
                {
                    break;
                }
                if(!HandleProtocolCommandHeader(protocolHeader, protocolCommandHeader))
                {
                    break;
                }
                ENetProtocol protocol = null;
                try
                {
                    protocol = ENetProtocol.CommandConstructors[protocolCommandHeader.Command](protocolHeader, protocolCommandHeader, reader);
                }
                catch (Exception)
                {
                    //FIXME: optional strict flag
                    break;
                }
                if (protocol != null)
                {
                    if (!HandleProtocol(protocolHeader, protocolCommandHeader, protocol))
                    {
                        break;
                    }
                }
            }
        }
    }
}

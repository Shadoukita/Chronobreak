using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace ENetUnpack.ReplayParser
{
    public class ChunkParserSpectator : HttpProtocolHandler, IChunkParser
    {
        private BlowFish _blowfish;

        public List<ENetPacket> Packets { get; } =  new List<ENetPacket>();

        public ChunkParserSpectator(byte[] key, int matchID)
        {
            var keyBlowfish = new BlowFish(Encoding.ASCII.GetBytes(matchID.ToString()));

            _blowfish = new BlowFish(keyBlowfish.Decrypt(key).Take(16).ToArray());
        }

        public override void HandleBinaryPacket(byte[] data, float timeHttp)
        {
            var decrypted = _blowfish.Decrypt(data);
            using (var decompressed = new MemoryStream())
            {
                using (var compressed = new GZipStream(new MemoryStream(decrypted), CompressionMode.Decompress))
                {
                    compressed.CopyTo(decompressed);
                }
                decompressed.Seek(0, SeekOrigin.Begin);
                using (BinaryReader reader = new BinaryReader(decompressed))
                {
                    ReadSpectatorChunks(reader);
                }
            }
        }

        public void ReadSpectatorChunks(BinaryReader reader)
        {
            float time = 0.0f;
            byte packetType = 0;
            int blockparam = 0;
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                byte marker = reader.ReadByte();
                byte flags = (byte)(marker >> 4);
                byte channel = (byte)(marker & 0x0F);
                int length;
                if ((flags & 0x8) == 0)
                {
                    time = reader.ReadSingle();
                }
                else
                {
                    time += reader.ReadByte() / 1000.0f;
                }
                if ((flags & 0x1) == 0)
                {
                    length = reader.ReadInt32();
                }
                else
                {
                    length = reader.ReadByte();
                }
                if ((flags & 0x4) == 0)
                {
                    packetType = reader.ReadByte();
                }
                if ((flags & 0x2) == 0)
                {
                    blockparam = reader.ReadInt32();
                }
                else
                {
                    blockparam += reader.ReadByte();
                }
                byte[] packetData = reader.ReadExactBytes(length);
                AddPacket(packetType, channel, blockparam, packetData, time);
            }
        }


        private void AddPacket(byte packetType, byte channel, int blockparam, byte[] data, float time)
        {
            var buffer = new List<byte>();
            buffer.Add(packetType);
            buffer.AddRange(BitConverter.GetBytes(blockparam));
            buffer.AddRange(data);
            Packets.Add(new ENetPacket
            {
                Channel = channel,
                Bytes = buffer.ToArray(),
                Flags = ENetPacketFlags.None,
                Time = time,
            });
        }
    }
}

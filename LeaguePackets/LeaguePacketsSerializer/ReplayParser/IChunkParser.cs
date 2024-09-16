using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetUnpack.ReplayParser
{
    public interface IChunkParser
    {
        List<ENetPacket> Packets { get; }
        void Read(byte[] data, float time);
    }
}

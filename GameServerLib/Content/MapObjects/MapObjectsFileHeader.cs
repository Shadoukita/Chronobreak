using System.IO;

namespace Chronobreak.GameServer.Content;

class MapObjectsFileHeader
{
    internal uint FileTypeId;
    internal uint Version;
    internal uint ObjectCount;
    internal uint Reserved;
    internal MapObjectsFileHeader(BinaryReader br)
    {
        FileTypeId = br.ReadUInt32();
        Version = br.ReadUInt32();
        ObjectCount = br.ReadUInt32();
        Reserved = br.ReadUInt32();
    }
}
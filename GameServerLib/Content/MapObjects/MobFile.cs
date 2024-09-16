using Chronobreak.GameServer.Logging;
using log4net;
using System.Collections.Generic;
using System.IO;

namespace Chronobreak.GameServer.Content;

internal class MobFile
{
    private readonly ILog _logger = LoggerProvider.GetLogger();

    internal MapObjectsFileHeader MapObjectsFileHeader { get; private set; }
    internal readonly List<MapObject> MapObjects = [];

    internal MobFile(string path)
    {
        BinaryReader br = new(File.OpenRead(path));

        MapObjectsFileHeader = new(br);

        if (MapObjectsFileHeader.FileTypeId is 1296126031)
        {
            if (MapObjectsFileHeader.Version is 2)
            {
                for (int i = 0; i < MapObjectsFileHeader.ObjectCount; i++)
                {
                    MapObjects.Add(new(br));
                }
            }
            else
            {
                _logger.Error($"Map object file {path} has version {MapObjectsFileHeader.Version} where {MapObject.kFileVer} is expected; skipping load!\n");
            }
        }
        else
        {
            _logger.Error($"Map object file {path} has type {MapObjectsFileHeader.FileTypeId} where {MapObject.kFileID} is expected; skipping load!\n");
        }

        br.Close();
    }
}
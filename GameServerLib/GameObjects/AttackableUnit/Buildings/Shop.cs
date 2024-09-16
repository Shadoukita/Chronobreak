using System.Numerics;
using Force.Crc32;
using System.Text;
using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.StatsNS;

namespace Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings;

public class Shop : ObjBuilding
{
    Region VisionRegion;
    bool PostPlayerInit;
    bool PlayerCanShop;

    public Shop(string name,
        Vector2 position = new(),
        TeamId team = TeamId.TEAM_ORDER,
        Stats stats = null) :
        base(name, "", 0, position, 0, Crc32Algorithm.Compute(Encoding.UTF8.GetBytes(name)) | 0xFF000000, team, stats)
    {
    }
}
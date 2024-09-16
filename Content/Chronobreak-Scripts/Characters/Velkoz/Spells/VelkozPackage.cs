using GameServerCore;

namespace VelKoz;

public class VelkozPackage
{
    public static void CastQSplitMissiles(ObjAIBase velkoz, SpellMissile missile, Vector3 castPos)
    {
        var split1 = GetOffset(castPos, missile.Direction, 500, 90);
        var split2 = GetOffset(castPos, missile.Direction, 500, -90);
        
        
        velkoz.Spells.Extra[5].TryCast(
            default, 
            split1, 
            split1,
            0,
            false,
            true,
            false,
            false,
            false,
            true,
            castPos
        );
        
        velkoz.Spells.Extra[5].TryCast(
            default, 
            split2, 
            split2,
            0,
            false,
            true,
            false,
            false,
            false,
            true,
            castPos
        );
    }
    
    private static Vector3 GetOffset(Vector3 pos, Vector3 direction, float distance, float offsetAngle)
    {
        var dir = direction.Normalized();

        offsetAngle %= 360;
        if (offsetAngle > 180)
        {
            offsetAngle -= 360;
        }
        //if(offsetAngle < 0)
        //    offsetAngle += 360;

        return pos + (dir.Rotated(offsetAngle) * distance);
    }

}

using System.Numerics;

namespace Chronobreak.GameServer.Scripting.Lua
{
    public class LVector3
    {
        public float x;
        public float y;
        public float z;
        public LVector3(float _x, float _y, float _z)
        {
            x = _x; y = _y; z = _z;
        }
        public LVector3(Vector3 v)
        {
            x = v.X; y = v.Y; z = v.Z;
        }
        public LVector3(Vector2 v)
        {
            x = v.X; y = 0; z = v.Y;
        }
        public static implicit operator Vector3(LVector3 lv)
        {
            return new Vector3(lv.x, lv.y, lv.z);
        }
        public static LVector3 operator +(LVector3 llv, LVector3 rlv)
        {
            return new LVector3((Vector3)llv + (Vector3)rlv);
        }
        public static LVector3 operator -(LVector3 llv, LVector3 rlv)
        {
            return new LVector3((Vector3)llv - (Vector3)rlv);
        }
        public static LVector3 operator *(LVector3 llv, float rf)
        {
            return new LVector3((Vector3)llv * rf);
        }
        public float dot(LVector3 lv)
        {
            return Vector3.Dot(this, lv);
        }
        public LVector3 cross(LVector3 lv)
        {
            return new LVector3(Vector3.Cross(this, lv));
        }
        public float length()
        {
            return ((Vector3)this).Length();
        }
        public float lengthSq()
        {
            return ((Vector3)this).LengthSquared();
        }
        public LVector3 normalize()
        {
            return new LVector3(Vector3.Normalize(this));
        }
    }
}
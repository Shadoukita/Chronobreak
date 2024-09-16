using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using GameServerCore.Enums;

#nullable enable

namespace GameServerCore
{
    /// <summary>
    /// Class housing miscellaneous functions usually meant to make calculations look cleaner.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Float constant used in small float comparison operations.
        /// </summary>
        public const float COMPARE_EPSILON = 0.0001f;

        /// <summary>
        /// Whether or not the given Vector2 is within the specified boundaries
        /// </summary>
        /// <param name="v">Vector2 to check.</param>
        /// <param name="max">Vector2 maximums to check against.</param>
        /// <param name="min">Vector2 minimums to check against.</param>
        /// <returns>True/False</returns>
        public static bool IsVectorValid(Vector2 v, Vector2 max, Vector2 min)
        {
            return v.X <= max.X && v.Y <= max.Y && v.X >= min.X && v.Y >= min.Y;
        }

        /// <summary>
        /// Gets the squared length of the specified Vector2.
        /// </summary>
        /// <param name="v">Vector2 who's length should be squared.</param>
        /// <returns>Squared length of v</returns>
        public static float SqrLength(this Vector2 v)
        {
            return v.X * v.X + v.Y * v.Y;
        }

        /// <summary>
        /// Performs addition between a string and a list of bytes.
        /// Converts the string into an array of bytes, then adds the array to the list of bytes.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="val"></param>
        public static void Add(this List<byte> list, string val)
        {
            list.AddRange(Encoding.BigEndianUnicode.GetBytes(val));
        }

        /// <summary>
        /// Rotates v clockwise by the given angle with respect to the origin.
        /// </summary>
        /// <param name="v">Vector2 to rotate.</param>
        /// <param name="origin">Vector2 point to rotate around.</param>
        /// <param name="angle">Degrees to rotate by.</param>
        /// <returns>Rotated Vector2</returns>
        public static Vector2 Rotate(this Vector2 v, Vector2 origin, float angle)
        {
            // Rotating (px,py) around (ox, oy) with angle a
            // p'x = cos(a) * (px-ox) - sin(a) * (py-oy) + ox
            // p'y = sin(a) * (px-ox) + cos(a) * (py-oy) + oy
            angle = -DegreeToRadian(angle);
            var x = MathF.Cos(angle) * (v.X - origin.X) - MathF.Sin(angle) * (v.Y - origin.Y) + origin.X;
            var y = MathF.Sin(angle) * (v.X - origin.X) + MathF.Cos(angle) * (v.Y - origin.Y) + origin.Y;
            return new Vector2(x, y);
        }

        /// <summary>
        /// Rotates a Vector2 about the standard origin (0,0) by the given angle in degrees.
        /// </summary>
        /// <param name="v">Vector2 to rotate.</param>
        /// <param name="angle">Degrees to rotate.</param>
        /// <returns>Rotated Vector2</returns>
        public static Vector2 Rotate(this Vector2 v, float angle)
        {
            return v.Rotate(Vector2.Zero, angle);
        }

        /// <summary>
        /// Gets the angle from one Vector2 point to another relative to an origin point.
        /// </summary>
        /// <param name="v">Vector2 to start from.</param>
        /// <param name="vectorToGetAngle">Vector2 to point towards.</param>
        /// <param name="origin">Vector2 to orient around.</param>
        /// <returns>float Angle in degrees</returns>
        public static float AngleTo(this Vector2 v, Vector2 vectorToGetAngle, Vector2 origin)
        {
            // Make other vectors relative to the origin
            v -= origin;
            vectorToGetAngle -= origin;

            var norm = Vector2.Normalize(vectorToGetAngle - v);
            return UnitVectorToAngle(norm);
        }

        /// <summary>
        /// Calculates given triangle's area using Heron's formula.
        /// </summary>
        /// <param name="first">First corner of the triangle.</param>
        /// <param name="second">Second corner of the triangle</param>
        /// <param name="third">Third corner of the triangle.</param>
        /// <returns>the area of the triangle.</returns>
        public static float GetTriangleArea(Vector2 first, Vector2 second, Vector2 third)
        {
            var line1Length = Vector2.Distance(first, second);
            var line2Length = Vector2.Distance(second, third);
            var line3Length = Vector2.Distance(third, first);

            var s = (line1Length + line2Length + line3Length) / 2;

            return (float)Math.Sqrt(s * (s - line1Length) * (s - line2Length) * (s - line3Length));
        }

        /// <summary>
        /// Gets the squared distance from a specific point to a rectangle's border.
        /// </summary>
        /// <param name="rect">Rectangle center point.</param>
        /// <param name="width">Width of the rectangle.</param>
        /// <param name="height">Height of the rectangle.</param>
        /// <param name="origin">Point to check distance from.</param>
        /// <param name="rotation">Optional rotation of the rectangle in degrees (expensive).</param>
        /// <returns>Float squared distance.</returns>
        public static float DistanceSquaredToRectangle(Vector2 rect, float width, float height, Vector2 origin, float rotation = 0)
        {
            if (rotation != 0)
            {
                rect = Rotate(rect, rect, rotation);
                origin = Rotate(origin, rect, rotation);
            }

            float dx = MathF.Max(MathF.Abs(origin.X - rect.X) - width / 2, 0);
            float dy = MathF.Max(MathF.Abs(origin.Y - rect.Y) - height / 2, 0);

            return dx * dx + dy * dy;
        }

        /// <summary>
        /// Whether or not the given origin point lies inside the boundaries of the given rectangle.
        /// </summary>
        /// <param name="rect">Center point of the rectangle.</param>
        /// <param name="width">Width of the rectangle.</param>
        /// <param name="height">Height of the rectangle.</param>
        /// <param name="origin">Origin point to check.</param>
        /// <param name="rotation">Optional rotation of the rectangle in degrees (expensive).</param>
        /// <returns></returns>
        public static bool IsPointInRectangle(Vector2 rect, float width, float height, Vector2 origin, float rotation = 0)
        {
            if (rotation != 0)
            {
                // What? You thought the rectangle rotated? No, everything rotated around the rectangle.
                rect = Rotate(rect, rect, rotation);
                origin = Rotate(origin, rect, rotation);
            }

            float dx = MathF.Max(MathF.Abs(origin.X - rect.X) - width / 2, 0);
            float dy = MathF.Max(MathF.Abs(origin.Y - rect.Y) - height / 2, 0);

            if (dx == 0 && dy == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Whether or not the specified Vector2 v is within the specified range of the given Vector2 start.
        /// </summary>
        /// <param name="v">Vector2 to check.</param>
        /// <param name="start">Vector2 where the range starts.</param>
        /// <param name="range">Range to check around the start position.</param>
        /// <returns></returns>
        public static bool IsVectorWithinRange(Vector2 v, Vector2 start, float range)
        {
            float v1 = v.X - start.X, v2 = v.Y - start.Y;
            return ((v1 * v1) + (v2 * v2)) <= (range * range);
        }

        #region Trustworthy
        public const float RAD2DEG = 180 / MathF.PI;
        public const float DEG2RAD = MathF.PI / 180;
        public static float Angle(this Vector2 v) => MathF.Atan2(v.Y, v.X) * RAD2DEG;
        public static float AngleTo(this Vector2 A, Vector2 B) => MathF.Atan2(Cross(A, B), Dot(A, B)) * RAD2DEG;
        public static float Dot(Vector2 A, Vector2 B) => A.X * B.X + A.Y * B.Y;
        public static float Cross(Vector2 A, Vector2 B) => A.X * B.Y - A.Y * B.X;
        public static Vector2 FromAngle(float angle)
        {
            (float sin, float cos) = MathF.SinCos(angle * DEG2RAD);
            return new Vector2(cos, sin);
        }
        public static Vector2 ToVector2(this Vector3 vector) => new Vector2(vector.X, vector.Z);
        public static Vector3 ToVector3(this Vector2 vector, float Y) => new Vector3(vector.X, Y, vector.Y);
        public static float Distance(this Vector2 value1, Vector2 value2) => Vector2.Distance(value1, value2);
        public static float DistanceSquared(this Vector2 value1, Vector2 value2) => Vector2.DistanceSquared(value1, value2);
        public static float Distance(this Vector3 value1, Vector3 value2) => Vector3.Distance(value1, value2);
        public static float DistanceSquared(this Vector3 value1, Vector3 value2) => Vector3.DistanceSquared(value1, value2);
        public static float Dot(this Vector3 left, Vector3 right) => Vector3.Dot(left, right);
        public static Vector3 Rotated(this Vector3 vector3, float angle) => vector3.ToVector2().Rotated(angle).ToVector3(vector3.Y);
        public static Vector2 Rotated(this Vector2 vector2, float angle) // anticlockwise
        {
            var (sin, cos) = MathF.SinCos(angle * DEG2RAD);
            return new Vector2(
                (vector2.X * cos) - (vector2.Y * sin),
                (vector2.X * sin) + (vector2.Y * cos));
        }
        public static bool IsZero(this float a) => Math.Abs(a) < 1e-6f;
        public static Vector2 Normalized(this Vector2 vector2) => (vector2 == Vector2.Zero) ? Vector2.Zero : Vector2.Normalize(vector2);
        public static Vector3 Normalized(this Vector3 vector3) => (vector3 == Vector3.Zero) ? Vector3.Zero : Vector3.Normalize(vector3);
        public static Vector2 Perpendicular(this Vector2 vector2, bool clockwise = false) => clockwise ? new Vector2(vector2.Y, -vector2.X) : new Vector2(-vector2.Y, vector2.X);
        public static Vector3 Perpendicular(this Vector3 vector3, bool clockwise = false) => clockwise ? new Vector3(vector3.Z, vector3.Y, -vector3.X) : new Vector3(-vector3.Z, vector3.Y, vector3.X);

        // Based on https://stackoverflow.com/a/1501725
        public static float DistanceToSegmentSquared(this Vector2 p, Vector2 v, Vector2 w)
        {
            var l2 = v.DistanceSquared(w);
            //if (l2 == 0)
            //    return p.DistanceSquared(v);
            var wv = w - v;
            var t = Math.Clamp(Vector2.Dot(p - v, wv) / l2, 0, 1);
            var projection = v + t * wv;
            return p.DistanceSquared(projection);
        }
        #endregion

        /// <summary>
        /// Converts the given angle from degrees to radians.
        /// </summary>
        /// <param name="angle">Angle in degrees.</param>
        /// <returns>Angle in radians.</returns>
        public static float DegreeToRadian(float angle)
        {
            return MathF.PI * angle / 180.0f;
        }

        /// <summary>
        /// Converts the given angle from radians to degrees.
        /// </summary>
        /// <param name="angle">Angle in radians.</param>
        /// <returns>Angle in degrees.</returns>
        public static float RadianToDegree(float angle)
        {
            return angle * (180.0f / MathF.PI);
        }

        /// <summary>
        /// Converts the given normalized vector (such that |v| = 1) to an angle in degrees (0 -> 360).
        /// </summary>
        /// <param name="v">Vector2 to convert.</param>
        /// <returns>Angle in degrees (0 -> 360)</returns>
        public static float UnitVectorToAngle(Vector2 v)
        {
            var angle = RadianToDegree(MathF.Atan2(v.Y, v.X));

            // Clamp Atan2 degrees to 0 -> 360.
            return (angle + 360f) % 360f;
        }

        /// <summary>
        /// Gets the closest edge point on a circle from the given starting position.
        /// </summary>
        /// <param name="from">Starting position.</param>
        /// <param name="circlePos">Circle position.</param>
        /// <param name="radius">Radius of the circle.</param>
        /// <returns>Vector2 point on the circle closest to the starting position..</returns>
        public static Vector2 GetClosestCircleEdgePoint(Vector2 from, Vector2 circlePos, float radius)
        {
            return new Vector2(circlePos.X + (MathF.Cos(MathF.Atan2(from.Y - circlePos.Y, from.X - circlePos.X)) * radius),
                               circlePos.Y + (MathF.Sin(MathF.Atan2(from.Y - circlePos.Y, from.X - circlePos.X)) * radius));
        }

        /// <summary>
        /// Attempts to find the points of intersection between a line and a circle which is located at (0,0)
        /// </summary>
        /// <param name="p1">Closest line endpoint to the circle</param>
        /// <param name="p2">Second closest line endpoint to the circle</param>
        /// <param name="r">Radius of the circle</param>
        /// <returns>0 to 2 points of intersection</returns>
        public static List<Vector2> CircleLineIntersection(Vector2 p1, Vector2 p2, float r)
        {
            List<Vector2> intersections = new List<Vector2>();

            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            float dr = MathF.Sqrt((dx * dx) + (dy * dy));
            // Dot Product
            float D = (p1.X * p2.Y) - (p2.X * p1.Y);

            // Sign variable to account for the square rooting of distance;
            // this leads into two intersections
            int sgn = 1;
            if (dy < 0)
            {
                sgn = -1;
            }

            // Make nullable floats so we can check for 0 intersections
            float? x1 = (D * dy + (sgn * dx * MathF.Sqrt((r * r) * (dr * dr) - (D * D)))) / (dr * dr);
            float? y1 = (-D * dx + (MathF.Abs(dy) * MathF.Sqrt((r * r) * (dr * dr) - (D * D)))) / (dr * dr);
            float? x2 = (D * dy - (sgn * dx * MathF.Sqrt((r * r) * (dr * dr) - (D * D)))) / (dr * dr);
            float? y2 = (-D * dx - (MathF.Abs(dy) * MathF.Sqrt((r * r) * (dr * dr) - (D * D)))) / (dr * dr);

            float discriminant = (r * r) * (dr * dr) - (D * D);

            // Checking for no intersections
            if (discriminant > 0 && (x1 != null || x1.GetValueOrDefault() != 0) && (x2 != null || x2.GetValueOrDefault() != 0))
            {
                intersections.Add(new Vector2((float)x1, (float)y1));
                intersections.Add(new Vector2((float)x2, (float)y2));
            }
            // In the case of 1 intersection
            else if (discriminant == 0)
            {
                // Check which side intersected
                if (x1 != null || x1.GetValueOrDefault() != 0)
                {
                    intersections.Add(new Vector2((float)x1, (float)y1));
                }
                else if (x2 != null || x2.GetValueOrDefault() != 0)
                {
                    intersections.Add(new Vector2((float)x2, (float)y2));
                }
            }

            return intersections;
        }

        /// <summary>
        /// Attempts to find an escape point for the first given circle.
        /// Should only be used when the two given circles are intersecting.
        /// </summary>
        /// <param name="p1">Position of the first circle</param>
        /// <param name="r1">Radius of the first circle</param>
        /// <param name="p2">Position of the second circle</param>
        /// <param name="r2">Radius of the second circle</param>
        /// <returns></returns>
        public static Vector2 GetCircleEscapePoint(Vector2 p1, float r1, Vector2 p2, float r2)
        {
            Vector2 edgepoint1 = GetClosestCircleEdgePoint(p2, p1, r1);
            Vector2 edgepoint2 = GetClosestCircleEdgePoint(p1, p2, r2);

            return Vector2.Add(p1, Vector2.Subtract(edgepoint2, edgepoint1));
        }

        /// <summary>
        /// Casts two rays from point p to the left and right boundaries of a given circle.
        /// </summary>
        /// <param name="p">Point to cast the rays from.</param>
        /// <param name="c">Center of the circle.</param>
        /// <param name="r">Radius of the circle.</param>
        /// <returns>Array of 2 points representing the left and right bounds of the circle respectively.</returns>
        /// TODO: Could probably be more efficient by using an alternative to Cos and Sin.
        public static Vector2[] CastRayCircleBounds(Vector2 p, Vector2 c, float r)
        {
            var angleToCenter = p.AngleTo(c, p);
            var angleToLeft = angleToCenter + 270f;
            var angleToRight = angleToCenter + 90f;

            var cLeftBound = c + new Vector2(MathF.Cos(angleToLeft), MathF.Sin(angleToLeft)) * r;
            var cRightBound = c + new Vector2(MathF.Cos(angleToRight), MathF.Sin(angleToRight)) * r;

            return new Vector2[] { cLeftBound, cRightBound };
        }

        public static bool CheckCircleCollision(Vector2 c1Pos, float c1Radius, Vector2 c2Pos, float c2Radius)
        {
            float radius = c1Radius + c2Radius;
            float deltaX = c1Pos.X - c2Pos.X;
            float deltaY = c1Pos.Y - c2Pos.Y;
            return deltaX * deltaX + deltaY * deltaY <= radius * radius;
        }
    }

    /// <summary>
    /// Class for simple conversions between mathematical values and enums.
    /// </summary>
    public static class CustomConvert
    {
        /// <summary>
        /// Converts an integer to the TeamId assigned to it.
        /// </summary>
        /// <param name="i">Integer to convert.</param>
        /// <returns>TeamId.</returns>
        public static TeamId ToTeamId(this int i)
        {
            var dic = new Dictionary<int, TeamId>
            {
                { 0, TeamId.TEAM_ORDER },
                { (int)TeamId.TEAM_ORDER, TeamId.TEAM_ORDER },
                { 1, TeamId.TEAM_CHAOS },
                { (int)TeamId.TEAM_CHAOS, TeamId.TEAM_CHAOS }
            };

            if (!dic.ContainsKey(i))
            {
                return (TeamId)2;
            }

            return dic[i];
        }

        /// <summary>
        /// Converts the TeamId to the integer representing it.
        /// </summary>
        /// <param name="team">TeamId to convert.</param>
        /// <returns>Integer.</returns>
        public static int FromTeamId(this TeamId team)
        {
            var dic = new Dictionary<TeamId, int>
            {
                { TeamId.TEAM_ORDER, 0 },
                { TeamId.TEAM_CHAOS, 1 }
            };

            if (!dic.ContainsKey(team))
            {
                return 2;
            }

            return dic[team];
        }

        /// <summary>
        /// Gets the opposite team of the given team.
        /// </summary>
        /// <param name="team">TeamId to check.</param>
        /// <returns>Enemy TeamId.</returns>
        public static TeamId GetEnemyTeam(this TeamId team)
        {
            var dic = new Dictionary<TeamId, TeamId>
            {
                { TeamId.TEAM_ORDER, TeamId.TEAM_CHAOS },
                { TeamId.TEAM_CHAOS, TeamId.TEAM_ORDER }
            };

            if (!dic.ContainsKey(team))
            {
                return (TeamId)2;
            }

            return dic[team];
        }
    }
}

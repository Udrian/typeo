using System;

namespace Typedeaf.TypeO.Common
{
    public class Ray
    {
        public Vec3 Position;
        public Vec3 Direction;

        public Ray(Vec3 position, Vec3 direction) {
            Position = position;
            Direction = direction;
        }

        public float? Intersects(Plane plane) {
            var den = Vec3.Dot(Direction, plane.Normal);
            if(Math.Abs(den) < 0.00001f) {
                return null;
            }

            var result = (-plane.D - Vec3.Dot(plane.Normal, Position)) / den;

            if(result < 0.0f) {
                if(result < -0.00001f) {
                    return null;
                }

                result = 0.0f;
            }

            return result;
        }
    }
}

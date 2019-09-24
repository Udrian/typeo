using System;

namespace Typedeaf.TypeOCommon
{
    public struct Vec2 : IEquatable<Vec2>
    {
        public float X;
        public float Y;

        public Vec2(float xy) {
            X = xy;
            Y = xy;
        }

        public Vec2(float x, float y) {
            X = x;
            Y = y;
        }

        public Vec2(Vec2 vec) {
            X = vec.X;
            Y = vec.Y;
        }

        public void Set(float x, float y) {
            X = x;
            Y = y;
        }

        public void Set(Vec2 vec) {
            X = vec.X;
            Y = vec.Y;
        }

        public static Vec2 operator+(Vec2 a, Vec2 b) {
            return new Vec2(a.X + b.X, a.Y + b.Y);
        }

        public static Vec2 operator+(Vec2 a, float b) {
            return new Vec2(a.X + b, a.Y + b);
        }

        public static Vec2 operator+(float a, Vec2 b) {
            return new Vec2(a + b.X, a + b.Y);
        }

        public static Vec2 operator-(Vec2 a, Vec2 b) {
            return new Vec2(a.X - b.X, a.Y - b.Y);
        }

        public static Vec2 operator-(Vec2 a, float b) {
            return new Vec2(a.X - b, a.Y - b);
        }

        public static Vec2 operator-(float a, Vec2 b) {
            return new Vec2(a - b.X, a - b.Y);
        }

        public static Vec2 operator-(Vec2 a) {
            return new Vec2(-a.X, -a.Y);
        }

        public float LengthSquared() {
            return (X * X) + (Y * Y);
        }

        public float Length() {
            return (float)Math.Sqrt(LengthSquared());
        }

        public float Distance(Vec2 to) {
            return Distance(this, to);
        }

        public float DistanceSquared(Vec2 to) {
            return DistanceSquared(this, to);
        }

        public static float Distance(Vec2 from, Vec2 to) {
            return (to - from).Length();
        }

        public static float DistanceSquared(Vec2 from, Vec2 to) {
            return (to - from).LengthSquared();
        }

        public void Normalize() {
            float l = Length();
            if(l <= 0) return;
            float factor = 1.0f/l;
            X *= factor;
            Y *= factor;
        }

        public Vec2 Rotate(float radians)
        {
            float cosRadians = (float)Math.Cos(radians);
            float sinRadians = (float)Math.Sin(radians);

            return new Vec2(
                X * cosRadians - Y * sinRadians,
                X * sinRadians + Y * cosRadians);
        }

        public static Vec2 operator*(Vec2 a, Vec2 b) {
            return new Vec2(a.X * b.X, a.Y * b.Y);
        }

        public static Vec2 operator/(Vec2 a, Vec2 b) {
            return new Vec2(a.X / b.X, a.Y / b.Y);
        }

        public static Vec2 operator*(Vec2 a, float scalar) {
            return new Vec2(a.X * scalar, a.Y * scalar);
        }

        public static Vec2 operator*(float scalar, Vec2 a) {
            return new Vec2(a.X * scalar, a.Y * scalar);
        }

        public static Vec2 operator/(Vec2 a, float scalar) {
            return new Vec2(a.X / scalar, a.Y / scalar);
        }

        public static Vec2 operator/(float scalar, Vec2 a) {
            return new Vec2(scalar / a.X, scalar / a.Y);
        }

        public static bool operator==(Vec2 a, Vec2 b) {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator!=(Vec2 a, Vec2 b) {
            return !(a == b);
        }

        public float Dot(Vec2 vec) {
            return (X * vec.X) + (Y * vec.Y);
        }

        public static float Dot(Vec2 a, Vec2 b) {
            return a.Dot(b);
        }

        public static Vec2 Max(Vec2 a, Vec2 b) {
            return new Vec2(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y));
        }

        public static Vec2 Min(Vec2 a, Vec2 b) {
            return new Vec2(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y));
        }

        public static Vec2 One     { get { return new Vec2(1); } }
        public static Vec2 Zero  { get { return new Vec2(); } }
        public static Vec2 UnitY { get { return new Vec2(0, 1); } }
        public static Vec2 UnitX { get { return new Vec2(1, 0); } }

        public bool Equals(Vec2 other) {
            return (X == other.X && Y == other.Y);
        }
    }
}

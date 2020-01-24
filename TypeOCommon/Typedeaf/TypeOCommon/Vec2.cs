﻿using System;

namespace Typedeaf.TypeOCommon
{
    public class Vec2 : IEquatable<Vec2>
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vec2()
        {
            X = 0;
            Y = 0;
        }

        public Vec2(double xy) {
            X = xy;
            Y = xy;
        }

        public Vec2(double x, double y) {
            X = x;
            Y = y;
        }

        public Vec2(Vec2 vec) {
            X = vec.X;
            Y = vec.Y;
        }

        public void Set(double x, double y) {
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

        public static Vec2 operator+(Vec2 a, double b) {
            return new Vec2(a.X + b, a.Y + b);
        }

        public static Vec2 operator+(double a, Vec2 b) {
            return new Vec2(a + b.X, a + b.Y);
        }

        public static Vec2 operator-(Vec2 a, Vec2 b) {
            return new Vec2(a.X - b.X, a.Y - b.Y);
        }

        public static Vec2 operator-(Vec2 a, double b) {
            return new Vec2(a.X - b, a.Y - b);
        }

        public static Vec2 operator-(double a, Vec2 b) {
            return new Vec2(a - b.X, a - b.Y);
        }

        public static Vec2 operator-(Vec2 a) {
            return new Vec2(-a.X, -a.Y);
        }

        public double LengthSquared() {
            return (X * X) + (Y * Y);
        }

        public double Length() {
            return Math.Sqrt(LengthSquared());
        }

        public double Distance(Vec2 to) {
            return Distance(this, to);
        }

        public double DistanceSquared(Vec2 to) {
            return DistanceSquared(this, to);
        }

        public static double Distance(Vec2 from, Vec2 to) {
            return (to - from).Length();
        }

        public static double DistanceSquared(Vec2 from, Vec2 to) {
            return (to - from).LengthSquared();
        }

        public void Normalize() {
            var l = Length();
            if(l <= 0) return;
            var factor = 1.0/l;
            X *= factor;
            Y *= factor;
        }

        public Vec2 Rotate(double radians)
        {
            var cosRadians = Math.Cos(radians);
            var sinRadians = Math.Sin(radians);

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

        public static Vec2 operator*(Vec2 a, double scalar) {
            return new Vec2(a.X * scalar, a.Y * scalar);
        }

        public static Vec2 operator*(double scalar, Vec2 a) {
            return new Vec2(a.X * scalar, a.Y * scalar);
        }

        public static Vec2 operator/(Vec2 a, double scalar) {
            return new Vec2(a.X / scalar, a.Y / scalar);
        }

        public static Vec2 operator/(double scalar, Vec2 a) {
            return new Vec2(scalar / a.X, scalar / a.Y);
        }

        public static bool operator==(Vec2 a, Vec2 b) {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator!=(Vec2 a, Vec2 b) {
            return !(a == b);
        }

        public double Dot(Vec2 vec) {
            return (X * vec.X) + (Y * vec.Y);
        }

        public static double Dot(Vec2 a, Vec2 b) {
            return a.Dot(b);
        }

        public static Vec2 Max(Vec2 a, Vec2 b) {
            return new Vec2(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y));
        }

        public static Vec2 Min(Vec2 a, Vec2 b) {
            return new Vec2(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y));
        }

        public static Vec2 One   { get { return new Vec2(1); } }
        public static Vec2 Zero  { get { return new Vec2(); } }
        public static Vec2 UnitY { get { return new Vec2(0, 1); } }
        public static Vec2 UnitX { get { return new Vec2(1, 0); } }

        public bool Equals(Vec2 other) {
            return (X == other.X && Y == other.Y);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 23;
                hash = hash * 31 + X.GetHashCode();
                hash = hash * 31 + Y.GetHashCode();
                return hash;
            }
        }
    }
}

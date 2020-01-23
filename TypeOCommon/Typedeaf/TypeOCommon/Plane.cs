namespace Typedeaf.TypeOCommon
{
    public class Plane
    {
        public Vec3 Normal;
        public double D;

        public Plane(Vec3 normal, double d) {
            Normal = normal;
            Normal.Normalize();
            D = d;
        }
    }
}

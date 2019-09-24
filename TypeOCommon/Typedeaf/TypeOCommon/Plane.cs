namespace Typedeaf.TypeOCommon
{
    public class Plane
    {
        public Vec3 Normal;
        public float D;

        public Plane(Vec3 normal, float d) {
            Normal = normal;
            Normal.Normalize();
            D = d;
        }
    }
}

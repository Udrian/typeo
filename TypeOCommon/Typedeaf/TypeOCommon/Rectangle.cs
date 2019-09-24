namespace Typedeaf.TypeOCommon
{
    public class Rectangle
    {
        public Vec2 Pos;
        public Vec2 Size;

        public Rectangle() {
            Pos = new Vec2();
            Size = new Vec2();
        }

        public Rectangle(Vec2 pos, Vec2 size) {
            Pos = pos;
            Size = size;
        }

        public Rectangle(float x, float y, float width, float height) {
            Pos = new Vec2(x, y);
            Size = new Vec2(width, height);
        }
    }
}

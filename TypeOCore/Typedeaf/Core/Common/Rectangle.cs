namespace TypeOEngine.Typedeaf.Core
{
    namespace Common
    {
        public class Rectangle
        {
            public Vec2 Pos { get; set; }
            public Vec2 Size { get; set; }

            public Rectangle()
            {
                Pos = new Vec2();
                Size = new Vec2();
            }

            public Rectangle(Vec2 pos, Vec2 size)
            {
                Pos = pos;
                Size = size;
            }

            public Rectangle(double x, double y, double width, double height)
            {
                Pos = new Vec2(x, y);
                Size = new Vec2(width, height);
            }
        }
    }
}
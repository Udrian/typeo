namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine
    {
        public class Version
        {
            public int Major { get; private set; }
            public int Minor { get; private set; }
            public int Patch { get; private set; }

            public Version(int major, int minor, int patch)
            {
                Major = major;
                Minor = minor;
                Patch = patch;
            }

            public override string ToString()
            {
                return $"{Major}.{Minor}.{Patch}";
            }

            public bool Eligable(Version to)
            {
                if (Major > to.Major) return true;
                if (Major == to.Major && Minor >= to.Minor) return true;
                return false;
                //return (Major  to.Major && Minor >= to.Minor);
            }
        }
    }
}

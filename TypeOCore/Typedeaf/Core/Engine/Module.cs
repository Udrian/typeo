namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine
    {
        public abstract class Module
        {
            public TypeO TypeO { get; protected set; }
            public Version Version { get; protected set; }
            public bool WillLoadExtensions { get; protected set; }

            internal Module(Version version)
            {
                Version = version;
            }

            public abstract void Initialize();
            public abstract void Cleanup();
            public abstract void LoadExtensions();
            public abstract void CreateOption();
        }

        public abstract class Module<O> : Module where O : ModuleOption, new()
        {
            public O Option { get; protected set; }

            protected Module(Version version) : base(version) { }

            public override void CreateOption()
            {
                Option = new O();
            }
        }
    }
}

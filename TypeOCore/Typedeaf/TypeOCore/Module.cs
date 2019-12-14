using Typedeaf.TypeOCore.Input;

namespace Typedeaf.TypeOCore
{
    public abstract class Module : ITypeO, IHasTypeO
    {
        public KeyboardInput.Internal KeyHandler { get { return TypeO.KeyHandler; } set { TypeO.KeyHandler = value; } }
        public KeyConverter KeyConverter { get { return TypeO.KeyConverter; } set { TypeO.KeyConverter = value; } }

        ITypeO IHasTypeO.TypeO { get; set; }
        protected ITypeO TypeO { get { return (this as IHasTypeO).GetTypeO(); } }

        public abstract void Initialize();
        public abstract void Cleanup();
        public abstract void Update(float dt);

        public void Exit()
        {
            TypeO.Exit();
        }

        public ITypeO SetKeyAlias(object input, object key)
        {
            return TypeO.SetKeyAlias(input, key);
        }

        public M LoadModule<M>() where M : Module, new()
        {
            return TypeO.LoadModule<M>();
        }

        public void Start()
        {
            TypeO.Start();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Typedeaf.TypeOCore.Services;

namespace Typedeaf.TypeOCore
{
    namespace Services
    {

        public abstract class Service : IHasTypeO
        {
            ITypeO IHasTypeO.TypeO { get; set; }
            protected ITypeO TypeO { get { return (this as IHasTypeO).GetTypeO(); } }

            public bool Pause { get; set; }

            protected Service() { }

            public abstract void Initialize();
        }
    }
}

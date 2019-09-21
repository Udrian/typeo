using System.Collections.Generic;
using Typedeaf.TypeO.Engine.Content;
using Typedeaf.TypeO.Engine.Input;

namespace Typedeaf.TypeO.Engine
{
    namespace Input
    {
        public partial class KeyConverter
        {
            protected Core.TypeO TypeO { get; private set; }

            public KeyConverter(Core.TypeO typeO)
            {
                TypeO = typeO;
                InputToKeyConverter = new Dictionary<object, object>();
            }

            private Dictionary<object, object> InputToKeyConverter { get; set; }

            public KeyConverter SetKey(object input, object key)
            {
                InputToKeyConverter.Add(input, key);
                return this;
            }

            public bool ContainsInput(object input)
            {
                return InputToKeyConverter.ContainsKey(input);
            }

            public object GetKey(object input)
            {
                return InputToKeyConverter[input];
            }
        }
    }

    namespace Core
    {
        public partial class TypeO
        {
            public KeyConverter KeyConverter { get; private set; }
        }
    }
}

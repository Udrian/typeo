using System.Collections.Generic;
using Typedeaf.TypeOCore.Content;
using Typedeaf.TypeOCore.Input;

namespace Typedeaf.TypeOCore
{
    namespace Input
    {
        public partial class KeyConverter
        {
            protected TypeO TypeO { get; private set; }

            public KeyConverter(TypeO typeO)
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

    public partial class TypeO
    {
        public KeyConverter KeyConverter { get; private set; }
    }
}

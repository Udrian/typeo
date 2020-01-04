﻿using System.Collections.Generic;

namespace Typedeaf.TypeOCore
{
    namespace Input
    {
        public partial class KeyConverter
        {
            public KeyConverter()
            {
                InputToKeyConverter = new Dictionary<object, object>();
            }

            private Dictionary<object, object> InputToKeyConverter { get; set; }

            public KeyConverter SetKeyAlias(object input, object key)
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
}

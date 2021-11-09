﻿using System.Collections.Generic;
using TypeD;

namespace TypeDCore.Code.Entity
{
    class EntityCode : Codalyzer
    {
        public EntityCode(string className, string @namespace) : base()
        {
            Init(className, @namespace);
            AutoGeneratedFile = false;
            PartialClass = true;
            TypeDClass = false;

            BaseClass = "Entity2d";
        }

        public override void Init()
        {
            Usings.AddRange(new List<string>()
            {
                "TypeOEngine.Typedeaf.Core.Entities"
            });

            AddFunction(new Function("protected void InternalInitialize()", () => { }));
            AddFunction(new Function("public override void Cleanup()", () => { }));
        }
    }
}

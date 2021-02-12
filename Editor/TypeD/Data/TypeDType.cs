using System;
using System.Collections.Generic;
using System.Reflection;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;

namespace TypeD.Data
{
    public enum TypeDTypeType
    {
        Game,
        Scene,
        Entity,
        Stub,
        Logic,
        Drawable,
        EntityData
    }

    public class TypeDType
    {
        public static TypeDTypeType? SubclasToTypeDType(Type type)
        {
            if (type.IsSubclassOf(typeof(Game))) return TypeDTypeType.Game;
            if (type.IsSubclassOf(typeof(Scene))) return TypeDTypeType.Scene;
            if (type.IsSubclassOf(typeof(Entity))) return TypeDTypeType.Entity;
            if (type.IsSubclassOf(typeof(Stub))) return TypeDTypeType.Stub;
            if (type.IsSubclassOf(typeof(Logic))) return TypeDTypeType.Logic;
            if (type.IsSubclassOf(typeof(Drawable))) return TypeDTypeType.Drawable;
            if (type.IsSubclassOf(typeof(EntityData))) return TypeDTypeType.EntityData;

            return null;
        }

        public List<Codalyzer> Codes { get; private set; }
        public TypeInfo TypeInfo { get; set; }
        public TypeDTypeType TypeType { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string FullName { get { return $"{Namespace}.{Name}"; } }

        public TypeDType()
        {
            Codes = new List<Codalyzer>();
        }
    }
}

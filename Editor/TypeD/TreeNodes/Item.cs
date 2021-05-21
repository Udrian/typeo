using System;

namespace TypeD.TreeNodes
{
    public abstract class Item
    {
        public static string? SubclasToTypeDType(Type type)
        {
            return type.Name;

            /*if(type.IsSubclassOf(typeof(Game))) return TypeDTypeType.Game;
            if(type.IsSubclassOf(typeof(Scene))) return TypeDTypeType.Scene;
            if(type.IsSubclassOf(typeof(Entity))) return TypeDTypeType.Entity;
            if(type.IsSubclassOf(typeof(Stub))) return TypeDTypeType.Stub;
            if(type.IsSubclassOf(typeof(Logic))) return TypeDTypeType.Logic;
            if(type.IsSubclassOf(typeof(Drawable))) return TypeDTypeType.Drawable;
            if(type.IsSubclassOf(typeof(EntityData))) return TypeDTypeType.EntityData;

            return null;*/
        }

        public string Type { get; set; }
        public string Name { get; set; }

        public abstract void Save();
    }
}

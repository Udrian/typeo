using System;
using System.Collections.Generic;
using System.Reflection;
using TypeD.Models;

namespace TypeD.Types
{
    public abstract class TypeOType
    {
        private static Dictionary<Type, Type> TypeOTypeTypes { get; set; } = new();

        public static void AddTypeOType(Type baseType, Type typeOType)
        {
            TypeOTypeTypes.Add(baseType, typeOType);
        }

        public static string GetBaseTypeOClassName(Type subType)
        {
            foreach(var type in TypeOTypeTypes.Keys)
            {
                if (subType.IsSubclassOf(type)) return type.Name;
            }

            return "";
        }

        public static TypeOType InstantiateTypeOType(string baseTypeName, string className, string @namespace, string typeOBaseType, TypeInfo typeInfo, ProjectModel project)
        {
            Type baseType = null;
            foreach(var type in TypeOTypeTypes.Keys)
            {
                if(type.Name == baseTypeName)
                {
                    baseType = type;
                }
            }
            if(baseType == null) return null;
            return (TypeOType)Activator.CreateInstance(baseType, new object[] { className, @namespace, typeOBaseType, typeInfo, project });
        }

        public string ClassName { get; internal set; }
        public string Namespace { get; internal set; }
        public string FullName { get { return $"{Namespace}.{ClassName}"; } }
        public string TypeOBaseType { get; internal set; }

        public List<Codalyzer> Codes { get; private set; }
        public TypeInfo TypeInfo { get; internal set; }
        public ProjectModel Project { get; internal set; }

        public TypeOType(string className, string @namespace, string typeOBaseType, TypeInfo typeInfo, ProjectModel project)
        {
            Codes = new List<Codalyzer>();

            ClassName = className;
            Namespace = @namespace;
            TypeOBaseType = typeOBaseType;
            TypeInfo = typeInfo;
            Project = project;
        }

        public void Save()
        {
            foreach (var code in Codes)
            {
                code.Generate();
                code.Save();
            }
        }
    }
}

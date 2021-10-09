using System;
using System.Collections.Generic;
using System.Reflection;
using TypeD.Models.Data;

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

        public static TypeOType InstantiateTypeOType(string typeOBaseType, string classname, string @namespace, TypeInfo typeInfo, Project project)
        {
            Type baseType = null;
            foreach(var keyValuePair in TypeOTypeTypes)
            {
                if(keyValuePair.Key.Name == typeOBaseType)
                {
                    baseType = keyValuePair.Value;
                }
            }
            if(baseType == null) return null;
            var retObj = (TypeOType)Activator.CreateInstance(baseType);
            retObj.TypeOBaseType = typeOBaseType;
            retObj.Project = project;
            if(typeInfo != null)
            {
                retObj.TypeInfo = typeInfo;
                retObj.ClassName = typeInfo.Name;
                retObj.Namespace = typeInfo.Namespace;
                retObj.Init();
            }
            else
            {
                retObj.ClassName = classname;
                retObj.Namespace = @namespace;
            }
            return retObj;
        }

        public string ClassName { get; internal set; }
        public string Namespace { get; internal set; }
        public string FullName { get { return $"{Namespace}.{ClassName}"; } }
        public string TypeOBaseType { get; internal set; }

        public List<Codalyzer> Codes { get; private set; }
        public TypeInfo TypeInfo { get; internal set; }
        public Project Project { get; internal set; }

        public TypeOType()
        {
            Codes = new List<Codalyzer>();
        }

        public abstract void Init();

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

using System;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;

namespace TypeD.Models
{
    public class TypeOTypeModel : ITypeOTypeModel
    {
        public Type GetType(TypeOType typeOType)
        {
            return Type.GetType(typeOType.FullName);
        }
    }
}

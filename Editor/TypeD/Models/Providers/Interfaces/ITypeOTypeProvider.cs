using System.Collections.Generic;
using TypeD.Models.Data;

namespace TypeD.Models.Providers.Interfaces
{
    public interface ITypeOTypeProvider
    {
        public TypeOType Load(Project project, string fullName);
        public void Save(Project project, TypeOType typeOType);
        public bool Exists(Project project, TypeOType typeOType);
        public List<TypeOType> ListAll(Project project);
    }
}

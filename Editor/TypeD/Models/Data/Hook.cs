namespace TypeD.Models.Data
{
    public class Hook
    {
        public override string ToString()
        {
            var type = GetType();
            var name = type.Name;
            return name.EndsWith("Hook") ? name.Substring(0, name.Length - "Hook".Length) : type.FullName;
        }
    }
}

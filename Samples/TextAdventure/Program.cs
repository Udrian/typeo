using TypeOEngine.Typedeaf.Core.Engine;

namespace TextAdventure
{
    class Program
    {
        static void Main()
        {
            TypeO.Create<TextAdventureGame>("Text Adventure")
                .SetLogger(LogLevel.Info)
                .Start();
        }
    }
}

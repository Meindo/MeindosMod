using System.IO;

namespace MeindosMod
{
    public class FileWriter
    {
        public static void CreateTFile()
        {
            const string file = "Thanks_for_using_MeindosMod!.txt";
            if (!File.Exists(file))
            {
                File.WriteAllText(file, "<3");
            }
        }
    }
}
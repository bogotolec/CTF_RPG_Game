using System;
using System.IO;
using CTF_RPG_Game.MapComponents;

namespace CTF_RPG_Game_Server
{
    class Program
    {
        public static int PORT;
        public static string CONFIGURATION_FILE = "config";

        static void Main(string[] args)
        {
            Map map = new Map();
            Console.ReadKey();
            InitializeServerConfiguration(); 
        }

        private static void InitializeServerConfiguration()
        {
            string[] configStrings = File.ReadAllText(CONFIGURATION_FILE).ToLower().Split('\n');
            foreach (var cString in configStrings)
            {
                if (cString.StartsWith("#"))
                    continue;

                else if (cString.StartsWith("port="))
                    PORT = int.Parse(cString.Substring(5));
            }
        }
    }
}

using System;
using System.IO;
using CTF_RPG_Game.MapComponents;
using CTF_RPG_Game.ClientInteraction;
using System.Data.SqlClient;
using System.Collections;

namespace CTF_RPG_Game_Server
{
    class Program
    {
        public static int PORT = 8888;
        public static string DBConnectionString;
        public static string CONFIGURATION_FILE = "config";
        public static bool ConsoleMessages = true;

        static void Main(string[] args)
        {
            InitializeServerConfiguration();
            ConnectionManager CManager = ConnectionManager.GetManager();
            CManager.StartListen();
        }

        private static void InitializeServerConfiguration()
        {
            string[] configStrings = File.ReadAllText(CONFIGURATION_FILE).Split('\n');
            foreach (var cString in configStrings)
            {
                if (cString.StartsWith("#"))
                    continue;
                else if (cString.ToLower().StartsWith("port="))
                    PORT = int.Parse(cString.Substring("port=".Length));
                else if (cString.ToLower().StartsWith("dbconnectionstring=\""))
                    DBConnectionString = cString.Substring(20).Trim('"');
                else if (cString.ToLower().StartsWith("ConsoleMessages="))
                    ConsoleMessages = bool.Parse(cString.Substring("ConsoleMessages=".Length));          
            }

            if (ConsoleMessages)
                Console.WriteLine("Initialization complete");
        }
    }

    class NoDBConnectionStringException : Exception
    {
        public override string Message
        {
            get
            {
                return "В файле конфигурации нет параметра \"BDConnectionString\".";
            }
        }
    }
}

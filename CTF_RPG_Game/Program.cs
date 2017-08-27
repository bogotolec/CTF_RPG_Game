using System;
using System.IO;
using CTF_RPG_Game.MapComponents;
using CTF_RPG_Game.ClientInteraction;
using System.Threading;

namespace CTF_RPG_Game_Server
{
    class Program
    {
        public static int PORT = 8888;
        public static string DBConnectionString;
        public static string CONFIGURATION_FILE = "config";
        public static string MAP_NAME = "none";
        public static bool ConsoleMessages = true;

        private static ConnectionManager CManager = ConnectionManager.GetManager();
        private static Thread Listener = new Thread(CManager.StartListen);

        static void Main(string[] args)
        {
            InitializeServerConfiguration();

            Task.CreateTasks();
            Map.GetMap();

            Listener.Start();

            ConsoleCommandHandle();
        }

        private static void ConsoleCommandHandle()
        {
            while (true)
            {
                string Command = Console.ReadLine().ToLower();

                switch (Command)
                {
                    case "ss":
                    case "stopserver":
                        Console.WriteLine("Do you really want to stop server? (y/n)");
                        if (Console.ReadLine().ToLower() != "y")
                            break;

                        foreach (var connection in ConnectionManager.ConnectionList)
                        {
                            Thread closer = new Thread(connection.CloseConnection);
                            closer.Start();
                        }

                        Thread aborter = new Thread(new ThreadStart(Listener.Abort));
                        aborter.Start();

                        while (ConnectionManager.ConnectionList.Count != 0)
                        {
                            Thread.Sleep(1000);
                        }

                        Environment.Exit(0);
                        break;

                    case "sc":
                    case "showconnections":
                        foreach (var connection in ConnectionManager.ConnectionList)
                        {
                            Console.WriteLine(connection);
                        }
                        break;

                    case "?":
                    case "commands":
                    case "help":
                        Console.WriteLine("StopServer (SS): stop listener, close all connections and exit.");
                        Console.WriteLine("ShowConnections (SC): show information about every connection.");
                        Console.WriteLine();
                        break;

                    default:
                        if (ConsoleMessages)
                            Console.WriteLine("Unknown command, type 'commands' or 'help' for help");
                        break;
                }
            }
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
                else if (cString.ToLower().StartsWith("consolemessages="))
                    ConsoleMessages = bool.Parse(cString.Substring("consolemessages=".Length));
                else if (cString.ToLower().StartsWith("map="))
                    MAP_NAME = cString.Substring("map=".Length);
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

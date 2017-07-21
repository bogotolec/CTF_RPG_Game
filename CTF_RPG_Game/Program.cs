﻿using System;
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
                    PORT = int.Parse(cString.Substring(5));
                else if (cString.ToLower().StartsWith("dbconnectionstring=\""))
                    DBConnectionString = cString.Substring(20).Trim('"');
            }

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

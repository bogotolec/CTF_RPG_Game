﻿using System;
using System.IO;
using CTF_RPG_Game.MapComponents;
using System.Data.SqlClient;
using System.Collections;

namespace CTF_RPG_Game_Server
{
    class Program
    {
        public static int PORT = 8888;
        private static string DBConnectionString;
        public static string CONFIGURATION_FILE = "config";
        public static SqlConnection SQLCLIENT;


        static void Main(string[] args)
        {
            Map map = new Map();
            Console.WriteLine(map);
            Console.ReadKey();
            InitializeServerConfiguration();
            SQLCLIENT = ConnectToDB();
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
        }

        private static SqlConnection ConnectToDB()
        {
            if (DBConnectionString == null)
                throw new NoDBConnectionStringException();
            
            return new SqlConnection(DBConnectionString);
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

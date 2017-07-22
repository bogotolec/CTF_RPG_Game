using System;
using System.Text;
using System.Runtime.InteropServices;

namespace CTF_RPG_Game_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialization
            const string IP = "127.0.0.1";
            const int Port = 8888;

            // Console settings
            Console.Title = "CTF RPG GAME";
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            // Test
            Console.WriteLine("!!!");
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("!!!");
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("!!!");
            Console.ReadLine();

            // Create connection
            ConnectionManager Manager = new ConnectionManager(IP, Port);

            // Start Game
            Game Game = new Game(Manager);

        }
    }
}

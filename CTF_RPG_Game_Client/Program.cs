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

            // Create connection
            ConnectionManager Manager = null;

            try
            {
                Manager = new ConnectionManager(IP, Port);
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                Console.WriteLine("Connection error: " + ex.Message);
                Console.WriteLine("Press any key for close...");
                Console.ReadKey();
            }

            // Start Game
            Game Game = new Game(Manager);
            Game.Start();
        }
    }
}

using System;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using CTF_RPG_Game_Server;
using System.Collections.Generic;

namespace CTF_RPG_Game.ClientInteraction
{
    class ConnectionManager
    {
        private static IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Program.PORT);
        private static Socket ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public static List<SocketHandler> ConnectionList = new List<SocketHandler>();

        // !!!SINGLETON WARNING!!!
        private static ConnectionManager instance;

        static ConnectionManager()
        { }

        public static ConnectionManager GetManager()
        {
            if (instance == null)
            {
                instance = new ConnectionManager();
            }
            return instance;
        }
        // !!!END OF SINGLETON WARNING!!!

        public void StartListen()
        {
            try
            {
                // Start listening
                ListenSocket.Bind(ipPoint);
                ListenSocket.Listen(32);

                if (Program.ConsoleMessages)
                    Console.WriteLine("Start listening for connection");

                // Socket handler
                while (true)
                {
                    Socket handler = ListenSocket.Accept();
                    SocketHandler SH = new SocketHandler(handler);

                    if (Program.ConsoleMessages)
                        Console.WriteLine("Client connected");

                    ConnectionList.Add(SH);

                    Thread Handler = new Thread(SH.Handle);
                    Thread CloseChecker = new Thread(new ParameterizedThreadStart(SH.CloseCheck));

                    Handler.Start();
                    CloseChecker.Start(Handler);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                ListenSocket.Close();
            }
        }
    }
}

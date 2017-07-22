using System;
using System.Threading;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace CTF_RPG_Game_Client
{
    class ConnectionManager
    {
        Socket socket;

        public ConnectionManager(string ServerIP, int ServerPort)
        {
            // Create IP address
            IPAddress IPAddress = IPAddress.Parse(ServerIP);
            IPEndPoint EP = new IPEndPoint(IPAddress, ServerPort);

            // Create Socket
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(EP);
        }

        public void Send(string Data)
        {
            while (Data.Length == 0)
                Data = Console.ReadLine();
            byte[] DataBytes = Encoding.UTF8.GetBytes(Data);
            socket.Send(DataBytes);
        }

        public string Get()
        {
            StringBuilder StringBuilder = new StringBuilder();
            byte[] DataBytes = new byte[1024];
            int ReceivedBytes;

            // Wait for message
            while (socket.Available == 0)
            {
                Thread.Sleep(100);
            }

            // Get message
            while (socket.Available > 0)
            {
                ReceivedBytes = socket.Receive(DataBytes);
                StringBuilder.Append(Encoding.UTF8.GetString(DataBytes, 0, ReceivedBytes));
            }

            // Return complete message
            return StringBuilder.ToString();
        }
    }
}

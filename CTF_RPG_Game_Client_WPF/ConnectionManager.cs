using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows;

namespace CTF_RPG_Game_Client_WPF
{
    public class ConnectionManager
    {
        private IPEndPoint IPEP;
        Socket S;

        static ConnectionManager ConnectionManagerObject;

        private ConnectionManager()
        {
            try
            {
                string[] data = File.ReadAllLines("connection.cfg");

                IPAddress IP = IPAddress.Parse(data[0]);
                Int16 Port = Int16.Parse(data[1]);

                IPEP = new IPEndPoint(IP, Port);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ConnectionFileCreate window = new ConnectionFileCreate();
                window.Show();
                window.Closing += Window_Closing;
                return;
            }

            S = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                S.Connect(IPEP);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Не удаётся подключиться к серверу.");
                Environment.Exit(0);
            }

            Auth();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.Manager = new ConnectionManager();
        }

        private void Auth()
        {
            try
            {
                Thread.Sleep(1000);
                if (S.Available > 0)
                    Get();

                if (!File.Exists("auth.cfg"))
                {
                    AuthFileCreate window = new AuthFileCreate();
                    window.Show();
                    window.Closing += AuthWindow_Closing;
                    return;
                }

                string[] data = File.ReadAllLines("auth.cfg");

                Get(data[0]);
                Get("y");
                Get(data[1]);
                Get(data[2]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Произошла ошибка при входе. Возможно, неверен логин/пароль, либо вы не зарегистрированы", "ERROR", MessageBoxButton.OK);
            }
        }

        private void AuthWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Auth();
        }

        public void Send(string Data)
        {
            S.Send(Encoding.ASCII.GetBytes(Data));
        }

        public string Get()
        {
            while (S.Available == 0)
                Thread.Sleep(50);
            Thread.Sleep(100);
            byte[] buffer = new byte[S.Available];
            S.Receive(buffer);
            return Encoding.UTF8.GetString(buffer);
        }

        public string Get(string request)
        {
            Send(request);
            while (S.Available == 0)
                Thread.Sleep(50);
            Thread.Sleep(100);
            byte[] buffer = new byte[S.Available];
            S.Receive(buffer);
            return Encoding.UTF8.GetString(buffer);
        }

        public static ConnectionManager GetManager()
        {
            if (ConnectionManagerObject == null)
                ConnectionManagerObject = new ConnectionManager();
            return ConnectionManagerObject;
        }
    }
}

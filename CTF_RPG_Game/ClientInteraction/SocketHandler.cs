using System;
using System.Net.Sockets;
using System.Text;

namespace CTF_RPG_Game.ClientInteraction
{
    class SocketHandler
    {
        private Socket s;
        public Language lang;

        public SocketHandler(Socket socket)
        {
            s = socket;
        }

        public void Handle()
        {
            GetMessage();
            ChooseLanguage();
        }

        //////////////////////////////

        private string GetMessage()
        {
            StringBuilder SB = new StringBuilder();
            byte[] buffer = new byte[1024];
            int receivedBytes;

            while (s.Available > 0)
            {
                receivedBytes = s.Receive(buffer);
                SB.Append(Encoding.UTF8.GetString(buffer, 0, receivedBytes));
            }

            return SB.ToString().Trim().ToLower();
        }

        private void SendMessage(string message)
        {
            byte[] buffer = new byte[message.Length];
            buffer = Encoding.UTF8.GetBytes(message);
            s.Send(buffer);
        }

        private void CloseConnection()
        {
            s.Shutdown(SocketShutdown.Both);
        }

        private void ChooseLanguage()
        {
            SendMessage("Choose you language (type \"rus\" for russian or \"eng\" for english)\nВыберите язык (введите \"rus\" для русского или \'eng\' для английского)\n>");
            string answer = GetMessage();

            if (answer.StartsWith("rus"))
                lang = Language.Russian;
            else if (answer.StartsWith("eng"))
                lang = Language.English;
            else
            {
                SendMessage("I cannot understand you, try again\nЯ не могу понять вас, попробуйте еще\n\n");
                CloseConnection();
            }
        }

        private void Registration()
        {

        }
    }

    enum Language { English, Russian }
}

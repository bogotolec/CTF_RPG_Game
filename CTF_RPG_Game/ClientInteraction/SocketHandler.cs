using System;
using System.Net.Sockets;
using System.Text;
using CTF_RPG_Game.Languages;

namespace CTF_RPG_Game.ClientInteraction
{
    class SocketHandler
    {
        private Socket s;
        public ILanguage text;

        public SocketHandler(Socket socket)
        {
            s = socket;
        }

        public void Handle()
        {
            GetMessage();
            ChooseLanguage();
            bool needRegistration = AskForRegistration();
            if (needRegistration)
            {
                Registration();
            }
            else
            {
                // TODO
            }
        }

        //////////////////////////////

        private bool AskForRegistration()
        {
            SendMessage(text.RegistrationOrLoginText);
            string answer = GetMessage();

            if (answer.StartsWith("y"))
                return false;
            else
                return true;
        }

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
                text = Russian.GetLanguage();
            else if (answer.StartsWith("eng"))
                text = null;
            else
            {
                SendMessage("I cannot understand you, try again\nЯ не могу понять вас, попробуйте еще\n\n");
                CloseConnection();
            }
        }

        private void Registration()
        {
            SendMessage(text.AskForRegistrationLogin);
            string login = GetMessage();
            SendMessage(text.AskForRegistrationPassword);
            string password = GetMessage();
        }
    }
}

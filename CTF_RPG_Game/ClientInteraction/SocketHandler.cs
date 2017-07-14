using System;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Text;
using CTF_RPG_Game.Languages;
using CTF_RPG_Game_Server;
using CTF_RPG_Game.CharacterInteraction;

namespace CTF_RPG_Game.ClientInteraction
{
    class SocketHandler
    {
        private Socket s;
        private Character character;
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
                bool isRegistered;
                do
                {
                    isRegistered = Registration();
                }
                while (!isRegistered);
            }
            else
            {
                bool isLogined;
                do
                {
                    isLogined = Login();
                }
                while (!isLogined);
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

        private bool Registration()
        {
            SendMessage(text.AskForRegistrationLogin);
            string login = GetMessage();

            if (HasWrongSymbols(login))
            {
                SendMessage(text.IncorrectSymbol);
                return false;
            }

            using (SqlConnection connection = new SqlConnection(Program.DBConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM dbo.GameUsers WHERE UserLogin=" + login;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    SendMessage(text.UserAlreadyExist);
                    return false;
                }
            }

            SendMessage(text.AskForRegistrationPassword);
            string password = GetMessage();
            SendMessage(text.AskForConfirmPassword);
            string passconfirm = GetMessage();

            if (password != passconfirm)
            {
                SendMessage(text.PassesAreNotEqual);
                return false;
            }

            using (SqlConnection connection = new SqlConnection(Program.DBConnectionString))
            {
                string passwordhash = "";
                byte[] passbytes = Encoding.ASCII.GetBytes(password);

                MD5 md5 = MD5.Create();
                byte[] hash = md5.ComputeHash(passbytes);

                foreach (var b in hash)
                {
                    passwordhash += b.ToString("X2");
                }

                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO dbo.GameUsers (UserLogin, PasswordHash) VALUES ('" + 
                    login + "', '" + passwordhash + "')";
                command.Connection = connection;
                command.ExecuteNonQuery();
            }

            SendMessage(text.ChooseName);
            bool isNameCorrect;
            string name;
            do
            {
                name = GetMessage();
                isNameCorrect = !HasWrongSymbols(name);

                if (!isNameCorrect)
                {
                    SendMessage(text.NameHasIncorrectSymbols);
                }
            }
            while (!isNameCorrect);

            using (SqlConnection connection = new SqlConnection(Program.DBConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT Id FROM dbo.GameUsers WHERE Login=" + login;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                int id = (int)reader.GetValue(0);

                character = Character.Create(id, name);
                return true;
            }
        }

        private bool Login()
        {
            SendMessage(text.AskForLogin);
            string login = GetMessage();

            if (HasWrongSymbols(login))
            {
                SendMessage(text.IncorrectSymbol);
                return false;
            }

            SendMessage(text.AskForPassword);
            string password = GetMessage();

            using (SqlConnection connection = new SqlConnection(Program.DBConnectionString))
            {
                MD5 md5 = MD5.Create();
                byte[] passbytes = Encoding.ASCII.GetBytes(password);
                string hash = "";
                foreach (var b in passbytes)
                {
                    hash += b.ToString("X2");
                }

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT CharacterId FROM dbo.GameCharacters WHERE UserLogin='" + login +
                    "', PasswordHash='" + hash + "'";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    character = Character.Get((int)reader.GetValue(0));
                    return true;
                }
                else
                {
                    SendMessage(text.WrongLoginOrPassword);
                    return false;
                }
            }
        }

        ///////////////////////////

        private bool HasWrongSymbols(string data)
        {
            string[] WrongSymbols = { " ", ".", ",", "!", "@", "\"", "#", "№", "$", ";", "%",
                                    "^", ":", "&", "?", "*", "'", "(", ")", "{", "}", "[",
                                    "]", "<", ">", "/", "\\", "~", "`", "|", "\0", "\t", "\n"};

            foreach (var s in WrongSymbols)
            {
                if (data.Contains(s))
                    return true;
            }
            return false;
        }
    }
}

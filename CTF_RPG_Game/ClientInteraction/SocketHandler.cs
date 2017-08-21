using System;
using System.Threading;
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

        public override string ToString()
        {
            StringBuilder result = new StringBuilder("Connection:\n");
            result.Append("\tClient IP: " + s.RemoteEndPoint + "\n");
            result.Append("\tClient character name: " + (character == null ? "null" : character.Name) + "\n");
            result.Append("\tClient ID: " + (character == null ? "unknown" : character.ID.ToString()));
            return result.ToString();
        }

        public void Handle()
        {
            if (!ChooseLanguage())
                return;
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

            Game game = new Game(character, this, text);
            game.CommandHandlerStart();
        }

        public void CloseCheck(object obj)
        {
            Thread parent = (Thread)obj;
            do
            {
                Thread.Sleep(1000);
            }
            while (!(s.Poll(100000, SelectMode.SelectRead) && s.Available == 0));

            if (Program.ConsoleMessages)
                Console.WriteLine("Start closing connection");

            if (character != null)
                character.SaveCharacter();

            ConnectionManager.ConnectionList.Remove(this);

            parent.Abort();
            
            if (Program.ConsoleMessages)
                Console.WriteLine("Connection closed");
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

        public string GetMessage()
        {
            StringBuilder SB = new StringBuilder();
            byte[] buffer = new byte[1024];
            int receivedBytes;

            while (s.Available == 0)
            {
                Thread.Sleep(100);
            }

            while (s.Available > 0)
            {
                receivedBytes = s.Receive(buffer);
                SB.Append(Encoding.UTF8.GetString(buffer, 0, receivedBytes));
            }

            return SB.ToString().Trim();
        }

        public void SendMessage(string message)
        {
            byte[] buffer = new byte[message.Length];
            buffer = Encoding.UTF8.GetBytes(message);
            s.Send(buffer);
        }

        public void CloseConnection()
        {
            s.Shutdown(SocketShutdown.Both);
        }

        private bool ChooseLanguage()
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
                return false;
            }
            return true;
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
                command.Connection = connection;
                command.CommandText = "SELECT * FROM dbo.GameUsers WHERE UserLogin='" + login + "'";
                connection.Open();
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
                connection.Open();
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
                command.CommandText = "SELECT CharacterId FROM dbo.GameUsers WHERE UserLogin='" + login.ToLower() + "'";
                command.Connection = connection;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();
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
                byte[] hashbytes = md5.ComputeHash(passbytes);
                foreach (var b in hashbytes)
                {
                    hash += b.ToString("X2");
                }

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT CharacterId FROM dbo.GameUsers WHERE UserLogin='" + login +
                    "' AND PasswordHash='" + hash + "'";
                command.Connection = connection;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    int characterId = (int)reader.GetValue(0);

                    try
                    {
                        character = Character.Get(characterId);
                    }
                    catch (NoCharacterException ex)
                    {
                        SendMessage(text.ChooseName);
                        bool isNameCorrect;
                        string name = ex.Message;
                        name = null;
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

                        character = Character.Create(characterId, name);
                    }

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

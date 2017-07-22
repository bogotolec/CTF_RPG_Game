using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CTF_RPG_Game_Client
{
    class Game
    {
        ConnectionManager Manager;

        // CONSTS

        const int HEIGHT = 32;
        const int WIDTH = 72;

        const int BIG_WINDOW_WIDTH = 53;
        const int BIG_WINDOW_HEIGHT = HEIGHT - MESSAGE_WINDOW_HEIGHT;

        const int MESSAGE_WINDOW_WIDTH = BIG_WINDOW_WIDTH;
        const int MESSAGE_WINDOW_HEIGHT = 5;

        const int COMMAND_WINDOW_WIDTH = WIDTH - BIG_WINDOW_WIDTH;
        const int COMMAND_WINDOW_HEIGHT = 20;

        const int INFO_WINDOW_WIDTH = COMMAND_WINDOW_WIDTH;
        const int INFO_WINDOW_HEIGHT = HEIGHT - COMMAND_WINDOW_HEIGHT;

        // Simple constructor
        public Game(ConnectionManager ConnectionManager)
        {
            Manager = ConnectionManager;
        }

        public void Start()
        {
            JsonAnswer Json = Authorization();

            Console.Clear();
        }

        // This function returns first Json answer
        private JsonAnswer Authorization()
        {
            string Answer = Manager.Get();

            do
            {
                Console.WriteLine(Answer);

                string Message = Console.ReadLine();
                Manager.Send(Message);

                Answer = Manager.Get();
            }
            while (!IsJson(Answer));

            return JsonConvert.DeserializeObject<JsonAnswer>(Answer);
        }

        // Bad Json checker
        private bool IsJson(string String)
        {
            if (String.StartsWith("{\""))
                return true;
            return false;
        }

        // Very hard function
        public void DrowGame(JsonAnswer Answer)
        {
            Console.Clear();
            
            // Create message window
            StringBuilder MessageWindow = new StringBuilder();
            string[] MessageStrings = { "", "", "" };

            if (Answer.Message != null)
            {
                string[] MessageWords = Answer.Message.Split();

                int WordNumber = 0;
                for (int i = 0; i < 3; i++)
                {
                    int SymbolNumber = 0;

                    while ((SymbolNumber + MessageWords[WordNumber]).Length < MESSAGE_WINDOW_WIDTH - 3
                        && WordNumber != MessageWords.Length)
                    {
                        MessageStrings[i] += " " + MessageWords[WordNumber];
                        SymbolNumber += MessageWords[WordNumber].Length + 1;
                        WordNumber++;
                    }

                    MessageStrings[i] = MessageStrings[i].PadRight(21);
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    MessageStrings[i] = MessageStrings[i].PadRight(21);
                }
            }

            MessageWindow.Append('―', 23);
            MessageWindow.Append("|" + MessageStrings[0] + "||" +
                MessageStrings[1] + "||" + MessageStrings[2] + "|");
            MessageWindow.Append('―', 23);
            // End of create message window
            

        }
    }

    // Struct of Json answer
    struct JsonAnswer
    {
        public string Message;
        public string BigWindow;
        public string Commands;
        public string Info;
        public string Level;
    }
}

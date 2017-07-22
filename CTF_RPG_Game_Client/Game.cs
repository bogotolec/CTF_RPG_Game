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

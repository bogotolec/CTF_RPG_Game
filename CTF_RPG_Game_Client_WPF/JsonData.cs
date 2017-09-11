using Newtonsoft.Json;

namespace CTF_RPG_Game_Client_WPF
{
    class JsonData
    {
        public string Message { get; }
        public string BigWindow { get; }
        public string Commands { get; }
        public string Info { get; }
        public string Level { get; }
        public string Type { get; }

        private JsonData()
        { }

        public static JsonData Parse(string data)
        {
            JsonData that = JsonConvert.DeserializeObject<JsonData>(data);
            return that;
        }
    }
}

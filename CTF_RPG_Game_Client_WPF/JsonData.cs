using Newtonsoft.Json;

namespace CTF_RPG_Game_Client_WPF
{
    class JsonData
    {
        public string Message { get; set; }
        public string BigWindow { get; set; }
        public string Commands { get; set; }
        public string Info { get; set; }
        public string Level { get; set; }
        public string Type { get; set; }

        private JsonData()
        { }

        public static JsonData Parse(string data)
        {
            JsonData that = JsonConvert.DeserializeObject<JsonData>(data);
            return that;
        }
    }
}

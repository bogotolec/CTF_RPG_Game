using System;
using System.Web.WebSockets;
using System.Text;
using CTF_RPG_Game.CharacterInteraction;
using CTF_RPG_Game.Languages;
using CTF_RPG_Game.MapComponents;
using Newtonsoft.Json;

namespace CTF_RPG_Game.ClientInteraction
{
    class Game
    {
        private SocketHandler SH;
        private Character character;
        private Map map;
        private Result result;
        private ILanguage lang;

        const int HEIGHT = 25;
        const int WIDTH = 51;

        const string MAP_COMMANDS =
@"w, a, s, d - moving
inventory - open inventory";
        const string INV_COMMANDS =
@"page <number> - go to page
map - open map";

        public Game(Character Char, SocketHandler SocketHandler, ILanguage language)
        {
            SH = SocketHandler;
            character = Char;
            map = Map.GetMap();
            lang = language;

            // Result
            result = new Result();

            result.Commands = MAP_COMMANDS;


            result.Info = "";
            MapToResult();
            result.Level = character.Level.ToString();
        }

        public void CommandHandlerStart()
        {
            while (true)
            {
                SendResult();

                string[] commandwords = SH.GetMessage().ToLower().Split();

                int InventoryPage = 1;

                switch (commandwords[0])
                {
                    case "w":
                        if (!Move("up"))
                            result.Message = lang.CellIsNotPassable;
                        goto case "map";
                    case "a":
                        if (!Move("left"))
                            result.Message = lang.CellIsNotPassable;
                        goto case "map";
                    case "s":
                        if (!Move("down"))
                            result.Message = lang.CellIsNotPassable;
                        goto case "map";
                    case "d":
                        if (!Move("right"))
                            result.Message = lang.CellIsNotPassable;
                        goto case "map";
                    case "map":
                        MapToResult();
                        if (result.Message != lang.CellIsNotPassable)
                            result.Message = map[character.Y, character.X].Message;
                        result.Commands = MAP_COMMANDS;
                        break;

                    case "page":
                        if (int.TryParse(commandwords[1], out InventoryPage))
                            goto case "inventory";
                        else
                        {
                            result.Message = lang.BadNumber;
                            break;
                        }
                    case "inventory":
                        InventoryToResult(InventoryPage);
                        result.Message = "";
                        result.Commands = INV_COMMANDS; 
                        break;


                    default:
                        result.Message = lang.UnknownCommand;
                        break;
                }
            }
        }

        private void MapToResult()
        {
            result.Type = "Map";

            StringBuilder SB = new StringBuilder();
            const int HorisontalVisionRange = WIDTH / 2;
            const int VerticalVisionRange = HEIGHT / 2;

            const byte Void = 255;
            byte Temp = 0;

            for (int i = character.Y - VerticalVisionRange; i <= character.Y + VerticalVisionRange; i++)
            {
                for (int j = character.X - HorisontalVisionRange; j <= character.X + HorisontalVisionRange; j++)
                {
                    if (i == character.Y && j == character.X)
                    {
                        SB.Append("75");
                    }
                    else if (i < 0 || i >= map.Height || j < 0 || j >= map.Width)
                    {
                        SB.Append(Void.ToString("x2"));
                    }
                    else
                    {
                        Temp = map[i, j].GetCellByte();
                        SB.Append(Temp.ToString("x2"));
                    }
                }
            }
            result.BigWindow = SB.ToString();
        }

        private void InventoryToResult(int page)
        {
            result.Type = "Inventory";
            string[] equiped = new string[HEIGHT];
            string[] backpacked = new string[HEIGHT];

            if (lang.ToString() == "Russian")
            {
                equiped[0] = "НАДЕТО".PadLeft((WIDTH / 2 - "НАДЕТО".Length) / 2 + "НАДЕТО".Length).PadRight(WIDTH / 2);
                equiped[1] = (new StringBuilder()).Append(' ', WIDTH / 2).ToString();
                equiped[2] = (" Голова: " + (character.Head == null ? "Пусто" : character.Head.Name(lang))).PadRight(WIDTH / 2);
                equiped[3] = (" Тело: " + (character.Body == null ? "Пусто" : character.Body.Name(lang))).PadRight(WIDTH / 2);
                equiped[4] = (" Левая рука: " + (character.LHand == null ? "Пусто" : character.LHand.Name(lang))).PadRight(WIDTH / 2);
                equiped[5] = (" Правая рука: " + (character.RHand == null ? "Пусто" : character.RHand.Name(lang))).PadRight(WIDTH / 2);
                equiped[6] = (" Обувь: " + (character.Boots == null ? "Пусто" : character.Boots.Name(lang))).PadRight(WIDTH / 2);
                equiped[7] = (" Украшение 1: " + (character.JeweleryOne == null ? "Пусто" : character.JeweleryOne.Name(lang))).PadRight(WIDTH / 2);
                equiped[8] = (" Украшение 2: " + (character.LearnedSkills.Contains(Skill.GetById(2)) ? (character.JeweleryTwo == null ? "Пусто" : character.JeweleryTwo.Name(lang)) : "Заблокировано")).PadRight(WIDTH / 2);
                for (int i = 9; i < HEIGHT; i++)
                {
                    equiped[i] = (new StringBuilder()).Append(' ', WIDTH / 2).ToString();
                }

                int itemindex = 0;
                backpacked[0] = "РЮКЗАК".PadLeft((WIDTH / 2 - "РЮКЗАК".Length) / 2 + "РЮКЗАК".Length).PadRight(WIDTH / 2);
                backpacked[1] = (new StringBuilder()).Append(' ', WIDTH / 2).ToString();
                while (itemindex < 23 && (page - 1) * 23 + itemindex < character.Backpack.Count)
                {
                    if (character.Backpack[((page - 1) * 23) + itemindex] == null)
                        backpacked[itemindex + 2] =  (new StringBuilder()).Append(' ', WIDTH / 2).ToString();
                    else
                        backpacked[itemindex + 2] = (" " + character.Backpack[((page - 1) * 23) + itemindex].Name(lang)).PadRight(WIDTH / 2);
                    itemindex++;
                }
                while (itemindex < 23)
                {
                    backpacked[itemindex + 2] = (new StringBuilder()).Append(' ', WIDTH / 2).ToString();
                    itemindex++;
                }
            }

            string[] results = new string[HEIGHT];

            for (int i = 0; i < HEIGHT; i++)
                results[i] = equiped[i] + backpacked[i];

            result.BigWindow = String.Join("", results);
        }

        private bool Move(string direction)
        {
            int x = 0, y = 0;
            switch (direction)
            {
                case "up":
                    y = -1;
                    break;
                case "left":
                    x = -1;
                    break;
                case "down":
                    y = 1;
                    break;
                case "right":
                    x = 1;
                    break;
            }

            if (map[character.Y + y, character.X + x].IsPassable)
            {
                character.X += x;
                character.Y += y;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SendResult()
        {
            string json = JsonConvert.SerializeObject(result);
            SH.SendMessage(json);
        }
    }

    struct Result
    {
        public string Message;
        public string BigWindow;
        public string Commands;
        public string Info;
        public string Level;
        public string Type;
    }
}

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
                int beginX = character.X;
                int beginY = character.Y;

                switch (commandwords[0])
                {
                    case "w":
                        if (result.Type != "Map")
                            goto ImpossibleCommand;
                        if (!Move("up"))
                            result.Message = lang.CellIsNotPassable;
                        goto case "map";
                    case "a":
                        if (result.Type != "Map")
                            goto ImpossibleCommand;
                        if (!Move("left"))
                            result.Message = lang.CellIsNotPassable;
                        goto case "map";
                    case "s":
                        if (result.Type != "Map")
                            goto ImpossibleCommand;
                        if (!Move("down"))
                            result.Message = lang.CellIsNotPassable;
                        goto case "map";
                    case "d":
                        if (result.Type != "Map")
                            goto ImpossibleCommand;
                        if (!Move("right"))
                            result.Message = lang.CellIsNotPassable;
                        goto case "map";
                    case "map":
                        MapToResult();
                        if (beginX != character.X || beginY != character.Y)
                            result.Message = map[character.Y, character.X].Message;
                        result.Commands = MAP_COMMANDS;
                        break;

                    case "page":
                        if (result.Type != "Inventory")
                            goto ImpossibleCommand;
                        if (commandwords.Length > 1 && int.TryParse(commandwords[1], out InventoryPage))
                            if ((InventoryPage - 1) * (HEIGHT - 3) > character.Backpack.Count)
                                { result.Message = lang.TooBigPage; break; }
                            else
                                goto case "inventory";
                        else
                        {
                            result.Message = lang.BadNumber;
                            break;
                        }
                    case "inventory":
                        InventoryToResult(InventoryPage);
                        result.Message = lang.YourBackpackHas + ": " + (character.Backpack.Count - 1);
                        result.Commands = INV_COMMANDS; 
                        break;


                    default:
                        result.Message = lang.UnknownCommand;
                        break;

                    ImpossibleCommand:
                        result.Message = lang.ImpossibleCommand;
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

            string NOTHING = (new StringBuilder()).Append(' ', WIDTH / 2).ToString();

            // Equiped field filling
            equiped[0] = lang.Equiped.PadLeft((WIDTH / 2 - lang.Equiped.Length) / 2 + lang.Equiped.Length).PadRight(WIDTH / 2);
            equiped[1] = NOTHING;
            equiped[2] = (" " + lang.Head + ":").PadRight(WIDTH / 2);
            equiped[3] = ("   " + (character.Head == null ? lang.Empty : character.Head.Name(lang))).PadRight(WIDTH / 2);
            equiped[4] = (" " + lang.Body + ":").PadRight(WIDTH / 2);
            equiped[5] = ("   " + (character.Body == null ? lang.Empty : character.Body.Name(lang))).PadRight(WIDTH / 2);
            equiped[6] = (" " + lang.LHand + ":").PadRight(WIDTH / 2);
            equiped[7] = ("   " + (character.LHand == null ? lang.Empty : character.LHand.Name(lang))).PadRight(WIDTH / 2);
            equiped[8] = (" " + lang.Rhand + ":").PadRight(WIDTH / 2);
            equiped[9] = ("   " + (character.RHand == null ? lang.Empty : character.RHand.Name(lang))).PadRight(WIDTH / 2);
            equiped[10] = (" " + lang.Boots + ":").PadRight(WIDTH / 2);
            equiped[11] = ("   " + (character.Boots == null ? lang.Empty : character.Boots.Name(lang))).PadRight(WIDTH / 2);
            equiped[12] = (" " + lang.Jewelerry + "1:").PadRight(WIDTH / 2);
            equiped[13] = ("   " + (character.JeweleryOne == null ? lang.Empty : character.JeweleryOne.Name(lang))).PadRight(WIDTH / 2);
            equiped[14] = (" " + lang.Jewelerry + "2:").PadRight(WIDTH / 2);
            equiped[15] = ("   " + (character.LearnedSkills.Contains(Skill.GetById(2)) ? (character.JeweleryTwo == null ? lang.Empty : character.JeweleryTwo.Name(lang)) : lang.Locked)).PadRight(WIDTH / 2);
            for (int i = 16; i < HEIGHT - 3; i++)
            {
                equiped[i] = NOTHING;
            }
            equiped[HEIGHT - 3] = (" " + lang.Gold + " : " + character.Gold).PadRight(WIDTH / 2);
            equiped[HEIGHT - 2] = (" " + lang.Health + " : " + character.Health).PadRight(WIDTH / 2);
            equiped[HEIGHT - 1] = NOTHING;

            // Backpack field filling
            int itemindex = 0;
            backpacked[0] = lang.Backpacked.PadLeft((WIDTH / 2 - lang.Backpacked.Length) / 2 + lang.Backpacked.Length).PadRight(WIDTH / 2);
            backpacked[1] = (lang.Page + ' ' + page).PadLeft((WIDTH / 2 - (lang.Page + ' ' + page).Length) / 2 + (lang.Page + ' ' + page).Length).PadRight(WIDTH / 2);
            backpacked[2] = NOTHING;
            while (itemindex < HEIGHT - 3 && (page - 1) * (HEIGHT - 3) + itemindex < character.Backpack.Count)
            {
                if (character.Backpack[((page - 1) * (HEIGHT - 3)) + itemindex] == null)
                    backpacked[itemindex + 3] = NOTHING;
                else
                    backpacked[itemindex + 3] = (" " + character.Backpack[((page - 1) * (HEIGHT - 3)) + itemindex].Name(lang)).PadRight(WIDTH / 2);
                itemindex++;
            }
            while (itemindex < HEIGHT - 3)
            {
                backpacked[itemindex + 3] = NOTHING;
                itemindex++;
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

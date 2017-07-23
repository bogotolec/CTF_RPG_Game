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

        public Game(Character Char, SocketHandler SocketHandler, ILanguage language)
        {
            SH = SocketHandler;
            character = Char;
            map = Map.GetMap();
            lang = language;

            // Result
            result = new Result();

            result.Commands =
@"w, a, s, d - moving";

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

                switch (commandwords[0])
                {
                    case "w":
                        Move("up");
                        break;
                    case "a":
                        Move("left");
                        break;
                    case "s":
                        Move("down");
                        break;
                    case "d":
                        Move("right");
                        break;

                    default:
                        result.Message = lang.UnknownCommand;
                        break;
                }
            }
        }

        private void MapToResult()
        {
            StringBuilder SB = new StringBuilder();
            const int HorisontalVisionRange = 25;
            const int VerticalVisionRange = 12;

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

        private void Move(string direction)
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
                result.Message = map[character.Y, character.X].Message;
                MapToResult();
            }
            else
            {
                result.Message = lang.CellIsNotPassable;
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
    }
}

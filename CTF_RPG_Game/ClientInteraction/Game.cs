using System;
using System.Web.WebSockets;
using CTF_RPG_Game.CharacterInteraction;
using CTF_RPG_Game.Languages;
using CTF_RPG_Game.MapComponents;

namespace CTF_RPG_Game.ClientInteraction
{
    class Game
    {
        private SocketHandler SH;
        private Character character;
        private Map map;
        private Result result;
        private ILanguage lang;

        Game (Character Char, SocketHandler SocketHandler, ILanguage language)
        {
            SH = SocketHandler;
            character = Char;
            map = Map.GetMap();
            result = new Result();
            lang = language;
        }

        public void CommandHandlerStart()
        {
            while (true)
            {
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
            }
            else
            {
                result.Message = lang.CellIsNotPassable;
            }
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

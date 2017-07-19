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

        Game (Character Char, SocketHandler SocketHandler)
        {
            SH = SocketHandler;
            character = Char;
        }

        public void CommandHandlerStart()
        {
            while (true)
            {
                string[] commandwords = SH.GetMessage().ToLower().Split();

                switch (commandwords[0])
                {
                    case "w":

                }
            }
        }

        private void Move(string direction)
        {
            int x = 0, y = 0;
            switch (direction)
            {
                case "up":
                    x = -1;
                    break;
                case "left":
                    y = -1;
                    break;
                case "down":
                    x = 1;
                    break;
                case "right":
                    y = 1;
                    break;
            }


        }
    }
}

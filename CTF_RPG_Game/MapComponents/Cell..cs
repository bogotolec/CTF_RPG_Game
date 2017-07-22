using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF_RPG_Game.MapComponents
{
    public class Cell
    {
        public string Message;
        public int LandscapeId;
        public bool IsPassable;
        public bool IsTaskable;
        public bool IsTeleport;
        public bool IsVisibleWithSkills;
        public string Symbol;
        public string Color;
        private static Dictionary<string, int> ColorDict = new Dictionary<string, int>();
        private static Dictionary<string, int> SymbolDict = new Dictionary<string, int>();

        private void CreateDict()
        {
            ColorDict.Add("red", 0);
            ColorDict.Add("green", 1);
            ColorDict.Add("blue", 2); // To ConsoleColor.DarkBlue
            ColorDict.Add("yellow", 3);
            ColorDict.Add("black", 4);
            ColorDict.Add("white", 5);
            ColorDict.Add("orange", 6); // Color not exist
            ColorDict.Add("light-blue", 7); // To ConsoleColor.Blue
            ColorDict.Add("gray", 8);
            ColorDict.Add("purple", 9); // To ConsoleColor.Magenta
            //1111 - Reserved
            SymbolDict.Add(">", 0);
            SymbolDict.Add("<", 1);
            SymbolDict.Add("^", 2);
            SymbolDict.Add("v", 3);
            SymbolDict.Add("$", 4);
            SymbolDict.Add("X", 5);
            SymbolDict.Add("!", 6);
            SymbolDict.Add("@", 7);
            SymbolDict.Add("#", 8);
            SymbolDict.Add(" ", 9);
            SymbolDict.Add("-", 10);
            //1111 - Reserved
        }
        public override string ToString()
        {
            return Symbol.ToString();
        }
        public Cell()
        {
            if (SymbolDict.Count == 0 && ColorDict.Count == 0)
            {
                CreateDict();
            }
        }

        public byte GetCellByte()
        {
            return Convert.ToByte((ColorDict[Color] << 4) + SymbolDict[Symbol]);
        }
    }
}
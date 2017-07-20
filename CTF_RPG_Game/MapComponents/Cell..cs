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
        private Dictionary<string, string> ColorDict = new Dictionary<string, string>();
        private Dictionary<string, string> SymbolDict = new Dictionary<string, string>();
        public override string ToString()
        {
            return Symbol.ToString();
        }
        public Cell()
        {
            ColorDict.Add("red", "0001");
            ColorDict.Add("green", "0010");
            ColorDict.Add("blue", "0100");
            ColorDict.Add("yellow", "1000");
            ColorDict.Add("black", "0011");
            ColorDict.Add("white", "0110");
            ColorDict.Add("orange", "1100");
            ColorDict.Add("light-blue", "0111");
            ColorDict.Add("gray", "1110");
            ColorDict.Add("purple", "0000");
            //1111 - Reserved
            SymbolDict.Add(">", "0001");
            SymbolDict.Add("<", "0010");
            SymbolDict.Add("^", "0100");
            SymbolDict.Add("v", "1000");
            SymbolDict.Add("$", "0011");
            SymbolDict.Add("X", "0110");
            SymbolDict.Add("!", "1100");
            SymbolDict.Add("@", "0111");
            SymbolDict.Add("#", "1110");
            SymbolDict.Add(" ", "0000");
            //1111 - Reserved
        }
    
    public byte GetCellByte()
        {
            return Byte.Parse(ColorDict[Color] + SymbolDict[Symbol]);
        }
    }
}

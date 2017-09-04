using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF_RPG_Game.MapComponents
{
    public class Cell
    {
        public string RussianMessage;
        public string EnglishMessage;
        public int LandscapeId;
        public LandscapeCrgm Landscape;
        public bool IsPassable;
        public bool IsTaskable;
        public Task CellTask;
        public bool IsTeleport;
        public bool IsVisibleWithSkills;
        public string Symbol;
        public ConsoleColor Color;
        public ConsoleColor BackgroundColor = ConsoleColor.Black;
        private static Dictionary<ConsoleColor, int> ColorDict = new Dictionary<ConsoleColor, int>();

        private void CreateDict()
        {
            ColorDict.Add(ConsoleColor.Black, 0);
            ColorDict.Add(ConsoleColor.Blue, 1);
            ColorDict.Add(ConsoleColor.Cyan, 2);
            ColorDict.Add(ConsoleColor.DarkBlue, 3);
            ColorDict.Add(ConsoleColor.DarkCyan, 4);
            ColorDict.Add(ConsoleColor.DarkGray, 5);
            ColorDict.Add(ConsoleColor.DarkGreen, 6);
            ColorDict.Add(ConsoleColor.DarkMagenta, 7);
            ColorDict.Add(ConsoleColor.DarkRed, 8);
            ColorDict.Add(ConsoleColor.DarkYellow, 9);
            ColorDict.Add(ConsoleColor.Gray, 10);
            ColorDict.Add(ConsoleColor.Green, 11);
            ColorDict.Add(ConsoleColor.Magenta, 12);
            ColorDict.Add(ConsoleColor.Red, 13);
            ColorDict.Add(ConsoleColor.White, 14);
            ColorDict.Add(ConsoleColor.Yellow, 15);
        }

        public override string ToString()
        {
            return Symbol.ToString();
        }

        public Cell()
        {
            if (ColorDict.Count == 0)
            {
                CreateDict();
            }
        }

        public Cell(string configurarion)
        {
            if (ColorDict.Count == 0)
            {
                CreateDict();
            }

            foreach (var configstring in configurarion.Split('|'))
            {
                if (configstring.StartsWith("RM="))
                    RussianMessage = configstring.Substring("RM=".Length);

                if (configstring.StartsWith("EM="))
                    EnglishMessage = configstring.Substring("EM=".Length);

                if (configstring.StartsWith("L="))
                {
                    switch (configstring.Substring("L=".Length))
                    {
                        case "Fl":
                            Landscape = LandscapeCrgm.Field;
                            break;
                        case "Ds":
                            Landscape = LandscapeCrgm.Desert;
                            break;
                        case "Fr":
                            Landscape = LandscapeCrgm.Forest;
                            break;
                        case "Wt":
                            Landscape = LandscapeCrgm.Water;
                            break;
                        case "Br":
                            Landscape = LandscapeCrgm.Bricks;
                            break;
                        case "Lv":
                            Landscape = LandscapeCrgm.Lava;
                            break;
                        case "Sg":
                            Landscape = LandscapeCrgm.Sign;
                            break;
                        default:
                            Landscape = LandscapeCrgm.None;
                            break;
                    }
                }

                if (configstring.StartsWith("P="))
                {
                    if (configstring.Substring("P=".Length) == "1")
                        IsPassable = true;
                    else
                        IsPassable = false;
                }

                if (configstring.StartsWith("T="))
                {
                    if (configstring.Substring("T=".Length) == "")
                        IsTaskable = false;
                    else
                    {
                        IsTaskable = true;
                        int id;
                        if (int.TryParse(configstring.Substring("T=".Length), out id))
                        {
                            CellTask = Task.GetById(id);
                        }
                        else
                        {
                            IsTaskable = false;
                        }
                    }
                }

                if (configstring.StartsWith("Tp="))
                {
                    if (configstring.Substring("Tp=".Length) == "")
                    {
                        IsTeleport = false;
                    }
                    else
                    {
                        try
                        {
                            var c = configstring.Substring("Tp=".Length).Split(',');

                            int x = int.Parse(c[0]), y = int.Parse(c[1]);

                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.Message);
                            IsTeleport = false;
                        }
                    }
                }
            }

            InitFieldsCrgm();
        }

        private void InitFieldsCrgm()
        {
            switch (Landscape)
            {
                #region Field
                case LandscapeCrgm.Field:
                    BackgroundColor = ConsoleColor.Green;
                    
                    if (!IsPassable)
                    {
                        BackgroundColor = ConsoleColor.DarkGreen;
                        Color = ConsoleColor.Green;
                        Symbol = "#";
                    }
                    else if (IsTeleport)
                    {
                        Color = ConsoleColor.Magenta;
                        Symbol = "@";
                    }
                    else if (IsTaskable)
                    {
                        Color = ConsoleColor.Yellow;
                        Symbol = "?";
                    }
                    else
                    {
                        Color = ConsoleColor.DarkGreen;
                        switch ((new Random()).Next(0, 5))
                        {
                            case 0:
                                Symbol = ".";
                                break;
                            case 1:
                                Symbol = ",";
                                break;
                            case 2:
                                Symbol = ":";
                                break;
                            case 3:
                                Symbol = "\"";
                                break;
                            case 4:
                                Symbol = "'";
                                break;
                            case 5:
                                Symbol = "-";
                                break;
                        }
                    }
                    break;
                #endregion
                #region Desert
                case LandscapeCrgm.Desert:
                    BackgroundColor = ConsoleColor.Yellow;

                    if (!IsPassable)
                    {
                        BackgroundColor = ConsoleColor.DarkYellow;
                        Color = ConsoleColor.Black;
                        Symbol = "#";
                    }
                    else if (IsTeleport)
                    {
                        Color = ConsoleColor.Magenta;
                        Symbol = "@";
                    }
                    else if (IsTaskable)
                    {
                        Color = ConsoleColor.Red;
                        Symbol = "?";
                    }
                    else
                    {
                        Color = ConsoleColor.Black;
                        switch ((new Random()).Next(0, 5))
                        {
                            case 0:
                                Symbol = ".";
                                break;
                            case 1:
                                Symbol = ",";
                                break;
                            case 2:
                                Symbol = ":";
                                break;
                            case 3:
                                Symbol = "\"";
                                break;
                            case 4:
                                Symbol = "'";
                                break;
                            case 5:
                                Symbol = "-";
                                break;
                        }
                    }
                    break;
                #endregion
                #region Forest
                case LandscapeCrgm.Forest:
                    BackgroundColor = ConsoleColor.Green;

                    if (!IsPassable)
                    {
                        BackgroundColor = ConsoleColor.DarkGreen;
                        Color = ConsoleColor.Black;
                        Symbol = "#";
                    }
                    else if (IsTeleport)
                    {
                        Color = ConsoleColor.Magenta;
                        Symbol = "@";
                    }
                    else if (IsTaskable)
                    {
                        Color = ConsoleColor.Yellow;
                        Symbol = "?";
                    }
                    else
                    {
                        Color = ConsoleColor.Black;
                        switch ((new Random()).Next(0, 5))
                        {
                            case 0:
                                Symbol = ".";
                                break;
                            case 1:
                                Symbol = ",";
                                break;
                            case 2:
                                Symbol = ":";
                                break;
                            case 3:
                                Symbol = "\"";
                                break;
                            case 4:
                                Symbol = "'";
                                break;
                            case 5:
                                Symbol = "-";
                                break;
                        }
                    }
                    break;
                #endregion
                #region Water
                case LandscapeCrgm.Water:
                    BackgroundColor = ConsoleColor.Blue;
                    
                    if (IsTeleport)
                    {
                        Color = ConsoleColor.Magenta;
                        Symbol = "@";
                    }
                    else if (IsTaskable)
                    {
                        Color = ConsoleColor.Red;
                        Symbol = "?";
                    }
                    else
                    {
                        Color = ConsoleColor.DarkBlue;
                        Symbol = "~";
                    }
                    break;
                #endregion
                #region Lava
                case LandscapeCrgm.Lava:
                    BackgroundColor = ConsoleColor.Red;

                    if (IsTeleport)
                    {
                        Color = ConsoleColor.Cyan;
                        Symbol = "@";
                    }
                    else if (IsTaskable)
                    {
                        Color = ConsoleColor.Yellow;
                        Symbol = "?";
                    }
                    else
                    {
                        Color = ConsoleColor.Black;
                        Symbol = "~";
                    }
                    break;
                #endregion
                #region Bricks
                case LandscapeCrgm.Bricks:
                    Color = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.DarkRed;
                    Symbol = "#";
                    break;
                #endregion
                #region Sign
                case LandscapeCrgm.Sign:
                    Color = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.Gray;
                    Symbol = EnglishMessage.Substring(0, 1);
                    break;
                #endregion
            }
        }

        public byte[] GetCellBytes()
        {
            byte[] res = new byte[2];
            res[0] = (byte)((ColorDict[Color] << 4) + ColorDict[BackgroundColor]);
            res[1] = Encoding.ASCII.GetBytes(Symbol)[0];

            return res;
        }
    }
}
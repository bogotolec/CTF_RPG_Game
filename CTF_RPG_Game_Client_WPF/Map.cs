using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF_RPG_Game_Client_WPF
{
    public enum Landskape { Bricks, Desert, Field, Forest, Lava, Sign, Water, None }

    public class Map
    {
        public const int HEIGHT = 25;
        public const int WIDTH = 25;

        private Cell[,] CellMap = new Cell[HEIGHT, WIDTH];

        public Cell this[int y, int x]
        {
            get
            {
                return CellMap[y, x];
            }
        }

        public Map(string mapconfig, int height, int width)
        {
            byte[,] colors = new byte[height, width];
            char[,] symbols = new char[height, width];

            bool IsColor = true;
            int i = 0;
            foreach (var c in mapconfig)
            {
                if (IsColor)
                {
                    colors[i / width, i % width] = (byte)(Convert.ToUInt16(c) & 0xFF);
                }
                else
                {
                    symbols[i / width, i % width] = c;
                    i++;
                }

                IsColor = !IsColor;
            }

            int differenceH = height - HEIGHT,
                differenceW = width - WIDTH;
            for (i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    CellMap[i, j] = Cell.FromByte(colors[i + differenceH / 2, j + differenceW / 2], symbols[i + differenceH / 2, j + differenceW / 2]);
                }
            }
        }
    }

    public class Cell
    {
        public Landskape Land { get; }
        public bool IsPassable { get; }
        public bool IsTaskable { get; }
        public char SignSymbol { get; set; }

        Cell(Landskape Land, bool IsPassable = true, bool IsTaskable = false)
        {
            this.Land = Land;
            this.IsPassable = IsPassable;
            this.IsTaskable = IsTaskable;
        }

        public static Cell FromByte(byte colorbyte, char symbol)
        {
            Landskape Land;
            bool pass = true, task = false;
            if (symbol == '?')
                task = true;
            switch (colorbyte & 0xF)
            {
                case 0:
                    Land = Landskape.None;
                    pass = false;
                    break;
                case 1:
                    Land = Landskape.Water;
                    pass = true;
                    break;
                case 2:
                    Land = Landskape.None;
                    pass = false;
                    break;
                case 3:
                    Land = Landskape.Water;
                    pass = false;
                    break;
                case 4:
                    Land = Landskape.None;
                    pass = false;
                    break;
                case 5:
                    Land = Landskape.Sign;
                    pass = false;
                    break;
                case 6:
                    Land = Landskape.Forest;
                    if (symbol == '#')
                        pass = false;
                    else
                        pass = true;
                    break;
                case 7:
                    Land = Landskape.None;
                    pass = false;
                    break;
                case 8:
                    if (symbol == '#')
                        Land = Landskape.Bricks;
                    else
                        Land = Landskape.Lava;
                    break;
                case 9:
                    Land = Landskape.Desert;
                    pass = false;
                    break;
                case 10:
                    Land = Landskape.Sign;
                    pass = true;
                    break;
                case 11:
                    Land = Landskape.Field;
                    if (symbol == '#')
                        pass = false;
                    else
                        pass = true;
                    break;
                case 12:
                    Land = Landskape.None;
                    pass = false;
                    break;
                case 13:
                    if (symbol == '#')
                        Land = Landskape.Bricks;
                    else
                        Land = Landskape.Lava;
                    pass = false;
                    break;
                case 14:
                    Land = Landskape.None;
                    pass = false;
                    break;
                case 15:
                    Land = Landskape.Desert;
                    pass = true;
                    break;
                default:
                    Land = Landskape.None;
                    pass = false;
                    break;
            }
            Cell result = new Cell(Land, pass, task);
            if (Land == Landskape.Sign)
                result.SignSymbol = symbol;
            return result;
        }
    }
}

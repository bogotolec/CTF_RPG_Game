using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CTF_RPG_Game.MapComponents
{
    enum Landscape { TP = 31, UsualWay = 30, Task = 46, SkillWay = 10, Wall = 34 };

    public class Map
    {
        public readonly int Width;
        public readonly int Height;
        static int MapId;
        private Cell[,] CellsMassive;

        private static Map MapObject;

        public Map()
        {
            int[,] IDMap = LoadMapFile("Map/GameMap.tsx");
            Width = 17;
            Height = 17;
            CellsMassive = new Cell[Height, Width];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    CellsMassive[i, j] = CellFromId(IDMap[i, j]);
                }
            }
        }

        public Cell this[int indexY, int indexX]
        {
            get { return CellsMassive[indexY, indexX]; }
        }

        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    output += CellsMassive[i, j].ToString() + " ";
                }
                output += '\n';
            }
            return output;
        }

        private int[,] LoadMapFile(string name)
        {
            try
            {
                XmlReader reader = XmlReader.Create(name);
                reader.ReadToFollowing("data");
                string csv = (string)reader.ReadElementContentAsString().Trim();
                string[] csvArray = csv.Split('\n');
                int[,] IDMap = new int[csvArray.Length, csvArray[0].Split(',').Length - 1];
                for (int i = 0; i < IDMap.GetLength(0); i++)
                {
                    string row = csvArray[i].Trim();
                    string[] rowArray = row.Split(',');
                    for (int j = 0; j < IDMap.GetLength(1); j++)
                    {
                        IDMap[i, j] = int.Parse(rowArray[j]);
                    }
                }
                return IDMap;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return new int[,] { { }, { } };
            }
        }

        private Cell CellFromId(int id)
        {
            Landscape land = (Landscape)id;
            switch (land)
            {
                case Landscape.TP:
                    {
                        return new Cell { Message = "Teleport", IsPassable = true, IsTaskable = false, IsTeleport = true, IsVisibleWithSkills = false, Symbol = "@", Color = "blue" };
                    }
                case Landscape.UsualWay:
                    {
                        return new Cell { Message = "UsualWay", IsPassable = true, IsTaskable = false, IsTeleport = false, IsVisibleWithSkills = false, Symbol = " ", Color = "black" };
                    }
                case Landscape.Task:
                    {
                        return new Cell { Message = "TASK", IsPassable = true, IsTaskable = true, IsTeleport = false, IsVisibleWithSkills = false, Symbol = "!", Color = "green" };
                    }
                case Landscape.SkillWay:
                    {
                        return new Cell { Message = "SkillWay", IsPassable = true, IsTaskable = false, IsTeleport = false, IsVisibleWithSkills = true, Symbol = "-", Color = "red" };
                    }
                case Landscape.Wall:
                    {
                        return new Cell { Message = "Wall", IsPassable = false, IsTaskable = false, IsTeleport = false, IsVisibleWithSkills = false, Symbol = "#", Color = "gray" };
                    }
                default:
                    {
                        return new Cell { Message = "Wall", IsPassable = false, IsTaskable = false, IsTeleport = false, IsVisibleWithSkills = false, Symbol = "#", Color = "gray" };
                    }

            }
        }


        public static Map GetMap()
        {
            if (MapObject == null)
                MapObject = new Map();
            return MapObject;
        }
    }
}
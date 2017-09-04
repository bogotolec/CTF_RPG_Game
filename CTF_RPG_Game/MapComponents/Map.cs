using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using CTF_RPG_Game_Server;

namespace CTF_RPG_Game.MapComponents
{
    public enum Landscape { TP = 31, UsualWay = 30, Task = 46, SkillWay = 10, Wall = 34 };
    public enum LandscapeCrgm { None, Field, Desert, Forest, Water, Bricks, Lava, Sign}
    public enum MapFormat { Tsx, Crgm }

    public class Map
    {
        public readonly int Width;
        public readonly int Height;
        private Cell[][] CellsMassive;

        private static Map MapObject;

        private Map(string filename)
        {
            if (filename.EndsWith(".crgm"))
            {
                Stream stream = File.OpenRead(filename);

                byte[] buff = new byte[stream.Length];

                stream.Read(buff, 0, (int)stream.Length);

                string[] strings = Encoding.UTF8.GetString(buff).Split(';');

                Height = int.Parse(strings[0]);
                Width = int.Parse(strings[1]);

                CellsMassive = new Cell[Height][];
                for (int i = 0; i < Height; i++)
                {
                    CellsMassive[i] = new Cell[Width];
                    for (int j = 0; j < Width; j++)
                    {
                        CellsMassive[i][j] = new Cell(strings[i * Width + j + 2]);
                    }
                }

            }
            else if (filename.EndsWith(".tsx"))
            {
                int[,] IDMap = LoadMapFileTsx(filename);
                Width = 17;
                Height = 17;
                CellsMassive = new Cell[Height][];
                for (int i = 0; i < Height; i++)
                {
                    CellsMassive[i] = new Cell[Width];
                    for (int j = 0; j < Width; j++)
                    {
                        CellsMassive[i][j] = CellFromId(IDMap[i, j]);
                    }
                }
            }
            else
            {
                if (Program.ConsoleMessages)
                    Console.WriteLine("Unsupported map format");
                return;
            }

            if (Program.ConsoleMessages)
                Console.WriteLine("Map created succesfully");
        }

        private Map()
        {

        }

        public Cell this[int indexY, int indexX]
        {
            get { return CellsMassive[indexY][indexX]; }
        }

        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    output += CellsMassive[i][j].ToString() + " ";
                }
                output += '\n';
            }
            return output;
        }

        private int[,] LoadMapFileTsx(string name)
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

        private Map LoadMapFileCrgm(string name)
        {
            Stream stream = File.OpenRead(name);
            byte[] buff = new byte[stream.Length];

            stream.Read(buff, 0, (int)stream.Length);


            Map map = new Map();

            

            return map;
        }

        private Cell CellFromId(int id)
        {
            Landscape land = (Landscape)id;
            switch (land)
            {
                case Landscape.TP:
                    {
                        return new Cell { EnglishMessage = "Teleport", RussianMessage = "Teleport", IsPassable = true, IsTaskable = false, IsTeleport = true, IsVisibleWithSkills = false, Symbol = "@", Color = ConsoleColor.Blue };
                    }
                case Landscape.UsualWay:
                    {
                        return new Cell { EnglishMessage = "UsualWay", RussianMessage = "UsualWay", IsPassable = true, IsTaskable = false, IsTeleport = false, IsVisibleWithSkills = false, Symbol = " ", Color = ConsoleColor.Black };
                    }
                case Landscape.Task:
                    {
                        return new Cell { EnglishMessage = "TASK", RussianMessage = "TASK",  IsPassable = true, IsTaskable = true, IsTeleport = false, IsVisibleWithSkills = false, Symbol = "!", Color = ConsoleColor.Green };
                    }
                case Landscape.SkillWay:
                    {
                        return new Cell { EnglishMessage = "SkillWay", RussianMessage = "SkillWay", IsPassable = true, IsTaskable = false, IsTeleport = false, IsVisibleWithSkills = true, Symbol = "-", Color = ConsoleColor.Red };
                    }
                case Landscape.Wall:
                    {
                        return new Cell { EnglishMessage = "Wall", RussianMessage = "Wall", IsPassable = false, IsTaskable = false, IsTeleport = false, IsVisibleWithSkills = false, Symbol = "#", Color = ConsoleColor.Gray };
                    }
                default:
                    {
                        return new Cell { EnglishMessage = "Wall", RussianMessage = "Wall", IsPassable = false, IsTaskable = false, IsTeleport = false, IsVisibleWithSkills = false, Symbol = "#", Color = ConsoleColor.Gray };
                    }

            }
        }
        
        public static Map GetMap()
        {
            while (MapObject == null)
            {
                string filename = Program.MAP_NAME;

                while (!File.Exists("Map/" + filename))
                {
                    Console.Write("Enter the map name: ");
                    filename = Console.ReadLine();
                }

                MapObject = new Map("Map/" + filename);
            }

            return MapObject;
        }
    }
}
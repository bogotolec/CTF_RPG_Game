using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.H;

namespace CTF_RPG_Game.MapComponents
{
    enum Landscape { TP = 31, UsualWay = 30, Task = 46, SkillWay = 10, Wall = 34};

    public class Map
    {
        static int Width;
        static int Height;
        static int MapId;
        public Cell CellFromId(int id)
        {
            Landscape land = (Landscape)id;
            switch(land)
            {
                case Landscape.TP:
                    {
                        return new Cell { Message = "Teleport", IsPassable = true, IsTaskable = false, IsTeleport = true, IsVisibleWithSkills = false };
                    }
                case Landscape.UsualWay:
                    {
                        return new Cell { Message = "UsualWay", IsPassable = true, IsTaskable = false, IsTeleport = false, IsVisibleWithSkills = false };
                    }
                case Landscape.Task:
                    {
                        return new Cell { Message = "TASK", IsPassable = true, IsTaskable = true, IsTeleport = false, IsVisibleWithSkills = false };
                    }
                case Landscape.SkillWay:
                    {
                        return new Cell { Message = "SkillWay", IsPassable = true, IsTaskable = false, IsTeleport = false, IsVisibleWithSkills = true };
                    }
                case Landscape.Wall:
                    {
                        return new Cell { Message = "Wall", IsPassable = false, IsTaskable = false, IsTeleport = false, IsVisibleWithSkills = false };
                    }
                default:
                    {
                        return new Cell { Message = "Wall", IsPassable = false, IsTaskable = false, IsTeleport = false, IsVisibleWithSkills = false };
                    }
                     
            }
        }
        public Map()
        {
            Console.Write("HI");
            Width = 17;
            Height = 17;
            int[,] idMap = new int[,] { { 34,34,34,34,34,34,34,34,34,34,34,34,34,34,34,34,34 },
                                        { 34,34,34,34,34,34,34,34,34,34,34,34,34,34,34,34,34 },
                                        { 34,34,34,34,34,46,30,30,34,34,34,34,34,34,34,34,34 },
                                        { 34,46,34,34,34,34,34,30,30,30,34,34,34,34,34,34,34 },
                                        { 34,30,34,34,34,34,34,34,34,30,34,34,34,34,34,34,34 },
                                        { 34,30,30,30,34,34,34,34,30,30,34,34,34,34,34,34,34 },
                                        { 34,34,34,30,34,34,34,34,30,34,34,34,34,34,34,34,34 },
                                        { 34,34,34,30,34,34,34,34,30,34,34,34,34,34,34,34,34 },
                                        { 34,34,34,30,30,30,30,30,31,30,30,30,30,30,34,34,34 },
                                        { 34,34,34,34,34,34,34,34,30,34,34,34,34,30,34,34,34 },
                                        { 34,34,34,34,34,34,34,30,30,34,34,30,30,30,34,34,34 },
                                        { 34,34,34,34,34,34,30,30,34,34,34,30,34,34,34,34,34 },
                                        { 34,34,34,34,34,34,30,34,34,34,34,30,30,30,34,34,34 },
                                        { 34,34,34,34,34,34,30,34,34,34,34,34,34,30,34,34,34 },
                                        { 34,34,10,10,10,34,30,34,34,34,34,34,34,30,34,34,34 },
                                        { 34,46,10,10,10,10,30,30,46,34,34,34,34,46,34,34,34 },
                                        { 34,34,34,34,34,34,34,34,34,34,34,34,34,34,34,34,34 } };
            Cell[,] CellsMassive = new Cell[Height,Width];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    CellsMassive[i, j] = CellFromId(idMap[i, j]);
                }
            }
        }   
    }
}
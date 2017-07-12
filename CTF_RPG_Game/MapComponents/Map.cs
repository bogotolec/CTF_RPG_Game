using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.H;

namespace CTF_RPG_Game.MapComponents
{
    public class Map
    {
        static int Width;
        static int Height;
        static int MapId;
        public Map()
        {
            Console.Write("HI");
            Width = 16;
            Height = 16;
            Cell[,] CellsMassive = new Cell[Height,Width];
            CellsMassive[0, 0] = new Cell { Message="FirstCell", IsPassable=true, IsTaskable=false, IsTeleport=true, IsVisibleWithSkills=false};
            CellsMassive[1, 0] = new Cell { Message = "Tunnel", IsPassable = true, IsTaskable = false, IsTeleport = false, IsVisibleWithSkills = false};
            CellsMassive[2, 0] = new Cell { Message = "Tunnel", IsPassable = true, IsTaskable = false, IsTeleport = false, IsVisibleWithSkills = false};
            CellsMassive[0, 1] = new Cell { Message = "Wall", IsPassable = false, IsTaskable = false, IsTeleport = false, IsVisibleWithSkills = false};
            CellsMassive[1, 1] = new Cell { Message = "Wall", IsPassable = false, IsTaskable = false, IsTeleport = false, IsVisibleWithSkills = false};
            CellsMassive[2, 1] = new Cell { Message = "Wall", IsPassable = false, IsTaskable = false, IsTeleport = false, IsVisibleWithSkills = false};


        }   
    }
}
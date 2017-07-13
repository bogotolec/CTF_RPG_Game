using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF_RPG_Game.CharacterInteraction
{ 
    class Character
    {
        public int ID { get; }
        public string Name { get; }
        public int Level { get; }
        public int X { get; }
        public int Y { get; }
        public int SkillPoints { get; }
        ISkill LearnedSkills { get; }
        IItem Backpack { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF_RPG_Game.CharacterInteraction
{
    interface ISkill
    {
        int Id { get; }
        string Name { get; }
        string Description { get; }
        int PointsToLearn { get; } 
        SkillState State { get; }
        CharacterClass Class { get; }
        ISkill[] NeededSkills { get; }
    }

    interface IActive
    {
        string UseDescription { get; }
        void Use();
    }

    interface IPassive
    {
        string PassiveBonusDescription { get; }
    }

    enum SkillState { Passive, Active };
    enum CharacterClass { None, Hacker, SocEng }
}

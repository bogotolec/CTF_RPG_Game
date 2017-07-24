using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTF_RPG_Game.Languages;

namespace CTF_RPG_Game.CharacterInteraction
{
    interface ISkill
    {
        int Id { get; }
        string Name(ILanguage lang);
        string Description(ILanguage lang);
        int PointsToLearn { get; }
        SkillState State { get; }
        CharacterClass Class { get; }
        ISkill[] NeededSkills { get; }
    }

    interface IActive
    {
        string UseDescription(ILanguage lang);
        void Use(string parametrs, Character user);
    }

    interface IPassive
    {
        string PassiveBonusDescription(ILanguage lang);
    }

    enum SkillState { Passive, Active }
    enum CharacterClass { None, Hacker, SocEng }
}

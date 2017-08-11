using System;
using CTF_RPG_Game.Languages;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF_RPG_Game.CharacterInteraction
{
    static class Skill
    {
        private static ISkill[] SkillList = { new Teleportation() };

        static public ISkill GetById(int id)
        {
            foreach (ISkill skill in SkillList)
            {
                if (skill.Id == id)
                    return skill;
            }
            return null;
        }
    }

    abstract class BasePassiveSkill : ISkill, IPassive
    {
        virtual public int Id { get { return 0; } }
        virtual public int PointsToLearn { get { return 0; } }
        public SkillState State { get { return SkillState.Passive; } }
        virtual public CharacterClass Class { get { return CharacterClass.None; } }
        virtual public ISkill[] NeededSkills { get { return new ISkill[0]; } }

        virtual public string Name(ILanguage lang)
        {
            return lang.BasePassiveSkillName;
        }

        virtual public string Description(ILanguage lang)
        {
            return lang.BasePassiveSkillDescription;
        }

        virtual public string PassiveBonusDescription(ILanguage lang)
        {
            return lang.None;
        }
    }

    abstract class BaseActiveSkill : ISkill, IActive
    {
        virtual public int Id { get { return 0; } }
        virtual public int PointsToLearn { get { return 0; } }
        public SkillState State { get { return SkillState.Active; } }
        virtual public CharacterClass Class { get { return CharacterClass.None; } }
        virtual public ISkill[] NeededSkills { get { return new ISkill[0]; } }

        virtual public string Name(ILanguage lang)
        {
            return lang.BaseActiveSkillName;
        }

        virtual public string Description(ILanguage lang)
        {
            return lang.BasePassiveSkillDescription;
        }

        virtual public string UseDescription(ILanguage lang)
        {
            return lang.None;
        }

        virtual public void Use(string parametrs, Character user)
        {
            Console.WriteLine("Used base active skill.");
        } 
    }

    class Teleportation : BaseActiveSkill
    {
        public override int Id { get { return 1; } }
        public override int PointsToLearn { get { return 3; } }
        public override CharacterClass Class { get { return CharacterClass.Hacker; } }
        public override ISkill[] NeededSkills { get { return new ISkill[0]; } }

        public override string Name(ILanguage lang)
        {
            return lang.SkillTeleportationName;
        }

        public override string Description(ILanguage lang)
        {
            return lang.SkillTeleportationDescription;
        }

        public override string UseDescription(ILanguage lang)
        {
            return lang.SkillTeleportationUseDescription;
        }

        public override void Use(string paramets, Character user)
        {
            
        }
    }

    class Jeweller : BasePassiveSkill
    {
        public override int Id { get { return 2; } }
        public override CharacterClass Class { get { return CharacterClass.SocEng; } }
        public override int PointsToLearn { get { return 4; } }
        public override ISkill[] NeededSkills { get { return new ISkill[0]; } }

        public override string Description(ILanguage lang)
        {
            return base.Description(lang);
        }
    }
}

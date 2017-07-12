using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF_RPG_Game.CharacterInteraction
{
    abstract class BasePassiveSkill : ISkill, IPassive
    {
        virtual public int Id { get { return 0; } }
        virtual public string Name { get { return "Base_passive_skill"; } }
        virtual public string Description { get { return "No description."; } }
        virtual public int PointsToLearn { get { return 0; } }
        public SkillState State { get { return SkillState.Passive; } }
        virtual public CharacterClass Class { get { return CharacterClass.None; } }
        virtual public ISkill[] NeededSkills { get { return new ISkill[0]; } }
        virtual public string PassiveBonusDescription { get { return "That skill is giving nothing"; } }
    }

    abstract class BaseActiveSkill : ISkill, IActive
    {
        virtual public int Id { get { return 0; } }
        virtual public string Name { get { return "Base_active_skill"; } }
        virtual public string Description { get { return "No description."; } }
        virtual public int PointsToLearn { get { return 0; } }
        public SkillState State { get { return SkillState.Active; } }
        virtual public CharacterClass Class { get { return CharacterClass.None; } }
        virtual public ISkill[] NeededSkills { get { return new ISkill[0]; } }
        virtual public string UseDescription { get { return "Nothing."; } }
        virtual public void Use()
        {
            Console.WriteLine("Used base active skill.");
        } 
    }
}

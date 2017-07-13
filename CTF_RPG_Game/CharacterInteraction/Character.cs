using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CTF_RPG_Game_Server;

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
        List<ISkill> LearnedSkills;
        List<IItem> Backpack;
        public IItem Head { get; }
        public IItem Body { get; }
        public IItem LHand { get; }
        public IItem RHand { get; }
        public IItem Boots { get; }
        public IItem JeweleryOne { get; }
        public IItem JeweleryTwo { get; }

        private Character(int id, string name, int level, int x, int y,
            int skillpoints, string learnedskills, string backpack, int head, int body, int lhand,
            int rhand, int boots, int jewelery1, int jewelery2)
        {
            ID = id;
            Name = name;
            Level = level;
            X = x;
            Y = y;
            SkillPoints = skillpoints;

            LearnedSkills = new List<ISkill>();
            foreach (string skillid in learnedskills.Split(','))
            {
                LearnedSkills.Add(Skill.GetById(int.Parse(skillid)));
            }

            Backpack = new List<IItem>();
            foreach (string itemid in backpack.Split(','))
            {
                Backpack.Add(Item.GetById(int.Parse(itemid)));
            }

            Head = Item.GetById(head);
            Body = Item.GetById(body);
            LHand = Item.GetById(lhand);
            RHand = Item.GetById(rhand);
            Boots = Item.GetById(boots);
            JeweleryOne = Item.GetById(jewelery1);
            JeweleryTwo = Item.GetById(jewelery2);
        }

        public static Character GetCharacter(int id)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM GameCharacters WHERE Id=" + id.ToString();
            command.Connection = Program.SQLCLIENT;

            SqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows)
                throw new NoCharacterException();

            reader.Read();
            Character character = new Character(
                (int)reader.GetValue(0),
                (string)reader.GetValue(1),
                (int)reader.GetValue(2),
                (int)reader.GetValue(3),
                (int)reader.GetValue(4),
                (int)reader.GetValue(5),
                (string)reader.GetValue(14),
                (string)reader.GetValue(13),
                (int)reader.GetValue(6),
                (int)reader.GetValue(7),
                (int)reader.GetValue(8),
                (int)reader.GetValue(9),
                (int)reader.GetValue(10),
                (int)reader.GetValue(11),
                (int)reader.GetValue(12));

            return character;
        }
    }

    class NoCharacterException : Exception
    {
        public override string Message
        {
            get
            {
                return "Нет такого персонажа в базе данных.";
            }
        }
    }
}

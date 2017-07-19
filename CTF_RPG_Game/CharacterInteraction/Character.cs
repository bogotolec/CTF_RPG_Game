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
        public int SkillPoints { get; }
        public int Health { get; }
        public int Gold { get; }

        public int X { get; set; }
        public int Y { get; set; }

        public List<ISkill> LearnedSkills;
        public List<IItem> Backpack;

        public IItem Head { get; }
        public IItem Body { get; }
        public IItem LHand { get; }
        public IItem RHand { get; }
        public IItem Boots { get; }
        public IItem JeweleryOne { get; }
        public IItem JeweleryTwo { get; }

        private Character(int id, string name, int level, int x, int y,
            int skillpoints, string learnedskills, string backpack, int head, int body, int lhand,
            int rhand, int boots, int jewelery1, int jewelery2, int gold, int health)
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

            Gold = gold;
            Health = Health;
        }

        public static Character Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(Program.DBConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM dbo.GameCharacters WHERE Id=" + id.ToString();
                command.Connection = connection;

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
                    (string)reader.GetValue(16),
                    (string)reader.GetValue(15),
                    (int)reader.GetValue(6),
                    (int)reader.GetValue(7),
                    (int)reader.GetValue(8),
                    (int)reader.GetValue(9),
                    (int)reader.GetValue(10),
                    (int)reader.GetValue(11),
                    (int)reader.GetValue(12),
                    (int)reader.GetValue(13),
                    (int)reader.GetValue(14));

                return character;
            }
        }

        public static Character Create(int id, string name)
        {
            using (SqlConnection connection = new SqlConnection(Program.DBConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO dbo.GameCharacters " +
                    "(Id, Name, Lvl, CoordX, CoordY, SkillPoints, Gold, Health) VALUES " +
                    "(" + id.ToString() + ", '" + name + "', 8, 8, 0, 0, 100)";
                command.Connection = connection;
                command.ExecuteNonQuery();
            }

            return Get(id);
        }

        public void SaveCharacter()
        {
            using (SqlConnection connection = new SqlConnection(Program.DBConnectionString))
            {
                List<int> itemlist = new List<int>();
                foreach (IItem item in Backpack)
                {
                    itemlist.Add(item.Id);
                }
                string backpackDB = string.Join(",", itemlist);

                List<int> skilllist = new List<int>();
                foreach (ISkill skill in LearnedSkills)
                {
                    skilllist.Add(skill.Id);
                }
                string learnedskillsDB = string.Join(",", skilllist);

                SqlCommand command = new SqlCommand();
                command.CommandText = "UPDATE dbo.GameCharacters SET " +
                    "Name='" + Name.ToString() + "', " +
                    "Lvl=" + Level.ToString() + ", " +
                    "CoordX=" + X.ToString() + ", " +
                    "CoordY=" + Y.ToString() + ", " +
                    "SkillPoints=" + SkillPoints.ToString() + ", " +
                    "HeadId=" + Head == null ? "NULL" : Head.Id.ToString() + ", " +
                    "BodyId=" + Body == null ? "NULL" : Body.Id.ToString() + ", " +
                    "LHandId=" + LHand == null ? "NULL" : LHand.Id.ToString() + ", " +
                    "RHandId=" + RHand == null ? "NULL" : RHand.Id.ToString() + ", " +
                    "Boots=" + Boots == null ? "NULL" : Boots.Id.ToString() + ", " +
                    "JeweleryOne=" + JeweleryOne == null ? "NULL" : JeweleryOne.Id.ToString() + ", " +
                    "JeweleryTwo=" + JeweleryTwo == null ? "NULL" : JeweleryTwo.Id.ToString() + ", " +
                    "Gold=" + Gold.ToString() + ", " +
                    "Health=" + Health.ToString() + ", " +
                    "BackPack='" + backpackDB.ToString() + "', " +
                    "LearnedSkills=" + learnedskillsDB.ToString() +
                    " WHERE id=" + ID.ToString() + " LIMIT 1";

                command.Connection = connection;
                command.ExecuteNonQuery();
            }
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

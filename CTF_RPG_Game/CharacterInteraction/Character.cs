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
                command.CommandText = "SELECT * FROM dbo.GameСharacters WHERE Id=" + id.ToString();
                command.Connection = connection;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                    throw new NoCharacterException();

                reader.Read();
                int Id = reader.GetInt32(0);
                string name = reader.GetString(1);
                int level = reader.GetInt32(2);
                int x = reader.GetInt32(3);
                int y = reader.GetInt32(4);
                int skillpoints = reader.GetInt32(5);
                string learnedskills = reader.GetString(16);
                string backpack = reader.GetString(15);
                int head = reader.GetInt32(6);
                int body = reader.GetInt32(7);
                int lhand = reader.GetInt32(8);
                int rhand = reader.GetInt32(9);
                int boots = reader.GetInt32(10);
                int jewelery1 = reader.GetInt32(11);
                int jewelery2 = reader.GetInt32(12);
                int gold = reader.GetInt32(13);
                int health = reader.GetInt32(14);
                Character character = new Character(Id, name, level, x, y, skillpoints, learnedskills,
                    backpack, head, body, lhand, rhand, boots, jewelery1, jewelery2, gold, health);

                return character;
            }
        }

        public static Character Create(int id, string name)
        {
            using (SqlConnection connection = new SqlConnection(Program.DBConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO dbo.GameСharacters " +
                    "(Id, Name, Lvl, CoordX, CoordY, SkillPoints, HeadId, BodyId, LHandId, RHandId, Boots, JeweleryOne, JeweleryTwo, Gold, Health, BackPack, LearnedSkills) VALUES " +
                    "(" + id.ToString() + ", '" + name + "', 0, 8, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0)";
                command.Connection = connection;
                connection.Open();
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
                    if (item == null)
                        itemlist.Add(0);
                    else
                        itemlist.Add(item.Id);
                }
                string backpackDB = string.Join(",", itemlist);

                List<int> skilllist = new List<int>();
                foreach (ISkill skill in LearnedSkills)
                {
                    if (skill == null)
                        skilllist.Add(0);
                    else
                        skilllist.Add(skill.Id);
                }
                string learnedskillsDB = string.Join(",", skilllist);

                SqlCommand command = new SqlCommand();
                command.CommandText = "UPDATE dbo.GameСharacters SET " +
                    "Name='" + Name.ToString() + "', " +
                    "Lvl=" + Level.ToString() + ", " +
                    "CoordX=" + X.ToString() + ", " +
                    "CoordY=" + Y.ToString() + ", " +
                    "SkillPoints=" + SkillPoints.ToString() + ", " +
                    "HeadId=" + (Head == null ? "0" : Head.Id.ToString()) + ", " +
                    "BodyId=" + (Body == null ? "0" : Body.Id.ToString()) + ", " +
                    "LHandId=" + (LHand == null ? "0" : LHand.Id.ToString()) + ", " +
                    "RHandId=" + (RHand == null ? "0" : RHand.Id.ToString()) + ", " +
                    "Boots=" + (Boots == null ? "0" : Boots.Id.ToString()) + ", " +
                    "JeweleryOne=" + (JeweleryOne == null ? "0" : JeweleryOne.Id.ToString()) + ", " +
                    "JeweleryTwo=" + (JeweleryTwo == null ? "0" : JeweleryTwo.Id.ToString()) + ", " +
                    "Gold=" + Gold.ToString() + ", " +
                    "Health=" + Health.ToString() + ", " +
                    "BackPack='" + backpackDB.ToString() + "', " +
                    "LearnedSkills=" + learnedskillsDB.ToString() +
                    " WHERE id=" + ID.ToString();

                command.Connection = connection;
                connection.Open();
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

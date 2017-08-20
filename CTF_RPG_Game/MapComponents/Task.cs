using System;
using System.IO;
using System.Collections.Generic;
using CTF_RPG_Game.Languages;

namespace CTF_RPG_Game.MapComponents
{
    public class Task
    {
        private static List<Task> TaskList = new List<Task>();

        public int ID { get; }
        public string Name { get; }
        public string Flag { get; }
        public int Gold { get; }
        public int LearnPoints { get; }

        public string Message(ILanguage Language)
        {
            if (Language == Russian.GetLanguage())
                return MessageRussian;
            else
                return "Error";
        }

        public Task(string PathToConfig)
        {
            string[] config = File.ReadAllText(PathToConfig + "config").Split('\n');

            foreach (var str in config)
            {
                if (str.StartsWith("id="))
                    ID = int.Parse(str.Substring("id=".Length));

                if (str.StartsWith("name="))
                    Name = str.Substring("name=".Length);

                if (str.StartsWith("messagerussian="))
                    MessageRussian = str.Substring("messagerussian=".Length);

                if (str.StartsWith("messageenglish="))
                    MessageEnglish = str.Substring("messageenglish=".Length);

                if (str.StartsWith("flag="))
                    Flag = str.Substring("flag=".Length);

                if (str.StartsWith("gold="))
                    Gold = int.Parse(str.Substring("gold=".Length));

                if (str.StartsWith("learnpoints="))
                    LearnPoints = int.Parse(str.Substring("learnpoints=".Length));
            }

            TaskList.Add(this);
        }

        public static Task GetById(int Id)
        {
            foreach (var task in TaskList)
            {
                if (Id == task.ID)
                    return task;
            }
            return null;
        }

        public static Task GetByName(string Name)
        {
            foreach (var task in TaskList)
            {
                if (Name == task.Name)
                    return task;
            }
            return null;
        }

        public static void CreateTasks()
        {
            DirectoryInfo DInfo = new DirectoryInfo("Task");
            DirectoryInfo[] TaskDirs = DInfo.GetDirectories();

            foreach (var dir in TaskDirs)
            {
                new Task(dir.FullName + '\\');
            }
        }

        private string MessageRussian { get; }
        private string MessageEnglish { get; }
    }
}

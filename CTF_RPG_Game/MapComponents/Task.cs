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
        public string Category { get; }
        public int Gold { get; }
        public int LearnPoints { get; }

        public string Message(ILanguage Language)
        {
            if (Language == Russian.GetLanguage())
                return MessageRussian;
            else
                return "Error";
        }

        private Task(string PathToConfig)
        {
            string[] config = File.ReadAllText(PathToConfig + "config").Split('\n');

            foreach (var str in config)
            {
                if (str.StartsWith("id="))
                    ID = int.Parse(str.Substring("id=".Length).Trim('\r'));

                if (str.StartsWith("name="))
                    Name = str.Substring("name=".Length).Trim('\r');

                if (str.StartsWith("messagerussian="))
                    MessageRussian = str.Substring("messagerussian=".Length).Trim('\r');

                if (str.StartsWith("messageenglish="))
                    MessageEnglish = str.Substring("messageenglish=".Length).Trim('\r');

                if (str.StartsWith("flag="))
                    Flag = str.Substring("flag=".Length).Trim('\r');

                if (str.StartsWith("gold="))
                    Gold = int.Parse(str.Substring("gold=".Length).Trim('\r'));

                if (str.StartsWith("learnpoints="))
                    LearnPoints = int.Parse(str.Substring("learnpoints=".Length).Trim('\r'));

                if (str.StartsWith("category="))
                    Category = str.Substring("category=".Length).Trim('\r');
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
            DirectoryInfo DInfo = new DirectoryInfo("Tasks");
            DirectoryInfo[] TaskDirs = DInfo.GetDirectories();

            foreach (var dir in TaskDirs)
            {
                new Task(dir.FullName + '/');
            }
        }

        private string MessageRussian { get; }
        private string MessageEnglish { get; }
    }
}

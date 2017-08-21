using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTF_RPG_Game.Languages;

namespace CTF_RPG_Game.CharacterInteraction
{
    static class Item
    {
        private static IItem[] ItemList = { new Branch() };

        public static IItem GetById(int id)
        {
            if (id == 0)
                return null;

            foreach (IItem item in ItemList)
            {
                if (item.Id == id)
                    return item;
            }
            return null;
        }
    }

    class Branch : IItem
    {
        public int Id { get { return 1; } }
        public int SellCost { get { return 5; } }
        public int BuyCost { get { return 10; } }

        private string RussianName { get { return "Ветка"; } }
        private string EnglishName { get { return "Branch"; } }

        private string RussianDescription { get { return "Простая ветка, ничего особенного."; } }
        private string EnglishDescription { get { return "Just branch"; } }

        public string Name(ILanguage lang)
        {
            if (lang.ToString() == "Russian")
                return RussianName;
            return EnglishName;
        } 
        
        public string Description(ILanguage lang)
        {
            if (lang.ToString() == "Russian")
                return RussianDescription;
            return EnglishDescription;
        }
    }
}

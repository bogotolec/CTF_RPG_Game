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

        public string Name(ILanguage lang)
        {
            return lang.ItemBranchName;
        } 
        
        public string Description(ILanguage lang)
        {
            return lang.ItemBranchDescription;
        }
    }
}

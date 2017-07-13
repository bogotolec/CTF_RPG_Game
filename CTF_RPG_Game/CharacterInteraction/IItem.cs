using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTF_RPG_Game.Languages;

namespace CTF_RPG_Game.CharacterInteraction
{
    interface IItem
    {
        int Id { get; }
        string Name(ILanguage lang);
        string Description(ILanguage lang);
        int SellCost { get; }
        int BuyCost { get; }
    }

    interface IEquiped
    {
        string EquipDescription { get; }
        EquipSlot Slot { get; }
    }

    enum ItemState { Equiped, EqupedAndUsable, Usable, UsableOnce }
    enum EquipSlot { Head, Body, LHand, Rhand, Boots, Jewelery }
}

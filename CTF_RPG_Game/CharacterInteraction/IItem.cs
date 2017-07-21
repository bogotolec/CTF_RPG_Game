using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF_RPG_Game.CharacterInteraction
{
    interface IItem
    {
        int Id { get; }
        string Name { get; }
        string Description { get; }
        int SellCost { get; }
        int BuyCost { get; }
        void Sell();
        void Buy();
    }

    interface IEquiped
    {
        string EquipDescription { get; }
        EquipSlot Slot { get; }
    }

    enum ItemState { Equiped, EqupedAndUsable, Usable, UsableOnce }
    enum EquipSlot { Head, Body, LHand, Rhand, Boots, Jewelery }
}

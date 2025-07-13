using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Character;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Interfaces;

namespace TextRPG_TeamSix.Items
{
    internal class Weapon : Item, IEquippable
    {
        public Ability Ability { get; private set; }    //증가하는 능력 종류가 여러개면, Dictionary <Ability, uint> 
        public uint Enhancement { get; private set; }

        public EquipSlot EquipSlot { get; private set; }

        public Weapon(uint id, string name, string description, uint price) : base(id, name, description, price)
        {
        }

        public void Equip(Player player)
        {
        }

        public void Unequip(Player player)
        {
        }
    }
}

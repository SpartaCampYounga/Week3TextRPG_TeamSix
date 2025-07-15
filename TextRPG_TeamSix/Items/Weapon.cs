using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Enums;

namespace TextRPG_TeamSix.Items
{
    internal class Weapon : Item, IEquippable
    {
        public Ability Ability { get; private set; }    //증가하는 능력 종류가 여러개면, Dictionary <Ability, uint> 
        public uint Enhancement { get; private set; }
        public EquipSlot EquipSlot { get; private set; }

        public Weapon(uint id, string name, string description, uint price, Ability ability, uint enhancement, EquipSlot equipSlot) : base(id, name, description, price)
        {
            Ability = ability;
            Enhancement = enhancement;
            EquipSlot = equipSlot;
        }

        public void Equip(Character character)
        {
            //장착 구현
        }

        public void Unequip(Character character)
        {
            //해제 구현
        }
    }
}

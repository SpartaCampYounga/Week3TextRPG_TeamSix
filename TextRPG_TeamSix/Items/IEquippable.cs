using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Characters;

namespace TextRPG_TeamSix.Items
{
    //장비템 인터페이스
    internal interface IEquippable
    {
        public EquipSlot EquipSlot { get; }
        public void Equip(Character character);
        public void Unequip(Character character);
    }
}

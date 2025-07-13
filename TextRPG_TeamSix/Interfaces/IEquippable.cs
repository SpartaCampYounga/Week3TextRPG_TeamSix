using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Character;

namespace TextRPG_TeamSix.Interfaces
{
    //장비템 인터페이스
    internal interface IEquippable
    {
        public EquipSlot EquipSlot { get; }
        public void Equip(Player player);
        public void Unequip(Player player);
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Items;

namespace TextRPG_TeamSix.Items
{
    internal class Armor : Item, IEquippable
    {
        public Ability Ability { get; private set; } // 예: Defense 증가
        public uint Enhancement { get; private set; }
        public EquipSlot EquipSlot { get; private set; }
        protected Armor() { }

        [JsonConstructor]
        public Armor(uint id, string name, string description, uint price, ItemType type, Ability ability, uint enhancement, EquipSlot equipSlot)
            : base(id, name, description, price, type)
        {
            Ability = ability;
            Enhancement = enhancement;
            EquipSlot = equipSlot;
        }

        public void Equip(Character character)
        {
            // 장착 구현
        }

        public void Unequip(Character character)
        {
            // 해제 구현
        }
        public override void Clone<T>(T item)
        {
            base.Clone(item);
            if (item is Armor armor)    //패턴매칭 Younga TIL
            {
                this.Ability = armor.Ability;
                this.Enhancement = armor.Enhancement;
                this.EquipSlot = armor.EquipSlot;
            }
        }
        public override Item CreateInstance()
        {
            return new Armor();
        }
    }
}
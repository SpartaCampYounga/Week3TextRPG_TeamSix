using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Items
{
    internal class EquipItem : Item, IEquippable
    {
        public Ability Ability { get; private set; }    //증가하는 능력 종류가 여러개면, Dictionary <Ability, uint> 
        public uint Enhancement { get; private set; }
        public EquipSlot EquipSlot { get; private set; }
        protected EquipItem() { }

        [JsonConstructor]
        public EquipItem(uint id, string name, string description, uint price, ItemType type, Ability ability, uint enhancement, EquipSlot equipSlot, bool isSpecialItem = false) : base(id, name, description, price, type, isSpecialItem)
        {
            Ability = ability;
            Enhancement = enhancement;
            EquipSlot = equipSlot;
        }

        public override string ToString()
        {
            string display = "";
            if (PlayerManager.Instance.EquipmentList.ContainsKey(this.EquipSlot) 
                && PlayerManager.Instance.EquipmentList[EquipSlot].Id == Id)
            {
                display += "[E] ";
            }
            else
            {
                display += "    ";
            }
            display += FormatUtility.AlignLeftWithPadding(Name, 15) + " ┊ ";
            display += FormatUtility.AlignLeftWithPadding(Description, 50) + " ┊ ";
            switch (Ability)
            {
                case Ability.Attack:
                    display += FormatUtility.AlignLeftWithPadding("공격력 +" + Enhancement, 20) + " ┊ ";
                    break;
                case Ability.Defense:
                    display += FormatUtility.AlignLeftWithPadding("방어력 +" + Enhancement, 20) + " ┊ ";
                    break;
            }
            display += FormatUtility.AlignLeftWithPadding(Price.ToString() + " G", 8);
            return display;
        }

        public override void Clone<T>(T item)
        {
            base.Clone(item);
            if (item is EquipItem weapon)
            {
                this.Ability = weapon.Ability;
                this.Enhancement = weapon.Enhancement;
                this.EquipSlot = weapon.EquipSlot;
            }
        }
        public override Item CreateInstance()
        {
            return new EquipItem();
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Scenes;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Items
{
    //아이템 추상클래스 -> 각 아이템에 상속
    internal abstract class Item // 추상 클래스, 상속받아서 사용.
    {
        public uint Id { get; protected set; } // 아이템의 고유 ID
        public string Name { get; protected set; } // 아이템의 이름
        public string Description { get; protected set; } // 아이템의 설명
        public uint Price { get; protected set; } // 아이템의 가격
        //public bool IsEquipped { get; set; } // 아이템이 장착되었는지 여부
        //영아: 아이템 인스턴스들은 복사를 할때 참조 복사가 되므로, 이 항목이 장착할때마다 변경될 경우 복사된 아이템, 복사한 아이템 둘 다 해당 필드가 변하게 될 것입니다.
        //또한 장착 개념은 플레이어가 하는 것이므로, 맥락상 플레이어에서 활용하는 것이 알맞아보입니다.
        //저도 개인 과제에서는 Player.cs에 Dictionary<EquipSlot,Item> Equipment라는 필드를 사용했었습니다.
        //다만 저희 프로젝트는 PlayerManager가 있으니 이쪽으로 이동하여 진행했습니다.
        //해당 주석은 이 변수를 생성해서 활용하셨던 분들이 확인하시면 삭제 부탁드립니다.
        //[JsonProperty("ItemType")]
        public ItemType Type { get; protected set; }  // 아이템 종류 구분
        public bool IsSpecialItem { get; protected set; } = false; // 특별 아이템 여부(일반아이템은 false, 특별 아이템은 true)
        public enum ItemType 
        {
            Weapon, 
            Armor, 
            Accessory, 
            Consumable 
        } // 아이템의 종류 (ex : 무기, 방어구 등)
        protected Item() { }

        public Item(uint id, string name, string description, uint price, ItemType type, bool isSpecialItem = false) // 생성자(이 클래스(자식 클래스)가 생성될때 마다 필요한 값들)
        {
            Id = id; // 아이템의 고유 ID
            Name = name; // 아이템의 이름
            Description = description; // 아이템의 설명
            Price = price; // 아이템의 가격
            Type = type; // 아이템 종류 구분
            IsSpecialItem = isSpecialItem; // 특별 아이템 여부
        }

        public override string ToString()    //자식 클래스들도 override 메서드 생성해주기
        {
            string display = "";
            //if ()
            //{
            //    display += "[E] ";
            //}
            //else
            {
                display += "    ";
            }
            display += FormatUtility.AlignWithPadding(Name, 15) + " | ";
            display += FormatUtility.AlignWithPadding(Description, 50) + " | ";
            display += FormatUtility.AlignWithPadding(Price.ToString() + " G", 8);
            return display;
        }

        public virtual void Clone<T>(T item) where T : Item
        {
            this.Id = item.Id;
            this.Name = item.Name;
            this.Description = item.Description;
            this.Price = item.Price;
            this.Type = item.Type;
        }
        public abstract Item CreateInstance();
    }
}

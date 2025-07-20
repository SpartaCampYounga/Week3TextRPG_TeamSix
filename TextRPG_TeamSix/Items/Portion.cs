using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Items
{
    //포션 클래스는 구체 아이템을 나타내며, Item 클래스를 상속받고 IConsumable 인터페이스를 구현합니다.
    internal class Portion : Item, IConsumable
    {
        public RestoreType RestoreType { get; private set; } // 마나나 스테미나 포션 종류 추가할때. RestoreType Enum 추가하여 관리
        public uint RestoreAmount { get; private set; } //회복량 
        protected Portion() { }

        // 생성자: 포션의 ID, 이름, 설명, 가격, 회복량, 회복 타입을 초기화합니다.
        [JsonConstructor]
        public Portion(uint id, string name, string description, uint price, ItemType type, uint restoreAmount, RestoreType restoreType, bool isSpecialItem = false) : base(id, name, description, price, type, isSpecialItem) // Item(부모) 클래스의 생성자를 호출합니다.
        {
            RestoreAmount = restoreAmount;
            RestoreType = restoreType;

        }
        public override string ToString()
        {
            string display = "    ";
            display += FormatUtility.AlignLeftWithPadding(Name, 15) + " ┊ ";
            display += FormatUtility.AlignLeftWithPadding(Description, 50) + " ┊ ";
            switch (RestoreType)
            {
                case RestoreType.Health:
                    display += FormatUtility.AlignLeftWithPadding("체력 회복 +" + RestoreAmount, 20) + " ┊ ";
                    break;
                case RestoreType.Mana:
                    display += FormatUtility.AlignLeftWithPadding("마나 회복 +" + RestoreAmount, 20) + " ┊ ";
                    break;
                case RestoreType.All:
                    display += FormatUtility.AlignLeftWithPadding("체력과 마나 회복 +" + RestoreAmount, 20) + " ┊ ";
                    break;
            }
            display += FormatUtility.AlignLeftWithPadding(Price.ToString() + " G", 8);
            return display;
        }
        // IConsumable 인터페이스의 Consume 메서드 구현
        public void Consume<T>(Character character, List<T> list) // Consume 메서드는 Character 객체를 받아 해당 캐릭터의 상태를 회복합니다. 또한 해당 리스트에서 아이템을 삭제합니다.
        {
            var portion = list.FirstOrDefault(item => item is IConsumable);

            if (portion is IConsumable consumable)
            {
                if (RestoreType == RestoreType.Health)
                {
                    //체력 회복 로직
                    character.HealedHP(RestoreAmount);
                    Console.WriteLine($"{character.Name}의 체력이 {RestoreAmount}만큼 회복되었습니다."); //현재 체력: {character.HP}
                }
                else if (RestoreType == RestoreType.Mana)
                {
                    //마나 회복 로직
                    character.RecoveredMP(RestoreAmount);
                    Console.WriteLine($"{character.Name}의 마나가 {RestoreAmount}만큼 회복되었습니다."); //현재 마나: {character.MP}
                }
                else if (RestoreType == RestoreType.All)
                {
                    character.HealedHP(RestoreAmount);
                    character.RecoveredMP(RestoreAmount);
                    Console.WriteLine($"{character.Name}의 체력과 마나가 {RestoreAmount}만큼 회복되었습니다."); //현재 체력: {character.HP}, 현재 마나: {character.MP}
                }
                list.Remove(portion);
            }

        }

        public override void Clone<T>(T item)
        {
            base.Clone(item);
            if (item is Portion portion)
            {
                this.RestoreType = portion.RestoreType;
                this.RestoreAmount = portion.RestoreAmount;
            }
        }
        public override Item CreateInstance()
        {
            return new Portion();
        }
    }
}

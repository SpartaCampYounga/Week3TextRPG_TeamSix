using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_TeamSix.Items
{
    //아이템 추상클래스 -> 각 아이템에 상속
    internal abstract class Item // 추상 클래스, 상속받아서 사용.
    {
        public uint Id { get; private set; } // 아이템의 고유 ID
        public string Name { get; private set; } // 아이템의 이름
        public string Description { get; private set; } // 아이템의 설명
        public uint Price { get; private set; } // 아이템의 가격

        public Item(uint id, string name, string description, uint price) // 생성자(이 클래스(자식 클래스)가 생성될때 마다 필요한 값들)
        {
            Id = id; // 아이템의 고유 ID
            Name = name; // 아이템의 이름
            Description = description; // 아이템의 설명
            Price = price; // 아이템의 가격

        }
    }
}

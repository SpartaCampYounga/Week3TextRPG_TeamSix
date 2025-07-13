using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_TeamSix.Items
{
    //아이템 추상클래스 -> 각 아이템에 상속
    internal abstract class Item
    {
        public uint Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public uint Price { get; private set; }

        public Item(uint id, string name, string description, uint price)
        {

        }
    }
}

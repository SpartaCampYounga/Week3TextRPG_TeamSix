using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Interfaces;

namespace TextRPG_TeamSix.Items
{
    internal class Portion : Item, IConsumable
    {
        //public RestoreType RestoreType {get; private set;} // 마나나 스테미나 포션 종류 추가할때. RestoreType Enum 추가하여 관리
        public int RestoreAmount {  get; private set; } //체력회복량 
        public Portion(uint id, string name, string description, uint price, int restoreAmount) : base(id, name, description, price)
        {
        }

        public void Consume()
        {
        }
    }
}

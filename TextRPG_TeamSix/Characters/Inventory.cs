using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Items;

namespace TextRPG_TeamSix.Characters
{
    //인벤토리 관리
    internal class Inventory //: IContainableItems
    {
        public Player Owner { get; private set; }
        public List<Item> ItemList { get; private set; } = new List<Item>();

        public void DisplayItems()
        {
            //리스트 내부 아이템 전부 출력
            if(ItemList.Count == 0)
            {
                Console.WritelIne("인벤토리가 비어 있습니다.");
                return;
            }
            Console.WritelIne("인벤토리");
            Console.WritelIne("-------------------------------------");
            Cosnole.Wrtieline(" ID | 이름 | 능력 | 설명 |");

        }
        public Item? GetItem(uint id)
        {
            Item item = ItemList.FirstOrDefault(x => x.Id == id);
            return item;

        }
        public void PurchaseItem(uint itemId)
        {

        }
        public void SellItem(uint itemId)
        {

        }
    }
}

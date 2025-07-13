using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Interfaces;
using TextRPG_TeamSix.Items;

namespace TextRPG_TeamSix.Character
{
    //인벤토리 관리
    internal class Inventory //: IContainableItems
    {
        public List<Item> InventoryContainer { get; private set; } = new List<Item>();

        public Item? GetItem(uint id)
        {
            Item item = InventoryContainer.FirstOrDefault(x => x.Id == id);
            return item;

        }
        public void DisplayItems()
        {
            //리스트 내부 아이템 전부 출력
        }
        public void PurchaseItem(uint itemId)
        {

        }
        public void SellItem(uint itemId)
        {

        }
    }
}

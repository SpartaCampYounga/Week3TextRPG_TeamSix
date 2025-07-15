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

        public Inventory(Player owner) { 
            Owner = owner;
        }
        public void DisplayItems()
        {
            //리스트 내부 아이템 전부 출력
            if (ItemList.Count == 0)
            {
                Console.WriteLine("인벤토리가 비어 있습니다.");
                return;
            }

            Console.WriteLine("인벤토리");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine(" ID | 이름 | 능력 | 설명 |");

            for (int i = 0; i < ItemList.Count; i++)
            {
                var item = ItemList[i];
                Console.WriteLine($"({item.Id}) 번 | {item.Name} | {item.Description} | {item.Price}");
            }
        }
        public Item? GetItem(uint id)
        {
            Item item = ItemList.FirstOrDefault(x => x.Id == id);
            return item;

        }
        public void PurchaseItem(uint itemId)
        {
            Item? item = GetItem(itemId);
            if (item == null)
            {
                Console.WriteLine("해당 아이템이 존재하지 않습니다.");
                return;
            }
            // 구매 로직 추가
            // 예: 플레이어의 골드가 충분한지 확인하고, 아이템을 인벤토리에 추가
            if (Owner.Gold >= item.Price)
            {
                Owner.EarnGold(0 - item.Price);
                ItemList.Add(item);
                Console.WriteLine($"{item.Name}을(를) 구매했습니다.");
            }
            else
            {
                Console.WriteLine("골드가 부족합니다.");
            }
        }
        public void SellItem(uint itemId)
        {
            // 아이템 판매 로직
            // 아이템이 인벤토리에 있는지 확인
            // 아이템이 존재하지 않으면 메시지 출력
            // 아이템이 존재하면 플레이어의 골드를 증가시키고 인벤토리에서 제거
            Item? item = GetItem(itemId);
            if (item == null)
            {
                Console.WriteLine("해당 아이템이 인벤토리에 없습니다.");
                return;
            }
            // 판매 로직 추가
            Owner.EarnGold(0 + item.Price);
            ItemList.Remove(item);
            Console.WriteLine($"{item.Name}을 판매 했습니다.");

        }
    }
}

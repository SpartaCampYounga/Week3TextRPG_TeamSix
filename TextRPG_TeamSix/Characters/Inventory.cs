using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextRPG_TeamSix.Interfaces;
using System.Threading.Tasks;
using TextRPG_TeamSix.Items;
using System.Security.Cryptography.X509Certificates;

namespace TextRPG_TeamSix.Characters
{
   

    //인벤토리 관리
    internal class Inventory //: IContainableItems
    {
        public Player Owner { get; private set; }
        public List<Item> ItemList { get; private set; } = new List<Item>();
        // 생성자
        public Inventory(Player owner)
        {
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
            Console.WriteLine("-------------------------------------");

            for (int i = 0; i < ItemList.Count; i++)
            {
                var item = ItemList[i];
                if (item.IsEquipped)
                {
                    Console.WriteLine($"[E] ({item.Id}) 번 | {item.Name} | {item.Description} | {item.Price}");
                }
                else
                {
                    Console.WriteLine($"({item.Id}) 번 | {item.Name} | {item.Description} | {item.Price}");
                }
                
            }
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("아이템 장착.(숫자로 입력해 주세요)");

            int userInput;
            bool userChoice = int.TryParse(Console.ReadLine(), out userInput);

            if(userChoice && userInput >= 1 && userInput <= ItemList.Count)
            {
                var itemToEquip = ItemList[userInput - 1];
                if(!itemToEquip.IsEquipped)
                {
                    itemToEquip.IsEquipped = true;
                    Console.WriteLine($"{itemToEquip.Name}을(를) 장착했습니다.");
                }
                else
                {
                    itemToEquip.IsEquipped = false;
                    Console.WriteLine($"{itemToEquip.Name}을(를) 장착 해제했습니다.");
                }
            }
            
        }
        public Item? GetItem(uint id)
        {
            Item? item = ItemList.FirstOrDefault(x => x.Id == id);
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
                Owner.Gold -= item.Price;
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
            // 만약 아이템을 장착 하고 있다면 판매 불가
           
            // 판매 로직 추가
            Owner.Gold += item.Price;
            ItemList.Remove(item);
            Console.WriteLine($"{item.Name}을(를) 판매했습니다. 현재 골드: {Owner.Gold}");
        }
        //나아이스
    }
}

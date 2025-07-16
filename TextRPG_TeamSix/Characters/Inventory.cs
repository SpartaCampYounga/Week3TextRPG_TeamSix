using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Skills;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Scenes;
using System.Formats.Asn1;
namespace TextRPG_TeamSix.Characters
{
    //인벤토리 관리
    internal class Inventory //: IContainableItems
    {
        [JsonIgnore]
        
        public Player Owner { get; private set; }
        public List<Item> ItemList { get; private set; } = new List<Item>();

        public Inventory(Player owner) 
        { 
            Owner = owner;
        }
        public void DisplayItems()
        {
            //리스트 내부 아이템 전부 출력
            int indexPage = 0;
            int itemsPerPage = 5; // 한 페이지에 표시할 아이템 수

            while (true)
            {
                Console.Clear();

                Console.WriteLine("인벤토리");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine(" ID | 이름 | 능력 | 설명 |");
                Console.WriteLine("-------------------------------------");

                if (ItemList.Count == 0)
                {
                    Console.WriteLine("인벤토리가 비어 있습니다.");
                    return;
                }

                int startIndex = indexPage * itemsPerPage;
                int endIndex = startIndex + itemsPerPage;

                if(endIndex > ItemList.Count)
                {
                    endIndex = ItemList.Count;
                }
                for(int i = startIndex; i < endIndex; i++)
                {
                    Item item = ItemList[i];
                    string equippedStatus = item.IsEquipped? "[E]" : "[ ]"; // 아이템이 장착되었는지 여부 표시
                    Console.WriteLine($"{item.Id}. {equippedStatus} | {item.Name} | {item.Description} | {item.Price}");

                }
                Console.WriteLine("");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("페이지 네비게이션: <= 이전 페이지 || 다음 페이지 => , [Enter] 종료");
                Console.Write("명령어 입력: ");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    indexPage++;
                    if (indexPage * itemsPerPage >= ItemList.Count)
                    {
                        indexPage = 0; // 마지막 페이지에서 다음 페이지로 넘어가면 첫 페이지로 돌아가게
                    }
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    indexPage--;
                    if (indexPage < 0)
                    {
                        indexPage = (ItemList.Count - 1) / itemsPerPage; // 첫 페이지에서 이전 페이지로 넘어가면 마지막 페이지로 이동
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break; // 종료
                }


            }


        }
        public void EquipItem()
        {
            Console.WriteLine("장착할 아이템의 ID를 입력하세요 :");

            int itemId;
            bool isValid = int.TryParse(Console.ReadLine(), out itemId);

            if (!isValid)
            {
                Console.WriteLine("숫자를 올바르게 입력해주세요.");
                return;
            }

            Item? item = GetItem((uint)itemId);
            if (item == null)
            {
                Console.WriteLine("해당 아이템이 존재하지 않습니다.");
                return;
            }
            if (item.IsEquipped)
            {
                item.IsEquipped = false; // 아이템이 장착되어 있으면 장착 해제
                Console.WriteLine($"{item.Name}은(는) 이미 장착되어 있습니다.");
                return;
            }

            //foreach (var otherItem in ItemList)
            //{
            //    if(otherItem.IsEquipped && otherItem.GetType == item.Type)
            //    {
            //        otherItem.IsEquipped = false; // 같은 타입의 아이템이 장착되어 있으면 장착 해제
            //        Console.WriteLine($"{otherItem.Name}은(는) 장착 해제되었습니다.");
            //    }
            //}
            item.IsEquipped = true; // 아이템 장착
            Console.WriteLine($"{item.Name}을(를) 장착했습니다.");

        }
        public Item? GetItem(uint id)
        {
            Item? item = ItemList.FirstOrDefault(x => x.Id == id);
            return item;

        }
        public void PurchaseItem(uint itemId)
        {
            // 구매 bool 체크는 상점 SellToPlayer로 이동
            // 예: 플레이어의 골드가 충분한지 확인하고, 아이템을 인벤토리에 추가
            Item? item = GetItem(itemId);
            if (item == null)
            {
                Console.WriteLine("해당 아이템이 존재하지 않습니다.");
                return;
            }
            
            Owner.EarnGold(0 - item.Price);
            ItemList.Add(item);
            Console.WriteLine($"{item.Name}을(를) 구매했습니다.");
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
        public void SetOwnerAgain(Player player)    //SaveManager에서만 호출될것!
        {
            Owner = player;
        }

        public void Clone(Inventory inventory)
        {
            Owner = inventory.Owner;
            foreach(Item item in inventory.ItemList)
            {
                ItemList.Add(GameDataManager.Instance.AllItems.FirstOrDefault(x => x.Id == item.Id));
            }
        }
    }
}

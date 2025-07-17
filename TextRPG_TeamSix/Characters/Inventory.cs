using Newtonsoft.Json;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Items;
namespace TextRPG_TeamSix.Characters
{
    //인벤토리 관리
    internal class Inventory //: IContainableItems
    {
        [JsonIgnore]
        public Player Owner { get; private set; }

        public Character Character { get; private set; }
        public Weapon InvenWeapon { get; private set; }
        public List<Item> ItemList { get; set; } = new List<Item>();

        
        public Inventory(Player owner)
        {
            Owner = owner;
        }
       public void DisplayItems()
{
         int indexPage = 0;
        int itemsPerPage = 5;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║               인벤토리               ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("-------------------------------------");
            Console.WriteLine(" ID | [E] | 이름 | 설명 | 가격 ");
            Console.WriteLine("-------------------------------------");

            if (ItemList.Count == 0)
            {
                Console.WriteLine("인벤토리가 비어 있습니다.");
            }
            else
            {
                // 페이지별 출력
                int startIndex = indexPage * itemsPerPage;
                int endIndex = Math.Min(startIndex + itemsPerPage, ItemList.Count);
    
                for (int i = startIndex; i < endIndex; i++)
                {
                    Item item = ItemList[i];
                    string equippedStatus = item.IsEquipped ? "[E]" : " ";
                    Console.WriteLine($"{item.Id} | {equippedStatus} | {item.Name} | {item.Description} | {item.Price}");
                }

                Console.WriteLine("-------------------------------------");
                Console.WriteLine("[<=] 이전 페이지 |  다음 페이지 [=>] | [숫자 입력]  장착 | [Enter]  나가기");
            }
    
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
    
            if (keyInfo.Key == ConsoleKey.RightArrow)
            {
                indexPage = (indexPage + 1) % ((ItemList.Count + itemsPerPage - 1) / itemsPerPage);
            }
            else if (keyInfo.Key == ConsoleKey.LeftArrow)
            {
                indexPage--;
                if (indexPage < 0)
                {
                    indexPage = (ItemList.Count - 1) / itemsPerPage;
                }
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                SceneManager.Instance.SetScene(SceneType.Main);
                return;
            }
            else
            {
                // 숫자 키 입력 처리
                Console.Write("장착할 아이템 ID 입력: ");
                string input = Console.ReadLine();
            if (uint.TryParse(input, out uint itemId))
            {
                EquipItem(itemId);
                Console.WriteLine("아무 키나 누르면 계속...");
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 아무 키나 누르세요...");
                Console.ReadKey(true);
            }
        }
    }
}

        public void EquipItem(uint itemId)
        {
            Item? item = GetItem((uint)itemId);
            if (item == null && item.Type == Item.ItemType.Accessory)
            {
                Console.WriteLine("해당 아이템은 사용 할 수 없습니다.");
                return;
            }
            if (item.IsEquipped)
            {
                item.IsEquipped = false; // 아이템이 장착되어 있으면 장착 해제
                Console.WriteLine($"{item.Name} 해제되었습니다.");
                return;
            }
            foreach (var otherItem in ItemList)
            {
                if (otherItem.IsEquipped && otherItem.Type == item.Type)
                {
                    otherItem.IsEquipped = false; // 같은 타입의 아이템이 장착되어 있으면 장착 해제
                    Console.WriteLine($"{otherItem.Name}은(는) 장착 해제되었습니다.");
                    if (otherItem.Type == Item.ItemType.Weapon)
                    {
                        InvenWeapon = null; // 무기 장착 해제
                    }
                }
            }
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
            Item? item = GameDataManager.Instance.AllItems.FirstOrDefault(x => x.Id == itemId);
            if (item == null)
            {
                Console.WriteLine("해당 아이템이 존재하지 않습니다.");
                return;
            }

            if (Owner.Gold < item.Price)
            {
                Console.WriteLine("골드가 부족합니다.");
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
            this.ItemList = new List<Item>();
            foreach (Item item in inventory.ItemList)
            {
                Item clonedItem = item.CreateInstance();    //빈 객체 생성
                clonedItem.Clone(item);                     //깊은 복사
                ItemList.Add(clonedItem);
            }
        }
    }
}

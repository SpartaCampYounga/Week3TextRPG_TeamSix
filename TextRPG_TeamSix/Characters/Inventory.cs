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

        public List<Item> ItemList { get; private set; } = new List<Item>();
        public Dictionary<EquipSlot, Item> EquipmentList { get; private set; }

        public Inventory(Player owner)
        {
            Owner = owner;
        }
        [JsonConstructor]
        public Inventory(List<Item> itemList)
        {
            ItemList = itemList ?? new List<Item>();
        }
        //public void DisplayItems()    
        //영아: 주석처리하고 새로 만들었습니다.
        //인벤토리 내용 출력은 DisplayItems()로 분리하여 구현하는 것이 좋은 방향인 것 같습니다만
        //Inventory Scene 진입 이후 모든 출력과 입력을 담당하는 것은 과한 면이 있습니다.
        //특히나 Inventory DisplayItems()를 오로지 아이템 출력만 담당하게 만든다면, 추후 상점에서 내 아이템 판매할 때 재사용을 할 수도 있을 것입니다.
        //지금은 TextDisplayer.PageNavigation 기능을 사용하여 필요 없어져, 다시 만들진 않았습니다만, 만약 구현하게 된다면 for문을 이용해, 테이블 헤더를 제외한 아이템들 리스트만 표시했을 것 같네요.
        //{
        //    int indexPage = 0;
        //    int itemsPerPage = 5;

        //    while (true)
        //    {
        //        Console.Clear();
        //        Console.ForegroundColor = ConsoleColor.Green;
        //        Console.WriteLine("╔══════════════════════════════════════╗");
        //        Console.WriteLine("║               인벤토리               ║");
        //        Console.WriteLine("╚══════════════════════════════════════╝");
        //        Console.WriteLine();
        //        Console.WriteLine("-------------------------------------");
        //        Console.ForegroundColor = ConsoleColor.White;
        //        Console.WriteLine(" ID | [E] | 이름 | 설명 | 가격 ");
        //        Console.ForegroundColor = ConsoleColor.Green;
        //        Console.WriteLine("-------------------------------------");
        //        Console.ResetColor();
        //        if (ItemList.Count == 0)
        //        {
        //            Console.ForegroundColor = ConsoleColor.Red;
        //            Console.WriteLine("인벤토리가 비어 있습니다.");
        //            Console.ResetColor();
        //        }
        //        else
        //        {
        //            // 페이지별 출력
        //            int startIndex = indexPage * itemsPerPage;
        //            int endIndex = Math.Min(startIndex + itemsPerPage, ItemList.Count);

        //            for (int i = startIndex; i < endIndex; i++)
        //            {
        //                Item item = ItemList[i];
        //                string equippedStatus = item.IsEquipped ? "[E]" : " ";
        //                Console.WriteLine($"{item.Id} | {equippedStatus} | {item.Name} | {item.Description} | {item.Price}");
        //            }

        //                Console.ForegroundColor = ConsoleColor.Green;
        //                Console.WriteLine("-------------------------------------");
        //                Console.ResetColor();
        //                Console.WriteLine();
        //                Console.WriteLine("[<=] 이전 페이지 |  다음 페이지 [=>] ");
        //                Console.WriteLine("[ID 입력] 아이템 장착/사용 | [0] 나가기");
        //                Console.WriteLine();
        //            }



        //        Console.Write("장착할 아이템 ID 입력: ");

        //        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        //        if (keyInfo.Key == ConsoleKey.RightArrow)
        //        {
        //            indexPage = (indexPage + 1) % ((ItemList.Count + itemsPerPage - 1) / itemsPerPage);
        //        }
        //        else if (keyInfo.Key == ConsoleKey.LeftArrow)
        //        {
        //            indexPage--;
        //            if (indexPage < 0)
        //            {
        //                indexPage = (ItemList.Count - 1) / itemsPerPage;
        //            }
        //        }
        //        else if (keyInfo.Key == ConsoleKey.NumPad0)
        //        {
        //            SceneManager.Instance.SetScene(SceneType.Player);
        //            return;
        //        }

        //            if ((keyInfo.Key >= ConsoleKey.D1 && keyInfo.Key <= ConsoleKey.D9) ||
        //                (keyInfo.Key >= ConsoleKey.NumPad1 && keyInfo.Key <= ConsoleKey.NumPad9))
        //            {
        //                Console.Write(keyInfo.KeyChar);
        //                string input = keyInfo.KeyChar.ToString() + Console.ReadLine();
        //                if (uint.TryParse(input, out uint itemId))
        //                {
        //                    Item? item = GetItem(itemId);
        //                    if (item != null)
        //                    {
        //                        EquipItem(itemId);
        //                    }
        //                    else
        //                    {
        //                        Console.WriteLine("해당 ID의 아이템이 존재하지 않습니다.");
        //                    }
        //                }
        //                else
        //                {
        //                    Console.WriteLine("숫자를 입력해주세요.");

        //                }
        //                Console.WriteLine("아무 키나 누르면 계속...");
        //                Console.ReadKey(true); // 아무 키나 누를 때까지 대기
        //            }
        //            // 숫자 키 입력 처리



        //        }
        //}
        public void EquipItem(uint itemId)
        {
            Item? item = GetItem((uint)itemId); //인벤토리에서 확인함

            switch (item)    //패턴매칭 및 캐스팅 사용 가시성을 위해 if에서 switch문으로 변경하였습니다.
            {
                case null:                
                    //null일경우 인벤토리에서 아이템을 찾지 못한 것이므로 소지하지 않고 있다는 내용으로 변경함
                    Console.WriteLine("해당 아이템을 소지하고 있지 않습니다.");
                    //Console.WriteLine("해당 아이템은 사용 할 수 없습니다.");
                    break;
                case IConsumable portion:
                    Console.WriteLine("회복물약은 던전에서 사용해 주세요.");
                    break;
                case EquipItem equipment:
                    if (PlayerManager.Instance.EquipmentList.ContainsKey(equipment.EquipSlot))
                    {
                        EquipItem currentlyEquipped = PlayerManager.Instance.EquipmentList[equipment.EquipSlot];

                        if (PlayerManager.Instance.EquipmentList[equipment.EquipSlot].Id == item.Id)
                        {
                            //장착된 아이템을 재선택함.
                            //이미 딕셔너리에 같은 키가 저장되어있으므로, 해제. (딕셔너리에서 지우기)
                            PlayerManager.Instance.EquipmentList.Remove(equipment.EquipSlot);
                            Console.WriteLine($"{item.Name} 장착을 해제했습니다.");
                        }
                        else
                        {
                            //장착된 템과 다른 아이템을 선택함.
                            //이미 딕셔너리에 같은 키가 저장되어있으므로, 기존 장비 해제 먼저. (딕셔너리에서 지우기)
                            PlayerManager.Instance.EquipmentList.Remove(equipment.EquipSlot);
                            Console.WriteLine($"{currentlyEquipped.Name} 장착을 해제했습니다.");
                            //장착해야하는 아이템 더하기.
                            PlayerManager.Instance.EquipmentList.Add(equipment.EquipSlot, equipment);
                            Console.WriteLine($"{item.Name} 을 장착했습니다.");
                        }
                    }
                    else
                    {
                        //딕셔너리에 같은 키 없으므로, 장착. (딕셔너리에서 더하기)
                        PlayerManager.Instance.EquipmentList.Add(equipment.EquipSlot, equipment);
                        Console.WriteLine($"{item.Name} 을 장착했습니다.");
                    }
                    break;
            }

            //Item? item = GetItem((uint)itemId);
            //if (item == null)
            //{
            //    Console.WriteLine("해당 아이템은 사용 할 수 없습니다.");
            //    return;
            //}
            //if (item.Type == Item.ItemType.Consumable)
            //{
            //    Console.WriteLine("회복물약은 던전에서 사용해 주세요.");
            //    return;
            //}
            //if (item.IsEquipped)
            //{
            //    item.IsEquipped = false; // 아이템이 장착되어 있으면 장착 해제
            //    Console.WriteLine($"{item.Name} 해제되었습니다.");
            //}
            //foreach (var otherItem in ItemList)
            //{
            //    if (otherItem.IsEquipped && otherItem.Type == item.Type)
            //    {
            //        otherItem.IsEquipped = false; // 같은 타입의 아이템이 장착되어 있으면 장착 해제
            //        Console.WriteLine($"{otherItem.Name}은(는) 장착 해제되었습니다.");
            //        if (otherItem.Type == Item.ItemType.Weapon)
            //        {
            //            //InvenWeapon = null; // 무기 장착 해제
            //        }
            //    }
            //}
            //item.IsEquipped = true; // 아이템 장착

            //Console.WriteLine($"{item.Name}을(를) 장착했습니다.");

        }

        public Item? GetItem(uint id)
        {
            Item? item = ItemList.FirstOrDefault(x => x.Id == id);
            return item;
        }
        public void AddItem(uint itemId)  //공짜 템 주려고 만듬.
        {
            Item item = GameDataManager.Instance.AllItems.FirstOrDefault(x => x.Id == itemId);
            ItemList.Add(item);
            Console.WriteLine($"{item.Name}을 획득했다!");
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

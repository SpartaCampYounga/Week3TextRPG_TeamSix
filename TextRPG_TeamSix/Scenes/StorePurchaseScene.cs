using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Skills;
using TextRPG_TeamSix.Stores;
using TextRPG_TeamSix.Utilities;


namespace TextRPG_TeamSix.Scenes
{
    internal class StorePurchaseScene : SceneBase
    {
        public override SceneType SceneType => SceneType.StorePurchase;

        Player player;

        private Store currentStore;
                    
        bool isSpecial = false;

        int input;

        private List<Item> availableItems;
        
        public void SetStoreSpectial(bool isSpecial)  //여기서 스토어 타입 변경함.
        {
            this.isSpecial = isSpecial;
        }
        public override void DisplayScene() //출력 하는 시스템
        {
            player = PlayerManager.Instance.CurrentPlayer;
            if (!this.isSpecial) 
            { currentStore = new Store(StoreType.Normal, GameDataManager.Instance.AllItems.Where(x => x.IsSpecialItem == false).ToList()); }
            else 
            { currentStore = new Store(StoreType.Special, GameDataManager.Instance.AllItems.Where(x => x.IsSpecialItem == true).ToList()); }

            availableItems = currentStore.ItemList.Where(x => player.Inventory.ItemList.All(i => i.Id != x.Id)).ToList();

            Console.Clear();
            //Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║               상   점                ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.WriteLine($"보유 골드: {player.Gold} G");

            //아이템
            if (availableItems.Count == 0)
            {
                Console.WriteLine("판매 준비중입니다... 다음에 다시 오세요.");
                Console.WriteLine("아니면 던전 안에 출장 가 있을 수도?");
                input = -2;
            }
            else
            {
                //테이블 헤더
                Console.ForegroundColor = ConsoleColor.White;
                string header = "      ";
                header += FormatUtility.AlignWithPadding("이름", 15) + " | ";
                header += FormatUtility.AlignWithPadding("설명", 50) + " | ";
                header += FormatUtility.AlignWithPadding("금액" + " G", 8);
                Console.WriteLine(header);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('-', Console.WindowWidth));
                Console.ResetColor();

                input = TextDisplayer.SelectNavigation(availableItems);
            }
            HandleInput();

            //Console.WriteLine("1. 아이템 구매 ");
            //Console.WriteLine("2. 아이템 판매 ");
            //Console.WriteLine("0. 상점 나가기 ");

            //string menuInput = Console.ReadLine();

            //switch (menuInput)
            //{
            //    case "1":
            //        currentMode = StoreMode.Buy;
            //        Console.WriteLine("아이템 구매 모드로 전환되었습니다.");//debug용
            //        LoadBuyItems();
            //        break;
            //    case "2":
            //        currentMode = StoreMode.Sell;
            //        Console.WriteLine("아이템 판매 모드로 전환되었습니다."); //debug용
            //        LoadSellItems();
            //        break;
            //    case "0":
            //        SceneManager.Instance.SetScene(SceneType.Main);
            //        return;
            //    default:
            //        Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요."); //debug용
            //        Console.ReadLine();
            //        SceneManager.Instance.SetScene(SceneType.Store); 
            //        return;
            //}
            //input = TextDisplayer.PageNavigation(currentStore.ItemList);

            //HandleInput();
        }
        private void LoadBuyItems() // 상점에서 아이템 구매 모드로 전환
        {
            currentStore.ItemList.Clear();
            foreach (var item in GameDataManager.Instance.AllItems)
            {
                if (!item.IsSpecialItem)
                {
                    currentStore.ItemList.Add(item);
                }
            }
        }

        private void LoadSellItems() // 상점에서 아이템 판매 모드로 전환
        {
            currentStore.ItemList.Clear();
            foreach (var item in player.Inventory.ItemList)
            {
                currentStore.ItemList.Add(item);
            }
        }

        public override void HandleInput() //입력 받고 실행하는 시스템
        {
            switch(input)
            {
                case -1:
                case -2:
                    SceneManager.Instance.SetScene(SceneType.Store);
                    break;
                default:
                    //구매 로직
                    Item selectedItem = availableItems[input];
                    if (currentStore.SellToPlayer(selectedItem))
                    {
                        player.PurchaseItem(selectedItem.Id);
                        Console.WriteLine($"{selectedItem.Name}을 구매했다...");
                        InputHelper.WaitResponse();
                    }
                    else
                    {
                        Console.WriteLine("보유한 금액이 부족해서 구매하지 못했다...");
                        InputHelper.WaitResponse();
                    }

                    SceneManager.Instance.SetScene(SceneType.StorePurchase);
                    break;

            }
            //switch (input)
            //{
            //    case -1:
            //        SceneManager.Instance.SetScene(SceneType.Store);
            //        break;
            //    default:
            //        Item selectedItem = currentStore.ItemList[input];

            //        if (currentMode == StoreMode.Buy)
            //        {
            //            player.Inventory.PurchaseItem(selectedItem.Id);
            //            Console.WriteLine($"{selectedItem.Name}을 구매했습니다.");
            //        }
            //        else if (currentMode == StoreMode.Sell)
            //        {
            //            player.Inventory.SellItem(selectedItem.Id);
            //            Console.WriteLine($"{selectedItem.Name}을 판매했습니다.");
            //        }

            //        Console.WriteLine($"[보유 골드] {player.Gold} Gold");
            //        Console.ReadLine();
            //        SceneManager.Instance.SetScene(SceneType.Store);
            //        break;
            //}            
        }
    }
}

//프로그램 시작
//    ↓
//SceneManager.SetScene(SceneType.Store)
//    ↓
//StoresScene.DisplayScene()  → 아이템 목록 출력
//    ↓
//유저 입력 (ex: 1번)
//    ↓
//StoresScene.HandleInput()
//    ↓
//Player.Inventory.PurchaseItem(itemId)
//    ↓
//Inventory.PurchaseItem() 내부
//    ↓
//아이템 존재 확인 → 골드 충분 확인
//    ↓
//골드 차감 & 인벤토리에 아이템 추가
//    ↓
//구매 성공/실패 메시지 출력

//private int currentPage = 0; // 현재 페이지 (페이지네이션을 위한 변수)
//private const int ItemsPerPage = 5; // 한 페이지에 표시할 아이템 수

//private void PrintPageItems() // 현재 페이지의 아이템을 출력
//{
//    Console.Clear();
//    Console.WriteLine($"상점에 오신 것을 환영합니다.!");
//    Console.WriteLine($"페이지 {currentPage + 1} --\n");

//    var itemsToDisplay = currentStore.ItemList // 현재 페이지에 해당하는 아이템을 가져옴
//        .Skip(currentPage * ItemsPerPage) // 현재 페이지에 해당하는 아이템을 건너뜀
//        .Take(ItemsPerPage) // 현재 페이지에 해당하는 아이템 수만큼 가져옴
//        .ToList();

//    for (int i = 0; i < itemsToDisplay.Count; i++)
//    {
//        var item = itemsToDisplay[i];
//        Console.WriteLine($"{i + 1}. {item.Name} : {item.Description} (가격: {item.Price}G)");
//    }
//    Console.WriteLine("\n원하는 아이템의 번호를 입력하세요 (0: 나가기, 방향키로 페이지 이동):");
//}

//var input = Console.ReadKey(true);

//if (input.Key == ConsoleKey.RightArrow)
//{
//    if ((currentPage + 1) * ItemsPerPage < currentStore.ItemList.Count)
//        currentPage++;
//    else
//        currentPage = 0;
//}
//else if (input.Key == ConsoleKey.LeftArrow)
//{
//    currentPage--;
//    if (currentPage < 0)
//        currentPage = (currentStore.ItemList.Count - 1) / ItemsPerPage;
//}
//else if (input.Key == ConsoleKey.D0 || input.Key == ConsoleKey.NumPad0)
//{
//    SceneManager.Instance.SetScene(SceneType.Main);
//}
//var input = Console.ReadLine()?.Trim();
//if (input == null) return;

//if (int.TryParse(input, out int selectedNumber))
//{
//    // 입력 번호를 통해 아이템 선택
//    int itemIndex = currentPage * ItemsPerPage + (selectedNumber - 1);
//    if (itemIndex >= 0 && itemIndex < currentStore.ItemList.Count)
//    {
//        var selectedItem = currentStore.ItemList[itemIndex];

//        // 구매 시도 (플레이어 인벤토리에 요청)
//        Player player = PlayerManager.Instance.CurrentPlayer;
//        player.Inventory.PurchaseItem(selectedItem.Id);
//    }
//}

//public StoresScene()
//{
//    currentStore = new Store();
//}
//private void PrintItems()
//{
//    currentStore.ItemList.Clear();
//    foreach (var item in GameDataManager.Instance.AllItems)
//    {
//        currentStore.ItemList.Add(item);
//    }

//    Console.Clear();
//    Console.WriteLine("===== 상점 =====");
//    Console.WriteLine("");
//    Player player = PlayerManager.Instance.CurrentPlayer;
//    Console.WriteLine($"상점 아이템 수: {currentStore.ItemList.Count}"); //테스트로 아이템 갯수 출력넣음


//    //for (int i = 0; i < currentStore.ItemList.Count; i++)
//    //{
//    //    Item item = currentStore.ItemList[i];
//    //    bool alreadyOwned = player.Inventory.ItemList.Any(x => x.Id == item.Id); // 구매 여부 체크
//    //    Console.WriteLine($"{i + 1}. {item.Name} : {item.Description} (가격: {item.Price}G) {(alreadyOwned ? "(구매완료)" : "")}");
//    //}


//    Console.WriteLine("\n원하는 아이템의 번호를 입력하세요 (0: 나가기, 방향키로 페이지 이동):"); //페이지네비게이션은 나중에 추가예정   
//}

//player.Inventory.PurchaseItem(currentStore.ItemList[input].Id);
//Console.WriteLine(currentStore.ItemList[input].Name + "을 구매했습니다.");
//Console.ReadLine();
//SceneManager.Instance.SetScene(SceneType.Store);
//break;

//Player player = PlayerManager.Instance.CurrentPlayer;
//while (true)
//{
//    string input = Console.ReadLine(); // 사용자 입력 받기

//    if (uint.TryParse(input, out uint itemId)) // 입력이 숫자인지 확인
//    {
//        if (itemId == 0)
//        {
//            SceneManager.Instance.SetScene(SceneType.Main); // 0 입력 시 마을로 이동
//            return;
//        }
//        if (itemId > currentStore.ItemList.Count) // 입력된 아이템 ID가 상점 아이템 목록의 범위를 벗어나는지 확인
//        {
//            Console.WriteLine("잘못된 입력입니다. 목록에 있는 아이템 번호를 입력해주세요.");
//            continue;
//        }

//        itemId--; // 아이템 ID는 0부터 시작하므로 입력값에서 1을 빼줌
//        Item selectedItem = currentStore.ItemList[(int)itemId]; // 선택된 아이템 가져오기

//        if (player.Inventory.ItemList.Any(x => x.Id == selectedItem.Id)) // 아이템이 이미 인벤로이에 있는지 확인
//        {
//            Console.WriteLine("이미 해당 아이템을 보유 중입니다. 구매할 수 없습니다.");
//            continue;
//        }
//        bool result = currentStore.SellToPlayer(selectedItem);

//        if (result)
//        {
//            // 플레이어 인벤토리에 아이템 추가
//            player.Inventory.PurchaseItem(selectedItem.Id);
//            SaveManager.Instance.SaveGame();
//            SceneManager.Instance.SetScene(SceneType.Store);
//        }
//        else
//        {
//            Console.WriteLine("구매에 실패했습니다. (골드 부족 또는 기타 사유)");
//        }

//        return;
//    }
//    else
//    {
//        Console.WriteLine("잘못된 입력입니다. 숫자를 입력해주세요.");

//    }
//}
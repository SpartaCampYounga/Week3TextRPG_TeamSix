using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Stores;


namespace TextRPG_TeamSix.Scenes
{
    internal class StoresScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Store; // Enum-SceneType에 원하는 enum값 생성.

        private Store currentStore; // 현재 상점
        private int currentPage = 0; // 현재 페이지 (페이지네이션을 위한 변수)
        private const int ItemsPerPage = 5; // 한 페이지에 표시할 아이템 수
        public StoresScene(StoreType type) // 생성자: 상점의 종류를 받아 해당 상점을 초기화합니다.
        {
            currentStore = new Store(type); // 상점 타입에 따라 상점 생성
        }
        private void ShowStoreSelectMenu()
        {
            Console.Clear();
            Console.WriteLine("===== 마을 상점 =====");
            Console.WriteLine("어떤 상점에 가시겠습니까?");
            Console.WriteLine("1. 무기 상점");
            Console.WriteLine("2. 방어구 상점");
            Console.WriteLine("3. 물약 상점");
            Console.WriteLine("4. 일반 상점");
            Console.WriteLine("0. 마을로 돌아가기");
            Console.Write("> ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    currentStore = new Store(StoreType.WeaponStore);
                    break;
                case "2":
                    currentStore = new Store(StoreType.ArmorStore);
                    break;
                case "3":
                    currentStore = new Store(StoreType.PotionStore);
                    break;
                case "4":
                    currentStore = new Store(StoreType.GeneralStore);
                    break;
                case "0":
                    SceneManager.Instance.SetScene(SceneType.Main);
                    break;
            }
        }
        private void PrintPageItems() // 현재 페이지의 아이템을 출력
        {
            Console.Clear();
            Console.WriteLine($"{currentStore.Type} 상점에 오신 것을 환영합니다.!");
            Console.WriteLine($"페이지 {currentPage + 1} --\n");

            var itemsToDisplay = currentStore.ItemList // 현재 페이지에 해당하는 아이템을 가져옴
                .Skip(currentPage * ItemsPerPage) // 현재 페이지에 해당하는 아이템을 건너뜀
                .Take(ItemsPerPage) // 현재 페이지에 해당하는 아이템 수만큼 가져옴
                .ToList();

            for (int i = 0; i < itemsToDisplay.Count; i++)
            {
                var item = itemsToDisplay[i];
                Console.WriteLine($"{i + 1}. {item.Name} : {item.Description} (가격: {item.Price}G)");
            }
            Console.WriteLine("\n원하는 아이템의 번호를 입력하세요 (0: 나가기, 방향키로 페이지 이동):");
        }

        public override void DisplayScene() //출력 하는 시스템
        {
            if (currentStore == null)
            {
                ShowStoreSelectMenu();
            }
            else
                PrintPageItems();
        }

        public override void HandleInput() //입력 받고 실행하는 시스템
        {
            var input = Console.ReadLine();

            if (currentStore == null)
            {
                ShowStoreSelectMenu();
                return;
            }

            if (input == "0")
            {
                currentStore = null;
                SceneManager.Instance.SetScene(SceneType.Main);
            }
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

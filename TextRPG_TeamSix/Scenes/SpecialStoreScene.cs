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
using TextRPG_TeamSix.Stores;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Scenes
{
    internal class SpecialStoreScene : SceneBase
    {
        public override SceneType SceneType => SceneType.SpecialStore;

        Player player = PlayerManager.Instance.CurrentPlayer;
        private Store specialStore = new Store(); // 스페셜 상점 인스턴스
        int input;
        private StoreMode currentMode = StoreMode.None; // 상점 모드 (구매, 판매 등)

        public override void DisplayScene() //출력 하는 시스템
        {
            Console.Clear();
            Console.WriteLine("===== 스페셜 상점 =====");
            Console.WriteLine($"보유 골드: {player.Gold} G");
            Console.WriteLine("1. 아이템 구매 ");
            Console.WriteLine("2. 아이템 판매 ");
            Console.WriteLine("0. 상점 나가기 ");
            Console.WriteLine(">");

            string menuInput = Console.ReadLine();

            switch (menuInput)
            {
                case "1":
                    currentMode = StoreMode.Buy;
                    Console.WriteLine("아이템 구매 모드로 전환되었습니다.");
                    LoadBuyItems();
                    break;
                case "2":
                    currentMode = StoreMode.Sell;
                    Console.WriteLine("아이템 판매 모드로 전환되었습니다.");
                    LoadSellItems();
                    break;
                case "0":
                    SceneManager.Instance.SetScene(SceneType.Main);
                    return;
                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    Console.ReadLine();
                    SceneManager.Instance.SetScene(SceneType.SpecialStore);
                    return;
            }

            input = TextDisplayer.PageNavigation(specialStore.ItemList);
            HandleInput();
        }

        private void LoadBuyItems()
        {
            specialStore.ItemList.Clear();
            foreach (var item in GameDataManager.Instance.AllItems) //스페셜상점은 기존의 상점과 같은아이템을 출력하면안된는데 이기능 알아보기
            {
                specialStore.ItemList.Add(item);
            }
        }

        private void LoadSellItems()
        {
            specialStore.ItemList.Clear();
            foreach (var item in player.Inventory.ItemList)
            {
                specialStore.ItemList.Add(item);
            }
        }

        public override void HandleInput()
        {
            switch (input)
            {
                case -1:
                    SceneManager.Instance.SetScene(SceneType.SpecialStore);
                    break;
                default:
                    Item selectedItem = specialStore.ItemList[input];

                    if (currentMode == StoreMode.Buy)
                    {
                        player.Inventory.PurchaseItem(selectedItem.Id);
                        Console.WriteLine($"{selectedItem.Name}을 구매했습니다.");
                    }
                    else if (currentMode == StoreMode.Sell)
                    {
                        player.Inventory.SellItem(selectedItem.Id);
                        Console.WriteLine($"{selectedItem.Name}을 판매했습니다.");
                    }

                    Console.WriteLine($"[보유 골드] {player.Gold} Gold");
                    Console.ReadLine();
                    SceneManager.Instance.SetScene(SceneType.SpecialStore);
                    break;
            }
        }
    }
}

//스페셜상점 기존확률(0.1f) + 행운(0.0.f) 하여 등장확률로 넣기(던전이 끝나면)
//스페셜상점은 기존의 상점과 같은아이템을 출력하면안됨(아이템을 가져오되 기존데이터만 제외하고 보여주기)
//위에기능을 이용해 아이템을 추가하고 이것또한 상점에서 스페셜상점아이템은 보여지지 않게 하기
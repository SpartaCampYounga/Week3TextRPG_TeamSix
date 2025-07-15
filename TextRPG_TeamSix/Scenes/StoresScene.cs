using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Stores;
using TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;

namespace TextRPG_TeamSix.Scenes
{
    internal class StoresScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Store; // Enum-SceneType에 원하는 enum값 생성.

        private Store currentStore; // 현재 상점
        private int currentPage = 0; // 현재 페이지 (페이지네이션을 위한 변수)
        private const int ItemsPerPage = 5; // 한 페이지에 표시할 아이템 수

        public StoresScene()
        {
            currentStore = new Store(StoreType.GeneralStore); // 일반 상점 생성
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
            PrintPageItems(); // 현재 페이지의 아이템을 출력합니다.
        }

        public override void HandleInput() //입력 받고 실행하는 시스템
        {
            var input = Console.ReadLine()?.Trim();
            if (input == null) return;

            switch (input.ToUpper())
            {
                case "0":
                    Console.WriteLine("상점을 나갑니다.");
                    break;

                case "(화살표 >)":
                    if ((currentPage + 1) * ItemsPerPage < currentStore.ItemList.Count)
                    {
                        currentPage++;
                        DisplayScene();
                    }
                    else
                    {
                        Console.WriteLine("마지막 페이지입니다..");
                    }
                    break;

                case "(화살표 <)":
                    if (currentPage > 0)
                    {
                        currentPage--;
                        DisplayScene();
                    }
                    else
                    {
                        Console.WriteLine("첫 페이지입니다.");
                    }
                    break;

                //default:
                //    // 숫자 입력인 경우 아이템 구매 시도
                //    if (int.TryParse(input, out int selectedNumber))
                //    {
                //        if (selectedNumber >= 1 && selectedNumber <= ItemsPerPage)
                //        {
                //            // 현재 페이지의 아이템 중 선택된 번호에 해당하는 아이템 가져오기
                //            int itemIndex = currentPage * ItemsPerPage + (selectedNumber - 1);

                //            if (itemIndex < currentStore.ItemList.Count)
                //            {
                //                var selectedItem = currentStore.ItemList[itemIndex];

                //                // 구매 시도, 성공 여부 반환
                //                bool success = currentStore.SellToPlayer(selectedItem);
                //                if (success)
                //                {
                //                    Console.WriteLine("구매가 완료되었습니다.");
                //                }
                //                else
                //                {
                //                    Console.WriteLine("구매에 실패했습니다.");
                //                }
                //            }
                //            else
                //            {
                //                Console.WriteLine("잘못된 아이템 번호입니다.");
                //            }
                //        }
                //        else
                //        {
                //            Console.WriteLine("입력한 번호가 범위를 벗어났습니다.");
                //        }
                //    }
                //    else
                //    {
                //        Console.WriteLine("잘못된 입력입니다.");
                //    }
                //    break;
            }
        }
    }
}


//1.Store 클래스(상점 로직 담당)
//아이템 목록 관리
//구매, 판매 기능 처리 (예: SellToPlayer 메서드)
//실제 데이터와 계산 담당
//2. StoresScene 클래스 (UI 및 사용자 입력 담당)
//화면에 아이템 리스트를 출력(페이지네이션 포함 가능)
//사용자의 입력을 받아 구매, 판매, 나가기 등의 명령 처리 요청
//단, 실제 구매/판매 로직은 Store 클래스에 위임

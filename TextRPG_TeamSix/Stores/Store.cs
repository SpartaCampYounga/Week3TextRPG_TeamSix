using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Enums;

namespace TextRPG_TeamSix.Stores
    {
        //    // 상점 종류 바꿀께 있는지 확인 필요
        internal class Store // 상점 클래스
        {
            public StoreType Type { get; private set; } // 상점의 종류
            public List<Item> ItemList { get; private set; } // 상점에서 판매하는 아이템 리스트
            public Store(StoreType type) // 생성자: 상점의 종류를 초기화하고 아이템 리스트를 생성합니다.
            {
                Type = type;
                ItemList = new List<Item>();
            }

            public void PrintItemList() // 상점의 아이템 리스트를 출력하는 메서드
            {
                Console.WriteLine($"{Type}의 아이템 목록:");
                foreach (var item in ItemList)
                {
                    Console.WriteLine($"- {item.Name}: {item.Description} (가격: {item.Price}G)");
                }
            }
        }

        //public bool SellToPlayer(Item item)
        //{
        //    Player player = PlayerManager.Instance.CurrentPlayer;
        //    if (player.Gold >= item.Price)
        //    {
        //        player.Gold -= item.Price;
        //        player.Inventory.AddItem(item);
        //        Console.WriteLine($"{player.Name}이(가) {item.Name}을(를) {item.Price}G에 구매했습니다.");
        //        return true; // 판매 성공
        //    }
        //    else
        //    {
        //        Console.WriteLine($"{player.Name}의 금액이 부족하여 {item.Name}을(를) 구매할 수 없습니다.");
        //        return false; // 판매 실패
        //    }
        //}
        //상점 관리 //아이템 리스트 따로 뺄까... 고민중

    }

    //player.Gold 확인 필요
    //player.Inventory.AddItem(item) 확인 필요
    //제대로 된 변수가 맞는지 확인 필요
    //플레이어 플레이어(매개변수) 커런트 플레이어로 변경


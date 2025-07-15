using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Scenes;

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
            //TestItems(); // 테스트용 아이템 추가
        }
        //private void TestItems()
        //{
        //    for (int i = 1; i <= 20; i++)
        //    {
        //        //ItemList.Add(new Item((uint)i, $"아이템{i}", $"설명{i}", (uint)(i * 10)));
        //    }
        //}

        public List<Item> GetPageItems(int currentPage, int itemsPerPage)
        {
            return ItemList
                .Skip(currentPage * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();
        }
        public bool HasNextPage(int currentPage, int itemsPerPage)
        {
            return (currentPage + 1) * itemsPerPage < ItemList.Count;
        }
        public bool TryGetItemAtPageIndex(int currentPage, int itemsPerPage, int indexInPage, out Item item)
        {
            item = null!;
            var items = GetPageItems(currentPage, itemsPerPage);

            if (indexInPage >= 0 && indexInPage < items.Count)
            {
                item = items[indexInPage];
                return true;
            }
            return false;
        }

        //public bool SellToPlayer(Item item)
        //{
        //    Player player = PlayerManager.Instance.CurrentPlayer;
        //    if (player.Gold >= item.Price)
        //    {
        //        player.EarnGold(- (int)item.Price);
        //        player.Inventory.AddItem(item);
        //        Console.WriteLine($"{player.Name}이(가) {item.Name}을(를) {item.Price}G에 구매했습니다.");
        //        return true; // 구매 성공
        //    }
        //    else
        //    {
        //        Console.WriteLine($"{player.Name}의 금액이 부족하여 {item.Name}을(를) 구매할 수 없습니다.");
        //        return false; // 구매 실패
        //    }
        //}
    }
}
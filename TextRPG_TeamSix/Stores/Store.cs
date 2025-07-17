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
    internal class Store // 상점 클래스
    {
        public List<Item> ItemList { get; private set; } // 상점에서 판매하는 아이템 리스트
        public Store() // 
        {
            ItemList = new List<Item>();
            // GameDataManager에 있는 모든 아이템을 상점에서 판매
            foreach (var item in GameDataManager.Instance.AllItems) // GameDataManager에서 모든 아이템을 가져와 상점의 아이템 리스트에 추가
            {
                ItemList.Add(item);
            }

        }
        public bool SellToPlayer(Item item) // 플레이어에게 아이템을 판매하는 메서드
        {
            Player player = PlayerManager.Instance.CurrentPlayer;

            if (player.Gold < item.Price)
                return false;
            {
                if(item is IConsumable) // 아이템이 소비성 아이템인지 확인
                {
                    return true; // 소비성 아이템은 구매 가능
                }
            }

            // 플레이어가 아이템을 구매할 충분한 골드가 있는지 판단
            // 이미 보유한 아이템 체크 =>IConsumable 어케함...?
            if (player.Gold >= item.Price && player.Inventory.ItemList.FirstOrDefault(x => x.Id == item.Id) == null) 
            {
                // 구매 가능
                return true;
            }
            else
            {
                // 구매 불가능
                return false;
            }
        }
    }
}



//TestItems(); // 테스트용 아이템 추가
//private void TestItems()
//{
//    for (int i = 1; i <= 20; i++)
//    {
//        //ItemList.Add(new Item((uint)i, $"아이템{i}", $"설명{i}", (uint)(i * 10)));
//    }
//}

//public List<Item> GetPageItems(int currentPage, int itemsPerPage) // 현재 페이지와 아이템 당 페이지 수를 받아 해당 페이지의 아이템 리스트를 반환
//{
//    return ItemList // 아이템 리스트를 페이지 단위로 나누어 반환
//        .Skip(currentPage * itemsPerPage) // 현재 페이지에 해당하는 아이템을 건너뜀
//        .Take(itemsPerPage) // 아이템 당 페이지 수만큼 아이템을 가져옴
//        .ToList();
//}
//public bool HasNextPage(int currentPage, int itemsPerPage) // 현재 페이지와 아이템 당 페이지 수를 받아 다음 페이지가 있는지 확인
//{
//    return (currentPage + 1) * itemsPerPage < ItemList.Count;
//}
//public bool TryGetItemAtPageIndex(int currentPage, int itemsPerPage, int indexInPage, out Item item) // 현재 페이지, 아이템 당 페이지 수, 페이지 내 인덱스를 받아 해당 인덱스의 아이템을 반환
//{
//    item = null!;
//    var items = GetPageItems(currentPage, itemsPerPage);

//    if (indexInPage >= 0 && indexInPage < items.Count)
//    {
//        item = items[indexInPage];
//        return true;
//    }
//    return false;
//}


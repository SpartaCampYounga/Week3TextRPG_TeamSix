using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Scenes;

namespace TextRPG_TeamSix.Stores
{
    internal class SpecialStore // 스페셜 상점 클래스
    {
        public List<Item> ItemList { get; private set; } // 스페셜 상점 아이템 리스트

        public SpecialStore()
        {
            ItemList = new List<Item>();

            // 일단 일반 아이템 전체 추가 (테스트용)
            foreach (var item in GameDataManager.Instance.AllItems)
            {
                ItemList.Add(item);
            }

            // 추후 조건 추가: 예) if (item.IsSpecial) { ItemList.Add(item); }
        }

        public bool SellToPlayer(Item item)
        {
            Player player = PlayerManager.Instance.CurrentPlayer;

            if (player.Gold < item.Price)
                return false;

            if (item.GetType().Name == "Portion")
            {
                return true;
            }

            if (player.Gold >= item.Price && player.Inventory.ItemList.FirstOrDefault(x => x.Id == item.Id) == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
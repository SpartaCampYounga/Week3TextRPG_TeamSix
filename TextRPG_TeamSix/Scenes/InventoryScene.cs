using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Stores;

namespace TextRPG_TeamSix.Scenes
{
    //모든 씬이 상속 받는 추상 클래스
    internal class InventoryScene : SceneBase
    {
        Player player = PlayerManager.Instance.CurrentPlayer;
        public override SceneType SceneType => SceneType.Inventory;

        public override void DisplayScene()
        {
            Console.Clear();

            Console.WriteLine("인벤토리");

            player.Inventory.PurchaseItem(1);   //Debug용
            Console.WriteLine($"인벤토리 방문 서비스로 {player.Name}이 아이템을 획득했다!");    //Debug용

            //foreach(Item item in player.Inventory.ItemList) //Debug용
            //{
            //    Console.WriteLine(item.Name);
            //}

            player.Inventory.DisplayItems();

        }    //출력 하는 시스템

        public override void HandleInput() 
        {
            //player.Inventory.SellItem(0);         //??
            //player.Inventory.PurchaseItem(0);     //??
        } //입력 받고 실행하는 시스템

    }
}

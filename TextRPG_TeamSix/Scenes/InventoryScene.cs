using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
    //모든 씬이 상속 받는 추상 클래스
    internal class InventoryScene : SceneBase
    {
        Player player = PlayerManager.Instance.CurrentPlayer;
        public override SceneType SceneType => SceneType.Inventory;
        int input;

        public override void DisplayScene()
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8; 
            Console.WriteLine("InventoryScene");

            //player.Inventory.PurchaseItem(1);   //Debug용
            //Console.WriteLine($"인벤토리 방문 서비스로 {player.Name}이 아이템을 획득했다!");    //Debug용

            //foreach (Item item in GameDataManager.Instance.AllItems) //Debug용
            //{
            //    player.Inventory.PurchaseItem(item.Id);
            //    Console.WriteLine(item.Name+"을 강매당했다!");
            //}
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║               인벤토리               ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            //Console.WriteLine();
            //Console.WriteLine("-------------------------------------");
            
            //아이템
            if (player.Inventory.ItemList.Count == 0)
            {
                Console.WriteLine("그지여? 암것도 없네...");
                Console.WriteLine();
                InputHelper.WaitResponse();
                Console.WriteLine("옛다, 이거라도 받아라.");
                player.Inventory.AddItem(1);
                player.Inventory.AddItem(5);
                InputHelper.WaitResponse();
                input = -2;
            } else
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

                input = TextDisplayer.PageNavigation(player.Inventory.ItemList);
            }
            HandleInput();
        }

        public override void HandleInput() 
        {
            switch (input)
            {
                case -2:
                    SceneManager.Instance.SetScene(SceneType.Inventory);
                    break;
                case -1:
                    SceneManager.Instance.SetScene(SceneType.Player);
                    break;
                default:
                    Item selectedItem = player.Inventory.ItemList[input];

                    if (selectedItem is Portion portion)
                    {
                        Console.WriteLine($"{selectedItem.Name}은 여기서 사용할 수 없다.");
                        InputHelper.WaitResponse();
                    }
                    else //if (selectedItem is Weapon weapon || selectedItem is Armor armor) //장비 아이템일 경우
                    {
                        player.Inventory.EquipItem(selectedItem.Id);
                        InputHelper.WaitResponse();
                    }
                    SceneManager.Instance.SetScene(SceneType.Inventory);
                    break;
            }
        } //입력 받고 실행하는 시스템

    }
}

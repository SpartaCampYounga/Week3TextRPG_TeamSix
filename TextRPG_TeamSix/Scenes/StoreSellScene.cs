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
    internal class StoreSellScene : SceneBase
    {
        public override SceneType SceneType => SceneType.StoreSell;

        Player player;

        int input;

        public override void DisplayScene() //출력 하는 시스템
        {
            player = PlayerManager.Instance.CurrentPlayer;


            //Console.Clear();
            ////Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine("╔══════════════════════════════════════╗");
            //Console.WriteLine("║               상   점                ║");
            //Console.WriteLine("╚══════════════════════════════════════╝");
            //Console.WriteLine("상점 판매 - 물건을 80%의 가격으로 판매할 수 있습니다.");

            FormatUtility.DisplayHeader("상점에 물건의 80% 가격으로 판매할 수 있습니다.");
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(FormatUtility.AlignCenterWithPadding($"보유 골드: {player.Gold} G", Console.WindowWidth - 6));
            Console.ResetColor();
            Console.WriteLine();

            //아이템
            if (player.Inventory.ItemList.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("너.. 팔게 하나도 없는데...?");
                Console.WriteLine("좀... 불쌍할지도..?");
                Console.ResetColor();
                InputHelper.WaitResponse();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("옛다, 이거라도 받아라.");
                Console.ResetColor();

                player.Inventory.AddItem(1);
                player.Inventory.AddItem(5);
                InputHelper.WaitResponse();
                input = -2;
            }
            else
            {
                Console.WriteLine(new string('═', Console.WindowWidth));
                //테이블 헤더
                Console.ForegroundColor = ConsoleColor.White;
                string header = "      ";
                header += FormatUtility.AlignLeftWithPadding("이름", 15) + " | ";
                header += FormatUtility.AlignLeftWithPadding("설명", 50) + " | ";
                header += FormatUtility.AlignLeftWithPadding("효과", 20) + " ┊ ";
                header += FormatUtility.AlignLeftWithPadding("금액" + " G", 8);
                Console.WriteLine(header);
                Console.WriteLine(new string('═', Console.WindowWidth));

                input = TextDisplayer.SelectNavigation(player.Inventory.ItemList);
            }
            HandleInput();
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
                    //판매
                    player.SellItem(player.Inventory.ItemList[input].Id);
                    Console.WriteLine("판매완료! 다음 번에도 좋은 거래 부탁한다구~");
                    InputHelper.WaitResponse();

                    SceneManager.Instance.SetScene(SceneType.StoreSell);
                    break;
            }
        }
    }
}
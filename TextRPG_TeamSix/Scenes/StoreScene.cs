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
    internal class StoreScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Store;

        Player player = PlayerManager.Instance.CurrentPlayer;

        int input;

        
        public override void DisplayScene() //출력 하는 시스템
        {

            //Console.Clear();
            ////Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine("╔══════════════════════════════════════╗");
            //Console.WriteLine("║               상   점                ║");
            //Console.WriteLine("╚══════════════════════════════════════╝");

            FormatUtility.DisplayHeader("상점에 오신 것을 환영합니다!");

            Console.WriteLine($"보유 골드: {player.Gold} G");

            Console.WriteLine();
            Console.WriteLine();

            List<string> selections = new List<string>()
                {
                    "구매하기",  //0
                    "판매하기",  //1
                };

            input = TextDisplayer.SelectNavigation(selections);
            HandleInput();
        }
        public override void HandleInput() //입력 받고 실행하는 시스템
        {
            switch(input)
            {
                case -1:
                    SceneManager.Instance.SetScene(SceneType.Main);
                    break;
                case 0:
                    SceneManager.Instance.SetScene(SceneType.StorePurchase);
                    break;
                case 1:
                    SceneManager.Instance.SetScene(SceneType.StoreSell);
                    break;
            }
        }
    }
}
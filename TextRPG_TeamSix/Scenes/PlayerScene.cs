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
using TextRPG_TeamSix.Quests;
using TextRPG_TeamSix.Utilities;
using TextRPG_TeamSix.Game;

namespace TextRPG_TeamSix.Scenes
{

    //모든 씬이 상속 받는 추상 클래스
    internal class PlayerScene : SceneBase
    {
        Player player = PlayerManager.Instance.CurrentPlayer;
        public override SceneType SceneType => SceneType.Player;

        private List<Item> items = new List<Item>();
        private List<Quest> Quests = new List<Quest>();

        int input;

        //private MainScene mainScene = new MainScene(); // MainScene 인스턴스 생성

        public override void DisplayScene()
        {
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║           🛡️  상태창 보기            ║");
                Console.WriteLine("╚══════════════════════════════════════╝");

                Thread.Sleep(200);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(" 캐릭터 정보");
                Console.ResetColor();

                Console.WriteLine("----------------------------------------");


                PrintStat(" 이름", $"{player.Name} ({player.JobType})", ConsoleColor.Green);
                PrintStat(" 공격력", $"{player.Attack}", ConsoleColor.Red);
                PrintStat(" 방어력", $"{player.Defense}", ConsoleColor.Blue);
                PrintStat(" 체력", $"{player.HP}", ConsoleColor.DarkRed);
                PrintStat(" 마나", $"{player.MP}", ConsoleColor.DarkCyan);

                Console.WriteLine();

                PrintStat(" 골드", $"{player.Gold}", ConsoleColor.Green);
                PrintStat(" 경험치", $"{player.Exp} / 100", ConsoleColor.Green);
                PrintStat(" 돌의 개수", $"{player.NumOfStones}", ConsoleColor.Green);

                Console.WriteLine("----------------------------------------\n");

                Thread.Sleep(200);
                Console.WriteLine();
                List<string> selections = new List<string>()
            {
                "인벤토리",  //0
                "스킬",  //1
                "나가기",   //2
            };

                input = TextDisplayer.PageNavigation(selections);
                HandleInput();

            }

            // 헬퍼: 스탯 출력
            void PrintStat(string label, string value, ConsoleColor color)
            {
                Console.ForegroundColor = color;
                Console.Write($"{label,-10} : ");
                Console.ResetColor();
                Console.WriteLine($"{value}");
            }

            // 헬퍼: 메뉴 옵션 출력
            void PrintMenuOption(string key, string description, ConsoleColor color)
            {
                Console.ForegroundColor = color;
                Console.Write($"[{key}] ");
                Console.ResetColor();
                Console.WriteLine(description);
            }


      
            //while (true)
            //{
            //    Console.Write("원하는 번호를 입력하세요: ");
            //    int userInpt;
            //    bool InputChoice = int.TryParse(Console.ReadLine(), out userInpt);
            //    if (!InputChoice)
            //    {
            //        Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
            //        return;
            //    }
            //    switch (userInpt)
            //    {
            //        case 1:
            //            SceneManager.Instance.SetScene(SceneType.Inventory); //씬 호출하는 방식으로 변경
            //            //player.Inventory.DisplayItems(); // 인벤토리 출력
            //            break;
            //        case 2:
            //            SceneManager.Instance.SetScene(SceneType.Skill);
            //            break;
            //        case 0:
            //            Console.WriteLine("상태창을 나갑니다.");
            //            SceneManager.Instance.SetScene(SceneType.Main);
            //            //mainScene.DisplayScene(); // MainScene 인스턴스를 사용하여 호출
            //            break;
            //        default:
            //            Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
            //            break;
            //    }
            //    //DisplayScene(); //다시 출력  //이렇게 진행할 경우, 메인 화면에서 나가기를 선택하였을 때 종료가 되지 않고 캐릭터 선택 창으로 오게 됩니다. 비교를 위해서 while문 놔두고 주석 처리 하였습니다.
            //}

        }    //출력 하는 시스템

        public void QuestShowin()
        {

        }

        public override void HandleInput()
        {
            switch (input)
            {
                case 0:
                    SceneManager.Instance.SetScene(SceneType.Inventory); //씬 호출하는 방식으로 변경
                    //player.Inventory.DisplayItems(); // 인벤토리 출력
                    break;
                case 1:
                    SceneManager.Instance.SetScene(SceneType.Skill);
                    break;
                case 2:
                    Console.WriteLine("상태창을 나갑니다.");
                    SceneManager.Instance.SetScene(SceneType.Main);
                    //mainScene.DisplayScene(); // MainScene 인스턴스를 사용하여 호출
                    break;
                case -1:
                    break;
            }
        } //입력 받고 실행하는 시스템

    }
}

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
            Console.Clear();
            Console.WriteLine("상태창 보기");
            Console.WriteLine("캐릭터 정보가 표시됩니다.");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"이름: {player.Name}({player.JobType})");
            Console.WriteLine($"공격력 : {player.Attack}");
            Console.WriteLine($"방어력 : {player.Defense}");
            Console.WriteLine($"체력 : {player.HP}");
            Console.WriteLine($"마나 : {player.MP}");
            Console.WriteLine($"골드 : {player.Gold}");
            Console.WriteLine($"Exp : {player.Exp} / 100");
            Console.WriteLine($"돌의 개수 : {player.NumOfStones}");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("1. 인벤토리");
            Console.WriteLine("2. 스킬");
            Console.WriteLine("0. 나가기");


            Console.Write("번호를 입력하세요 :");

            input = InputHelper.GetIntegerRange(0, 3);
            HandleInput();
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

        public override void HandleInput()
        {
            switch (input)
            {
                case 1:
                    Console.WriteLine("1선택함");
                    Console.Read();
                    SceneManager.Instance.SetScene(SceneType.Inventory); //씬 호출하는 방식으로 변경
                    //player.Inventory.DisplayItems(); // 인벤토리 출력
                    break;
                case 2:
                    SceneManager.Instance.SetScene(SceneType.Skill);
                    break;
                case 0:
                    Console.WriteLine("상태창을 나갑니다.");
                    SceneManager.Instance.SetScene(SceneType.Main);
                    //mainScene.DisplayScene(); // MainScene 인스턴스를 사용하여 호출
                    break;
            }
        } //입력 받고 실행하는 시스템

    }
}

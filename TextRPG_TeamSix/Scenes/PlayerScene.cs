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

namespace TextRPG_TeamSix.Scenes
{

    //모든 씬이 상속 받는 추상 클래스
    internal class PlayerScene : SceneBase
    {
        Player player = PlayerManager.Instance.CurrentPlayer;
        public override SceneType SceneType => SceneType.Player;

        private List<Item> items = new List<Item>();
        private List<Quest> Quests = new List<Quest>();

        private MainScene mainScene = new MainScene(); // MainScene 인스턴스 생성

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
            Console.WriteLine("1. 인벤토리 확인");
            Console.WriteLine("2. 받은 퀘스트 확인하기");
            Console.WriteLine("0. 나가기");
            while (true)
            {
                Console.Write("원하는 번호를 입력하세요: ");
                int userInpt;
                bool InputChoice = int.TryParse(Console.ReadLine(), out userInpt);
                if (!InputChoice)
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                }
                switch (userInpt)
                {
                    case 1:
                        player.Inventory.DisplayItems(); // 인벤토리 출력
                        break;
                    case 2:

                        break;
                    case 0:
                        Console.WriteLine("상태창을 나갑니다.");
                        mainScene.DisplayScene(); // MainScene 인스턴스를 사용하여 호출
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                        break;
                }
                DisplayScene(); //다시 출력
            }

        }    //출력 하는 시스템

        public void QuestShowin()
        {

        }

        public override void HandleInput()
        {

        } //입력 받고 실행하는 시스템

    }
}

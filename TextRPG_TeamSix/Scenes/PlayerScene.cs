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
        int input;

        //private MainScene mainScene = new MainScene(); // MainScene 인스턴스 생성

        public override void DisplayScene()
        {
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("╔══════════════════상태창 보기════════════════════╗");
                Console.WriteLine("                        O                          ");
                Console.WriteLine("╚══════════════════    /|[*]   ═══════════════════╝");
                Console.WriteLine("______________________ / | ________________________");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(" 캐릭터 정보");
                Console.ResetColor();

                Console.WriteLine("----------------------------------------");


                PrintStat(" 이름", $"{player.Name} ({player.JobType})", ConsoleColor.Green);
                PrintStat(" 공격력", $"{player.GetTotalAttack()} (+ {player.GetEquipBonusAttack()})", ConsoleColor.Red);
                PrintStat(" 방어력", $"{player.GetTotalDefense()} (+ {player.GetEquipBonusDefense()})", ConsoleColor.Blue);
                PrintStat(" 체력", $"{player.HP}", ConsoleColor.DarkRed);
                PrintStat(" 마나", $"{player.MP}", ConsoleColor.DarkCyan);

                Console.WriteLine();

                PrintStat(" 골드", $"{player.Gold}", ConsoleColor.Green);
                PrintStat(" 경험치", $"{player.Exp} / 100", ConsoleColor.Green);
                PrintStat(" 돌의 개수", $"{player.NumOfStones}", ConsoleColor.Green);

                Console.WriteLine("----------------------------------------\n");

                Console.WriteLine();
                List<string> selections = new List<string>()
                {
                    "인벤토리",  //0
                    "스킬",  //1
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
                case -1:
                    //Console.WriteLine("상태창을 나갑니다.");
                    SceneManager.Instance.SetScene(SceneType.Main);
                    //mainScene.DisplayScene(); // MainScene 인스턴스를 사용하여 호출
                    break;
            }
        } //입력 받고 실행하는 시스템

    }
}

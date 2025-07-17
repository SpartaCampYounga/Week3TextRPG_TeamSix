using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Quests;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Scenes
{
    //모든 씬이 상속 받는 추상 클래스
    internal class MainScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Main;
        private int input;


        public override void DisplayScene() //출력 하는 시스템
        {
            Console.OutputEncoding = Encoding.UTF8; // 아스키아트 한글 깨짐 방지


            // 상태보기,상점,던전,퀘스트 순으로 정렬(추후)
            Console.Clear();
            Console.WriteLine("MainScene");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('=', 120));

            Console.WriteLine(@"


                 ______   ______   ______
                 |    |  |      |  |    |
            _____| [] |__| ____ |__| [] |_____ 
            |                                 |                                       🔥       🔥      
            |                                 |                                        ▒▒▒▒▒▒▒▒▒▒
            |        📜 QUEST BOARD           |                                     🔥▒▒▒ _____ ▒▒▒🔥
            |                                 |                                     ▒▒▒  [  #  ]  ▒▒▒
            |        🍞 Item Shop             |                                    ▒▒▒   [ ### ]   ▒▒▒
            |                                 |                                     ▒▒▒  [ ### ]  ▒▒▒
            |_________________________________|======================================▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
                           
                            마을                                                           던전

            ");


            Console.WriteLine(new string('=', 120));

            Console.ResetColor();// 그린컬러 초기화


            Console.WriteLine();
            List<string> selections = new List<string>()
            {
                "캐릭터",  //0
                "퀘스트",  //1
                "상점",   //2
                "던전",   //3
                "휴식",   //4
                "저장하기"  //5
            };

            input = TextDisplayer.PageNavigation(selections);

            HandleInput();
        }


        public override void HandleInput()
        {
            switch (input)
            {
                case 0:
                    SceneManager.Instance.SetScene(SceneType.Player);
                    break;
                case 1:
                    SceneManager.Instance.SetScene(SceneType.Quest);
                    break;
                case 2:
                    SceneManager.Instance.SetScene(SceneType.Store);
                    break;
                case 3:
                    SceneManager.Instance.SetScene(SceneType.Dungeon);
                    break;
                case 4:
                    //SceneManager.Instance.SetScene(SceneType.Dungeon); //휴식
                    break;
                case 5:
                    SaveManager.Instance.SaveGame();
                    InputHelper.WaitResponse();
                    SceneManager.Instance.SetScene(SceneType.Main);
                    break;
                case -1:
                    //Program.cs로 돌아가서 재실행
                    break;
                case 8:
                    SceneManager.Instance.SetScene(SceneType.SpecialStore); //test용 스페셜 상점 진입씬
                    break;
            }
        }
    }
}

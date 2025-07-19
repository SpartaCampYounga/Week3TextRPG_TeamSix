using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Scenes
{
    //모든 씬이 상속 받는 추상 클래스
    internal class TitleScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Title;
        private int input;
        List<string> selection;

        public override void DisplayScene() //출력 하는 시스템
        {
            Console.Clear();
            Console.SetWindowSize(120, 50);
            Console.OutputEncoding = Encoding.UTF8; // 아스키아트 한글 깨짐 방지
            string stars = new string('*', 120);
            string lineBar = new string('=', 120);

            Console.WriteLine("TitleScene");

            Console.ForegroundColor = ConsoleColor.Green;


            Console.WriteLine(stars);
            Console.WriteLine(lineBar);
            Console.WriteLine("");
            Console.WriteLine(@"

                                  ███╗   ██╗ ██████╗     ███████╗██╗  ██╗████╗████████╗
                                  ████╗  ██║██╔═══██╗    ██╔════╝██║  ██║ ██╔╝╚══██╔══╝
                                  ██╔██╗ ██║██║   ██║    █████╗    ███╔═╝ ██║    ██║   
                                  ██║╚██╗██║██║   ██║    ██╔══╝  ██╔══██║ ██║    ██║   
                                  ██║ ╚████║╚██████╔╝    ███████║██║  ██║████║   ██║   
                                  ╚═╝  ╚═══╝ ╚═════╝     ╚══════╝╚═╝  ╚═╝╚═══╝   ╚═╝   
            ");
            Console.WriteLine("");
            Console.WriteLine(lineBar);
            Console.WriteLine(stars);
            Console.WriteLine("");
            Console.WriteLine("");

            Console.ResetColor();// 그린컬러 초기화


            selection = new List<string>()
            {
                "시작하기",
                "종료하기"
            };
            input = TextDisplayer.SelectNavigation(selection);
            HandleInput();
        }

        public override void HandleInput() //입력 받고 실행하는 시스템
        {
            switch (input)
            {
                case 0:
                    SceneManager.Instance.SetScene(SceneType.PlayerSetup);
                    break;
                case 1:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}

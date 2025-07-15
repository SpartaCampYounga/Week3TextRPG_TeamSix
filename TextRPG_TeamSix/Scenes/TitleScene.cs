using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;

namespace TextRPG_TeamSix.Scenes
{
    //모든 씬이 상속 받는 추상 클래스
    internal class TitleScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Title;
        private int input;


        public override void DisplayScene() //출력 하는 시스템
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // 아스키아트 한글 깨짐 방지
            Console.WriteLine("TitleScene");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('*', Console.WindowWidth));
            Console.WriteLine(new string('=', Console.WindowWidth));
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
            Console.WriteLine(new string('=', Console.WindowWidth));
            Console.WriteLine(new string('*', Console.WindowWidth));
            Console.WriteLine("");
            Console.WriteLine("");

            Console.Write(
                "1. 시작\n" +
                "0. 종료"
                );

            Console.ResetColor();// 그린컬러 초기화


            Console.Write("\n번호를 입력하세요: ");
            string? input = Console.ReadLine();

            if (input == "1")
            {
                SceneManager.Instance.SetScene(SceneType.PlayerSetup); // Controllers 네임스페이스에서 바로 사용
            }
            else if (input == "0")
            {
                Console.WriteLine("게임을 종료합니다.");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("잘못된 입력 입니다.");
            }


        }

        public override void HandleInput() //입력 받고 실행하는 시스템
        {
            
        }
    }
}

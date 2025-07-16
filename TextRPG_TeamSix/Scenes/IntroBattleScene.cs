using System;
using System.Threading;
using TextRPG_TeamSix.Enums;

namespace TextRPG_TeamSix.Scenes
{
    internal class IntroScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Title;

        public override void DisplayScene()
        {
            Console.Clear();
            DisplayTitle();
            DisplaySubtitle();
            DisplayDescription();
            WaitForInput();
        }

        public override void HandleInput()
        {
            // 인트로는 특별한 입력이 없을 수도 있음
        }

        private static void DisplayTitle()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string title = @"
                                  ███╗   ██╗ ██████╗     ███████╗██╗  ██╗████╗████████╗
                                  ████╗  ██║██╔═══██╗    ██╔════╝██║  ██║ ██╔╝╚══██╔══╝
                                  ██╔██╗ ██║██║   ██║    █████╗    ███╔═╝ ██║    ██║   
                                  ██║╚██╗██║██║   ██║    ██╔══╝  ██╔══██║ ██║    ██║   
                                  ██║ ╚████║╚██████╔╝    ███████║██║  ██║████║   ██║   
                                  ╚═╝  ╚═══╝ ╚═════╝     ╚══════╝╚═╝  ╚═╝╚═══╝   ╚═╝   
";
            Console.WriteLine(title);
            Thread.Sleep(1000);
            Console.ResetColor();
        }

        private static void DisplaySubtitle()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n♜ NO EXIT 던전에 오신 것을 환영합니다! ♜\n");
            Thread.Sleep(1000);
        }

        private static void DisplayDescription()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("유일한 탈출구는...");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("저 너머의 좁은 던전 출구");

            Console.ResetColor();
            Console.WriteLine(" 뿐입니다.");
            Thread.Sleep(1000);
        }

        private static void WaitForInput()
        {
            Console.WriteLine("\n아무 키나 눌러 전투로...");
            Console.ReadKey();
        }
    }
}

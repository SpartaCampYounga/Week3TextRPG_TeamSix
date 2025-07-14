using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;

namespace TextRPG_TeamSix.Scenes
{
    //모든 씬이 상속 받는 추상 클래스
    internal class TitleScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Title;
        private int input;


        public override void DisplayScene() //출력 하는 시스템
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("TitleScene");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('*', Console.WindowWidth));
            Console.WriteLine(new string('=', Console.WindowWidth));
            Console.WriteLine("");
            Console.WriteLine(@"
                            ████████╗███████╗██╗  ██╗████████╗    ██████╗ ██████╗  ██████╗ 
                            ╚══██╔══╝██╔════╝██║  ██║╚══██╔══╝    ██╔══██╗██╔══██╗██╔════╝
                               ██║   █████╗    ███╔═╝   ██║       ██████╔╝██████╔╝██║ ███║
                               ██║   ██╔══╝  ██╔══██║   ██║       ██╔═██║ ██╔═══╝ ██║   ██║
                               ██║   ███████╗██║  ██║   ██║       ██║ ██║ ██║     ╚██████╔╝
                               ╚═╝   ╚══════╝╚═╝  ╚═╝   ╚═╝       ╚═╝ ╚═╝ ╚═╝      ╚═════╝ 
            ");
            Console.WriteLine("");
            Console.WriteLine(new string('=', Console.WindowWidth));
            Console.WriteLine(new string('*', Console.WindowWidth));
            Console.WriteLine("");
            Console.WriteLine("");


            Console.Write(
                "1. 시작\n" +
                "2. 종료"
                );

            Console.ResetColor();

        }

        public override void HandleInput() //입력 받고 실행하는 시스템
        {
        }
    }
}

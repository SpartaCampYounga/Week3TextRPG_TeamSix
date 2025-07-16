using System;
using System.Text;
using System.Threading;
using TextRPG_TeamSix.Enums;

namespace TextRPG_TeamSix.Scenes
{
    internal class IntroScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Title;

        public override void DisplayScene()
        {
            ShowIntro();
        }

        public override void HandleInput()
        {
           
        }

        private void TypeEffect(string text, int delay = 50)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    Console.Write(text.Substring(i)); 
                    break;
                }

                Console.Write(text[i]);
                Thread.Sleep(delay);
            }

            Console.WriteLine();
        }



        private void FlashEffect(int times = 2, int delay = 150)
        {
            for (int i = 0; i < times; i++)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.Clear();
                Thread.Sleep(delay);

                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
                Thread.Sleep(delay);
            }

            Console.ResetColor();
        }

        public void ShowIntro()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.OutputEncoding = Encoding.UTF8;

            string title = @"
                                  ███╗   ██╗ ██████╗     ███████╗██╗  ██╗████╗████████╗
                                  ████╗  ██║██╔═══██╗    ██╔════╝██║  ██║ ██╔╝╚══██╔══╝
                                  ██╔██╗ ██║██║   ██║    █████╗    ███╔═╝ ██║    ██║   
                                  ██║╚██╗██║██║   ██║    ██╔══╝  ██╔══██║ ██║    ██║   
                                  ██║ ╚████║╚██████╔╝    ███████║██║  ██║████║   ██║   
                                  ╚═╝  ╚═══╝ ╚═════╝     ╚══════╝╚═╝  ╚═╝╚═══╝   ╚═╝   
";

            string desc1 = "…여긴 대체 뭐지?\n바람 한 점 없고, 앞에는 이상하게 생긴 문밖에 없네…";
            string desc2 = "잠깐… 바닥에 이 빛나는 돌은 뭐지? 어디보자.. ";
            string redPard1 = "^$#^%@#$!$%... 우와아아아아악 이게뭐야아아아아!!!!";
            string proceed = "Press enter key to Continue..";

            Console.ForegroundColor = ConsoleColor.Green;
            TypeEffect(desc1, 70);
            Thread.Sleep(1000);

            TypeEffect(desc2, 70);
            Thread.Sleep(1000);

            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            TypeEffect(redPard1, 100);
            Console.ResetColor();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            FlashEffect();
            Console.WriteLine(title);
            Thread.Sleep(1000);

            Console.WriteLine();
            Console.WriteLine();

            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            TypeEffect(proceed, 40);
            Thread.Sleep(500);
            Console.ReadKey();
        }
    }
}

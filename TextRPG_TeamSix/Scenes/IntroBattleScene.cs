using System;
using System.Text;
using System.Threading;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Utils;

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



        public void FlashEffect(int times = 2, int delay = 150)
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

        private void PullInEffect()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            string[] portal = {
        "        [ P O R T A L ]",
        "           █████",
        "          █     █",
        "          █     █",
        "          ███████"
    };

            string[] stickman = {
        "   o",
        "  /|\\",
        "  / \\"
    };

            int totalSteps = 5;
            for (int step = totalSteps; step >= 0; step--)
            {
                Console.Clear();

                // 포탈 출력
                foreach (var line in portal)
                    Console.WriteLine(line);

                // 졸라맨이 점점 위로 이동
                for (int i = 0; i < step; i++)
                    Console.WriteLine();

                foreach (var line in stickman)
                    Console.WriteLine("      " + line);

                Thread.Sleep(180);
            }
        }

        private void PortalShakeEffect(int times = 6, int delay = 80)
        {
            string[] portalLeft = {
        "       [ P O R T A L ]",
        "          █████",
        "         █     █",
        "         █     █",
        "         ███████"
    };

            string[] portalRight = {
        "         [ P O R T A L ]",
        "            █████",
        "           █     █",
        "           █     █",
        "           ███████"
    };

            for (int i = 0; i < times; i++)
            {
                Console.Clear();
                var frame = i % 2 == 0 ? portalLeft : portalRight;
                foreach (var line in frame)
                    Console.WriteLine(line);
                Thread.Sleep(delay);
            }
        }

        private void EnergyPulseEffect()
        {
            string[] pulses = {
        "        ░░░░░░░░░░░        ",
        "      ▒▒▒▒▒▒▒▒▒▒▒▒▒▒      ",
        "    ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓    ",
        "  ██████████████████████  ",
        "██████████████████████████",
        "  ██████████████████████  ",
        "    ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓    ",
        "      ▒▒▒▒▒▒▒▒▒▒▒▒▒▒      ",
        "        ░░░░░░░░░░░        "
    };

            foreach (var line in pulses)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(line);
                Thread.Sleep(90);
            }
        }


        private void TileBreakEffect()
        {
            string[] cracks = {
        "        ▄       ▄        ",
        "      ▄█▄     ▄█▄      ",
        "    ▄███▄   ▄███▄    ",
        "  ▄██████████████▄  ",
        " ███████████████████ ",
        "▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒",
        "▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒"
    };

            foreach (var line in cracks)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(line);
                Thread.Sleep(100);
            }
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


            string redPard1 = "^$#^%@#$!$%... 우와아아아아악 살려주세요 튜텨님!!!!!!";
            string proceed = "Press enter key to Continue...";

            PullInEffect();               // 졸라맨 들어감
            PortalShakeEffect();          // 포탈 흔들림
            EnergyPulseEffect();          // 에너지 폭발
            TileBreakEffect();            // 바닥 깨짐


            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            TextEffect.TypeEffect(redPard1);
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
            TextEffect.TypeEffect(proceed,40);
            Thread.Sleep(500);
            Console.ReadKey();
        }
    }
}

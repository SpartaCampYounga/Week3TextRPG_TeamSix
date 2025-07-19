using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;

namespace TextRPG_TeamSix.Scenes
{
    internal class BattleScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Battle;

        public override void DisplayScene()
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔"+ new string('═', Console.WindowWidth -2) + "╗");
            Console.WriteLine("                        O                          ");
            Console.WriteLine("╚══════════════════    /|[*]   ═══════════════════╝");
            Console.WriteLine("______________________ / | ________________________");

            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        public override void HandleInput()
        {
            throw new NotImplementedException();
        }
    }
}

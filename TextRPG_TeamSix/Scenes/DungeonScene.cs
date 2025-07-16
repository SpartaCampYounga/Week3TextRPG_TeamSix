using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Dungeons;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Scenes
{
    internal class DungeonScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Dungeon;

        public override void DisplayScene()
        {
            Console.Clear();

            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("DungeonScene Loaded");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('=', Console.WindowWidth));
            Console.WriteLine("던전 - 원하는 던전에 진입할 수 있습니다.");
            Console.WriteLine(new string('=', Console.WindowWidth));

            List<Dungeon> dungeons = GameDataManager.Instance.AllDungeons;

            //테이블 헤더
            string header = "";
            header += FormatUtility.AlignWithPadding("No.", 3) + " | ";
            //header += FormatUtility.AlignWithPadding("소지여부", 8) + " | ";
            header += FormatUtility.AlignWithPadding("던전이름", 15) + " | ";
            header += FormatUtility.AlignWithPadding("권장 방어력", 11) + " | ";

            Console.WriteLine(header);
            Console.WriteLine(new string('-', Console.WindowWidth));

            //던전 리스트 출력
            for (int i = 0; i < dungeons.Count(); i++)
            {
                string display = FormatUtility.AlignWithPadding((i + 1).ToString(), 3) + " | ";
                display += dungeons[i];
                Console.WriteLine(display);
            }




        }

        public override void HandleInput()
        {
        }
    }
}

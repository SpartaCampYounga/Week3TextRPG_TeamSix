using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;

namespace TextRPG_TeamSix.Scenes
{
    internal class DungeonScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Dungeon;

        public override void DisplayScene()
        {
            Console.Clear();

            Console.OutputEncoding = Encoding.UTF8; //Younga TIL
            Console.WriteLine("SkillScene Loaded");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('=', Console.WindowWidth)); //Younga TIL
            Console.WriteLine("스킬 - 보유 중인 스킬을 볼 수 있습니다.");
            Console.WriteLine(new string('=', Console.WindowWidth));
        }

        public override void HandleInput()
        {
        }
    }
}

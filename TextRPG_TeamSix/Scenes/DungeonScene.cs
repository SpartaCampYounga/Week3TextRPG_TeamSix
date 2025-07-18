using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Dungeons;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Skills;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Scenes
{
    internal class DungeonScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Dungeon;

        private int input;
        Player player = PlayerManager.Instance.CurrentPlayer;
        List<Dungeon> dungeons = GameDataManager.Instance.AllDungeons;
        //List<uint> clearedDungeonList = PlayerManager.Instance.ClearedDungeonList;
        public override void DisplayScene()
        {
            Console.Clear();

            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("DungeonScene Loaded");

            Console.WriteLine(new string('=', Console.WindowWidth));
            Console.WriteLine("던전 - 원하는 던전에 진입할 수 있습니다.");
            Console.WriteLine(new string('=', Console.WindowWidth));


            //테이블 헤더
            string header = "";
            header += FormatUtility.AlignWithPadding("No.", 3) + " | ";
            //header += FormatUtility.AlignWithPadding("소지여부", 8) + " | ";
            header += FormatUtility.AlignWithPadding("던전이름", 15) + " | ";
            header += FormatUtility.AlignWithPadding("권장 방어력", 11) + " | ";

            Console.WriteLine(header);
            Console.WriteLine(new string('-', Console.WindowWidth));


            input = TextDisplayer.PageNavigation(dungeons);


            //던전 리스트 출력
            //for (int i = 0; i < dungeons.Count(); i++)
            //{
            //    string display = FormatUtility.AlignWithPadding((i + 1).ToString(), 3) + " | ";
            //    display += dungeons[i];
            //    Console.WriteLine(display);
            //}

            Console.WriteLine(new string('-', Console.WindowWidth));
            Console.WriteLine();
            Console.WriteLine();


            //Console.WriteLine("입장하고 싶은 스킬의 숫자를 입력하세요.");
            //Console.WriteLine("0. 나가기");
            //Console.Write(">>");
            //input = InputHelper.GetIntegerRange(0, dungeons.Count() + 1);

            HandleInput();
        }

        public override void HandleInput()
        {
            switch (input)
            {
                case -1:
                    SceneManager.Instance.SetScene(SceneType.Main);
                    break;
                default:
                    Dungeon selectedDungeon = dungeons[input];
                    if (selectedDungeon.TryEnterDungeon(player))
                    {
                        SceneManager.Instance.SetScene(SceneType.Battle);
                    }
                    else
                    {
                        InputHelper.WaitResponse();
                        SceneManager.Instance.SetScene(SceneType.Dungeon);
                    }
                    break;
            }
        }
    }
}

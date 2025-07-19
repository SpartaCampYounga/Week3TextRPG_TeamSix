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
            //Console.Clear();

            //Console.OutputEncoding = Encoding.UTF8;
            //Console.WriteLine("DungeonScene Loaded");

            FormatUtility.DisplayHeader("던전 - 원하는 던전에 진입할 수 있습니다.");

            //테이블 헤더
            string header = "";
            header += FormatUtility.AlignLeftWithPadding("  ", 2) + " | ";
            header += FormatUtility.AlignLeftWithPadding("던전이름", 15) + " | ";
            header += FormatUtility.AlignLeftWithPadding("권장 방어력", 11) + " | ";

            Console.WriteLine(header);
            Console.WriteLine();
            Console.WriteLine();
            //Console.WriteLine(new string('-', Console.WindowWidth));


            input = TextDisplayer.SelectNavigation(dungeons);

            //Console.WriteLine(new string('-', Console.WindowWidth));
            Console.WriteLine();
            Console.WriteLine();

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
                        PlayerManager.Instance.CurrentDungeon.Clone(selectedDungeon);
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

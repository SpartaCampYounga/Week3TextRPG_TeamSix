using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Skills;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Scenes
{
    internal class SkillLearnScene : SceneBase
    {
        //Controllers - GameInitializer에 _scenes 배열에 추가하면 됨.
        public override SceneType SceneType => SceneType.SkillLearn; //Enum-SceneType에 원하는 enum값 생성.
        private int input;
        Player player;
        List<Skill> availableToLearn;

        public override void DisplayScene()
        {
            Console.Clear();

            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("SkillLearnScene Loaded");

            //Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('=', Console.WindowWidth));
            Console.WriteLine("스킬 - 번호를 선택하여 스킬을 배울 수 있습니다.");
            Console.WriteLine(new string('=', Console.WindowWidth));
            player = PlayerManager.Instance.CurrentPlayer;
            availableToLearn = GameDataManager.Instance.AllSkills.Where(x => !player.SkillList.Contains(x)).ToList();


            ////플레이어가 미보유중인 스킬만 띄우기
            if (availableToLearn.Count == 0)
            {
                Console.WriteLine("하산해라.. 더는 배울게 없다..");
                Console.WriteLine();
                input = -2;
                InputHelper.WaitResponse();
            }
            else
            {   //테이블 헤더
                string header = "";
                header += FormatUtility.AlignWithPadding("No.", 3) + " | ";
                //header += FormatUtility.AlignWithPadding("소지여부", 8) + " | ";
                header += FormatUtility.AlignWithPadding("이름", 15) + " | ";
                header += FormatUtility.AlignWithPadding("설명", 30) + " | ";
                header += FormatUtility.AlignWithPadding("소모MP", 7) + " | ";
                header += FormatUtility.AlignWithPadding("스킬석", 7) + " | ";
                header += FormatUtility.AlignWithPadding("스킬타입", 10) + " | ";
                header += FormatUtility.AlignWithPadding("효과량", 7) + " | ";

                Console.WriteLine(header);
                Console.WriteLine(new string('-', Console.WindowWidth));


                input = TextDisplayer.PageNavigation(availableToLearn);

                Console.WriteLine(new string('-', Console.WindowWidth));

                player.AcquireSkillStone(100);
                Console.WriteLine("스킬석 100개 깜짝 선물을 받았다!");
            }

            Console.WriteLine();
            Console.WriteLine();

            HandleInput();
        }

        public override void HandleInput()
        {
            switch (input)
            {
                case -2:
                    SceneManager.Instance.SetScene(SceneType.Skill);
                    break;
                case -1:
                    SceneManager.Instance.SetScene(SceneType.Skill);    //0번 누르면 해당 타입의 씬 출력
                    break;
                default:
                    Skill selectedSkill = availableToLearn[input];
                    if (selectedSkill.IsAvailableToLearn(player))
                    {
                        player.LearnSkill(selectedSkill);
                        Console.WriteLine($"{selectedSkill.Name}을 배웠다...");
                        InputHelper.WaitResponse();
                    }
                    else
                    {
                        Console.WriteLine("보유한 스킬석이 부족해서 배우지 못했다...");
                        InputHelper.WaitResponse();
                    }

                    SceneManager.Instance.SetScene(SceneType.SkillLearn);
                    break;
            }
            //switch (input)
            //{
            //    case 0:
            //        Console.WriteLine("0선택함"); //debug용
            //        Console.WriteLine(PlayerManager.Instance.CurrentPlayer.SkillList.Count() + PlayerManager.Instance.CurrentPlayer.Name);
            //        SaveManager.Instance.SaveGame();
            //        Console.WriteLine(PlayerManager.Instance.CurrentPlayer.SkillList.Count() + PlayerManager.Instance.CurrentPlayer.Name);
            //        InputHelper.WaitResponse();
            //        SceneManager.Instance.SetScene(SceneType.Skill);    //0번 누르면 해당 타입의 씬 출력
            //        break;
            //    default:
            //        Skill selectedSkill = availableToLearn[input - 1];
            //        if (selectedSkill.IsAvailableToLearn(player))
            //        {
            //            player.LearnSkill(selectedSkill);
            //        }
            //        else
            //        {
            //            Console.WriteLine("보유한 스킬석이 부족해서 배우지 못했다...");
            //        }

            //        SceneManager.Instance.SetScene(SceneType.SkillLearn);
            //        break;
            //}
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Skills;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Scenes
{
    internal class SkillScene : SceneBase
    {
        //Controllers - GameInitializer에 _scenes 배열에 추가하면 됨.
        public override SceneType SceneType => SceneType.Skill; //Enum-SceneType에 원하는 enum값 생성.
        private int input;

        public override void DisplayScene()
        {
            //콘솔창 셋업
            //Console.SetWindowSize(200, 50);
            //Console.WriteLine(Console.LargestWindowHeight.ToString() + Console.LargestWindowWidth.ToString());
            //Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.Clear();

            Player player = PlayerManager.Instance.CurrentPlayer;

            Console.OutputEncoding = Encoding.UTF8; //Younga TIL
            Console.WriteLine("SkillScene Loaded");

            Console.WriteLine(new string('=', Console.WindowWidth)); //Younga TIL
            Console.WriteLine("스킬 - 보유 중인 스킬을 볼 수 있습니다.");
            Console.WriteLine(new string('=', Console.WindowWidth));

            //테이블 헤더
            string header = "";
            //header += FormatUtility.AlignWithPadding("No.", 3) + " | ";
            //header += FormatUtility.AlignWithPadding("소지여부", 8) + " | ";
            header += FormatUtility.AlignWithPadding("이름", 15) + " | ";
            header += FormatUtility.AlignWithPadding("설명", 30) + " | ";
            header += FormatUtility.AlignWithPadding("소모MP", 7) + " | ";
            header += FormatUtility.AlignWithPadding("스킬석", 7) + " | ";
            header += FormatUtility.AlignWithPadding("스킬타입", 10) + " | ";
            header += FormatUtility.AlignWithPadding("효과량", 7) + " | ";

            Console.WriteLine(header);
            Console.WriteLine(new string('-', Console.WindowWidth));

            //플레이어가 보유중인 스킬만 띄우기
            if (player.SkillList != null && player.SkillList.Count != 0)
            {
                for (int i = 0; i < player.SkillList.Count(); i++)
                {
                    Console.WriteLine(player.SkillList[i]);
                }
            }
            else
            {
                Console.WriteLine("보유 중인 스킬이 없습니다.");
            }
            Console.WriteLine(new string('-', Console.WindowWidth));
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("1. 스킬 배우기");
            Console.WriteLine("0. 나가기");
            Console.Write(">>");
            input = InputHelper.GetIntegerRange(0, 3);
            HandleInput();
        }

        public override void HandleInput()
        {
            switch (input)
            {
                case 0:
                    Console.WriteLine("0선택함"); //debug용 //현재는 Main으로... 추후 캐릭터창 생기면 하는 걸로
                    SceneManager.Instance.SetScene(SceneType.Main);    //0번 누르면 해당 타입의 씬 출력
                    break;
                case 1:
                    SceneManager.Instance.SetScene(SceneType.SkillLearn);
                    break;
            }
        }
    }
}

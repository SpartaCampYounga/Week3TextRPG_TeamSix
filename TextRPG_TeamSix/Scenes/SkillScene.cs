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
            //Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            //Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            Player player = PlayerManager.Instance.CurrentPlayer;

            Console.OutputEncoding = Encoding.UTF8; //Younga TIL
            Console.WriteLine("SkillScene Loaded");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('=', Console.WindowWidth)); //Younga TIL
            Console.WriteLine("스킬 - 번호를 선택하여 스킬을 배울 수 있습니다.");
            Console.WriteLine(new string('=', Console.WindowWidth));

            //테이블 헤더
            string header = "";
            header += FormatUtility.AlignWithPadding("No.", 3) + " | ";
            header += FormatUtility.AlignWithPadding("소지여부", 8) + " | ";
            header += FormatUtility.AlignWithPadding("이름", 15) + " | ";
            header += FormatUtility.AlignWithPadding("설명", 30) + " | ";
            header += FormatUtility.AlignWithPadding("소모MP", 7) + " | ";
            header += FormatUtility.AlignWithPadding("스킬석", 7) + " | ";
            header += FormatUtility.AlignWithPadding("스킬타입", 10) + " | ";
            header += FormatUtility.AlignWithPadding("효과량", 7) + " | ";

            Console.WriteLine(header);
            Console.WriteLine(new string('-', Console.WindowWidth));

            //게임 내 존재하는 모든 스킬을 보여주지만, 보유 중인 스킬만 [보유중] 띄우기
            List<Skill> allSkills = GameDataManager.Instance.AllSkills;


            for ( int i = 0; i < allSkills.Count(); i++)
            {
                string display = "";

                if (player.SkillList.FirstOrDefault(x => x.Id == allSkills[i].Id) == null)
                {
                    //갖고 있지 않으면
                    display += FormatUtility.AlignWithPadding((i + 1).ToString(), 3) + " | ";
                    display += FormatUtility.AlignWithPadding("미보유", 8) + " | ";
                }
                else
                {
                    display += FormatUtility.AlignWithPadding("", 3) + " | ";
                    display += FormatUtility.AlignWithPadding("보유중", 8) + " | ";
                }
                display += allSkills[i];
                Console.WriteLine(display);
            }
            Console.WriteLine();

            Console.WriteLine("배우고 싶은 스킬의 번호를 클릭하세요.");
            Console.Write(">>");
            input = InputHelper.GetIntegerRange(0, 1);
            HandleInput();
        }

        public override void HandleInput()
        {
            switch (input)
            {
                case 0:
                    Console.WriteLine("0선택함"); //debug용
                    SceneManager.Instance.SetScene(SceneType.Skill);    //0번 누르면 해당 타입의 씬 출력
                    break;
            }
        }
    }
}

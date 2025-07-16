using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Quest;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Scenes
{
    //모든 씬이 상속 받는 추상 클래스
    internal class QuestScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Quest;
        private int input;


        public override void DisplayScene() //출력 하는 시스템
        {
            Console.Clear();
            Console.WriteLine("QuestScene");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('=',120));
            Console.WriteLine("모험가 사무소에 온것을 환영합니다.");
            Console.WriteLine(new string('=', 120));


            // Hard던전 클리어  | Hard던전을 소탕하세요. | 보상: 1000골드, 100경험치
            string header = "";
            header += FormatUtility.AlignWithPadding("퀘스트 목표", 15) + " | ";
            header += FormatUtility.AlignWithPadding("내용", 30) + " | ";
            header += FormatUtility.AlignWithPadding("보상", 30) + " | ";

            Console.WriteLine(header);
            Console.WriteLine(new string('-', 120));


            Console.ResetColor();


            Console.WriteLine("퀘스트 배치구역");


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('=', 120));
            Console.ResetColor();




            Console.WriteLine("0. 나가기");


            Console.WriteLine("번호를 입력해 주세요 : ");
            input = InputHelper.GetIntegerRange(0, 1);
            HandleInput();
        }

        public override void HandleInput()
        {
            switch (input)
            {
                case 0:
                    SceneManager.Instance.SetScene(SceneType.Main);
                    break;
            }
        }
    }
}

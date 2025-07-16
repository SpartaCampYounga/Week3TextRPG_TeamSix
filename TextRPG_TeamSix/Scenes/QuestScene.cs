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


            // 들어갈 내용 예시) Hard던전 클리어  | Hard던전에 모든 적들을 소탕해주세요! | 보상: 1000골드, 100경험치
            string header = "";
            header += FormatUtility.AlignWithPadding("퀘스트 목표", 15) + " | ";
            header += FormatUtility.AlignWithPadding("내용", 30) + " | ";
            header += FormatUtility.AlignWithPadding("보상", 30) + " | ";
            header += FormatUtility.AlignWithPadding("진행사항", 0);

            Console.WriteLine(header);
            Console.WriteLine(new string('-', 120));
            Console.ResetColor();

            // 퀘스트 수락시 퀘스트 리스트를 번호로 선택가능하게 해야함
            // 선택한 퀘스트는 진행중으로 표기, 완료시 다시 선택가능하게 해야함

            Console.WriteLine("퀘스트 배치구역");


            // 퀘스트 끝나는 부분
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('=', 120));
            Console.ResetColor();



            Console.WriteLine("1. 퀘스트 수락(미구현)");
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

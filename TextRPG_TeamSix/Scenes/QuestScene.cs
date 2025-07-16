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
            Console.WriteLine("0. 나가기");


            Console.WriteLine("번호를 입력해 주세요 : ");

            input = InputHelper.GetIntegerRange(0, 2);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Scenes
{
    //모든 씬이 상속 받는 추상 클래스
    internal class MainScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Main;
        private int input;


        public override void DisplayScene() //출력 하는 시스템
        {
            Console.Clear();
            Console.WriteLine("MainScene");
            Console.WriteLine("마을에 오신 것을 환영합니다.");
            Console.WriteLine("");
            Console.WriteLine("1. 스킬보기");
            Console.WriteLine("2. 퀘스트");
            Console.WriteLine("3. 던전");
            Console.WriteLine("4. 퀘스트");
            Console.WriteLine("");
            Console.Write("번호를 입력하세요 : ");

            input = InputHelper.GetIntegerRange(1, 3);
            HandleInput();
        }

            

        public override void HandleInput()
        {
            switch (input)
            {
                case 1:
                    SceneManager.Instance.SetScene(SceneType.SkillLearn);
                    break;
                case 2:
                    SceneManager.Instance.SetScene(SceneType.Quest);
                    break;
            }
        }
    }
}

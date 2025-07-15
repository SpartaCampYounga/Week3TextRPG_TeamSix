using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Quest;

namespace TextRPG_TeamSix.Scenes
{
    //모든 씬이 상속 받는 추상 클래스
    internal class QuestScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Quest;


        public override void DisplayScene() //출력 하는 시스템
        {
            Console.Clear();
            Console.WriteLine("QuestScene");


            int input = Console.Read();

            switch (input)
            {
                case 0:
                    SceneManager.Instance.SetScene(SceneType.Main);
                    break;
                    //case 1:
                    //    SceneManager.Instance.SetScene(SceneType.퀘스트 수락);
                    //    break;
            }
        }

        public override void HandleInput() //입력 받고 실행하는 시스템
        {

        }
    }
}

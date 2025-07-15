using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;

namespace TextRPG_TeamSix.Scenes
{
    //모든 씬이 상속 받는 추상 클래스
    internal class MainScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Main;


        public override void DisplayScene() //출력 하는 시스템
        {
            Console.Clear();
            Console.WriteLine("MainScene");
            Console.WriteLine("마을에 오신 것을 환영합니다.");
            Console.WriteLine("");
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 상점");
            Console.WriteLine("3. 던전");
            Console.WriteLine("4. 퀘스트");
            Console.WriteLine("");
            Console.Write("번호를 입력하세요 : ");
            Console.ReadLine();

            int input = int.Parse(Console.ReadLine());

            // 팀 원들의 진행에 따라 반영처리

            //switch (input)
            //{
            //    case 1:
            //        SceneManager.Instance.SetScene(SceneType.);
            //        break;
            //    case 2:
            //        SceneManager.Instance.SetScene(SceneType.);
            //        break;
            //    case 3:
            //        SceneManager.Instance.SetScene(SceneType.);
            //        break;
            //    case 4:
            //        SceneManager.Instance.SetScene(SceneType.);
            //        break;
            //}
        }

        public override void HandleInput() //입력 받고 실행하는 시스템
        {

        }
    }
}

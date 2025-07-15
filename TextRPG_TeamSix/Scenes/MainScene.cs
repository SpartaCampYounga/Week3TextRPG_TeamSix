using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;

namespace TextRPG_TeamSix.Scenes
{
    //모든 씬이 상속 받는 추상 클래스
    internal class MainScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Main;


        public override void DisplayScene() //출력 하는 시스템
        {
            Console.WriteLine("MainScene");
            Console.Write(
                "마을에 오신 것을 환영합니다.\n" +
                "\n" +
                "1. 상태보기\n" +
                "2. 인벤토리\n" +
                "3. 상점\n" +
                "4. 던전\n" +
                "5. 퀘스트\n" +
                "\n");
        }

        public override void HandleInput() //입력 받고 실행하는 시스템
        {

        }
    }
}

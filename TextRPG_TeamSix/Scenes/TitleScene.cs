using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;

namespace TextRPG_TeamSix.Scenes
{
    //모든 씬이 상속 받는 추상 클래스
    internal class TitleScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Title;


        public override void DisplayScene() //출력 하는 시스템
        {
            Console.WriteLine("MainScene");
            Console.Write(
                "당신의 이름은?" +
                "당신의 직업은?"
                );
        }

        public override void HandleInput() //입력 받고 실행하는 시스템
        {
        }
    }
}

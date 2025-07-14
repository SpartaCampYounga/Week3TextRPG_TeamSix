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
    internal class SkillScene : SceneBase
    {
        //Controllers - GameInitializer에 _scenes 배열에 추가하면 됨.
        public override SceneType SceneType => SceneType.Skill; //Enum-SceneType에 원하는 enum값 생성.
        private int input;

        public override void DisplayScene()
        {
            Console.WriteLine("출력~!~!");
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

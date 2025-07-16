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
            // 상태보기,상점,던전,퀘스트 순으로 정렬(추후)
            Console.Clear();
            Console.WriteLine("MainScene");
            Console.WriteLine("마을에 오신 것을 환영합니다.");
            Console.WriteLine("");
            Console.WriteLine("1. 스킬보기");
            Console.WriteLine("2. 퀘스트");
            Console.WriteLine("3. [미구현]");
            Console.WriteLine("4. 던전");
            Console.WriteLine("");
            Console.Write("번호를 입력하세요 : ");

            // 현재 스킬보기와 퀘스트만 구현되어 있습니다. 혹시라도 연결이 필요하시면Handleinput에 추가해주세요.
            // 1. GetintegerRange의 Max 값 변경
            // 2. HandleInput에 case 추가  
            // 4. 던전 추가함. (3번 상점이 나을 것 같아서)
            // 애매하면 그냥 채팅에 무슨씬 연결요청해주세요. 남겨주시면 반영할게용
            input = InputHelper.GetIntegerRange(1, 5);
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
                case 4:
                    SceneManager.Instance.SetScene(SceneType.Dungeon);
                    break;
            }
        }
    }
}

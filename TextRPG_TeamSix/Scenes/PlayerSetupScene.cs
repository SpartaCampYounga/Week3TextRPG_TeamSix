using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;

namespace TextRPG_TeamSix.Scenes
{
    //모든 씬이 상속 받는 추상 클래스
    internal class PlayerSetupScene : SceneBase
    {
        public override SceneType SceneType => SceneType.PlayerSetup;


        public override void DisplayScene() //출력 하는 시스템
        {
            Console.Clear();
            Console.WriteLine("PlayerSetupScene");
            string input;

            Console.WriteLine("사용자를 생성합니다. (동일한 사용자명은 불러오기)");
            Console.Write("사용자 명을 입력해주세요: ");
            input = Console.ReadLine();

            Player player = SceneManager.Instance.LoadPlayer(input);    //파일 찾아서 시도해보고
            if (player == null) //파일 못찾아서 비어있으면 새로 생성
            {
                player = new Player(input);

                Console.WriteLine($"{input}이 생성되었습니다. ");
            }
            else
            {
                Console.WriteLine($"{input}을 불러왔습니다. ");
            }
        }

        public override void HandleInput() //입력 받고 실행하는 시스템
        {
        }
    }
}

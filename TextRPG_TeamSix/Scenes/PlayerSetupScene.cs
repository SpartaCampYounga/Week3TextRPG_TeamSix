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

            Console.Write("사용자 명을 입력해주세요: ");
            string userName = Console.ReadLine();


            Console.WriteLine("사용자 직업을 선택해주세요:");
            Console.WriteLine("1. 전사(Warrior)");
            Console.WriteLine("2. 마법사(Magician)");
            Console.WriteLine("3. 도적(Assassin) (jobtype 미구현)");
            Console.WriteLine("4. 궁수(Archer) (jobtype 미구현)");
            Console.Write("번호를 입력하세요: ");
            string jobInput = Console.ReadLine();

            JobType selectedJob = jobInput switch
            {
                "1" => JobType.Warrior,
                "2" => JobType.Magician,
                _ => JobType.Warrior // 기본값
            };

        }

        public override void HandleInput() //입력 받고 실행하는 시스템
        {
        }
    }
}

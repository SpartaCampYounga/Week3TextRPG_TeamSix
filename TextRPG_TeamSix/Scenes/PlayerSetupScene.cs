using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Scenes
{
    //모든 씬이 상속 받는 추상 클래스   //
    internal class PlayerSetupScene : SceneBase
    {
        public override SceneType SceneType => SceneType.PlayerSetup;

        public override void DisplayScene() //출력 하는 시스템
        {
            Console.Clear();
            Console.WriteLine("PlayerSetupScene");

            Console.WriteLine("[1] 불러오기     [2] 새로만들기");
            Console.Write("번호를 입력하세요 :");

            string userName = Console.ReadLine();
            if (userName == "1")
            {
                Console.Clear();
                Console.Write("이름을 입력하세요: ");
                string input = Console.ReadLine();
                
                if (PlayerManager.Instance.InitializePlayerFromSaveData(input))  //데이터에서 못 불러옴
                {
                    Console.Write(PlayerManager.Instance.CurrentPlayer.Name);
                    Console.WriteLine("사용자를 불러옵니다.");
                    // 불러오기 로직 후 바로 MainScene으로 이동
                    Console.ReadKey(true);
                    SceneManager.Instance.SetScene(SceneType.Main);
                }
                else
                {
                    Console.WriteLine("사용자를 불러오지 못했습니다.");
                    InputHelper.WaitResponse();
                    SceneManager.Instance.SetScene(SceneType.PlayerSetup);
                }
            }
            // 사용자를 새로 선택함에 따라 직업을 선택하는 로직
            else if (userName == "2")
            {
                Console.Clear();
                Console.WriteLine("사용자를 새로 만듭니다.");
                Console.WriteLine("");


                //이름 입력받아 신규 생성
                Console.Write("이름을 입력하세요: ");
                Console.ReadLine();


                string jobInput;
                while (true)
                {
                    Console.WriteLine("사용자 직업을 선택해주세요:");
                    Console.WriteLine("1. 전사(Warrior)");
                    Console.WriteLine("2. 마법사(Magician)");
                    Console.WriteLine("3. 도적(Assassin) (jobtype 미구현)");
                    Console.WriteLine("4. 궁수(Archer) (jobtype 미구현)");

                    Console.WriteLine("");
                    Console.Write("번호를 입력하세요: ");
                    jobInput = Console.ReadLine();

                    if (jobInput == "1" || jobInput == "2") // || jobInput == "3" || jobInput == "4"
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("잘못된 입력 입니다. 1~2번 중에서 선택해주세요.");
                    }
                }

                JobType selectedJob = jobInput switch
                {
                    "1" => JobType.Warrior,
                    "2" => JobType.Magician,
                    //_ => throw new NotImplementedException(), // 미구현 직업 예외처리
                };

                Console.WriteLine("");
                Console.WriteLine($"선택한 직업: {selectedJob}\n");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.1~2번 중에서 선택해주세요.");
                Console.ReadKey(true);
                return;
            }

            // 생성이 완료되면 MainScene으로 이동
            // 타이머 기능으로 · · · · 1.5초 후 메인씬으로
            Console.WriteLine("");
            Console.WriteLine("사용자를 생성중 입니다");
            Console.Write("        ");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(500);
                Console.Write(" ·");
            }
            Console.WriteLine();
            SaveManager.Instance.SaveGame();    //저장
            SceneManager.Instance.SetScene(SceneType.Main);
            return;
        }

        public override void HandleInput() //입력 받고 실행하는 시스템
        {
        }
    }
}

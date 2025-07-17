using System;
using System.Collections.Generic;
using System.Dynamic;
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
    //모든 씬이 상속 받는 추상 클래스
    internal class PlayerSetupScene : SceneBase
    {
        public override SceneType SceneType => SceneType.PlayerSetup;
        private int input;
        Player player;

        public override void DisplayScene() //출력 하는 시스템
        {
            Console.Clear();
            Console.WriteLine("PlayerSetupScene");

            Console.WriteLine("[1] 불러오기     [2] 새로만들기");
            Console.Write("번호를 입력하세요 :");

            input = InputHelper.GetIntegerRange(1, 3);

            switch (input)
            {
                case 1:
                    LoadPlayer();
                    break;
                case 2:
                    CreateNewPlayer();
                    break;
            }

            // 생성이 완료되면 MainScene으로 이동
            // 타이머 기능으로 · · · · 1.5초 후 메인씬으로
            Console.WriteLine("");
            Console.WriteLine("게임을 로드합니다");
            Console.Write("        ");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(500);
                Console.Write(" ·");
            }
            Console.WriteLine();
            //SaveManager.Instance.SaveGame();    //저장
            SceneManager.Instance.SetScene(SceneType.Main);
        }

        public override void HandleInput() //입력 받고 실행하는 시스템
        {
        }

        public void LoadPlayer()
        {
            //이름을 입력 받아 일치하는 플레이어를 불러오는 로직
            Console.Write("이름을 입력하세요: ");
            string nameInput = Console.ReadLine();

            if (PlayerManager.Instance.InitializePlayerFromSaveData(nameInput))  //데이터에서 불러옴
            {
                Console.Write(PlayerManager.Instance.CurrentPlayer.Name);
                Console.WriteLine("사용자를 불러왔습니다.");
                // 불러오기 로직 후 바로 MainScene으로 이동
                //Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine("사용자를 불러오지 못했습니다.");
                InputHelper.WaitResponse();
                SceneManager.Instance.SetScene(SceneType.PlayerSetup);
            }
        }
        public void CreateNewPlayer()
        {
            // 사용자를 새로 선택함에 따라 직업을 선택하는 로직
            Console.WriteLine("사용자를 새로 만듭니다.");
            Console.WriteLine("");


            //이름 입력받아 신규 생성
            Console.Write("이름을 입력하세요: ");
            string nameInput = Console.ReadLine();

            int jobInput;

            Console.WriteLine("사용자 직업을 선택해주세요:");
            Console.WriteLine("1. 전사(Warrior)");
            Console.WriteLine("2. 마법사(Magician)");
            Console.WriteLine("현재 불가. 도적(Assassin) (jobtype 미구현)");
            Console.WriteLine("현재 불가. 궁수(Archer) (jobtype 미구현)");

            Console.WriteLine("");
            Console.Write("번호를 입력하세요: ");
            jobInput = InputHelper.GetIntegerRange(1, 3);

            switch (jobInput)
            {
                case 1:
                    player = new Player(nameInput, JobType.Warrior);
                    break;
                case 2:
                    player = new Player(nameInput, JobType.Magician);
                    break;
            }

            PlayerManager.Instance.CurrentPlayer.Clone(player);
            Console.WriteLine(PlayerManager.Instance.CurrentPlayer.Name);
            SaveManager.Instance.SaveGame();

            Console.WriteLine("");
            Console.WriteLine($"선택한 직업: {player.JobType}\n");
        }
    }
}
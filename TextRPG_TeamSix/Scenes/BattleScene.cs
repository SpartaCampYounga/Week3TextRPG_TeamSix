using System;
using System.Collections.Generic;
using TextRPG_TeamSix.Battle;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Controllers;

namespace TextRPG_TeamSix.Scenes
{
    internal class BattleScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Battle;

        public override void DisplayScene()
        {
            Enemy enemy = new Enemy("미니언", EnemyType.Type1);
            Enemy enemy2 = new Enemy("대포미니언", EnemyType.Type1);

            Player player = new Player("SCV", JobType.Warrior);

            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    // 플레이어 정보 어딨지.
                    
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("Battle!!");
                    Console.WriteLine();
                    Console.WriteLine($"Lv2. {enemy.Name}     | {enemy.HP}");
                    Console.WriteLine($"Lv5. {enemy2.Name} | {enemy2.HP}");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine($"이름 : {player.Name}");
                    Console.WriteLine($"HP : {player.HP}/{player.HP}");
                    Console.WriteLine();
                    Console.WriteLine("1. 공격 | 2. 스킬 공격 | 3. 아이템 사용 | 4.도망");
                    break;
            }
        }

        public override void HandleInput()
        {

        }
    }
}
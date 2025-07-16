using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using TextRPG_TeamSix.Battle;
using TextRPG_TeamSix.Battle.Actions;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Skills;

namespace TextRPG_TeamSix.Scenes
{
    internal class BattleScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Battle;

        public void Character_Status(Player A)
        {
            Console.Clear();
            Console.WriteLine($"이름: {A.Name}");
            Console.WriteLine($"직업: {A.JobType}");
            Console.WriteLine($"HP: {A.HP}");
            Console.WriteLine($"MP: {A.MP}");
            Console.WriteLine($"공격력: {A.Attack}");
            Console.WriteLine($"방어력: {A.Defense}");
        }

        public override void DisplayScene()
        {
            Enemy enemy = new Enemy("미니언", EnemyType.Type1);
            Enemy enemy2 = new Enemy("대포미니언", EnemyType.Type1);
            List<Enemy> enemies = new List<Enemy> { enemy, enemy2 };

            Player player = new Player("SCV", JobType.Warrior);

            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기\n2. 전투 시작");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");



            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Character_Status(player);
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("Battle!!");

                    while (true)
                    {
                        Console.WriteLine();
                        foreach (var e in enemies)
                        {
                            string status = e.IsAlive ? $"(HP: {e.HP})" : "(죽음)";
                            Console.WriteLine($"{e.Name} {status}");
                        }

                        Console.WriteLine();
                        Console.WriteLine($"{player.Name} (HP: {player.HP}/{player.HP})");
                        Console.WriteLine();
                        Console.WriteLine("1. 공격 | 2. 스킬 공격 | 3. 아이템 사용 | 4. 도망");
                        Console.Write(">> ");

                        string battleInput = Console.ReadLine();

                        switch (battleInput)
                        {
                            case "1":
                                IPlayerAction attackAction = new NormalAttack();
                                attackAction.Execute(player, enemies);
                                break;

                            case "2":
                                Console.WriteLine("스킬 공격은 아직 구현되지 않았습니다!");
                                break;

                            case "3":
                                Console.WriteLine("아이템 사용은 아직 구현되지 않았습니다!");
                                break;

                            case "4":
                                Console.WriteLine("도망쳤습니다!");
                                return; // 전투 탈출

                            default:
                                Console.WriteLine("잘못된 입력입니다.");
                                break;
                        }

                        // 모든 적이 죽었는지 확인
                        if (enemies.TrueForAll(e => !e.IsAlive))
                        {
                            Console.WriteLine("모든 적을 처치했습니다! 🎉");
                            break;
                        }

                        Console.WriteLine();
                    }
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }

        public override void HandleInput()
        {
            // 나중에 확장용
        }
    }
}

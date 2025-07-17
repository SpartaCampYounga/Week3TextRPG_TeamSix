using System;
using System.Collections.Generic;
using System.Text;
using TextRPG_TeamSix.Battle.Actions;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Scenes;
using TextRPG_TeamSix.Skills;
using TextRPG_TeamSix.Utils;

internal class BattleScene : SceneBase
{
    public override SceneType SceneType => SceneType.Battle;

    private Player player;
    private List<Enemy> enemies;

    public void Character_Status(Player A)
    {
        Console.Clear();
        BattleLog.Log($"이름: {A.Name}");
        BattleLog.Log($"직업: {A.JobType}");
        BattleLog.Log($"HP: {A.HP}");
        BattleLog.Log($"MP: {A.MP}");
        BattleLog.Log($"공격력: {A.Attack}");
        BattleLog.Log($"방어력: {A.Defense}");
        BattleLog.Log($"행운: {A.Luck}");
    }

    public override void DisplayScene()
    {
        player = new Player("SCV", JobType.Warrior);
        enemies = new List<Enemy>
        {
            new Enemy("미니언", EnemyType.Type1),
            new Enemy("대포미니언", EnemyType.Type1)
        };

        string desc1 = "...여긴 어디지?\n공기마저 숨을 죽인 듯, 적막만이 감도는 이곳. 앞엔 낯선 문 하나가 서 있을 뿐.";
        string desc2 = "문에서 희미하게 빛이 새어 나온다… 누군가, 혹은 무언가 날 기다리고 있는 걸까?";
        string desc3 = "...좋아, 무섭긴 하지만... 이런 순간을 위해 스파르타의 수강생이 된 거잖아. 가보자.";
        string choice1 = "1. 가까이 다가간다.";
        string choice2 = "2. 버즈 - 겁쟁이 처럼 물러난다.";
        string choice2_1 = "겁쟁이 녀석...썩 물러가라";

        Console.ForegroundColor = ConsoleColor.Green;
        TextEffect.TypeEffect(desc1,70);
        Thread.Sleep(1000);

        TextEffect.TypeEffect(desc2, 70);
        Thread.Sleep(1000);

        TextEffect.TypeEffect(desc3, 70);
        Thread.Sleep(1000);

        Console.WriteLine();
        Console.Write($"{choice1}   {choice2}");

        Console.WriteLine();
        while (true)
        {
            Console.WriteLine();
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    IntroScene intro = new IntroScene();
                    intro.DisplayScene();
                    StartBattleLoop();
                    return; // 또는 break; 이후 루프 바깥에서 이어지게 할 수도 있어

                case "2":
                    Console.Clear();
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{choice2_1}");
                    Console.ReadKey();
                    MainScene main = new MainScene();
                    TextFlash.TextFlasht();
                    main.DisplayScene();
                    Console.ResetColor();
                    return;

                default:
                    Console.Clear();
                    Console.WriteLine("섣부른 행동은 금물이다 애송이...");
                    Console.WriteLine();
                    Console.Write($"{choice1}   {choice2}");
                    break;
            }
        }

    }

    private void StartBattleLoop()
    {
        Console.Clear();
        BattleLog.DrawLogBox();     // 박스 먼저 그림
        BattleLog.ClearLogs();      // 로그 내부 클리어
        BattleLog.BattleStart();    // 로그 시작 메세지 출력

        while (true)
        {
            DisplayStatus();        // UI 그리기
            string input = GetPlayerInput();

            bool playerActed = PlayerTurn(input);

            if (!player.IsAlive)
            {
                BattleLog.Death(player.Name);
                return;
            }

            if (enemies.TrueForAll(e => !e.IsAlive))
            {
                BattleLog.Victory();
                break;
            }

            if (playerActed)
            {
                EnemyTurn();
            }

            if (!player.IsAlive)
            {
                BattleLog.Death(player.Name);
                return;
            }
        }
    }


    private void DisplayStatus()
    {
        // 매 턴마다 깔끔하게 새로 출력

        BattleUI.BattleStartInfo();
        BattleUI.DrawPlayerInfo(player);
        BattleUI.DrawEnemyList(enemies);
        BattleUI.DrawActionMenu();
    }

    private string GetPlayerInput()
    {
        return Console.ReadLine();
    }

    private bool PlayerTurn(string input)
    {
        switch (input)
        {
            case "1":
                IPlayerAction attackAction = new NormalAttack();
                attackAction.Execute(player, enemies);
                return true;

            case "2":
                if (player.SkillList.Count == 0)
                {
                    BattleLog.Log("사용할 수 있는 스킬이 없습니다.");
                    return false;
                }

                BattleLog.Log("사용할 스킬을 선택하세요:");
                for (int i = 0; i < player.SkillList.Count; i++)
                {
                    var skill = player.SkillList[i];
                    BattleLog.Log($"{i + 1}. {skill.Name} (MP: {skill.ConsumeMP}) - {skill.Description}");
                }

                Console.Write(">> ");
                string skillInput = Console.ReadLine();
                if (!int.TryParse(skillInput, out int skillIndex) || skillIndex < 1 || skillIndex > player.SkillList.Count)
                {
                    BattleLog.Log("잘못된 입력입니다.");
                    return false;
                }

                Skill selectedSkill = player.SkillList[skillIndex - 1];

                if (player.MP < selectedSkill.ConsumeMP)
                {
                    BattleLog.Log("MP가 부족합니다!");
                    return false;
                }

                // 적 선택 (살아있는 적만 보여줌)
                BattleLog.Log("대상을 선택하세요:");
                List<Enemy> aliveEnemies = new List<Enemy>();
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (enemies[i].IsAlive)
                    {
                        aliveEnemies.Add(enemies[i]);
                        BattleLog.Log($"{aliveEnemies.Count}. {enemies[i].Name} (HP: {enemies[i].HP})");
                    }
                }

                if (aliveEnemies.Count == 0)
                {
                    BattleLog.Log("공격할 수 있는 대상이 없습니다.");
                    return false;
                }

                Console.Write(">> ");
                string targetInput = Console.ReadLine();
                if (!int.TryParse(targetInput, out int targetIndex) || targetIndex < 1 || targetIndex > aliveEnemies.Count)
                {
                    BattleLog.Log("잘못된 입력입니다.");
                    return false;
                }

                var target = aliveEnemies[targetIndex - 1];

                // 스킬 사용
                selectedSkill.Cast(target);
                player.ConsumeMP(selectedSkill.ConsumeMP);
                BattleLog.SkillUse(player.Name, selectedSkill.Name, target.Name);
                return true;

            case "3":
                BattleLog.Log("아이템 사용은 아직 구현되지 않았습니다!");
                return false;

            case "4":
                BattleLog.RunAway();
                Environment.Exit(0);
                return false;

            default:
                BattleLog.Log("잘못된 입력입니다.");
                return false;
        }
    }

    private void EnemyTurn()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            Enemy enemyUnit = enemies[i];
            if (!enemyUnit.IsAlive)
                continue;

            int rawDamage = (int)enemyUnit.Attack - (int)player.Defense;
            int damage = Math.Max(rawDamage, 1);
            player.TakeDamage((uint)damage);

            BattleLog.EnemyAttack(enemyUnit.Name, player.Name, damage);
        }
    }

    public override void HandleInput()
    {
        // 추후 확장
    }
}
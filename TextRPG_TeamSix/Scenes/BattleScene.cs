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
    }

    public override void DisplayScene()
    {
        player = new Player("SCV", JobType.Warrior);
        enemies = new List<Enemy>
        {
            new Enemy("미니언", EnemyType.Type1),
            new Enemy("대포미니언", EnemyType.Type1)
        };

        BattleLog.Log("스파르타 던전에 오신 여러분 환영합니다.");
        BattleLog.Log("이제 전투를 시작할 수 있습니다.");
        BattleLog.Log("");
        BattleLog.Log("1. 상태 보기\n2. 전투 시작");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");
        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Character_Status(player);
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    Console.Write(">> ");
                    string output = Console.ReadLine();
                    if (output == "0")
                    {
                        Console.Clear();
                        DisplayScene();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }

            case "2":
                IntroScene intro = new IntroScene();
                intro.DisplayScene();
                StartBattleLoop();
                break;

            default:
                BattleLog.Log("잘못된 입력입니다.");
                break;
        }
    }

    private void StartBattleLoop()
    {
        Console.Clear();
        BattleLog.BattleStart();

        while (true)
        {
            DisplayStatus();
            string input = GetPlayerInput();

            bool playerActed = PlayerTurn(input); // ← 성공한 경우에만

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
        Console.Clear(); // 매 턴마다 깔끔하게 새로 출력

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

using System;
using System.Collections.Generic;
using System.Media;
using System.Text;
using TextRPG_TeamSix.Battle.Actions;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Scenes;
using TextRPG_TeamSix.Skills;
using TextRPG_TeamSix.Utils;

internal class BattleScene : SceneBase
{
    public override SceneType SceneType => SceneType.Battle;

    private Player player;
    private List<Enemy> enemies;
    MainScene main = new MainScene();
    public override void DisplayScene()
    {
        player = PlayerManager.Instance.CurrentPlayer;

        enemies = new List<Enemy>
            {
                 GameDataManager.Instance.AllEnemies.FirstOrDefault(e => e.Id == 1),
                 GameDataManager.Instance.AllEnemies.FirstOrDefault(e => e.Id == 2)
            };

        string desc1 = "...여긴 어디지?\n공기마저 숨을 죽인 듯, 적막만이 감도는 이곳. 앞에는 낯선 문 하나가 서 있을 뿐.";
        string desc2 = "문에서 희미하게 빛이 새어 나온다… 누군가, 혹은 무언가 날 기다리고 있는 걸까?\n";
        string desc3 = "오랜만에 두근두근하는군... 이런 순간을 위해 스파르타의 수강생이 된 거긴한데... 조금은... 무섭달까?";
        string choice1 = "1. 가까이 다가간다.";
        string choice2 = "2. 겁쟁이 처럼 물러난다.";
        string choice2_1 = "겁쟁이 녀석...썩 물러가라";

        SoundManager.Play("E:\\7.Data\\1.bgm\\착신아리-오르골.wav");
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        TextEffect.TypeEffect(desc1, 70);
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
                    SoundManager.Stop();
                    Console.Clear();
                    // IntroScene intro = new IntroScene();
                    // intro.DisplayScene();
                    StartBattleLoop();
                    return; // 또는 break; 이후 루프 바깥에서 이어지게 할 수도 있어

                case "2":
                    SoundManager.Stop();
                    Console.Clear();
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{choice2_1}");
                    Console.ReadKey();
                    TextFlash.TextFlasht();
                    main.DisplayScene();
                    Console.ResetColor();
                    return;

                default:
                    Console.Clear();
                    Console.WriteLine("아니야, 지금은 눈앞에 있는것에 집중하자.");
                    Console.WriteLine();
                    Console.Write($"{choice1}   {choice2}");
                    break;
            }
        }
    }

    private void StartBattleLoop()
    {
        int turnCount = 1;

        Console.Clear();
        BattleLog.DrawLogBox();     // 박스 먼저 그림
        BattleLog.ClearLogs();      // 로그 내부 클리어
        BattleLog.BattleStart();    // 로그 시작 메세지 출력
        SoundManager.Play("E:\\7.Data\\1.bgm\\출정(mix).wav");

        while (true)
        {
            if (turnCount % 4 == 0)
            {
                BattleLog.ClearLogs();
            }

            DisplayStatus();        // UI 그리기
            Console.WriteLine();
            Console.WriteLine("────────────────────────────");
            Console.Write("어떤 행동을 하시겠습니까? : ");
            string input = GetPlayerInput();

            bool playerActed = PlayerTurn(input);

            if (!player.IsAlive)
            {
                BattleLog.Death(player.Name);
                return;
            }

            if (enemies.TrueForAll(e => !e.IsAlive))
            {
                SoundManager.Stop();
                BattleLog.Victory();

                EndBattleScene endBattleScene = new EndBattleScene(player);
                endBattleScene.DisplayScene();                         // 보상 씬 실행

                return;
            }

            if (playerActed)
            {
                EnemyTurn();
                turnCount++;
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

                Console.Write("대상을 선택하세요: >> ");
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
                SoundManager.Stop();
                BattleLog.RunAway();
                TextFlash.TextFlasht();
                main.DisplayScene();
                Console.ResetColor();
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
using System.Text;
using TextRPG_TeamSix.Battle.Actions;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Scenes;
using TextRPG_TeamSix.Utils; // BattleLog 사용

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

        // 왼쪽 정보 UI
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
                BattleLog.NoSkill();
                return false;

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

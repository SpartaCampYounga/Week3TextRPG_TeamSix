using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Dungeons;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Skills;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Scenes
{
    internal class BattleScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Battle;
        private bool isPlayerTurn = true;
        private Dungeon currentDungeon; 
        private Player currentPlayer = PlayerManager.Instance.CurrentPlayer;
        List<Enemy> aliveEnemies;
        private string title = " 던전 전투가 진행중입니다...";
        public override void DisplayScene()
        {
            currentDungeon = PlayerManager.Instance.CurrentDungeon;
            if (currentDungeon == null)
            { currentDungeon.Clone(GameDataManager.Instance.AllDungeons[0]); }
            else { Console.WriteLine("던전이 없음.."); }
            title = currentDungeon.Name + " 던전 전투가 진행중입니다...";

            Console.OutputEncoding = Encoding.UTF8;


            while (currentDungeon.Enemies.Any(x => x.IsAlive == true))
            {
                if (isPlayerTurn)
                {
                    PlayerTurn();
                }
                else
                {
                    EnemyTurn();
                }
            };

            //모두 죽여서 탈출 보상 받아야함.
            GetDungeonClearReward();

            //스페셜 상점


            SceneManager.Instance.SetScene(SceneType.Main);
        }

        public override void HandleInput()
        {
            throw new NotImplementedException();
        }
        public int DisplayBattleInformation(bool selectingEnemies, List<Enemy> aliveEnemies)  //아.. 이거 맘에 안듬...ㅠ 나중에 시간 나면 수정하겟슴다.
        {
            int input;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(currentPlayer.DisplayPlayerStatusInBattle());
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("  " + FormatUtility.AlignLeftWithPadding("현재 남아있는 적들", Console.WindowWidth - 2) + "  ");

            Console.WriteLine();

            if (selectingEnemies == true)
            {
                foreach (Enemy enemy in currentDungeon.Enemies.Where(x => x.IsAlive == false))
                {
                    //Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - currentDungeon.Enemies.Count() - 2);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("  " + enemy);
                    Console.ResetColor();
                    //Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 2);
                }
                input = TextDisplayer.SelectNavigation(aliveEnemies);

            }
            else
            {
                foreach (Enemy enemy in currentDungeon.Enemies)
                {
                    Console.ForegroundColor = enemy.IsAlive == false ? ConsoleColor.DarkGray : ConsoleColor.White;
                    Console.WriteLine("  " + enemy);
                    Console.ResetColor();
                }
                input = -0;
            }
            return input;
            
        }
        public void PlayerTurn()
        {
            int input;

            FormatUtility.DisplayHeader(title);
            DisplayBattleInformation(false, currentDungeon.Enemies);
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("당신의 차례입니다. ");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("행동을 선택하세요.");
            List<string> selection = new List<string>()
            {
                "일반 공격",    //0
                "스킬",       //1
                "아이템",      //2
                "도망가기"      //3
            };

            Console.WriteLine();
            input = TextDisplayer.SelectNavigation(selection);
            int enemySelection;
            switch (input)
            {
                case 0: //일반 공격
                    NormalAttackByPlayer();
                    break;
                case 1: //스킬사용
                    SkillAttackByPlayer();
                    break;
                case 2: //아이템 사용
                    ConsumeItemByPlayer();
                    break;
                case 3: //도망가기
                case -1:
                    Console.WriteLine("이건 그냥 후퇴가 아니다. 이보 전진을 위한 일보 후퇴일 뿐이다..");
                    InputHelper.WaitResponse();
                    SceneManager.Instance.SetScene(SceneType.Dungeon);
                    break;

            }
            aliveEnemies = currentDungeon.Enemies.Where(x => x.IsAlive == true).ToList();
            isPlayerTurn = false;
        }

        public void NormalAttackByPlayer()
        {
            aliveEnemies = currentDungeon.Enemies.Where(x => x.IsAlive == true).ToList();
            currentDungeon.OrderEnemiesByIsAlive();   //죽은 애들 아래로 내리기..
            FormatUtility.DisplayHeader(title);
            int enemySelection = DisplayBattleInformation(true, aliveEnemies);

            if(enemySelection == -1)
            {
                //선택안하고 뒤로 돌아오면 다른 선택지..
                PlayerTurn();
                return;
            }

            Enemy targetEnemy = aliveEnemies[enemySelection];
            uint damage = currentPlayer.GetNormalAttackDamage(targetEnemy);

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"{targetEnemy.Name}(이)가 {damage} 데미지를 받았다!");
            Console.ResetColor();
            targetEnemy.Damaged(damage);

            InputHelper.WaitResponse();
        }

        public void SkillAttackByPlayer()
        {
            List<Skill> currentSkills = currentPlayer.SkillList;
            if (currentSkills.Count == 0)
            {
                Console.WriteLine("현재 사용할 수 있는 스킬이 없다..");
                InputHelper.WaitResponse();
                PlayerTurn();
            }
            else
            {

                Console.WriteLine("현재 사용할 수 있는 스킬들이다..");
                Console.WriteLine();
                int skillSelection = TextDisplayer.SelectNavigation(currentSkills);
                if (skillSelection == -1)
                {
                    //선택안하고 뒤로 돌아오면 다른 선택지..
                    PlayerTurn();
                }
                else
                {
                    //몬스터 선택
                    aliveEnemies = currentDungeon.Enemies.Where(x => x.IsAlive == true).ToList();
                    currentDungeon.OrderEnemiesByIsAlive();   //죽은 애들 아래로 내리기..
                    FormatUtility.DisplayHeader(title);

                    int enemySelection = DisplayBattleInformation(false, aliveEnemies);

                    Skill selectedSkill = currentSkills[skillSelection];

                    if (selectedSkill is HealSkill healSkill)
                    {
                        healSkill.Cast(currentPlayer);
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine(healSkill.Name + "을 사용했다.");
                    }
                    else
                    {
                        enemySelection = DisplayBattleInformation(true, aliveEnemies);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"{aliveEnemies[enemySelection].Name}에게 스킬을 사용했다.");
                        Console.ResetColor();
                        selectedSkill.Cast(aliveEnemies[enemySelection]);
                    }

                    InputHelper.WaitResponse();
                }

            }
        }
        public void ConsumeItemByPlayer()
        {
            List<IConsumable> consumables = currentPlayer.Inventory.ItemList.OfType<IConsumable>().ToList(); ;
            if (consumables.Count == 0)
            {
                Console.WriteLine("현재 사용할 수 있는 아이템이 없다.. ");
                InputHelper.WaitResponse();
                PlayerTurn();
            }
            else
            {
                Console.WriteLine("현재 사용할 수 있는 아이템들이다.. ");
                Console.WriteLine();
                int itemSelection = TextDisplayer.SelectNavigation(consumables);
                if (itemSelection == -1)
                {   //선택안하고 뒤로 돌아오면 다른 선택지..
                    PlayerTurn();
                }
                else
                {
                    consumables[itemSelection].Consume(currentPlayer, currentPlayer.Inventory.ItemList);
                }
            }
        }
        public void EnemyTurn()
        {
            FormatUtility.DisplayHeader(title);
            DisplayBattleInformation(false, currentDungeon.Enemies);
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("적의 차례입니다. ");
            Console.ResetColor();
            InputHelper.WaitResponse();

            foreach (Enemy enemy in aliveEnemies)
            {
                uint damage = enemy.GetNormalAttackDamage(currentPlayer);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{enemy.Name}으로부터 {damage} 데미지를 받았다!");
                Console.ResetColor();

                currentPlayer.Damaged(damage);

                if (currentPlayer.HP <= 0)
                {
                    Console.WriteLine("죽었다....");
                    InputHelper.WaitResponse();

                    SceneManager.Instance.SetScene(SceneType.Main);
                }

                InputHelper.WaitResponse();
                Console.WriteLine();
            }

            isPlayerTurn =true;
        }
        public void GetDungeonClearReward()
        {
            Console.Clear();
            FormatUtility.DisplayHeader(title);
            PlayerManager.Instance.ClearedDungeonList.Add(currentDungeon.Id);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n던전을 클리어했습니다!");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"보상으로 {currentDungeon.RewardGold} 골드와 {currentDungeon.RewardExp} 경험치를 획득했습니다.");
            Console.ResetColor();

            //이 보상들은 던전 클래스에서 ClearedDungeon 메소드로 처리하면 좋을 것 같음. (일단 시간 부족)

            //퀘스트 카운트
            PlayerManager.Instance.AcceptedQuestList
                .Where(x => x.QuestType == QuestType.Dungeon && x.GoalId == currentDungeon.Id)
                .ToList()
                .ForEach(x => x.CountGoal());
            //던전 보상
            currentPlayer.EarnGold(currentDungeon.RewardGold);
            currentPlayer.EarnExp(currentDungeon.RewardExp);
            if (currentDungeon.RewardGatcha != null)
            {   //현재 소지중인 아이템과 중복하여 획득할 수 있는 상황... =>  처리함.

                Item rewardItem = currentDungeon.RewardGatcha.GetItem();
                if (rewardItem != null)
                {
                    Console.WriteLine();
                    if (currentPlayer.Inventory.ItemList.FirstOrDefault(x => x.Id == rewardItem.Id) == null)
                    {
                        currentPlayer.Inventory.AddItem(rewardItem.Id);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"보상으로 {rewardItem.Name}을 획득했습니다.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"보상으로 {rewardItem.Name}을 획득할 수 없습니다.)");
                        Console.ResetColor();
                        Console.WriteLine("이미 소지중인 아이템입니다.");
                    }
                }
                else
                {
                    Console.WriteLine("보상으로 아이템을 획득하지 못했습니다.");
                    Console.WriteLine("꽝을 뽑았습니다.");
                }
            }
            Console.ResetColor();
            InputHelper.WaitResponse();
        }
    }
}

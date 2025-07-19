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
            //모두 죽여서 탈출

            SceneManager.Instance.SetScene(SceneType.Main);
        }

        public override void HandleInput()
        {
            throw new NotImplementedException();
        }
        public int DisplayBattleInformation(bool selectingEnemies, List<Enemy> aliveEnemies)  //아.. 이거 맘에 안듬...ㅠ 나중에 시간 나면 수정.
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
            Console.WriteLine("당신의 차례입니다. ");
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
                    aliveEnemies = currentDungeon.Enemies.Where(x => x.IsAlive == true).ToList();
                    currentDungeon.OrderEnemiesByIsAlive();   //죽은 애들 아래로 내리기..
                    FormatUtility.DisplayHeader(title);
                    enemySelection = DisplayBattleInformation(true, aliveEnemies);

                    Enemy targetEnemy = aliveEnemies[enemySelection];
                    uint damage = currentPlayer.GetNormalAttackDamage();
                    targetEnemy.Damaged(damage);

                    Console.WriteLine($"{targetEnemy.Name}(이)가 {damage} 데미지를 받았다!");
                    InputHelper.WaitResponse();
                    break;
                case 1: //스킬사용
                    List<Skill> currentSkills = currentPlayer.SkillList;
                    if(currentSkills.Count == 0)
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

                            enemySelection = DisplayBattleInformation(false, aliveEnemies);

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
                                selectedSkill.Cast(aliveEnemies[enemySelection]);
                                Console.WriteLine($"{aliveEnemies[enemySelection].Name}에게 스킬을 사용했다.");
                            }

                            InputHelper.WaitResponse();
                        }

                    }
                        break;
                case 2: //아이템 사용
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

        public void EnemyTurn()
        {
            FormatUtility.DisplayHeader(title);
            DisplayBattleInformation(false, currentDungeon.Enemies);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("적의 차례입니다. ");
            Console.WriteLine();
            Console.WriteLine();

            foreach (Enemy enemy in aliveEnemies)
            {
                uint damage = enemy.GetNormalAttackDamage();
                currentPlayer.Damaged(damage);
                if(currentPlayer.HP <= 0)
                {
                    Console.WriteLine("죽었다....");
                    InputHelper.WaitResponse();

                    SceneManager.Instance.SetScene(SceneType.Main);
                }

                Console.WriteLine($"{enemy.Name}으로부터 {damage} 데미지를 받았다!");
                InputHelper.WaitResponse();
                Console.WriteLine();
            }

            isPlayerTurn =true;
        }
    }
}

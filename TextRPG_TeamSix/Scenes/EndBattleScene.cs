using System;
using System.Collections.Generic;
using System.Threading;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Items;

namespace TextRPG_TeamSix.Scenes
{
    internal class EndBattleScene : SceneBase
    {
        public override SceneType SceneType => SceneType.EndBattle;

        private Player player;
        private string[] boxStates = { "□", "□", "□" };
        private bool[] isOpened = { false, false, false };
        private Random random = new Random();

        public EndBattleScene(Player player)
        {
            this.player = player;
        }

        public override void DisplayScene()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n전투에서 승리하셨습니다!");
            Console.WriteLine("보상 상자를 선택하세요!\n");
            Console.ResetColor();

            DrawBoxes();

            while (true)
            {
                Console.Write("\n상자를 선택하세요 (1~3): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 3)
                {
                    if (isOpened[choice - 1])
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("이미 열린 상자입니다.");
                        Console.ResetColor();
                        continue;
                    }

                    OpenBox(choice - 1);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못된 입력입니다. 1~3 숫자를 입력하세요.");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("\n아무 키나 누르면 메인 화면으로 돌아갑니다...");
            Console.ReadKey();
            SceneManager.Instance.SetScene(SceneType.Main);
        }

        private void DrawBoxes()
        {
            string top = "┌─────┐ ┌─────┐ ┌─────┐";
            string mid = $"│  {GetBoxSymbol(0)}  │ │  {GetBoxSymbol(1)}  │ │  {GetBoxSymbol(2)}  │";
            string bot = "└─────┘ └─────┘ └─────┘";
            string label = "   ①       ②       ③";

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("       [ 보상 상자 ]");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("    " + top);
            Console.WriteLine("    " + mid);
            Console.WriteLine("    " + bot);
            Console.WriteLine("    " + label);
        }

        private string GetBoxSymbol(int index)
        {
            return isOpened[index] ? "■" : "□";
        }

        private void OpenBox(int index)
        {
            isOpened[index] = true;
            boxStates[index] = "■";
            Console.Clear();

            DrawBoxes();
            Console.WriteLine();

            int roll = random.Next(100);
            if (roll < 40)
            {
                int goldAmount = random.Next(30, 100);
                player.EarnGold((uint)goldAmount);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{goldAmount}골드를 획득했습니다!");
            }
            else
            {
                var items = GameDataManager.Instance.AllItems;
                Item rewardItem = items[random.Next(items.Count)];
                player.Inventory.ItemList.Add(rewardItem);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{rewardItem.Name}을(를) 획득했습니다!");
            }

            Console.ResetColor();
        }

        public override void HandleInput()
        {
        }
    }
}

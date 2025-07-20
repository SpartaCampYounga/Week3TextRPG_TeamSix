using System;
using System.Collections.Generic;
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
    // 던전 진입 전에 포션 사용하라는 가이드가 있었으나...
    // 1. 포션은 개당 하나 밖에 소지 못하는 시스템을 이미 구축함
    // 2. 그렇다면 포션을 던전 진입 전에 사용하는 것보다는 아이템 창에서 소비 후 상점을 왔다갔다 하는 게 맞는 것 같음
    // 3. 하지만 아이템에선 이미 담당자 분이 공들여 소비아이템 사용 못하게 작업 하셨고...
    // 4. 그래서 포션보다 비싼 비용이라면? 휴식 시스템을 만들어 체력 만땅인 시스템이 조금 더 납득 가능한 전개라고 생각하여 개발.
    internal class RestScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Rest;

        Player player;
        int input;

        public override void DisplayScene()
        {
            player = PlayerManager.Instance.CurrentPlayer;
            FormatUtility.DisplayHeader("휴식을 취하여 회복할 수 있습니다.");
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(FormatUtility.AlignCenterWithPadding($"보유 골드: {player.Gold} G", Console.WindowWidth - 6));
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(FormatUtility.AlignCenterWithPadding(player.DisplayPlayerStatusInBattle(), Console.WindowWidth - 6));
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(FormatUtility.AlignCenterWithPadding("이미 체력과 마나가 가득 차 있어도 휴식을 취하면 골드는 소모됩니다.", Console.WindowWidth - 6));
            Console.ResetColor();
            Console.WriteLine();

            List<string> selections = new List<string>()
                {
                    "체력 회복하기 (500G)",  //0
                    "마나 회복하기 (500G)",  //1
                    "모두 회복하기 (800G)" //2
                };
            input = TextDisplayer.SelectNavigation(selections);
            HandleInput();
        }

        public override void HandleInput()
        {
            if(input == -1)
            {
                SceneManager.Instance.SetScene(SceneType.Main);
                return;
            }
            else
            {
                uint price = 0;
                RestoreType restoreType = RestoreType.None;

                switch (input)  //input은 지금 0, 1, 2로만 들어옴
                {
                    case 0:
                        price = 500;
                        restoreType = RestoreType.Health;
                        break;
                    case 1:
                        price = 500;
                        restoreType = RestoreType.Mana;
                        break;
                    case 2:
                        price = 800;
                        restoreType = RestoreType.All;
                        break;
                }

                if (player.SpendGold(price)) Restore(restoreType);
                SceneManager.Instance.SetScene(SceneType.Rest);
                return;
            }
        }

        //public bool IsEnoughGold(int gold)
        //{
        //    if (player.Gold >= gold)
        //    {
        //        player.EarnGold(-gold);
        //        return true;
        //    }
        //    else
        //    {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine("골드가 부족합니다.");
        //        Console.ResetColor();
        //        InputHelper.WaitResponse();
        //        return false;
        //    }
        //}
        public void Restore(RestoreType restoreType)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            switch (restoreType)
            {
                case RestoreType.Health:
                    player.HealedHP(player.MaxHP);
                    Console.WriteLine("체력이 모두 회복되었습니다.");
                    break;
                case RestoreType.Mana:
                    player.RecoveredMP(player.MaxMP);
                    Console.WriteLine("마나가 모두 회복되었습니다.");
                    break;
                case RestoreType.All:
                    player.HealedHP(player.MaxHP);
                    player.RecoveredMP(player.MaxMP);
                    Console.WriteLine("체력과 마나가 모두 회복되었습니다.");
                    break;

            }
            Console.ResetColor();
            InputHelper.WaitResponse();
        }

        public void FaildRestore()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("소지 골드가 부족하여 휴식을 취하지 못했습니다.");
            Console.ResetColor();
            InputHelper.WaitResponse();
        }
    }
}

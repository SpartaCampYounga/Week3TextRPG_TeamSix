using System;
using System.Collections.Generic;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Utils; // BattleLog

namespace TextRPG_TeamSix.Battle.Actions
{
    internal class NormalAttack : IPlayerAction
    {
        public void Execute(Player player, List<Enemy> enemies)
        {
            Console.WriteLine(); // 메뉴랑 간격
            Console.WriteLine("공격할 대상을 선택하세요:");
            for (int i = 0; i < enemies.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {enemies[i].Name} (HP: {enemies[i].HP})");
            }

            Console.Write(">> ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int index) && index >= 1 && index <= enemies.Count)
            {
                Enemy target = enemies[index - 1];
                uint playerDamage = player.Attack;

                // 로그는 BattleLog로 출력
                BattleLog.PlayerAttack(player.Name, target.Name, (int)playerDamage);
                target.TakeDamage(playerDamage);

                if (!target.IsAlive)
                {
                    BattleLog.Death(target.Name);
                }
            }
            else
            {
                BattleLog.Log("❗ 잘못된 입력입니다. 다시 시도해주세요!");
            }
        }
    }
}

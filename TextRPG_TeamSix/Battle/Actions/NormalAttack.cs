using System;
using System.Collections.Generic;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Utils;

namespace TextRPG_TeamSix.Battle.Actions
{
    internal class NormalAttack : IPlayerAction
    {
        public void Execute(Player player, List<Enemy> enemies)
        {
            Console.WriteLine();
            Console.WriteLine("공격할 대상을 선택하세요:");
            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy enemy = enemies[i];
                Console.WriteLine($"{i + 1}. {enemy.Name}");
            }

            Console.Write(">> ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int index) && index >= 1 && index <= enemies.Count)
            {
                Enemy target = enemies[index - 1];

                uint playerDamage = player.Attack;
                target.TakeDamage(playerDamage); // ✅ 체력 감소 먼저!

                BattleLog.PlayerAttack(player.Name, target.Name, (int)playerDamage); // 로그 출력

                // ✅ 감소된 체력 바로 출력
                Console.WriteLine($"{target.Name}의 남은 HP: {target.HP}");

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

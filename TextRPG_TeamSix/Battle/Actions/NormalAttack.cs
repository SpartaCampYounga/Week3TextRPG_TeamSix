using System;
using System.Collections.Generic;
using TextRPG_TeamSix.Characters;

namespace TextRPG_TeamSix.Battle.Actions
{
    internal class NormalAttack : IPlayerAction
    {
        public void Execute(Player player, List<Enemy> enemies)
        {
            Console.WriteLine("공격할 대상을 선택하세요:");

            // 적 목록 출력
            for (int i = 0; i < enemies.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {enemies[i].Name} (HP: {enemies[i].HP})");
            }

            Console.Write(">> ");
            string input = Console.ReadLine();

            // 입력이 숫자이고, 유효한 범위인지 확인
            if (int.TryParse(input, out int index) && index >= 1 && index <= enemies.Count)
            {
                Enemy target = enemies[index - 1];
                uint playerDamage = player.Attack;

                Console.WriteLine($"{player.Name}이(가) {target.Name}을(를) 공격합니다!");

                // 체력 감소는 TakeDamage() 메서드에서 처리
                target.TakeDamage(playerDamage);

                Console.WriteLine($"{target.Name}에게 {playerDamage} 데미지를 입혔습니다!");

                if (!target.IsAlive)
                {
                    Console.WriteLine($"{target.Name}은(는) 쓰러졌습니다!");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요!");
            }
        }
    }
}

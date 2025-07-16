using System;
using System.Collections.Generic;
using TextRPG_TeamSix.Characters;

namespace TextRPG_TeamSix.Battle.Actions
{
    internal class SkillAttack : IPlayerAction
    {
        public void Execute(Player player, List<Enemy> enemies)
        {
            Console.WriteLine("스킬을 사용합니다. (기본적으로 1.5배 데미지)");

            for (int i = 0; i < enemies.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {enemies[i].Name} (HP: {enemies[i].HP})");
            }

            Console.Write(">> ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= enemies.Count)
            {
                Enemy target = enemies[index - 1];

                uint damage = (uint)(player.Attack * 1.5);
                //target.HP = target.HP > damage ? target.HP - damage : 0;

                Console.WriteLine($"{player.Name}이(가) 스킬로 {target.Name}을(를) 공격! {damage}의 피해!");
                if (target.HP == 0)
                    Console.WriteLine($"{target.Name}은(는) 쓰러졌습니다!");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}

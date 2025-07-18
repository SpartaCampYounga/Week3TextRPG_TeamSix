using System;
using System.Collections.Generic;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Utils;
using TextRPG_TeamSix.Skills;

namespace TextRPG_TeamSix.Battle.Actions
{
    internal class SkillAttack : IPlayerAction
    {
        public void Execute(Player player, List<Enemy> enemies)
        {
            if (player.SkillList.Count == 0)
            {
                Console.WriteLine(); // 간격
                Console.WriteLine("사용할 수 있는 스킬이 없습니다!");
                return;
            }

            // 1. 스킬 선택
            Console.WriteLine();
            Console.WriteLine("사용할 스킬을 선택하세요:");
            for (int i = 0; i < player.SkillList.Count; i++)
            {
                Skill skill = player.SkillList[i];
                Console.WriteLine($"{i + 1}. {skill.Name} (MP: {skill.ConsumeMP}) - {skill.Description}");
            }

            Console.Write(">> ");
            string inputSkill = Console.ReadLine();

            if (!int.TryParse(inputSkill, out int skillIndex) || skillIndex < 1 || skillIndex > player.SkillList.Count)
            {
                Console.WriteLine("❗ 잘못된 입력입니다. 다시 시도해주세요!");
                return;
            }

            Skill selectedSkill = player.SkillList[skillIndex - 1];

            // 2. MP 체크
            if (player.MP < selectedSkill.ConsumeMP)
            {
                Console.WriteLine("❗ MP가 부족합니다!");
                return;
            }

            // 3. 대상 선택
            List<Enemy> aliveEnemies = enemies.FindAll(e => e.IsAlive);
            if (aliveEnemies.Count == 0)
            {
                Console.WriteLine("공격할 적이 없습니다!");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("공격할 대상을 선택하세요:");
            for (int i = 0; i < aliveEnemies.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {aliveEnemies[i].Name} (HP: {aliveEnemies[i].HP})");
            }

            Console.Write(">> ");
            string inputTarget = Console.ReadLine();

            if (!int.TryParse(inputTarget, out int targetIndex) || targetIndex < 1 || targetIndex > aliveEnemies.Count)
            {
                Console.WriteLine("❗ 잘못된 입력입니다. 다시 시도해주세요!");
                return;
            }

            Enemy target = aliveEnemies[targetIndex - 1];

            // 4. 스킬 실행
            player.ConsumeMP(selectedSkill.ConsumeMP);

            // 스킬 효과 적용 — 직접 데미지 처리
            int skillDamage = (int)(player.Attack + selectedSkill.Power);
            target.TakeDamage((uint)skillDamage);
            player.ConsumeMP(selectedSkill.ConsumeMP);

            BattleLog.SkillUse(player.Name, selectedSkill.Name, target.Name);
            BattleLog.Log($"💥 {target.Name}에게 {skillDamage}의 피해를 입혔습니다!");

            // 5. 로그 출력
            BattleLog.Log($"🔥 {player.Name}이(가) {target.Name}에게 {selectedSkill.Name}을(를) 사용했다! (데미지: {skillDamage})");

            if (!target.IsAlive)
            {
                BattleLog.Death(target.Name);
            }
        }
    }
}

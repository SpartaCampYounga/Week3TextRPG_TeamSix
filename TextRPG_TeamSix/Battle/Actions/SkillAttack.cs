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
                BattleLog.Log("사용할 수 있는 스킬이 없습니다!");
                return;
            }

            // 1. 스킬 선택
            BattleLog.Log("사용할 스킬을 선택하세요:");
            for (int i = 0; i < player.SkillList.Count; i++)
            {
                Skill skill = player.SkillList[i];
                BattleLog.Log($"{i + 1}. {skill.Name} (MP: {skill.ConsumeMP}) - {skill.Description}");
            }

            Console.Write(">> ");
            if (!int.TryParse(Console.ReadLine(), out int skillIndex) || skillIndex < 1 || skillIndex > player.SkillList.Count)
            {
                BattleLog.Log("잘못된 입력입니다.");
                return;
            }

            Skill selectedSkill = player.SkillList[skillIndex - 1];

            // 2. MP 확인
            if (player.MP < selectedSkill.ConsumeMP)
            {
                BattleLog.Log("MP가 부족합니다!");
                return;
            }

            // 3. 대상 선택 (살아있는 적)
            List<Enemy> aliveEnemies = enemies.FindAll(e => e.IsAlive);
            if (aliveEnemies.Count == 0)
            {
                BattleLog.Log("공격할 적이 없습니다!");
                return;
            }

            BattleLog.Log("대상을 선택하세요:");
            for (int i = 0; i < aliveEnemies.Count; i++)
            {
                var e = aliveEnemies[i];
                BattleLog.Log($"{i + 1}. {e.Name} (HP: {e.HP})");
            }

            Console.Write(">> ");
            if (!int.TryParse(Console.ReadLine(), out int targetIndex) || targetIndex < 1 || targetIndex > aliveEnemies.Count)
            {
                BattleLog.Log("잘못된 입력입니다.");
                return;
            }

            Character target = aliveEnemies[targetIndex - 1];

            // 4. 스킬 실행
            player.ConsumeMP(selectedSkill.ConsumeMP);
            //selectedSkill.Use(player, target);

            BattleLog.Log($"🔥 {player.Name}이(가) {target.Name}에게 {selectedSkill.Name}을(를) 사용했다!");
        }
    }
}

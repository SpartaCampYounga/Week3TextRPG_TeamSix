using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;

namespace TextRPG_TeamSix.Skills
{
    internal class AttackSkill : Skill
    {
        public AttackSkill(uint id, string name, string description, uint consumeMP, uint requiredStones, SkillType skillType, uint amount) : 
            base(id, name, description, consumeMP, requiredStones, skillType, amount)
        {

        }

        public override void Cast(Character opponent)
        {
            Player player = PlayerManager.Instance.CurrentPlayer;
            if (player.MP >= ConsumeMP)
            {
                //스킬 구현
                player.ConsumeMP(ConsumeMP);
                opponent.Damaged(Amount);
            }
            else
            {
                //스킬 구현 불가능.
                Console.WriteLine("MP가 부족하여 스킬이 취소됩니다.");
            }
        }
    }
}

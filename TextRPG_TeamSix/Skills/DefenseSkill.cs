using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Characters;

namespace TextRPG_TeamSix.Skills
{
    internal class DefenseSkill : Skill
    {
        public DefenseSkill(uint id, string name, string description, uint consumeMP, uint requiredStones, SkillType skillType, uint amount) : base(id, name, description, consumeMP, requiredStones, skillType, amount)
        {
        }

        public override void Cast(Character opponent)
        {
            //스킬 로직 구현
        }
    }
}

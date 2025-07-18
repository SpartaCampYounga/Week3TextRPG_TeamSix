using Newtonsoft.Json;
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
    internal class DefenseSkill : Skill
    {
        [JsonConstructor]
        public DefenseSkill(uint id, string name, string description, uint consumeMP, uint requiredStones, SkillType skillType, uint amount) : base(id, name, description, consumeMP, requiredStones, skillType, amount)
        {
        }

        public override void Cast(Character opponent)
        {
            //스킬 로직 구현 //Defense Skill 어떻게 구현할 지 정해질때까지 보류... 없으면 삭제.
        }
        public override void Clone(uint skillId)
        {
            //복사할 스킬
            Skill skill = GameDataManager.Instance.AllSkills.FirstOrDefault(x => x.Id == skillId);

            if (skill == null)
            {
                // 없을때.
            }
            else
            {
                Id = skill.Id;
                Name = skill.Name;
                Description = skill.Description;
                ConsumeMP = skill.ConsumeMP;
                RequiredStones = skill.RequiredStones;
                SkillType = skill.SkillType;
                Amount = skill.Amount;
            }
        }
    }
}

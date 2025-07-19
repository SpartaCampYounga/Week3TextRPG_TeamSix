using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Controllers;
using System.Text.Json.Serialization;

namespace TextRPG_TeamSix.Skills
{
    internal class HealSkill : Skill
    {
        [JsonConstructor]
        public HealSkill(uint id, string name, string description, uint consumeMP, uint requiredStones, SkillType skillType, uint amount)
            : base(id, name, description, consumeMP, requiredStones, skillType, amount)
        {
        }

        public override bool Cast(Character opponent)
        {
            Player player = PlayerManager.Instance.CurrentPlayer;
            if(player.MP >= ConsumeMP)
            {
                //스킬 구현
                player.ConsumeMP(ConsumeMP);
                opponent.HealedHP(Amount);
                Console.WriteLine($"{player.Name}의 체력이 {Amount}만큼 회복되었다!");
                return true;
            }
            else 
            {
                //스킬 구현 불가능.
                Console.WriteLine("MP가 부족하여 스킬이 취소됩니다.");
                return false;
            }
        }

        public override void Clone(uint skillId)
        {
            //복사할 스킬
            Skill skill = GameDataManager.Instance.AllSkills.FirstOrDefault(x => x.Id == skillId);

            if (skill == null)
            {
                Console.WriteLine($"{skillId}를 갖는 Skill을 찾을 수 없습니다.");
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

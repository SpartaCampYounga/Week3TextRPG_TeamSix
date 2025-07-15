using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Characters;

namespace TextRPG_TeamSix.Skills
{
    internal abstract class Skill
    {
        public uint Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public uint ConsumeMP { get; protected set; }
        public uint RequiredStones { get; protected set; }
        public SkillType SkillType { get; protected set; }
        public uint Amount { get; protected set; }

        public Skill(uint id, string name, string description, uint consumeMP, uint requiredStones, SkillType skillType, uint amount) 
        { 
            Id = id;
            Name = name;
            Description = description;
            ConsumeMP = consumeMP;
            RequiredStones = requiredStones;
            SkillType = skillType;
            Amount = amount;            
        }
        public abstract void Cast(Character opponent);
    }
}

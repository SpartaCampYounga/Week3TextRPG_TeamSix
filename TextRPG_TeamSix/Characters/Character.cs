using Newtonsoft.Json;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Skills;

namespace TextRPG_TeamSix.Characters
{
    //status 갖고 있는 모든 종류의 캐릭터가 상속 받는 추상 클래스.
    internal abstract class Character
    {
        private static uint nextId = 0;

        [JsonProperty]
        protected uint id;
        [JsonProperty]
        protected string name;
        [JsonProperty]
        protected uint hP;
        [JsonProperty]
        protected uint mP;
        [JsonProperty]
        protected uint attack;
        [JsonProperty]
        protected uint defense;
        [JsonProperty]
        protected List<Skill> skillList = new List<Skill>();
        [JsonProperty]
        protected bool isAlive;

        [JsonIgnore]
        public uint Id => id;
        [JsonIgnore]
        public string Name => name;
        [JsonIgnore]
        public uint HP => hP;
        [JsonIgnore]
        public uint MP => mP;
        [JsonIgnore]
        public uint Attack => attack;
        [JsonIgnore]
        public uint Defense => defense;
        [JsonIgnore]
        public List<Skill> SkillList => skillList;
        [JsonIgnore]
        public bool IsAlive => isAlive;
        public Character(string name)
        {
            id = nextId++;
            this.name = name;
            isAlive = true;
        }
        public void TakeDamage(uint damage)
        {
            if (HP > damage)
            {
                isAlive = true;
                hP -= damage;
            }
            else
            {
                hP = 0;
                isAlive = false;
            }
        }

        //스킬 구현 
        public void ConsumeMP(uint MP)
        {
            mP -= MP;
        }
        public void Healed(uint HP)
        {
            hP += HP;
        }
        public void Damaged(uint damage)
        {
            hP -= damage;
        }
    }
}

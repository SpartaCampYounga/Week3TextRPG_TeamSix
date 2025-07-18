using Newtonsoft.Json;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Quests;
using TextRPG_TeamSix.Skills;

namespace TextRPG_TeamSix.Characters
{
    //status 갖고 있는 모든 종류의 캐릭터가 상속 받는 추상 클래스.
    internal abstract class Character
    {
        private static uint nextId = 0;
        public uint Id { get; protected set; }
        public string Name { get; protected set; }
        public uint HP { get; protected set; }
        public uint MP { get; protected set; }
        public uint Attack { get; protected set; }
        public uint Defense { get; protected set; }
        public uint Luck { get; protected set; }
        public List<Skill> SkillList { get; protected set; } = new List<Skill>();
        public bool IsAlive { get; protected set; }

        public Character(string name)
        {
            Id = nextId++;
            Name = name;
            IsAlive = true;
        }
        [JsonConstructor]
        public Character(
            uint id,
            string name,
            uint hP,
            uint mP,
            uint attack,
            uint defense,
            uint luck)
        {
            this.Id = id;
            this.Name = name;
            this.HP = hP;
            this.MP = mP;
            this.Attack = attack;
            this.Defense = defense;
            this.Luck = luck;
        }
        public void TakeDamage(uint damage)
        {
            HP = Math.Max(0, HP > damage ? HP - damage : 0);
            IsAlive = HP > 0;
        }

        //스킬 구현 
        public void ConsumeMP(uint MP)
        {
            this.MP -= MP;
        }
        public void Healed(uint HP)
        {
            this.HP += HP;
        }
        public void Damaged(uint damage)
        {
            if (HP <= damage)
            {
                HP = 0;
                IsAlive = false;
            }
            else
            {
                HP -= damage;
            }
        }
    }
}

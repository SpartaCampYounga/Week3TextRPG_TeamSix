using TextRPG_TeamSix.Controllers;
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
        public List<Skill> SkillList { get; protected set; } = new List<Skill>(); 
        public bool IsAlive { get; protected set; }

        public Character(string name)
        {
            Id = nextId++;
            Name = name;
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
            this.HP -= damage;
        }
    }
}

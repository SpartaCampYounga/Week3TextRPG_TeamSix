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
        public uint MaxHP { get; protected set; } // 최대 체력
        public uint MaxMP { get; protected set; } // 최대 마나
        public List<Skill> SkillList { get; protected set; } = new List<Skill>();
        public bool IsAlive { get; protected set; }
        protected Character() { }
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
            uint luck,
            uint maxHp,
            uint maxMp)
        {
            this.Id = id;
            this.Name = name;
            this.HP = hP;
            this.MP = mP;
            this.Attack = attack;
            this.Defense = defense;
            this.Luck = luck;
            this.IsAlive = true;
            this.MaxHP = maxHp;
            this.MaxMP = maxMp;
        }
        //public void TakeDamage(uint damage)
        //{
        //    HP = Math.Max(0, HP > damage ? HP - damage : 0);
        //    IsAlive = HP > 0;
        //    if (IsAlive == false && this is Enemy enemy)
        //    {
        //        foreach (Quest quest in PlayerManager.Instance.AcceptedQuestList)
        //        {
        //            if (quest.QuestType == Enums.QuestType.Enemy && quest.GoalId == enemy.Id)
        //            {
        //                quest.CountGoal();
        //            }
        //        }
        //    }
        //}

        //스킬 구현 
        public void ConsumeMP(uint MP)
        {
            this.MP -= MP;
            if(this.MP < 0) this.MP = 0;
        }
        public void HealedHP(uint HP)
        {
            this.HP += HP;
            if (this.HP > this.MaxHP) { this.HP = this.MaxHP; }
        }
        public void RecoveredMP(uint MP)
        {
            this.MP += MP;
            if (this.MP > this.MaxMP) { this.MP = this.MaxMP; }
        }
        public virtual void Damaged(uint damage)
        {
            Random random = new Random();

            if (random.Next(1,101) <= 15)    //15퍼 확률로 치명타 발동
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("치명타!!!");
                Console.ResetColor();
                damage = (uint)(damage * 1.6f);
                Console.WriteLine($"데미지가 160%로 증가되어 {damage}가 되었습니다.");
            }
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
        public uint GetNormalAttackDamage(Character opponent)
        {
            Random random = new Random();

            if (Attack == 0)
            {
                return 0;
            }

            if(random.Next(1,101) <= 10)
            {
                Console.WriteLine($"{opponent.Name}(은)는 회피했다!");
                return 0;  // (회피)
            }
            int min = (int)Math.Ceiling(Attack * 0.9f);
            int max = (int)Math.Ceiling(Attack * 1.1f);

            int rawDamage = random.Next(min, max + 1);

            double reducedDamage = rawDamage * (100.0 / (100 + opponent.Defense));
           return (uint)Math.Ceiling(reducedDamage);
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Skills;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Characters
{
    //
    internal class Player : Character
    {
        [JsonProperty]
        protected JobType jobType;
        [JsonProperty]
        protected Inventory inventory;
        [JsonProperty]
        protected uint numOfStones;
        [JsonProperty]
        protected uint gold; // 플레이어의 금액
        [JsonProperty]
        protected uint exp; // 플레이어의 경험치

        [JsonIgnore]
        public JobType JobType => jobType;
        [JsonIgnore]
        public Inventory Inventory => inventory;
        [JsonIgnore]
        public uint NumOfStones => numOfStones;
        [JsonIgnore]
        public uint Gold => gold; // 플레이어의 금액
        [JsonIgnore]
        public uint Exp => exp; // 플레이어의 경험치
        public Player(string name, JobType jobType) : base(name)
        {
            switch (this.jobType)
            {
                case JobType.Magician:
                    hP = 100;
                    mP = 300;
                    attack = 10;
                    defense = 10;
                    numOfStones = 0; // 초기 돌의 개수 설정
                    gold = 1000; // 초기 금액 설정
                    exp = 0; // 초기 경험치 설정
                    skillList.Add(GameDataManager.Instance.AllSkills[0]);
                    inventory = new Inventory(this);
                    break;
                case JobType.Warrior:
                    hP = 300;
                    mP = 100;
                    attack = 10;
                    defense = 10;
                    numOfStones = 0; // 초기 돌의 개수 설정
                    gold = 1000; // 초기 금액 설정
                    exp = 0; // 초기 경험치 설정
                    skillList.Add(GameDataManager.Instance.AllSkills[2]);
                    inventory = new Inventory(this);
                    break;
            }
        }
        [JsonConstructor]
        public Player(
            uint id,
            string name,
            uint hp,
            uint mp,
            uint attack,
            uint defense,
            List<Skill> skillList,
            bool isAlive,
            JobType jobType,
            uint numOfStones,
            Inventory inventory,
            uint gold,
            uint exp
        ) : base(name)
        {
            this.id = id;
            this.name = name;
            this.hP = hp;
            this.mP = mp;
            this.attack = attack;
            this.defense = defense;
            this.skillList = new List<Skill>();
            foreach (Skill skill in skillList)
            {
                this.skillList.Add(GameDataManager.Instance.AllSkills.FirstOrDefault(x => x.Id == skill.Id));
            }
            this.isAlive = isAlive;
            this.jobType = jobType;
            this.numOfStones = numOfStones;
            this.inventory = inventory;
            this.gold = gold;
            this.exp = exp;
        }

        public void DisplayPlayerStatus()
        {
            Console.WriteLine("DisplayPlayerStatus");
        } 
        public void ConsumeMP(uint MP)
        {
            this.mP -= MP;
        }
        public void Healed(uint HP)
        {
            this.hP += HP;
        }

        public void EarnGold(uint gold)
        {
            this.gold += gold; 
        }
        public void AcquireSkillStone(uint numOfStones)
        {
            this.numOfStones += numOfStones;
        }


        //스킬 습득 //가능한지는 Skill에서 체크
        public void LearnSkill(Skill skillToLearn)
        {
            this.numOfStones -= skillToLearn.RequiredStones;
            this.skillList.Add(skillToLearn);
            Console.WriteLine($"{skillToLearn.Name}을 배웠다!");
        }

        // 사용자 이름
        // 이 부분 변수명을 어디를 사용자Name으로 할지 캐릭터Name으로 할지 의논필요.
        public void Rename(string newName)
        {
            this.name = newName;
        }



        //SaveData Load시 Deep Copy 위함
        public void Clone(Player player)
        {
            Console.WriteLine("Clone 시작전:" + name);
            this.id = player.Id;
            Console.WriteLine(name);
            Console.WriteLine(player.Name);
            this.name = player.Name;
            Console.WriteLine(name);
            Console.WriteLine(player.Name);
            Console.Read();
            this.hP = player.HP;
            this.mP = player.MP;
            this.attack = player.Attack;
            this.defense = player.Defense;

            this.skillList = new List<Skill>();
            foreach (Skill skill in player.SkillList)
            {
                this.SkillList.Add(GameDataManager.Instance.AllSkills.FirstOrDefault(x => x.Id == skill.Id));
            }

            this.isAlive = player.IsAlive;
            this.jobType = player.JobType;
            this.numOfStones = player.NumOfStones;
            this.inventory.Clone(player.Inventory);
            Console.WriteLine("Clone 완료후:" + name);
        }

    }
}


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
        public JobType JobType { get; private set; }
        public Inventory Inventory { get; private set; }

        public Weapon Weapon { get; private set; }
        public Armor Armor { get; private set; }
        public uint NumOfStones { get; private set; }

        public uint Gold { get; private set; } // 플레이어의 금액

        public uint Exp { get; private set; } // 플레이어의 경험치
        public Player(string name, JobType jobType) : base(name)
        {
            switch (jobType)
            {
                case JobType.Magician:
                    HP = 100;
                    MP = 300;
                    Attack = 10;
                    Defense = 10;
                    NumOfStones = 0; // 초기 돌의 개수 설정
                    Gold = 1000; // 초기 금액 설정
                    Exp = 0; // 초기 경험치 설정
                    //SkillList.Add(GameDataManager.Instance.AllSkills[0]);
                    Inventory = new Inventory(this);
                    break;
                case JobType.Warrior:
                    HP = 300;
                    MP = 100;
                    Attack = 10;
                    Defense = 10;
                    NumOfStones = 0; // 초기 돌의 개수 설정
                    Gold = 1000; // 초기 금액 설정
                    Exp = 0; // 초기 경험치 설정
                    //SkillList.Add(GameDataManager.Instance.AllSkills[0]);
                    Inventory = new Inventory(this);
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
            uint luck,
            List<Skill> skillList,
            bool isAlive,
            JobType jobType,
            uint numOfStones,
            Inventory inventory,
            uint gold,
            uint exp
        ) : base(name)
        {
            Console.WriteLine("Player 역직렬화 생성자");
            this.Id = id;
            this.Name = name;
            this.HP = hp;
            this.MP = mp;
            this.Attack = attack;
            this.Defense = defense;
            this.Luck = luck;
            this.SkillList = new List<Skill>();
            foreach (Skill skill in skillList)
            {
                this.SkillList.Add(GameDataManager.Instance.AllSkills.FirstOrDefault(x => x.Id == skill.Id));
            }
            this.IsAlive = isAlive;
            this.JobType = jobType;
            this.NumOfStones = numOfStones;
            this.Inventory = new Inventory(this);
            this.Inventory.Clone(inventory);
            this.Gold = gold;
            this.Exp = exp;
        }

        public void DisplayPlayerStatus()
        {
            Console.WriteLine("DisplayPlayerStatus");
        } 
        public void ConsumeMP(uint MP)
        {
            this.MP -= MP;
        }
        public void Healed(uint HP)
        {
            this.HP += HP;
        }

        public void EarnGold(uint gold)
        {
            this.Gold += gold; 
        }
        public void AcquireSkillStone(uint numOfStones)
        {
            NumOfStones += numOfStones;
        }
        public void RecalculateStats()
        {
            uint bonusAttack = 0;   
            uint bonusDefense = 0;
            // 인벤토리에서 장착된 아이템의 능력치 보너스를 계산
            foreach(var item in Inventory.ItemList)
            {
                if (item.IsEquipped)
                {
                    if(item is Weapon weapon)
                    {
                        bonusAttack += this.Attack; // 무기의 능력치 보너스 나중에 웨폰에 비례 추가
                    }
                    else if (item is Armor armor)
                    {
                        bonusDefense += this.Defense; // 방어구의 능력치 보너스 나중ㅇ에 아머에 비례 추가
                    }
                }
            }
        }


        //스킬 습득 //가능한지는 Skill에서 체크
        public void LearnSkill(Skill skillToLearn)
        {
            NumOfStones -= skillToLearn.RequiredStones;
            SkillList.Add(skillToLearn);
            Console.WriteLine($"{skillToLearn.Name}을 배웠다!");
        }

        // 사용자 이름
        // 이 부분 변수명을 어디를 사용자Name으로 할지 캐릭터Name으로 할지 의논필요.
        public void Rename(string newName)
        {
            Name = newName;
        }



        //SaveData Load시 Deep Copy 위함
        public void Clone(Player player)
        {
            this.Id = player.Id;
            Console.WriteLine(Name);
            Console.WriteLine(player.Name);
            this.Name = player.Name;
            Console.WriteLine(Name);
            Console.WriteLine(player.Name);
            this.HP = player.HP;
            this.MP = player.MP;
            this.Attack = player.Attack;
            this.Defense = player.Defense;

            this.SkillList = new List<Skill>();
            foreach (Skill skill in player.SkillList)
            {
                SkillList.Add(GameDataManager.Instance.AllSkills.FirstOrDefault(x => x.Id == skill.Id));
            }
            IsAlive = player.IsAlive;
            JobType = player.JobType;
            Inventory.Clone(player.Inventory);
            NumOfStones = player.NumOfStones;
        }

    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Skills;

namespace TextRPG_TeamSix.Characters
{
    //
    internal class Player : Character
    {
        public JobType JobType { get; private set; }
        public Inventory Inventory { get; private set; }
        public uint NumOfStones { get; private set; }

        public uint Gold { get; private set; } // 플레이어의 금액

        public uint Exp { get; private set; } // 플레이어의 경험치
        public Player(string name, JobType jobType) : base(name)
        {
            switch (JobType)
            {
                case JobType.Magician:
                    HP = 100;
                    MP = 300;
                    Attack = 10;
                    Defense = 10;
                    NumOfStones = 0; // 초기 돌의 개수 설정
                    Gold = 1000; // 초기 금액 설정
                    Exp = 0; // 초기 경험치 설정
                    SkillList.Add(GameDataManager.Instance.AllSkills[0]);
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
                    SkillList.Add(GameDataManager.Instance.AllSkills[2]);
                    Inventory = new Inventory(this);
                    break;
            }
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
            Id = player.Id;
            Name = player.Name;
            HP = player.HP;
            MP = player.MP;
            Attack = player.Attack;
            Defense = player.Defense;
            SkillList = new List<Skill>();
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


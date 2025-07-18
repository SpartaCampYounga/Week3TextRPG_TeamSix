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
    internal class Player : Character
    {
        public JobType JobType { get; private set; }
        public Inventory Inventory { get; private set; }
        public uint NumOfStones { get; private set; }
        public uint Gold { get; private set; } // 플레이어의 금액
        public uint Exp { get; private set; } // 플레이어의 경험치
        public uint MaxHP { get; private set; } // 최대 체력
        public uint MaxMP { get; private set; } // 최대 마나

        public Player(string name, JobType jobType) : base(name)
        {
            switch (jobType)
            {
                case JobType.Magician:
                    this.JobType = JobType.Magician;
                    MaxHP = 100;
                    MaxMP = 300;
                    HP = MaxHP;
                    MP = MaxMP;
                    Attack = 10;
                    Defense = 10;
                    NumOfStones = 0;
                    Gold = 1000;
                    Exp = 0;
                    Inventory = new Inventory(this);
                    break;
                case JobType.Warrior:
                    this.JobType = JobType.Warrior;
                    MaxHP = 300;
                    MaxMP = 100;
                    HP = MaxHP;
                    MP = MaxMP;
                    Attack = 10;
                    Defense = 10;
                    NumOfStones = 0;
                    Gold = 1000;
                    Exp = 0;
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
            uint exp,
            uint maxHp,
            uint maxMp
        ) : base(name)
        {
            Console.WriteLine("Player 역직렬화 생성자");
            this.Id = id;
            this.Name = name;
            this.HP = hp;
            this.MP = mp;
            this.MaxHP = hp;
            this.MaxMP = mp;
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
            this.MaxHP = maxHp;
            this.MaxMP = maxMp;
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
        public void EarnExp(uint exp)
        {
            this.Exp += exp;
        }
        public uint GetTotalAttack()
        {
            return Attack + GetEquipBonusAttack();
        }
        public uint GetEquipBonusAttack()
        {
            Dictionary<EquipSlot, EquipItem> EquipmentList = PlayerManager.Instance.EquipmentList;
            uint totalAttackBonus = 0;

            if (EquipmentList.Count != 0)
            {
                foreach(EquipItem item in EquipmentList.Values)
                {
                    if(item.Ability == Ability.Attack)
                    {
                        totalAttackBonus += item.Enhancement;
                    }
                }
            }

            return totalAttackBonus;
        }
        public uint GetTotalDefense()
        {
            return Defense + GetEquipBonusDefense();
        }
        public uint GetEquipBonusDefense()
        {
            Dictionary<EquipSlot, EquipItem> EquipmentList = PlayerManager.Instance.EquipmentList;
            uint totalDefenseBonus = 0;

            if (EquipmentList.Count != 0)
            {
                foreach (EquipItem item in EquipmentList.Values)
                {
                    if (item.Ability == Ability.Defense)
                    {
                        totalDefenseBonus += item.Enhancement;
                    }
                }
            }

            return totalDefenseBonus;
        }
        public void AcquireSkillStone(uint numOfStones)
        {
            NumOfStones += numOfStones;
        }

        public void RecalculateStats()
        {
            uint bonusAttack = 0;
            uint bonusDefense = 0;
            foreach (var item in Inventory.ItemList)
            {
                //if (item.IsEquipped)
                //{
                //    if (item is Weapon weapon)
                //    {
                //        bonusAttack += this.Attack;
                //    }
                //    else if (item is Armor armor)
                //    {
                //        bonusDefense += this.Defense;
                //    }
                //}
            }
        }

        public void LearnSkill(Skill skillToLearn)
        {
            NumOfStones -= skillToLearn.RequiredStones;
            SkillList.Add(skillToLearn);
            //Console.WriteLine($"{skillToLearn.Name}을 배웠다!");
        }

        public void Rename(string newName)
        {
            Name = newName;
        }

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
            this.MaxHP = player.MaxHP;
            this.MaxMP = player.MaxMP;
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
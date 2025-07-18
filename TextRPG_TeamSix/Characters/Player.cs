using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
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
        public uint Level { get; private set; }
        public uint MaxHP { get; private set; } // 최대 체력
        public uint MaxMP { get; private set; } // 최대 마나

        public uint BaseAttack { get; private set; } // 기본 공격력
        public uint BaseDefense { get; private set; } // 기본 방어력

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
                    BaseAttack = 10;
                    BaseDefense = 5;
                    Attack = BaseAttack;
                    Defense = BaseDefense;
                    NumOfStones = 0;
                    Gold = 1000;
                    Exp = 0;
                    Level = 1;
                    Inventory = new Inventory(this);
                    break;
                case JobType.Warrior:
                    this.JobType = JobType.Warrior;
                    MaxHP = 300;
                    MaxMP = 100;
                    HP = MaxHP;
                    MP = MaxMP;
                    BaseAttack = 10;
                    BaseDefense = 10;
                    Attack = BaseAttack;
                    Defense = BaseDefense;
                    NumOfStones = 0;
                    Gold = 1000;
                    Exp = 0;
                    Level = 1;
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
            uint level,
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
            this.Level = level;
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
            if (this.Exp >= 1000)
            {
                this.Exp -= 1000;
                Console.WriteLine($"레벨이 증가하여 {this.Level++}이 {this.Level} 되었다!");
                LevelUp();
            }
        }
        public void LevelUp()
        {
            this.Attack += 10;
            this.Defense += 10;
            this.Luck++;
            this.MaxHP += 10;
            this.MaxMP += 10;
            this.HP = this.MaxHP;
            this.MP = this.MaxMP;

            Console.WriteLine($"공격력이 증가하여 {this.Level} 되었다!");
            Console.WriteLine($"방어력이 증가하여 {this.Level} 되었다!");
            Console.WriteLine($"행운이 증가하여 {this.Level} 되었다!");
            Console.WriteLine($"최대체력이 증가하여 {this.Level} 되었다!");
            Console.WriteLine($"최대마나가 증가하여 {this.Level} 되었다!");
            Console.WriteLine("레벨업 보너스로 체력과 마나가 완전히 회복되었다!");
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
        public void PurchaseItem(uint itemId)
        {
            // 구매 bool 체크는 상점 SellToPlayer로 이동
            // 예: 플레이어의 골드가 충분한지 확인하고, 아이템을 인벤토리에 추가
            Item? item = GameDataManager.Instance.AllItems.FirstOrDefault(x => x.Id == itemId);
            if (item == null)
            {

                Console.WriteLine("해당 아이템이 존재하지 않습니다.");
                return;
            }

            if (Gold < item.Price)
            {
                Console.WriteLine("골드가 부족합니다.");
                return;
            }

            Gold -= item.Price;
            this.Inventory.ItemList.Add(item);
            Console.WriteLine($"{item.Name}을(를) 구매했습니다.");

        }
        public void SellItem(uint itemId)
        {
            // 아이템 판매 로직
            // 아이템이 인벤토리에 있는지 확인
            // 아이템이 존재하지 않으면 메시지 출력
            // 아이템이 존재하면 플레이어의 골드를 증가시키고 인벤토리에서 제거
            Item? item = this.Inventory.GetItem(itemId);
            if (item == null)
            {
                Console.WriteLine("해당 아이템이 인벤토리에 없습니다.");
                return;
            }
            // 판매 로직 추가
            uint sellPrice = (uint)(item.Price * 0.85f); // 판매 가격은 원래 가격의 85%
            Gold += sellPrice;
            this.Inventory.ItemList.Remove(item);
            Console.WriteLine($"{item.Name}을 {sellPrice}G에 판매 했습니다.");
        }
        //public void RecalculateStats()
        //{
        //    uint bonusAttack = 0;
        //    uint bonusDefense = 0;
        //    foreach (var item in Inventory.ItemList)
        //    {
        //        //if (item.IsEquipped)
        //        //{
        //        //    if (item is Weapon weapon)
        //        //    {
        //        //        bonusAttack += this.Attack;
        //        //    }
        //        //    else if (item is Armor armor)
        //        //    {
        //        //        bonusDefense += this.Defense;
        //        //    }
        //        //}
        //    }
        //}

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

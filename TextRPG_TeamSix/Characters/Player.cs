using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
                    Defense = 5;
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
                    Attack = 10;
                    Defense = 5;
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
            uint maxHp,
            uint maxMp,
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
            uint level
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
            this.Level = level;
            this.MaxHP = maxHp;
            this.MaxMP = maxMp;
        }

        public string DisplayPlayerStatusInBattle()
        {
            string display = "";
            display += FormatUtility.AlignLeftWithPadding($"{Name}의 현재 상태", 17) + "   ";
            display += FormatUtility.AlignLeftWithPadding($"HP : {HP} / {MaxHP}   ", 10);
            display += FormatUtility.AlignLeftWithPadding($"MP : {MP} / {MaxMP}   ", 10);
            return display;
        }

        public void EarnGold(uint gold)
        {
            this.Gold += gold;
        }
        public void EarnExp(uint exp)
        {
            this.Exp += exp;

            while (true)
            {
                uint requiredExp = Level == 1
                    ? CalculateLevelUpExp(1)
                    : CalculateLevelUpExp(Level) - CalculateLevelUpExp(Level - 1);

                if (this.Exp >= requiredExp)
                {
                    this.Exp -= requiredExp;
                    uint previousLevel = this.Level;
                    this.Level++;
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"레벨이 증가하여 {previousLevel}이 {this.Level} 되었다!");
                    Console.ResetColor();
                    LevelUp();
                }
                else
                {
                    break;
                }
            }
        }

        public uint CalculateLevelUpExp(uint level)
        {
            //재귀함수 //가이드 따라 산출한 경험치 공식
            return (uint)((5 * level * level + 35 * level - 20) / 2);

        }
        public void LevelUp()
        {
            this.Attack += 2;
            this.Defense += 1;
            this.Luck++;
            this.MaxHP += 10;
            this.MaxMP += 10;
            this.HP = this.MaxHP;
            this.MP = this.MaxMP;

            Console.WriteLine($"공격력이 증가하여 {this.Attack} 되었다!");
            Console.WriteLine($"방어력이 증가하여 {this.Defense} 되었다!");
            Console.WriteLine($"행운이 증가하여 {this.Luck} 되었다!");
            Console.WriteLine($"최대체력이 증가하여 {this.MaxHP} 되었다!");
            Console.WriteLine($"최대마나가 증가하여 {this.MaxMP} 되었다!");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("레벨업 보너스로 체력과 마나가 완전히 회복되었다!");
            Console.ResetColor();
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

            if (item is EquipItem equipItem)
            {
                if (PlayerManager.Instance.EquipmentList.ContainsKey(equipItem.EquipSlot))
                {
                    this.Inventory.EquipItem(item.Id);  //장착해제
                }
            }

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

        //public override uint GetNormalAttackDamage()
        //{

        //    if (GetTotalAttack() == 0)
        //    { return 0; }
        //    //10퍼 오차 //Percent
        //    int min = (int)Math.Ceiling(GetTotalAttack() * 0.9f);
        //    int max = (int)Math.Ceiling(GetTotalAttack() * 1.1f);

        //    Random random = new Random();
        //    return (uint)random.Next(min, max + 1);
        //}

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
            this.Gold = player.Gold;
            this.Exp = player.Exp;
            this.Level = player.Level;
        }
    }
}
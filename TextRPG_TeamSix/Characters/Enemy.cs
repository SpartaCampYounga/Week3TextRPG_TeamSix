using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Characters
{
    internal class Enemy : Character
    {
        public EnemyType EnemyType { get; set; }
        public Enemy(uint id, string name, EnemyType enemyType) : base(name)
        {
            EnemyType = enemyType;
            Id = id;
            switch (EnemyType)
            {
                case EnemyType.Type1:
                    HP = 100;
                    MP = 300;
                    MaxHP = HP;
                    MaxMP = MP;
                    Attack = 10;
                    Defense = 10;
                    break;
                case EnemyType.Type2:
                    HP = 300;
                    MP = 100;
                    MaxHP = HP;
                    MaxMP = MP;
                    Attack = 10;
                    Defense = 10;
                    break;
            }
        }
        protected Enemy() { }
        public Enemy CreateInstance()
        {
            return new Enemy();
        }
        public Enemy Clone(Enemy target)
        {
            this.Id = target.Id;
            this.Name = target.Name;
            this.HP = target.HP;
            this.MP = target.MP;
            this.Attack = target.Attack;
            this.Defense = target.Defense;
            this.Luck = target.Luck;
            this.EnemyType = target.EnemyType;
            this.IsAlive = target.IsAlive;
            this.MaxHP = target.MaxHP;
            this.MaxMP = target.MaxMP;
            return this;
        }

        [JsonConstructor]
        public Enemy(
            uint id,
            string name,
            uint hP,
            uint mP,
            uint attack,
            uint defense,
            uint luck, 
            EnemyType enemyType,
            bool isAlive,
            uint maxHp,
            uint maxMp)
            : base(id, name, hP,mP, attack,defense, luck, maxHp, maxMp) 
        {
            this.Id = id;
            this.Name = name;
            this.HP = hP;
            this.MP = mP;
            MaxHP = HP;
            MaxMP = MP;
            this.Attack = attack;
            this.Defense = defense;
            this.Luck = luck;
            this.EnemyType = enemyType;
            this.IsAlive = isAlive;
        }

        public override string ToString()
        {
            string display = "";
            if (this.IsAlive == false)
            {
                display += FormatUtility.AlignLeftWithPadding(Name, 15) + "   ";
                display += FormatUtility.AlignLeftWithPadding("💀", 50); //여기서 컬러 바꾸고 싶은데.......
            }
            else
            {
                display += FormatUtility.AlignLeftWithPadding(Name, 15) + "   ";
                display += FormatUtility.AlignLeftWithPadding($"HP : {HP} / {MaxHP}", 50);
            }
            
            return display;
        }

    }
}
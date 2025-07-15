using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Items;

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
        public Player(string name) : base(name)
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
                    break;
                case JobType.Warrior:
                    HP = 300;
                    MP = 100;
                    Attack = 10;
                    Defense = 10;
                    NumOfStones = 0; // 초기 돌의 개수 설정
                    Gold = 1000; // 초기 금액 설정
                    Exp = 0; // 초기 경험치 설정
                    break;
            }
        }
        public void DisplayPlayerStatus()
        {
            Console.WriteLine("DisplayPlayerStatus");
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


        // 사용자 이름
        // 이 부분 변수명을 어디를 사용자Name으로 할지 캐릭터Name으로 할지 의논필요.
        public void Rename(string newName)
        {
            Name = newName;
        }

    }
}


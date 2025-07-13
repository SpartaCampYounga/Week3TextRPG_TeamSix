using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Items;

namespace TextRPG_TeamSix.Character
{
    //기본 정보 // Status는 클래스를 필드로 가질것
    internal class Player
    {
        private static uint nextId = 0;
        public uint Id { get; private set; }
        public string Name { get; private set; }
        public uint Level { get; private set; }
        public string Job { get; private set; } //Job 여러종류면 Enum으로 JobType 관리
        public uint Gold { get; private set; }
        public uint Exp {  get; private set; }
        public Inventory InventoryContainer { get; private set; }
        public Dictionary<EquipSlot, Item> EquippedItems { get; private set; }

        public Player(string name)
        {
            Id = nextId++;
            Name = name;
            Level = 1;
            Job = "전사";
            Gold = 1500;
            Exp = 0;
        }

        public void DisplayPlayerStatus()
        {

        }
    }
}

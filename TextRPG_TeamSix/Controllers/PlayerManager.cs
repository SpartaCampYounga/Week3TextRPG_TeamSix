using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Dungeons;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Quests;
using TextRPG_TeamSix.Scenes;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Controllers
{
    //현재 플레이어 정보를 로드/생성.
    //플레이어 싱글톤화하여 단순 저장하는 저장소 개념.   //전역에서 접근 가능함.
    internal class PlayerManager
    {
        public Player CurrentPlayer { get; private set; }
        public List<uint> ClearedDungeonList { get; private set;}
        public List<Quest> AcceptedQuestList { get; private set; }
        public Dictionary<EquipSlot, EquipItem> EquipmentList { get; private set; }
        public Dungeon CurrentDungeon { get; set; } //전투 중에만 활성화되고 SaveData에 저장되지 않으므로 public set
        private PlayerManager()
        {
            CurrentPlayer = new Player("PlayerManager", JobType.Warrior);
            ClearedDungeonList = new List<uint>();
            EquipmentList = new Dictionary<EquipSlot, EquipItem>();
            AcceptedQuestList = new List<Quest>();
            CurrentDungeon = new Dungeon(0, "", 0, 0, 0, 0, null, null);
        }
        private static PlayerManager instance;
        public static PlayerManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlayerManager();
                }
                return instance;
            }
        }

        public bool InitializePlayerFromSaveData(string playerName)
        {
            if (SaveManager.Instance.LoadGame(playerName))
            {
                //로드 성공!
                CurrentPlayer.Clone(SaveManager.Instance.SaveData.PlayerSave);
                foreach(uint id in SaveManager.Instance.SaveData.ClearedDungeonList)
                {
                    this.ClearedDungeonList.Add(id);
                }
                foreach(Quest quest in SaveManager.Instance.SaveData.AcceptedQuestList)
                {
                    Quest temp = quest.CreateInstance();    //빈객체 활용
                    temp.Clone(quest);
                    this.AcceptedQuestList.Add(temp);
                }
                //Console.WriteLine($"PlayerManager AcceptedQuestList Count: {this.AcceptedQuestList.Count}");
                //InputHelper.WaitResponse();
                Console.WriteLine($"In SaveManager EquipList null" + SaveManager.Instance.SaveData.EquipmentList.Values == null);
                foreach(EquipItem equipItem in SaveManager.Instance.SaveData.EquipmentList.Values)
                {
                    Console.WriteLine($"In SaveManager EquipItem null " + equipItem);
                    EquipItem temp = (EquipItem)equipItem.CreateInstance();
                    temp.Clone(equipItem);
                    this.EquipmentList.Add(temp.EquipSlot, temp);
                }
                //Dictionary<EquipSlot, EquipItem> EquipmentList
                Console.WriteLine("플레이어 데이터를 불러왔습니다.");
                Console.WriteLine($"불러온 플레이어 이름: {SaveManager.Instance.SaveData.PlayerSave.Name}");
                Console.WriteLine($"CurrentPlayer 이름: {CurrentPlayer.Name}");
                return true;
            }
            else
            {
                //로드 실패!
                return false;
            }
        }
    }
}

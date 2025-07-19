using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Dungeons;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Quests;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Controllers
{
    internal class SaveData //현재까지 플레이중인 정보를 저장
    {

        public Player PlayerSave { get; set; }
        public List<uint> ClearedDungeonList { get; set; }   //입장 가능한 던전 아이디만 저장
                                                             //그 외에 도감, 던전 진행도 등 저장할 것들 필드로 삼고, 생성자에 입력. 
        public List<Quest> AcceptedQuestList { get; private set; }
        public Dictionary<EquipSlot, EquipItem> EquipmentList { get; private set; }
        [JsonConstructor]
        public SaveData(Player playerSave, List<uint> clearedDungeonList, List<Quest> acceptedQuestList, Dictionary<EquipSlot, EquipItem> equipmentList)
        {
            this.PlayerSave = playerSave;
            this.ClearedDungeonList = clearedDungeonList;
            this.AcceptedQuestList = acceptedQuestList;
            this.EquipmentList = equipmentList;

            Console.WriteLine("equipmentList");
            foreach (var item in equipmentList.Values)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("EquipmentList");
            foreach (var item in EquipmentList.Values)
            {
                Console.WriteLine(item);
            }
            InputHelper.WaitResponse();
        }
    }
}

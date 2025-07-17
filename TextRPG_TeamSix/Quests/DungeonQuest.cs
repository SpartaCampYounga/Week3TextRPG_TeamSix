//using System;
//using System.Collections.Generic;
//using System.Formats.Asn1;
//using System.Linq;
//using System.Text;
//using Newtonsoft.Json;
//using System.Threading.Tasks;
//using System.Xml.Linq;
//using TextRPG_TeamSix.Characters;
//using TextRPG_TeamSix.Dungeons;
//using TextRPG_TeamSix.Enums;
//using TextRPG_TeamSix.Utilities;

//namespace TextRPG_TeamSix.Quests
//{
//    internal class DungeonQuest: Quest
//    {
//        public uint DungeonId { get; protected set; } // 퀘스트에 해당하는 던전
//        public override uint Count { get; protected set; }


//        [JsonConstructor]
//        public DungeonQuest(uint id, QuestType questType, string description, uint rewardGold, uint rewardExp, uint count, Enemy enemy, uint dungeonId)
//            : base(id, questType, description, rewardGold, rewardExp, count)
//        {
//            DungeonId = dungeonId;
//        }
//        public override string ToString()
//        {
//            string display = base.ToString(); // 부모의 ToString() 결과 포함
//            display += FormatUtility.AlignWithPadding(Dungeon.Name, 5) + " | ";
//            display += FormatUtility.AlignWithPadding(Count.ToString(), 5) + " | ";
//            return display;
//        }
//    }
//}

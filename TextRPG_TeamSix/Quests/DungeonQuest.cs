using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Dungeons;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Quests
{
    internal abstract class DungeonQuest: Quest
    {
        public Dungeon Dungeon { get; protected set; } // 퀘스트에 해당하는 던전
        public uint Count { get; } // 퀘스트 완료 횟수

        public DungeonQuest(QuestType questType, string description, uint rewardGold, uint rewardExp, Dungeon dungeon, uint count)
            : base(questType, description, rewardGold, rewardExp)
        {
            Dungeon = dungeon;
            Count = count;
        }

    }
}

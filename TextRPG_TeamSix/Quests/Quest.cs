using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Dungeons;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Quests
{
    internal class Quest
    {
        public uint Id { get; protected set; }
        public QuestType QuestType { get; protected set; }
        public string Description { get; protected set; }
        public uint RewardGold { get; protected set; }
        public uint RewardExp { get; protected set; }
        public uint GoalId { get; protected set; } // 퀘스트에 해당하는 던전
        public uint Count { get; protected set; }

        public Quest(uint id, QuestType questType, string description, uint rewardGold, uint rewardExp, uint goalId, uint count)
        {
            Id = id;
            QuestType = questType;
            Description = description;
            RewardGold = rewardGold;
            RewardExp = rewardExp;
            GoalId = goalId;
            Count = count;
        }


        // ㅁㅁㅁ크리처 사냥 | ㅁㅁㅁ크리처를 잡아주세요. | 보상: 100골드, 10경험치
        // Hard던전 클리어  | Hard던전을 소탕하세요. | 보상: 1000골드, 100경험치

        public override string ToString()
        {
            string goalName = "";
            
            switch (QuestType)  
            {
                case QuestType.Enemy:
                    Enemy enemy = GameDataManager.Instance.AllEnemies.FirstOrDefault(x => x.Id == GoalId);
                    goalName = enemy?.Name ?? "Unknown";
                    break;
                case QuestType.Dungeon:
                    Dungeon dungeon = GameDataManager.Instance.AllDungeons.FirstOrDefault(x => x.Id == GoalId);
                    goalName = dungeon?.Name ?? "Unknown";
                    break;
            }

            string display = "";
            display += FormatUtility.AlignWithPadding(Id.ToString(), 3) + " | ";
            display += FormatUtility.AlignWithPadding(QuestType.ToString(), 10) + " | ";
            display += FormatUtility.AlignWithPadding(Description, 30) + " | ";
            display += FormatUtility.AlignWithPadding(RewardGold.ToString(), 10) + " | ";
            display += FormatUtility.AlignWithPadding(RewardExp.ToString(), 10) + " | ";
            display += FormatUtility.AlignWithPadding(goalName, 10) + " | ";
            display += FormatUtility.AlignWithPadding(Count.ToString(), 5) + " | ";
            return display;
        }
    }
}

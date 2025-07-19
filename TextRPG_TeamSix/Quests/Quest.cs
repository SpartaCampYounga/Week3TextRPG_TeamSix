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
        public uint GoalCount { get; protected set; }
        public uint Count { get; protected set; }
        public bool IsFinished { get; protected set; }  //끝냇냐?

        [JsonConstructor]
        public Quest(uint id, QuestType questType, string description, uint rewardGold, uint rewardExp, uint goalId, uint goalCount, uint count, bool isFinished)
        {
            Id = id;
            QuestType = questType;
            Description = description;
            RewardGold = rewardGold;
            RewardExp = rewardExp;
            GoalCount = goalCount;
            GoalId = goalId;
            Count = count;
            IsFinished = isFinished;
        }

        private Quest() { }
        public Quest CreateInstance()
        {
            return new Quest();
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
            display += FormatUtility.AlignLeftWithPadding(QuestType.ToString(), 10) + " | ";
            display += FormatUtility.AlignLeftWithPadding(Description, 30) + " | ";
            display += FormatUtility.AlignLeftWithPadding(RewardGold.ToString(), 10) + " | ";
            display += FormatUtility.AlignLeftWithPadding(RewardExp.ToString(), 10) + " | ";
            display += FormatUtility.AlignLeftWithPadding(goalName, 10) + " | ";
            display += FormatUtility.AlignLeftWithPadding(Count.ToString() + "/" + GoalCount.ToString(), 10);
            return display;
        }
        public void AcceptThisQuest()
        {
            PlayerManager.Instance.AcceptedQuestList.Add(this);
        }
        public void CountGoal()     //호출될때 GoalId체크하고 호출할것. //아직은.. 
        {
            Count++;

            if (Count == GoalCount)
            {
                IsFinished = true;
            }
        }
        public bool IsRewarded()
        {
            Player player = PlayerManager.Instance.CurrentPlayer;
            if (IsFinished)
            {
                Console.WriteLine($"{RewardGold}G를 획득합니다.");
                Console.WriteLine($"경험치 {RewardExp}를 획득합니다.");
                player.EarnGold(RewardGold);
                player.EarnExp(RewardExp);
                return true;
            }
            else
            {
                Console.WriteLine("아직 조건을 달성하지 못했다...");
                return false;
            }
        }

        public void Clone(Quest quest)
        {
            Id = quest.Id;
            QuestType = quest.QuestType;
            Description = quest.Description;
            RewardGold = quest.RewardGold;
            RewardExp = quest.RewardExp;
            GoalCount = quest.GoalCount;
            GoalId = quest.GoalId;
            Count = quest.Count;
            IsFinished = quest.IsFinished;
        }
    }
}

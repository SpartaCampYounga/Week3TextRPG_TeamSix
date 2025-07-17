using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Quests
{
    internal abstract class Quest
    {
        public uint Id { get; protected set; }
        public QuestType QuestType { get; }
        public string Description { get; protected set; }
        public uint RewardGold { get; private set; }
        public uint RewardExp { get; private set; }
        public abstract uint Count { get; }
        //public void Reward(Player player);

        public Quest(uint id, QuestType questType, string description, uint rewardGold, uint rewardExp, uint count)
        {
            Id = id;
            QuestType = questType;
            Description = description;
            RewardGold = rewardGold;
            RewardExp = rewardExp;
        }


        // ㅁㅁㅁ크리처 사냥 | ㅁㅁㅁ크리처를 잡아주세요. | 보상: 100골드, 10경험치
        // Hard던전 클리어  | Hard던전을 소탕하세요. | 보상: 1000골드, 100경험치

        public override string ToString()
        {
            string display = "";
            display += FormatUtility.AlignWithPadding(Id.ToString(), 3) + " | ";
            display += FormatUtility.AlignWithPadding(QuestType.ToString(), 5) + " | ";
            display += FormatUtility.AlignWithPadding(Description, 5) + " | ";
            display += FormatUtility.AlignWithPadding(RewardGold.ToString(), 5) + " | ";
            display += FormatUtility.AlignWithPadding(RewardExp.ToString(), 5) + " | ";
            display += FormatUtility.AlignWithPadding(Count.ToString(), 5) + " | ";
            return display;
        }
    }
}

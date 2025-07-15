using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Enums;

namespace TextRPG_TeamSix.Quest
{
    internal abstract class Quest
    {
        public QuestType QuestType { get; }
        public string Description { get; protected set; }
        public uint RewardGold { get; private set; }
        public uint RewardExp { get; private set; }
        //public void Reward(Player player);

        public Quest(QuestType questType, string description, uint rewardGold, uint rewardExp)
        {
            QuestType = questType;
            Description = description;
            RewardGold = rewardGold;
            RewardExp = rewardExp;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Quests
{
    internal abstract class EnemyQuest : Quest
    {
        public Enemy Enemy { get; private set; } // 퀘스트에 필요한 적 캐릭터
        public uint Count { get; } // 퀘스트 완료를 위한 적 처치 수

        public EnemyQuest(QuestType questType, string description, uint rewardGold, uint rewardExp, Enemy enemy, uint count)
            : base(questType, description, rewardGold, rewardExp)
        {
            Enemy = enemy;
            Count = count;
        }

    }
}

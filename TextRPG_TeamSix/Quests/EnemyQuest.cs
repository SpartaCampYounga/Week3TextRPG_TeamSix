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
    internal class EnemyQuest : Quest
    {
        public Enemy Enemy { get; private set; } // 퀘스트에 필요한 적 캐릭터
        public uint Count { get; } // 퀘스트 완료를 위한 적 처치 수

        public EnemyQuest(uint id, QuestType questType, string description, uint rewardGold, uint rewardExp, Enemy enemy, uint count)
            : base(id, questType, description, rewardGold, rewardExp)
        {
        }

        public override string ToString()
        {
            string display = base.ToString(); // 부모의 ToString() 결과 포함
            display += FormatUtility.AlignWithPadding(Enemy.Name, 5) + " | ";
            display += FormatUtility.AlignWithPadding(Count.ToString(), 5) + " | ";
            return display;
        }
    }
}

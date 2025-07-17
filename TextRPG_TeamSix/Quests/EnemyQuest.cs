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
    internal class EnemyQuest : Quest
    {
        public Enemy Enemy { get; private set; } // 퀘스트에 필요한 적 캐릭터? 지금생각하니 굳이 넣어야하나?
        public override uint Count { get; }

        public EnemyQuest(uint id, QuestType questType, string description, uint rewardGold, uint rewardExp, Enemy enemy, uint count)
            : base(id, questType, description, rewardGold, rewardExp, count)
        {
            Enemy = enemy;
            Count = count;
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

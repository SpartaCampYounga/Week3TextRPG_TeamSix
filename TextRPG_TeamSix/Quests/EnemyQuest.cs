using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Dungeons;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Utilities;
/*
namespace TextRPG_TeamSix.Quests
{
    internal class EnemyQuest : Quest
    {
        public uint EnemyId { get; protected set; } //Enemy 타입으로 구현하신걸 EnemyId만 받아오게 했습니다. 오브젝트간 비교는 하지 않는 것이 좋습니다.
        //어차피 Id 값만 비교하니, uint 값만 받아서, 처치한 몬스터의 Id값만 비교해서 처리하면 되니 간단합니다.
        //던전도 똑같이 변경하였습니다.
        //이제 둘은 필요한 필드가 똑같아 졌으니 상속 받지 않고, 그냥 클래스 한종류로 진행이 가능해집니다. 왜냐면 QuestType으로 구분 해주셨기 때문!
        //그래서 현재 이 클래스는 참고를 위해서 구현은 해두었으나, 확인하고 나시면 삭제 부탁드립니다.
        public override uint Count { get; protected set; }
        
        [JsonConstructor]
        public EnemyQuest(uint id, QuestType questType, string description, uint rewardGold, uint rewardExp, uint count, uint enemyId)
            : base(id, questType, description, rewardGold, rewardExp, count)
        {
            EnemyId = enemyId;
        }

        public override string ToString()
        {
            string enemyName = GameDataManager.Instance.AllEnemies.FirstOrDefault(x => x.Id == EnemyId).Name;
            
            string display = base.ToString(); // 부모의 ToString() 결과 포함
            display += FormatUtility.AlignWithPadding(enemyName,  5) + " | ";
            display += FormatUtility.AlignWithPadding(Count.ToString(), 5) + " | ";
            return display;
        }

        public void CountGoalEnemy(uint enemyId)    //일단 카운트만..
        {
            if(EnemyId == enemyId)
            {
                Count--;
            }

            if(Count == 0)
            {
                Reward();
            }
        }
        public void Reward()
        {
            Player player = PlayerManager.Instance.CurrentPlayer;
            player.EarnGold(RewardGold);
            player.EarnExp(RewardExp);
        }
    }
}
*/

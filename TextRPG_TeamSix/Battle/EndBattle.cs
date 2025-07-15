using System;

namespace TextRPG_TeamSix.Battle
{
    internal class EndBattle
    {
        public void Execute(BattleManager manager)
        {
            manager.EndCurrentBattle();
            int rewardGold = 100;
            Console.WriteLine("전투 종료!");
            Console.WriteLine($"보상으로 {rewardGold} Gold를 획득했습니다.");
        }
    }
}

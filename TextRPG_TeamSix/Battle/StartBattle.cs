using System;
using System.Collections.Generic;
using TextRPG_TeamSix.Characters;

namespace TextRPG_TeamSix.Battle
{
    internal class StartBattle
    {
        public void Execute(BattleManager manager, List<Enemy> enemies)
        {
            manager.InitializeBattle(enemies);
            Console.Clear();
            Console.WriteLine("⚔️ 전투가 시작되었습니다!");
        }
    }
}

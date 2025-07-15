using System;
using System.Collections.Generic;
using TextRPG_TeamSix.Characters;

namespace TextRPG_TeamSix.Game
{
    internal class BattleManager
    {
        private static BattleManager instance;

        private BattleManager() { }

        public List<Enemy> Enemys { get; private set; }

        public bool IsBattleActive { get; private set; }

        public int StartBattle(List<Enemy> enemyList)
        {

            return Enemys.Count;
        }

        public void ExecuteTurn(Player player)
        {
            
        }

        public bool BattleOver()
        {
            return true;
        }

        public int EndBattle()
        {
            Console.WriteLine("전투 종료!");
            IsBattleActive = false;

            int rewardGold = 1;

            return rewardGold;
        }
    }
}

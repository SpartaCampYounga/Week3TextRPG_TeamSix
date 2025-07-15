using System.Collections.Generic;
using TextRPG_TeamSix.Characters;

namespace TextRPG_TeamSix.Battle
{
    internal class BattleManager
    {
        public List<Enemy> Enemies { get; private set; }
        public bool IsBattleActive { get; private set; }

        public void InitializeBattle(List<Enemy> enemyList)
        {
            Enemies = enemyList;
            IsBattleActive = true;
        }

        public void EndCurrentBattle()
        {
            IsBattleActive = false;
        }
    }
}

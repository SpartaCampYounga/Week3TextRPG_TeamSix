using System;
using System.Collections.Generic;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Items;

namespace TextRPG_TeamSix.Battle.Actions
{
    internal class IPlayerAction
    {
        private Player player;
        private List<Enemy> enemies;

        public IPlayerAction(Player player, List<Enemy> enemies)
        {
            this.player = player;
            this.enemies = enemies;
        }

        public void NormalAttack(int targetIndex)
        {

        }

        public void SkillAttack(int targetIndex)
        {

        }

        public void UseItem(string itemName)
        {

        }

        public bool Run()
        {
            Random rand = new Random();
            bool success = rand.Next(0, 100) < 50;

            if (success)
            {
                Console.WriteLine($"{player.Name}이(가) 도망에 성공했습니다!");
            }
            else
            {
                Console.WriteLine($"{player.Name}이(가) 도망에 실패했습니다...");
            }

            return success;
        }
    }
}

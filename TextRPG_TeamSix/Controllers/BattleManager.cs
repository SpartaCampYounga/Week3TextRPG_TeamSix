using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Scenes;
using TextRPG_TeamSix.Skills;

namespace TextRPG_TeamSix.Controllers
{
    internal class BattleManager
    {
        public Player Player { get; private set; }
        public List<Enemy> Enemies { get; private set; } = new();
        public bool IsBattleActive { get; private set; }

        private int currentEnemyIndex = 0;

        public void StartBattle(Player player, List<Enemy> enemies)
        {
            Player = player;
            Enemies = enemies;
            IsBattleActive = true;
        }

        public void PlayerTurn(ActionType actionType, int? targetIndex = null)
        {
            if (!IsBattleActive || Player == null || Enemies.Count == 0) return;

            switch (actionType)
            {
                case ActionType.NormalAttack:
                    if (targetIndex.HasValue && IsValidTarget(targetIndex.Value))
                    {
                        NormalAttack(targetIndex.Value);
                    }
                    break;

                case ActionType.SkillAttack:
                    break;

                case ActionType.ItemUse:
                    break;

                case ActionType.Run:
                    Flee();
                    break;
            }
        }

        private void NormalAttack(int targetIndex)
        {
            
        }

        public void EnemyTurn()
        {
            //foreach (var enemy in Enemies)
            //{
            //    if (enemy.IsAlive)
            //    {
            //        Player.HP -= enemy.Attack;
            //        if (Player.HP <= 0)
            //        {
            //            Player.HP = 0;
            //            IsBattleActive = false;
            //            return;
            //        }
            //    }
            //}
        }

        public bool IsBattleOver()
        {
            bool allEnemiesDead = Enemies.TrueForAll(e => !e.IsAlive);
            bool playerDead = Player == null || Player.HP <= 0;
            return allEnemiesDead || playerDead;
        }

        private void Flee()
        {
            IsBattleActive = false;
        }

        private bool IsValidTarget(int index)
        {
            return index >= 0 && index < Enemies.Count && Enemies[index].IsAlive;
        }
    }

    internal enum ActionType
    {
        NormalAttack,
        SkillAttack,
        ItemUse,
        Run
    }
}

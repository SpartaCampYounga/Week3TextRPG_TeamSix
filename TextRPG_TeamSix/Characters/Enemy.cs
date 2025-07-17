using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;

namespace TextRPG_TeamSix.Characters
{
    internal class Enemy : Character
    {
        public EnemyType EnemyType { get; set; }
        public Enemy(uint id, string name, EnemyType enemyType) : base(name)
        {
            EnemyType = enemyType;
            Id = id;
            switch (EnemyType)
            {
                case EnemyType.Type1:
                    HP = 100;
                    MP = 300;
                    Attack = 10;
                    Defense = 10;
                    break;
                case EnemyType.Type2:
                    HP = 300;
                    MP = 100;
                    Attack = 10;
                    Defense = 10;
                    break;
            }
        }
    }
}
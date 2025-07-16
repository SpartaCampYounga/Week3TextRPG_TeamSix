using Newtonsoft.Json;
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
        [JsonProperty]
        protected EnemyType enemyType;
        [JsonIgnore]
        public EnemyType EnemyType => enemyType;
        public Enemy(string name, EnemyType enemyType) : base(name)
        {
            this.enemyType = enemyType;

            switch (EnemyType)
            {
                case EnemyType.Type1:
                    hP = 100;
                    mP = 300;
                    attack = 10;
                    defense = 10;
                    break;
                case EnemyType.Type2:
                    hP = 300;
                    mP = 100;
                    attack = 10;
                    defense = 10;
                    break;
            }
        }
    }
}

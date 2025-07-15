using System;
using System.Collections.Generic;
using TextRPG_TeamSix.Battle;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Enums;

namespace TextRPG_TeamSix.Scenes
{
    internal class BattleScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Battle;

        public override void DisplayScene()
        {
            Console.WriteLine("🗡️ 전투 씬에 진입합니다!");
        }

        public override void HandleInput()
        {
            Run();
        }

        public void Run()
        {
            Player player = new Player("커피", JobType.Magician);

            List<Enemy> enemies = new List<Enemy>
            {
                new Enemy("슬라임", EnemyType.Type1),
                new Enemy("고블린", EnemyType.Type2)
            };

            BattleManager manager = new BattleManager();
            StartBattle starter = new StartBattle();
            EndBattle ender = new EndBattle();

            starter.Execute(manager, enemies);

            while (manager.IsBattleActive)
            {
                Console.WriteLine("▶ 아무 키나 누르면 턴 진행 (테스트용)");
                Console.ReadKey();
                ender.Execute(manager);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Scenes;
using TextRPG_TeamSix.Skills;
using TextRPG_TeamSix.Dungeons;
using TextRPG_TeamSix.Quests;
using static TextRPG_TeamSix.Items.Item;

namespace TextRPG_TeamSix.Controllers
{
    //게임 시작 시 싱글톤 처리된 매니저(Controller)들 초기화/로드 작업
    internal static class GameInitializer
    {
        //일단 하드코딩. 추후 Json 으로 담당할 것.  (Json 생성용)
        private static SceneBase[] _scenes = {
            new SkillScene(),
            new InventoryScene(),
            new TitleScene(),
            new PlayerSetupScene(),
            new SkillLearnScene(),
            new MainScene(),
            new BattleScene(),
            new QuestScene(),
            new StoresScene(),
            new DungeonScene(),
            new PlayerScene(),
            new QuestAcceptScene(),
            new QuestRewardScene()
        };

        private static Skill[] _skills =
        {
            //이름: 15자, 설명 30자 (한글은 2자, 영문은 1자, 특문 포함)
            new AttackSkill(1, "스매시", "강하게 내려칩니다.", 10, 3, SkillType.Attack, 20),
            new AttackSkill(2, "후려치기", "강하게 후려칩니다.", 5, 1, SkillType.Attack, 10),
            new AttackSkill(3, "파이어볼", "불을 소환합니다.", 20, 3, SkillType.Attack, 30),
            new HealSkill(4, "치료하기", "HP를 회복합니다.", 30, 5, SkillType.Heal, 30)
        };

        private static Item[] _items =
        {
            new Portion(1, "소형 회복물약", "작은 체력을 회복합니다", 50, ItemType.Consumable, 30, RestoreType.Health),
            new Portion(2, "중형 회복물약", "중간 정도의 체력을 회복합니다", 100, ItemType.Consumable, 100, RestoreType.Health),
            new Portion(3, "대형 회복물약", "많은 체력을 회복합니다", 300, ItemType.Consumable, 450, RestoreType.Health),


            new Weapon(4, "녹슨검", "낡았습니다", 80, ItemType.Weapon, Ability.Attack, 10, EquipSlot.Weapon),
            new Weapon(5, "나무검", "가볍고 약한 검입니다.", 120, ItemType.Weapon, Ability.Attack, 10, EquipSlot.Weapon),
            new Weapon(6, "포레스트검f", "Just Do it의 정수 우리도 노력하면 됩니다.", 300, ItemType.Weapon, Ability.Attack, 10, EquipSlot.Weapon),



            new Armor(7, "천옷", "기본 복장입니다.", 50, ItemType.Armor, Ability.Defense, 10, EquipSlot.Armor),
            new Armor(8, "가죽 갑옷", "튼튼한 가죽으로 만들었습니다.", 80, ItemType.Armor, Ability.Defense, 10, EquipSlot.Armor),
            new Armor(9, "강철갑옷", "단단한 금속 갑옷 입니다.", 50, ItemType.Armor, Ability.Defense, 10, EquipSlot.Armor),

        };

        private static Gatcha[] _gatchas =
        {
            new Gatcha("일반", new List<Item>{_items[0], _items[1] })
        };

        private static Enemy[] _emenies =
        {
            new Enemy(1, "고블린", EnemyType.Type1),
            new Enemy(2, "슬라임", EnemyType.Type1)
        };

        private static Dungeon[] _dungeons =
        {
            new Dungeon(1, "Easy", 10, 1000, 100, _gatchas[0], new List<Enemy>{_emenies[0], _emenies[0] }),
            new Dungeon(2, "Normal", 20, 3000, 300, _gatchas[0], new List<Enemy>{_emenies[0], _emenies[0] })
        };

        private static Quest[] _quests =
        {
            new Quest(1, QuestType.Enemy, "고블린 3마리를 처치하세요.", 100, 10, _emenies[0].Id,3, 0, false),
            new Quest(2, QuestType.Dungeon, "Easy 던전을 클리어하세요", 100, 10, _dungeons[0].Id,3, 0, false)
            //new Quest(1, QuestType.Enemy, "고블린 2마리를 처치하세요.", 100, 10, _emenies[0], 3), // "고블린" _enemies[0]
            //new Quest(2, QuestType.Dungeon, "Easy 던전을 클리어하세요", 300, 30, _dungeons[0], 1) // "Easy" _dungeons[0]
        };

        public static void InitializeAll() 
        {
            SceneManager.Instance.InitializeScenes(_scenes);
            GameDataManager.Instance.InitializeSkills(_skills);
            GameDataManager.Instance.InitializeItems(_items);
            GameDataManager.Instance.InitializeEnemies(_emenies);
            GameDataManager.Instance.InitializeDungeons(_dungeons);
            GameDataManager.Instance.InitializeQuests(_quests);
        }
    }
}

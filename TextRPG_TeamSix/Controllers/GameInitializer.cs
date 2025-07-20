using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Dungeons;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Quests;
using TextRPG_TeamSix.Scenes;
using TextRPG_TeamSix.Skills;
using TextRPG_TeamSix.Stores;
using TextRPG_TeamSix.Utilities;
using static TextRPG_TeamSix.Items.Item;

namespace TextRPG_TeamSix.Controllers
{
    //게임 시작 시 싱글톤 처리된 매니저(Controller)들 초기화/로드 작업
    internal class GameInitializer
    {
        public static GamaData GamaData { get; private set; }

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
            new StorePurchaseScene(),
            new DungeonScene(),
            new PlayerScene(),
            new QuestAcceptScene(),
            new QuestRewardScene(),
            new SpecialStoreScene(),
            new SpecialStoreEnterScene(),
            new StoreScene(),
            new StoreSellScene(),
            new RestScene()
        };

        private static Skill[] _skills =
        {
            new AttackSkill(1, "후려치기", "강하게 후려칩니다.", 5, 1, SkillType.Attack, 10),
            new AttackSkill(2, "스매시", "강하게 내려칩니다.", 10, 3, SkillType.Attack, 20),
            new AttackSkill(3, "파이어볼", "불을 소환합니다.", 20, 3, SkillType.Attack, 30),
            new HealSkill(4, "치료하기", "HP를 회복합니다.", 30, 5, SkillType.Heal, 30)
        };

        private static Item[] _items =
        {
            // 포션
            new Portion(1, "소형 회복물약", "작은 체력을 회복합니다", 50, ItemType.Consumable, 60, RestoreType.Health),
            new Portion(2, "중형 회복물약", "중간 정도의 체력을 회복합니다", 100, ItemType.Consumable, 180, RestoreType.Health),
            new Portion(3, "대형 회복물약", "많은 체력을 회복합니다", 300, ItemType.Consumable, 350, RestoreType.Health),
            new Portion(103, "엘릭서", "마법의 힘이 담긴 물약입니다.", 500, ItemType.Consumable, 600, RestoreType.All, true),

            // 무기
            new EquipItem(5, "나무검", "가볍고 약한 검입니다.", 80, ItemType.Weapon, Ability.Attack, 4, EquipSlot.Weapon),
            new EquipItem(4, "녹슨검", "낡았습니다", 200, ItemType.Weapon, Ability.Attack, 6, EquipSlot.Weapon),
            new EquipItem(7, "철검", "무딘감이 있지만 꽤 강한 검입니다.", 1000, ItemType.Weapon, Ability.Attack, 12, EquipSlot.Weapon),
            new EquipItem(6, "포레스트검", "Just Do it의 정수 우리도 노력하면 됩니다.", 1500, ItemType.Weapon, Ability.Attack, 18, EquipSlot.Weapon),
            new EquipItem(8, "장인의 검", "날카롭게 제련된 강한 검입니다.", 2500, ItemType.Weapon, Ability.Attack, 30, EquipSlot.Weapon),
            new EquipItem(101, "완성", "과거와 현재 미래가 합친 작품입니다.", 4000, ItemType.Weapon, Ability.Attack, 45, EquipSlot.Weapon, true),
            new EquipItem(105, "화염검", "불의 속성이 담긴 강력한 무기입니다.", 7000, ItemType.Weapon, Ability.Attack, 75, EquipSlot.Weapon, true),
            new EquipItem(104, "용의 검", "드래곤의 힘이 깃든 전설의 검입니다.", 9000, ItemType.Weapon, Ability.Attack, 100, EquipSlot.Weapon, true),

            // 방어구
            new EquipItem(11, "천옷", "기본 복장입니다.", 50, ItemType.Armor, Ability.Defense, 2, EquipSlot.Armor),
            new EquipItem(12, "가죽 갑옷", "튼튼한 가죽으로 만들었습니다.", 200, ItemType.Armor, Ability.Defense, 5, EquipSlot.Armor),
            new EquipItem(13, "강철갑옷", "단단한 금속 갑옷 입니다.", 500, ItemType.Armor, Ability.Defense, 8, EquipSlot.Armor),
            new EquipItem(14, "성기사의 갑옷", "신의 가호가 깃든 중갑입니다.", 2000, ItemType.Armor, Ability.Defense, 18, EquipSlot.Armor),
            new EquipItem(15, "어둠의 로브", "마력이 깃든 강철갑옷입니다.", 2500, ItemType.Armor, Ability.Defense, 20, EquipSlot.Armor),
            new EquipItem(102, "협업", "여러 명인들이 모여 만든 작품입니다.", 3500, ItemType.Armor, Ability.Defense, 35, EquipSlot.Armor, true)

        };

        private static Gatcha[] _gatchas =
        {
            new Gatcha("일반", new List<Item>{_items[0], _items[1] }),
            new Gatcha("고급", new List<Item>{_items[2], _items[3], _items[4] }),
        };


        private static Enemy[] _emenies =
        {
            new Enemy(1, "슬라임", 30, 0, 5, 1, 1, EnemyType.Slime, true, 30, 0),
            new Enemy(2, "고블린", 40, 0, 10, 3, 2, EnemyType.Goblin, true, 40, 0),
            new Enemy(3, "스켈레톤", 60, 0, 15, 5, 3, EnemyType.Skeleton, true, 60, 0),
            new Enemy(4, "늑대인간", 80, 10, 25, 8, 5, EnemyType.Werewolf, true, 80, 10),
            new Enemy(5, "오우거", 150, 10, 30, 15, 2, EnemyType.Ogre, true, 150, 10),
            new Enemy(6, "서큐버스", 100, 50, 35, 10, 8, EnemyType.Succubus, true, 100, 50),
            new Enemy(7, "드래곤", 400, 100, 60, 20, 10, EnemyType.Dragon, true, 400, 100)
        };

        private static Dungeon[] _dungeons =
        {
            new Dungeon(1, "Easy", 5, 0, 500, 20, _gatchas[0],
                new List<Enemy> { _emenies[0], _emenies[0], _emenies[1] }),

            new Dungeon(2, "Normal", 15, 1, 1500, 40, _gatchas[0],
                new List<Enemy> { _emenies[1], _emenies[2], _emenies[3] }),

            new Dungeon(3, "Hard", 30, 2, 3000, 80, _gatchas[0],
                new List<Enemy> { _emenies[3], _emenies[4], _emenies[5] }),

            new Dungeon(4, "Hell", 50, 3, 5000, 160, _gatchas[0],
                new List<Enemy> { _emenies[5], _emenies[6] })
        };

        private static Quest[] _quests =
        {
            new Quest(1, QuestType.Enemy, "고블린 3마리를 처치하세요.", 100, 10, _emenies[0].Id,3, 0, false),
            new Quest(2, QuestType.Enemy, "슬라임 5마리를 처치하세요.", 200, 20, _emenies[1].Id,5, 0, false),
            new Quest(3, QuestType.Dungeon, "Easy 던전을 클리어하세요", 100, 10, _dungeons[0].Id,1, 0, false),
            new Quest(4, QuestType.Dungeon, "Normal 던전을 클리어하세요", 300, 30, _dungeons[1].Id,1, 0, false),
            new Quest(5, QuestType.Enemy, "고블린 1마리를 처치하세요.", 100, 10, _emenies[0].Id,1, 0, false)
        };

        private static Store[] _stores =
        {
            new Store(StoreType.Normal, new List<Item>{_items[0], _items[1] })
        };

        public static void InitializeAll() 
        {
            SceneManager.Instance.InitializeScenes(_scenes);
            GameDataManager.Instance.InitializeSkills(_skills);
            GameDataManager.Instance.InitializeItems(_items);
            GameDataManager.Instance.InitializeEnemies(_emenies);
            GameDataManager.Instance.InitializeDungeons(_dungeons);
            GameDataManager.Instance.InitializeQuests(_quests);
            CreateJsonGameData();
        }


        public static void CreateJsonGameData()
        {
            //저장해야하는 데이터 리스트가 담김 (생성자에서)
            GamaData = new GamaData(_scenes,_skills, _items, _gatchas, _emenies, _dungeons, _quests, _stores);

            JsonSerializerSettings setting = JsonHelper.GetJsonSetting();
            // 파일 생성 후 쓰기

            File.WriteAllText(JsonHelper.path + $@"\\GameData.json", JsonConvert.SerializeObject(GamaData, setting));
            Console.WriteLine($"인게임 데이터가 저장되었습니다.");
        }
        public static bool InitializeFromJson()
        {
            JsonSerializerSettings setting = JsonHelper.GetJsonSetting();

            //Console.WriteLine("1매개변수: " + playerName);
            //Console.WriteLine("1CurrentPlayer: " + PlayerManager.Instance.CurrentPlayer.Name);
            try
            {
                //JsonConvert.PopulateObject(File.ReadAllText(path + $@"\\player_{playerName}.json"), player);
                GamaData = JsonConvert.DeserializeObject<GamaData>(File.ReadAllText(JsonHelper.path + $@"\\GameData.json"), setting);
                //Console.WriteLine("역직렬화된 Player 이름: " + SaveData.PlayerSave.Name);
                //Console.WriteLine("2매개변수: " + playerName);
                //Console.WriteLine("2CurrentPlayer: " + PlayerManager.Instance.CurrentPlayer.Name);

                Console.WriteLine($"GameData를 불러왔습니다.");

                SceneManager.Instance.InitializeScenes(GamaData._scenes);
                GameDataManager.Instance.InitializeSkills(GamaData._skills);
                GameDataManager.Instance.InitializeItems(GamaData._items);
                GameDataManager.Instance.InitializeEnemies(GamaData._emenies);
                GameDataManager.Instance.InitializeDungeons(GamaData._dungeons);
                GameDataManager.Instance.InitializeQuests(GamaData._quests);
                GameDataManager.Instance.InitializeStores(GamaData._stores);

                //Console.WriteLine("3매개변수: " + playerName);
                //Console.WriteLine("3CurrentPlayer: " + PlayerManager.Instance.CurrentPlayer.Name);
                return true;
            }
            catch (Exception ex)
            {
                CreateJsonGameData();
                Console.WriteLine(ex.Message);
                Console.WriteLine("GameData가 존재하지 않습니다.");
                Console.WriteLine("다시 시작해보세요.");
                return false;
            }
        }
    }
}

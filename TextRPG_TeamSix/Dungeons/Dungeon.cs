using Newtonsoft.Json;
using System.Xml.Linq;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Dungeons
{
    //던전 관리
    //던전 종류가 많으면 Interface 혹은 추상클래스 상속하여 확장할 것
    internal class Dungeon
    {
        public uint Id { get; private set; }
        public string Name { get; private set; }
        public uint RequiredDefense { get; private set; }
        public uint RequiredDungeonId { get; private set; } //0일 경우 기본 던전 (항상 열려있음)
        public uint RewardGold { get; private set; }
        public uint RewardExp { get; private set; }
        public Gatcha RewardGatcha { get; private set; }
        public List<Enemy> Enemies { get; private set; }

        private Dungeon() { }
        public Dungeon CreateInstance()
        {
            return new Dungeon();
        }
        public void Clone(Dungeon target)
        {
            Id = target.Id;
            Name = target.Name;
            RequiredDefense = target.RequiredDefense;
            RequiredDungeonId = target.RequiredDungeonId;
            RewardGold = target.RewardGold;
            RewardExp = target.RewardExp;
            RewardGatcha = target.RewardGatcha;
            Enemies = new List<Enemy>();
            foreach (Enemy enemy in target.Enemies)
            {
                Enemy temp = enemy.CreateInstance();
                temp.Clone(enemy);
                Enemies.Add(temp);
            }
        }

        [JsonConstructor]
        public Dungeon (
            uint id,string name, uint requiredDefense, uint requiredDungeonId,
            uint rewardGold, uint rewardExp,
            Gatcha rewardGatcha, List<Enemy> enemies)
        {
            Id = id;
            Name = name;
            RequiredDefense = requiredDefense;
            RequiredDungeonId = requiredDungeonId;
            RewardGold = rewardGold;
            RewardExp = rewardExp;
            RewardGatcha = rewardGatcha;
            Enemies = enemies;
        }

        public override string ToString()
        {
            string display = "";

            display += FormatUtility.AlignLeftWithPadding(Name, 15) + " | ";
            display += FormatUtility.AlignLeftWithPadding(RequiredDefense.ToString(), 11) + " | ";

            return display;
        }

        public bool TryEnterDungeon(Player player)
        {
            //진입 로직 //배틀매니저 로드 등
            //권장 방어력 미만시 막는거 없으면 구현부 없을수도 있음.
            if(this.RequiredDefense > player.Defense)
            {
                Console.WriteLine("방어력이 부족하여 입장할 수 없습니다.");
                return false;
            }
            else if(PlayerManager.Instance.ClearedDungeonList.Contains(this.RequiredDungeonId) || this.RequiredDungeonId == 0)
            {
                Console.WriteLine($"{this.Name}던전에 입장합니다.");
                return true;
            }
            else //AvailableDungeonList에 없을 경우
            {
                string requiredDungeonName = GameDataManager.Instance.AllDungeons.FirstOrDefault(x => x.Id == this.RequiredDungeonId).Name;
                Console.WriteLine($"{requiredDungeonName} 던전을 먼저 도전해주세요.");
                return false;
            }
        }

            public void OrderEnemiesByIsAlive()
            {
                Enemies = Enemies.OrderByDescending(x => x.IsAlive).ToList();
            }
    }
}

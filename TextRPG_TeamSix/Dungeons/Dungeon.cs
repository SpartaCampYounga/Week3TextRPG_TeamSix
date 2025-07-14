using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Items;

namespace TextRPG_TeamSix.Dungeons
{
    //던전 관리
    //던전 종류가 많으면 Interface 혹은 추상클래스 상속하여 확장할 것
    internal class Dungeon
    {
        public string Name { get; private set; }
        public uint RequiredDefense { get; private set; }
        public uint RewardGold { get; private set; }
        public uint RewardExp { get; private set; }
        public Gatcha RewardGatcha { get; private set; }
        public List<Enemy> Enemies { get; private set; }
        //public DungeonType DungeonType { get; private set; } //던전 타입 별로 구현하실거면...

        public Dungeon (string name, uint requiredDefense, uint rewardGold, uint rewardExp, Gatcha rewardGatcha, List<Enemy> enemies)
        {
            Name = name;
            RequiredDefense = requiredDefense;
            RewardGold = rewardGold;
            RewardExp = rewardExp;
            RewardGatcha = rewardGatcha;
            Enemies = enemies;
        }

        public void EnterDungeon(Player player)
        {
            //진입 로직 //배틀매니저 로드 등
            //권장 방어력 미만시 막는거 없으면 구현부 없을수도 있음.
        }
    }
}

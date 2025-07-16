using System.Numerics;
using System.Text;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Enums;

namespace TextRPG_TeamSix.Utils
{
    internal static class BattleUI
    {
        public static void BattleStartInfo()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("┌────────────[ 전투시작 ]────────────┐");
            Console.WriteLine("└────────────────────────────────────┘");
        }

        public static void DrawPlayerInfo(Player player)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("┌────────────[ 플레이어 ]────────────┐");
            Console.WriteLine($"│ 이름: {player.Name}");
            Console.WriteLine($"│ HP: {player.HP} / {player.HP}"); //Max 변수는 추후 만들어야할듯?
            Console.WriteLine($"│ MP: {player.MP} / {player.MP}");
            Console.WriteLine($"│ 방어력: {player.Defense}");
            Console.WriteLine("└──────────────────────────────────┘");
            Console.ResetColor();
        }

        public static void DrawEnemyList(List<Enemy> enemies)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("┌────────────[ 적 목록 ]────────────┐");
            for (int i = 0; i < enemies.Count; i++)
            {
                var e = enemies[i];
                string status = e.IsAlive ? $"HP: {e.HP}" : "💀(죽음)";
                Console.WriteLine($"│ [{i + 1}] {e.Name} Lv. 상태 : {status}");
            }
            Console.WriteLine("└──────────────────────────────────┘");
            Console.ResetColor();
        }

        public static void DrawActionMenu()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("행동을 선택하세요:");
            Console.WriteLine("⚔️ 1. 일반 공격");
            Console.WriteLine("🔥 2. 스킬 사용");
            Console.WriteLine("⭐ 3. 아이템 사용");
            Console.WriteLine("🐿️ 4. 도망");
            Console.ResetColor();
            Console.Write(">> ");
        }
    }
}

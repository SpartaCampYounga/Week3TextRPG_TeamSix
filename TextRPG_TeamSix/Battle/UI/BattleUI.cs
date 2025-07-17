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
            Console.SetCursorPosition(2, 1);
            Console.WriteLine("┌────────────[ 전투 시작 ]────────────┐");
            Console.SetCursorPosition(2, 2);
            Console.WriteLine("│ ⚔️ 전투를 준비하세요!                  │");
            Console.SetCursorPosition(2, 3);
            Console.WriteLine("└────────────────────────────────────┘");
            Console.ResetColor();
        }

        public static void DrawPlayerInfo(Player player)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Yellow;
            int y = 5;

            Console.SetCursorPosition(2, y++);
            Console.WriteLine("┌────────────[ 플레이어 ]────────────┐");
            Console.SetCursorPosition(2, y++);
            Console.WriteLine($"│ ✨ 이름: {player.Name,-29}│");
            Console.SetCursorPosition(2, y++);
            Console.WriteLine($"│ ❤️ HP: {player.HP} / {player.HP,-25}│");
            Console.SetCursorPosition(2, y++);
            Console.WriteLine($"│ 💧 MP: {player.MP} / {player.MP,-25}│");
            Console.SetCursorPosition(2, y++);
            Console.WriteLine($"│ 🛡️ 방어력: {player.Defense,-27}│");
            Console.SetCursorPosition(2, y++);
            Console.WriteLine("└────────────────────────────────────┘");
            Console.ResetColor();
        }

        public static void DrawEnemyList(List<Enemy> enemies)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Red;
            int y = 12;

            Console.SetCursorPosition(2, y++);
            Console.WriteLine("┌────────────[ 적 목록 ]────────────┐");
            foreach (var e in enemies)
            {
                string status = e.IsAlive ? $"HP: {e.HP}" : "💀(죽음)";
                string line = $"{e.Name} Lv. 상태 : {status}";
                Console.SetCursorPosition(2, y++);
                Console.WriteLine($"│ {line,-32}│");
            }
            Console.SetCursorPosition(2, y++);
            Console.WriteLine("└───────────────────────────────────┘");
            Console.ResetColor();
        }

        public static void DrawActionMenu()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Cyan;
            int y = 17;

            Console.SetCursorPosition(2, y++);
            Console.WriteLine("╔══════════════════════╗");
            Console.SetCursorPosition(2, y++);
            Console.WriteLine("║ 행동을 선택하세요:       ║");
            Console.SetCursorPosition(2, y++);
            Console.WriteLine("║ ⚔️ 1. 일반 공격         ║");
            Console.SetCursorPosition(2, y++);
            Console.WriteLine("║ 🔥 2. 스킬 사용         ║");
            Console.SetCursorPosition(2, y++);
            Console.WriteLine("║ ⭐ 3. 아이템 사용        ║");
            Console.SetCursorPosition(2, y++);
            Console.WriteLine("║ 🐿️ 4. 도망             ║");
            Console.SetCursorPosition(2, y++);
            Console.WriteLine("╚══════════════════════╝");

            Console.ResetColor();
        }
    }
}

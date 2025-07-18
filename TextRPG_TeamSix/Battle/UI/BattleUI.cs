using System;
using System.Collections.Generic;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Scenes;

namespace TextRPG_TeamSix.Utils
{
    internal static class BattleUI
    {
        public static void BattleStartInfo()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(2, 0);
            Console.SetCursorPosition(2, 1);
            Console.WriteLine("⚔️ [ 전투 시작 ]");
            Console.ResetColor();
        }

        private static void DrawBar(uint current, uint max, int width, ConsoleColor color, int left, int top)
        {
            int filled = (int)((float)current / max * width);
            string bar = new string('■', filled).PadRight(width);

            Console.SetCursorPosition(left, top);
            Console.ForegroundColor = color;
            Console.Write($"│     {bar,-28}│");
            Console.ResetColor();
        }

        public static void DrawPlayerInfo(Player player)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            int y = 2;

            Console.SetCursorPosition(0, y++); Console.WriteLine("┌────────────[ 플레이어 ]────────────┐");
            Console.SetCursorPosition(0, y++); Console.WriteLine($"│ ✨ 이름: {player.Name,-26}│");
            Console.SetCursorPosition(0, y++); Console.WriteLine($"│ ❤️ HP: {player.HP,3} / {player.MaxHP,-22}│");
            DrawBar(player.HP, player.MaxHP, 20, ConsoleColor.Red, 0, y++);
            Console.SetCursorPosition(0, y++); Console.WriteLine($"│ 💧 MP: {player.MP,3} / {player.MaxMP,-22}│");
            DrawBar(player.MP, player.MaxMP, 20, ConsoleColor.Blue, 0, y++);
            Console.SetCursorPosition(0, y++); Console.WriteLine($"│ 🛡️ 방어력: {player.Defense,-23}│");
            Console.SetCursorPosition(0, y++); Console.WriteLine("└────────────────────────────────────┘");
            Console.ResetColor();
        }

        public static void DrawEnemyList(List<Enemy> enemies)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            int y = 9;

            Console.SetCursorPosition(0, y++); Console.WriteLine("┌────────────[ 적 목록 ]────────────┐");
            foreach (var e in enemies)
            {
                string status;
                if (e.IsAlive)
                {
                    status = $"HP: {e.HP}";
                }
                else
                {
                    status = "💀(죽음)";
                }

                string line = $"[{enemies.IndexOf(e) + 1}] {e.Name} Lv. 상태 : {status}";
                Console.SetCursorPosition(0, y++); Console.WriteLine($"│ {line,-29}│");
            }
            Console.SetCursorPosition(0, y++); Console.WriteLine("└───────────────────────────────────┘");
            Console.ResetColor();
        }

        public static void DrawActionMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            int y = 14;

            Console.SetCursorPosition(0, y++); Console.WriteLine("╔══════════════════════╗");
            Console.SetCursorPosition(0, y++); Console.WriteLine("║ 행동을 선택하세요:   ║");
            Console.SetCursorPosition(0, y++); Console.WriteLine("║ ⚔️ 1. 일반 공격      ║");
            Console.SetCursorPosition(0, y++); Console.WriteLine("║ 🔥 2. 스킬 사용      ║");
            Console.SetCursorPosition(0, y++); Console.WriteLine("║ ⭐ 3. 아이템 사용    ║");
            Console.SetCursorPosition(0, y++); Console.WriteLine("║ 🐿️ 4. 도망           ║");
            Console.SetCursorPosition(0, y++); Console.WriteLine("╚══════════════════════╝");
            Console.ResetColor();
        }
    }
}

using System;
using System.Collections.Generic;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Enums;

namespace TextRPG_TeamSix.Utils
{
    internal static class BattleUI
    {
        public static void BattleStartInfo()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.SetCursorPosition(2, 0);
            Console.WriteLine("⚔️ 전투를 준비하세요!");
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
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Yellow;
            int y = 2;

            Console.SetCursorPosition(2, y++);
            Console.WriteLine("┌────────────[ 플레이어 ]────────────┐");

            Console.SetCursorPosition(2, y++);
            Console.WriteLine($"│ ✨ 이름: {player.Name,-28}│");

            Console.SetCursorPosition(2, y++);
            Console.WriteLine($"│ ❤️ HP: {player.HP,3} / {player.MaxHP,-23}│");
            DrawBar(player.HP, player.MaxHP, 20, ConsoleColor.Red, 2, y++);

            Console.SetCursorPosition(2, y++);
            Console.WriteLine($"│ 💧 MP: {player.MP,3} / {player.MaxMP,-23}│");
            DrawBar(player.MP, player.MaxMP, 20, ConsoleColor.Blue, 2, y++);

            Console.SetCursorPosition(2, y++);
            Console.WriteLine($"│ 🛡️ 방어력: {player.Defense,-28}│");

            Console.SetCursorPosition(2, y++);
            Console.WriteLine("└────────────────────────────────────┘");

            Console.ResetColor();
        }



        public static void DrawEnemyList(List<Enemy> enemies)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Red;
            int y = 9;

            Console.SetCursorPosition(2, y++);
            Console.WriteLine("┌────────────[ 적 목록 ]────────────┐");
            foreach (var e in enemies)
            {
                string status = e.IsAlive ? $"HP: {e.HP}" : "💀(죽음)";
                string line = $"[{enemies.IndexOf(e) + 1}] {e.Name} Lv. 상태 : {status}";
                Console.SetCursorPosition(2, y++);
                Console.WriteLine($"│ {line,-32}│");
            }
            Console.SetCursorPosition(2, y++);
            Console.WriteLine("└───────────────────────────────────┘");
            Console.ResetColor();
        }

        public static void DrawActionMenu()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Cyan;
            int y = 14;

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

        public static int DrawCommandMenu()
        {
            int startX = 2;
            int startY = 15;

            Console.ForegroundColor = ConsoleColor.Cyan;

            // 커맨드 박스
            Console.SetCursorPosition(startX, startY++);
            Console.WriteLine("┌─────── 행동을 선택하세요: ───────┐");
            Console.SetCursorPosition(startX, startY++);
            Console.WriteLine("│ ⚔️ 1. 일반 공격                 │");
            Console.SetCursorPosition(startX, startY++);
            Console.WriteLine("│ 🔥 2. 스킬 사용                 │");
            Console.SetCursorPosition(startX, startY++);
            Console.WriteLine("│ ⭐ 3. 아이템 사용               │");
            Console.SetCursorPosition(startX, startY++);
            Console.WriteLine("│ 🐴 4. 도망                      │");
            Console.SetCursorPosition(startX, startY++);
            Console.WriteLine("└────────────────────────────────┘");

            Console.ResetColor();

            // 입력 라인 (아래쪽 고정된 위치에 출력)
            int inputY = startY + 1;
            Console.SetCursorPosition(startX, inputY);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("👉 입력: ");
            Console.ResetColor();

            string input = Console.ReadLine();
            Console.SetCursorPosition(startX, inputY + 1);

            if (int.TryParse(input, out int command))
            {
                switch (command)
                {
                    case 1: Console.WriteLine("🗡️ 1. 일반 공격을 선택하셨습니다."); break;
                    case 2: Console.WriteLine("🔥 2. 스킬 사용을 선택하셨습니다."); break;
                    case 3: Console.WriteLine("⭐ 3. 아이템 사용을 선택하셨습니다."); break;
                    case 4: Console.WriteLine("🐴 4. 도망을 선택하셨습니다."); break;
                    default: Console.WriteLine("❌ 잘못된 입력입니다."); break;
                }
                return command;
            }
            else
            {
                Console.WriteLine("❌ 숫자를 입력해주세요.");
                return -1;
            }
        }

    }
}

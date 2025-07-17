using System;
using System.Text;

namespace TextRPG_TeamSix.Utils
{
    internal static class BattleLog
    {
        private static int logStartX;
        private static int logStartY;
        private static int currentLine = 0;
        private static int maxLines = 17;

        public static void Log(string message)
        {
            if (currentLine >= maxLines)
            {
                ClearLogs();
            }

            Console.SetCursorPosition(logStartX, logStartY + currentLine);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message.PadRight(56)); // 박스 안 너비 맞추기
            Console.ResetColor();

            currentLine++;
        }

        public static void LogLine() => Log("");

        public static void ClearLogs()
        {
            for (int i = 0; i < maxLines; i++)
            {
                Console.SetCursorPosition(logStartX, logStartY + i);
                Console.Write(new string(' ', 56));
            }
            currentLine = 0;
        }

        public static void DrawLogBox()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Red;

            int x = 80;         // 박스 좌표
            int y = 1;
            int width = 60;
            int height = 20;

            Console.SetCursorPosition(x, y);
            Console.Write("┌" + new string('─', width - 2) + "┐");

            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write("│" + new string(' ', width - 2) + "│");
            }

            Console.SetCursorPosition(x, y + height - 1);
            Console.Write("└" + new string('─', width - 2) + "┘");

            Console.ResetColor();

            logStartX = x + 2;
            logStartY = y + 1;
            maxLines = height - 2;
            currentLine = 0;
        }

        // 👉 공격 로그
        public static void PlayerAttack(string player, string enemy, int damage)
        {
            Log($"🗡️ {player}이(가) {enemy}을(를) 공격!");
            Log($"💥 {enemy}에게 {damage}의 피해!");
            LogLine();
        }

        public static void EnemyAttack(string enemy, string player, int damage)
        {
            Log($"😈 {enemy}의 공격!");
            Log($"😨 {player}에게 {damage}의 피해!");
            LogLine();
        }

        // 👉 스킬 관련
        public static void SkillUse(string user, string skill, string target)
        {
            Log($"🌀 {user}이(가) {target}에게 {skill} 사용!");
            LogLine();
        }

        public static void NoSkill()
        {
            Log("❌ 사용할 수 있는 스킬이 없습니다.");
            LogLine();
        }

        // 👉 기타 상태
        public static void Death(string name)
        {
            Log($"💀 {name}은(는) 쓰러졌습니다!");
            LogLine();
        }

        public static void Victory()
        {
            Log("🎉 모든 적을 처치했습니다!");
            LogLine();
        }

        public static void RunAway()
        {
            Log("🏃 도망쳤습니다!");
            LogLine();
        }

        public static void BattleStart()
        {
            Log("⚔️ 전투 시작!");
            LogLine();
        }
    }
}

using System;
using System.Text;

namespace TextRPG_TeamSix.Utils
{
    internal static class BattleLog
    {
        private static int logStartX = 67; // 로그 박스 안쪽 X 좌표
        private static int logStartY = 2;  // 로그 박스 안쪽 Y 좌표
        private static int currentLine = 0;
        private static int maxLines = 17; // 로그 최대 줄 수 (20 - 상하 테두리)

        // 로그 출력
        public static void Log(string message)
        {
            if (currentLine >= maxLines)
            {
                ClearLogs(); // 다 차면 위에서부터 덮어쓰기
            }

            Console.SetCursorPosition(logStartX, logStartY + currentLine);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message.PadRight(48)); // 오른쪽 덮어쓰기 방지
            Console.ResetColor();

            currentLine++;
        }

        // 빈 줄 추가용
        public static void LogLine()
        {
            Log("");
        }

        // 로그 박스 클리어
        public static void ClearLogs()
        {
            for (int i = 0; i < maxLines; i++)
            {
                Console.SetCursorPosition(logStartX, logStartY + i);
                Console.Write(new string(' ', 48)); // 한 줄 전부 공백으로
            }
            currentLine = 0;
        }

        // 박스 테두리 출력
        public static void DrawLogBox()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Red;

            int x = 65;
            int y = 1;
            int width = 50;
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
        }

        // 전투 메시지들
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

        public static void SkillUse(string user, string skill, string target)
        {
            Log($"🌀 {user}이(가) {target}에게 {skill} 사용!");
            LogLine();
        }

        public static void Death(string name) => Log($"💀 {name}이(가) 쓰러졌습니다...");
        public static void Victory() => Log("🎉 모든 적을 처치했습니다!");
        public static void NoSkill() => Log("❌ 아직 구현되지 않은 스킬입니다.");
        public static void RunAway() => Log("🏃 도망쳤습니다!");
        public static void BattleStart() => Log("⚔️ 전투 시작!");
    }
}

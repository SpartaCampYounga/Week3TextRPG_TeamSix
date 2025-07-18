using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG_TeamSix.Utils
{
    internal static class BattleLog
    {
        private static int logTop = 2;     // 로그 박스 시작 Y
        private static int logLeft = 60;   // 로그 박스 시작 X
        private static int currentLine = 0;

        public static void DrawLogBox()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Red;

            int width = 50;
            int height = 20;

            /// 상단
            //Console.SetCursorPosition(logLeft, logTop - 1);
            //Console.Write("┌" + new string('─', width - 2) + "┐");

            //// 중간
            //for (int i = 0; i < height; i++)
            //{
            //    Console.SetCursorPosition(logLeft, logTop + i);
            //    Console.Write("│" + new string(' ', width - 2) + "│");
            //}

            //// 하단
            //Console.SetCursorPosition(logLeft, logTop + height);
            //Console.Write("└" + new string('─', width - 2) + "┘");

            //Console.ResetColor();
        }


        public static void ClearLogs()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.SetCursorPosition(logLeft + 1, logTop + i);
                Console.Write(new string(' ', 48));
            }
            currentLine = 0;
        }

        public static void Log(string message)
        {
            Console.SetCursorPosition(logLeft + 2, logTop + currentLine);
            Console.WriteLine(message);
            currentLine++;
        }

        public static void BattleStart()
        {
            Log("⚔️ 전투를 시작합니다!");
        }

        public static void Victory()
        {
            Log("🎉 승리했습니다!");
        }

        public static void Death(string name)
        {
            Log($"💀 {name}이(가) 쓰러졌습니다...");
        }
        public static void PlayerAttack(string playerName, string targetName, int damage)
        {
            Log($"📌 {playerName}이(가) {targetName}을(를) 공격했습니다!");
            Log($"💥 {targetName}에게 {damage}의 피해!");
        }
        public static void EnemyAttack(string enemyName, string playerName, int damage)
        {
            Log($"😈 {enemyName}의 공격!");
            Log($"😨 {playerName}에게 {damage}의 피해!");
        }

        public static void SkillUse(string playerName, string skillName, string targetName)
        {
            Log($"✨ {playerName}이(가) {skillName}을(를) 사용했습니다!");
            Log($"💥 {targetName}에게 효과 발생!");
        }

        public static void RunAway()
        {
            Log("🏃 도망쳤습니다...");
        }
    }
}

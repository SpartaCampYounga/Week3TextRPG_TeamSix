namespace TextRPG_TeamSix.Utils
{
    internal static class BattleLog
    {
        public static void Log(string message) => Console.WriteLine(message);
        public static void LogLine() => Console.WriteLine();

        public static void PlayerAttack(string player, string enemy, int damage)
        {
            Log($"{player}이(가) {enemy}을(를) 공격합니다!");
            Log($"{enemy}에게 {damage}의 피해를 입혔습니다!");
            LogLine();
        }

        public static void EnemyAttack(string enemy, string player, int damage)
        {
            Log($"{enemy}의 공격!");
            Log($"{player}에게 {damage}의 피해를 입혔습니다!");
            LogLine();
        }

        public static void Death(string name) => Log($"{name}이(가) 쓰러졌습니다... 게임 오버!");
        public static void Victory() => Log("모든 적을 처치했습니다! 🎉");

        public static void NoSkill() => Log("스킬은 아직 구현이...");
        public static void RunAway() => Log("도망쳤습니다!");
        public static void BattleStart() => Log("Battle!!");
    }
}

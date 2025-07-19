using System.Text;

namespace TextRPG_TeamSix.Utilities
{
    public static class FormatUtility
    {
        public static int GetStringWidth(string str)
        {
            int width = 0;
            foreach (char c in str)
            {
                width += c >= 0xAC00 && c <= 0xD7A3 ? 2 : 1;  //한글 전각 문자 범위
            }

            return width;
        }
        public static string AlignLeftWithPadding(string str, int width)
        {
            int padding = width - GetStringWidth(str);
            padding = Math.Max(0, padding);

            return str + new string(' ', padding);
        }
        public static string AlignRightWithPadding(string str, int width)
        {
            int padding = width - GetStringWidth(str);
            padding = Math.Max(0, padding);

            return new string(' ', padding) + str;
        }
        public static string AlignCenterWithPadding(string str, int width)
        {
            int padding = width - GetStringWidth(str);
            padding = Math.Max(0, padding);

            int padLeft = padding / 2;
            int padRight = padding - padLeft;

            return new string(' ', padRight) + str + new string(' ', padRight);
        }
        public static void DisplayHeader(string title)
        {
            Console.Clear();
            Console.WriteLine();
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔" + new string('═', Console.WindowWidth - 6) + "╗");
            Console.WriteLine("║" + new string(' ', Console.WindowWidth - 6) + "║");
            Console.WriteLine("║" + FormatUtility.AlignCenterWithPadding(title, Console.WindowWidth - 6) + "║");
            Console.WriteLine("║" + new string(' ', Console.WindowWidth - 6) + "║");
            Console.WriteLine("╚" + new string('═', Console.WindowWidth - 6) + "╝");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
        }

    }
}
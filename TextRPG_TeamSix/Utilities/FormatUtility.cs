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
        public static string AlignWithPadding(string str, int width)
        {
            int padding = width - GetStringWidth(str);
            padding = Math.Max(0, padding);

            return str + new string(' ', padding);
        }
    }
}
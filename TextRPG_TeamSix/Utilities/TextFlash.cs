using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Skills;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Utils
{
    public static class TextFlash
    {
        public static void TextFlasht(int times = 2, int delay = 150)
        {
            for (int i = 0; i < times; i++)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.Clear();
                Thread.Sleep(delay);

                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
                Thread.Sleep(delay);
            }

            Console.ResetColor();
        }
    }
}
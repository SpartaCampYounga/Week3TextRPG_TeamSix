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
    public static class TextEffect
    {
        public static void TypeEffect(string text, int delay = 50)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    Console.Write(text.Substring(i));
                    break;
                }

                Console.Write(text[i]);
                Thread.Sleep(delay);
            }

            Console.WriteLine();
        }
    }
}

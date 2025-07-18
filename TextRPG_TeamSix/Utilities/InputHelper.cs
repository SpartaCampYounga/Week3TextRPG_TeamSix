using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_TeamSix.Utilities
{
    internal static class InputHelper
    {
        //입력 받는 것 정리 ex) int GetIntegerInput()

        public static int GetIntegerRange(int min, int max) //min ~ (max-1) 사이의 입력을 받음.
        {
            bool isSuccessful = false;
            int integer;

            do
            {
                string input = Console.ReadLine();
                isSuccessful = int.TryParse(input, out integer) && integer >= min && integer < max;

                if (!isSuccessful)
                {
                    Console.Write($"{min}~{max - 1} 범위의 숫자만 입력해주세요.\n>>");
                }
            } while (!isSuccessful);
            return integer;
        }
        public static void WaitResponse()
        {
            Console.WriteLine("다음으로 넘어가려면 아무거나 누르세요");
            Console.ReadKey(true);
            //Console.WriteLine("0. 다음");
            //int input = GetIntegerRange(0, 1);
        }
    }
}

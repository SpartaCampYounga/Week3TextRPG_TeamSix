using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;

namespace TextRPG_TeamSix.Scenes
{
    //모든 씬이 상속 받는 추상 클래스
    internal abstract class SceneBase
    {
        public SceneBase()
        {

        }

        public abstract void DisplayScene();    //출력 하는 시스템

        public abstract void HandleInput(); //입력 받고 실행하는 시스템

        protected int GetIntegerRange(int min, int max) //min ~ (max-1) 사이의 입력을 받음.
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
        protected void WaitResponse()
        {
            Console.WriteLine("0. 다음");
            int input = GetIntegerRange(0, 1);
        }
    }
}

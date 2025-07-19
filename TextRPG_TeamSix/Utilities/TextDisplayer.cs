using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Skills;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Utilities
{
    internal static class TextDisplayer
    {
        public static int SelectNavigation<T>(List<T> list) //아이템 선택 안하고 탈출할 경우 -1 반환
        {
            Console.OutputEncoding = Encoding.UTF8;
            //header는 밖에서 출력하고 들어올 것

            Console.CursorVisible = false;  //커서 숨김


            //현재 위치 저장
            int currentLeft = Console.CursorLeft;
            int currentTop = Console.CursorTop;

            int selectedIndex = 0;
            ConsoleKey key;

            do
            {
                for(int i = 0; i < list.Count(); i++)
                {
                    if(selectedIndex == i)  //현재 항목이 선택된 경우
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"‣ {list[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {list[i]}");
                    }
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("원하는 항목을 선택한 후 Enter를 눌러 선택하거나 Esc 혹은 Backspace를 눌러 이전 화면으로 이동합니다.");
                Console.ResetColor();

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow) 
                {
                    //selectedIndex = (selectedIndex - 1) % list.Count();   //음수 나와버림...
                    selectedIndex = (list.Count() + selectedIndex - 1) % list.Count(); //+ - 방향 TIL 
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    
                    selectedIndex = (selectedIndex + 1) % list.Count();
                }

                Console.SetCursorPosition(0, currentTop);   //커서 옮기기

            } while (key != ConsoleKey.Enter && key != ConsoleKey.Escape && key != ConsoleKey.Backspace);
            Console.SetCursorPosition(0, currentTop + list.Count() + 2);   //커서 옮기기
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.CursorVisible = true;  //커서 다시 보임
            //Console.Clear();
            if (key == ConsoleKey.Enter)
            {
                return selectedIndex;
            }
            else
            {
                return -1;
            }

            ////사용예제
            //{
            //    int id = TextDisplayer.PageNavigation(GameDataManager.Instance.AllSkills);
            //    if (id >= 0)
            //    {
            //        Console.WriteLine(id);
            //        Skill selectedSkill = new AttackSkill(100, "", "", 0, 0, SkillType.Attack, 0);
            //        selectedSkill.Clone(GameDataManager.Instance.AllSkills[id].Id);
            //        Console.WriteLine(selectedSkill);
            //    }
            //    else
            //    {
            //        Console.WriteLine(id);
            //    }
            //    InputHelper.WaitResponse();
            //}
        }
    }
}

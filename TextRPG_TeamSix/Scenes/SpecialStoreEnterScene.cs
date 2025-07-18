using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Stores;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Scenes
{
    internal class SpecialStoreEnterScene : SceneBase
    {
        public override SceneType SceneType => SceneType.SpecialStoreEnter;

        public override void DisplayScene()
        {
            Console.Clear();
            Console.WriteLine("✨ 어두운 그림자 속에서 비밀스러운 상인이 나타났습니다!");
            Console.WriteLine("상점에 입장하시겠습니까?");
            Console.WriteLine("1. 입장한다");
            Console.WriteLine("2. 돌아간다");
            Console.Write("> ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    SceneManager.Instance.SetScene(SceneType.SpecialStore);
                    break;
                case "2":
                    SceneManager.Instance.SetScene(SceneType.Main);
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                    Console.ReadKey();
                    DisplayScene(); // 다시 보여줌
                    break;

            }
        }
        public override void HandleInput()
        {            
        }
    }

}

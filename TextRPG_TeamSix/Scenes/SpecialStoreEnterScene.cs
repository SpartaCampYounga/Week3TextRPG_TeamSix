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
using TextRPG_TeamSix.Utils;

namespace TextRPG_TeamSix.Scenes
{
    internal class SpecialStoreEnterScene : SceneBase
    {
        public override SceneType SceneType => SceneType.SpecialStoreEnter;

        public override void DisplayScene()
        {
            Console.Clear();
            TextFlash.TextFlasht();
            TextEffect.TypeEffect("✨ 어두운 그림자 속에서 비밀스러운 상점이 나타났습니다!", 40);
            TextEffect.TypeEffect("수상한 기운이 감도는 상점에 입장하시겠습니까?", 30);


            List<string> options = new List<string> { "입장한다", "돌아간다" };
            int selected = TextDisplayer.PageNavigation(options);

            switch (selected)
            {
                case 0:
                    SceneManager.Instance.SetScene(SceneType.SpecialStore);
                    break;
                case 1:
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

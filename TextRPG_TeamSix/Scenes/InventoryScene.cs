using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Characters;

namespace TextRPG_TeamSix.Scenes
{
    //모든 씬이 상속 받는 추상 클래스
    internal abstract class InventoryScene : SceneBase
    {

        public override SceneType SceneType => SceneType.Inventory;
        public InventoryScene()
        {

        }


        public override void DisplayScene() 
        {
           
        }    //출력 하는 시스템

        public override void HandleInput() 
        {

        } //입력 받고 실행하는 시스템

    }
}

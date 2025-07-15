using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Scenes;

namespace TextRPG_TeamSix.Controllers
{
    //게임 시작 시 싱글톤 처리된 매니저(Controller)들 초기화/로드 작업
    internal static class GameInitializer
    {
        private static SceneBase[] _scenes = {
            new SkillScene()
        };
        public static void InitializeAll() 
        {
            SceneManager.Instance.InitializeScenes(_scenes);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Scenes;
using TextRPG_TeamSix.Stores;

namespace TextRPG_TeamSix.Game
{

    //씬 전환 관리
    //Scene 인스턴스들 보관
    internal class SceneManager
    {
        public Store _store { get; private set; }
        public Dictionary<SceneType, SceneBase> _scenes = new Dictionary<SceneType, SceneBase>();
    }
}

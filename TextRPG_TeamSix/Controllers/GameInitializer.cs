using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Scenes;
using TextRPG_TeamSix.Skills;

namespace TextRPG_TeamSix.Controllers
{
    //게임 시작 시 싱글톤 처리된 매니저(Controller)들 초기화/로드 작업
    internal static class GameInitializer
    {
        //일단 하드코딩. 추후 Json 으로 담당할 것.  (Json 생성용)
        private static SceneBase[] _scenes = {
            new SkillScene(),
            new TitleScene(),
            new PlayerSetupScene(),
            new SkillLearnScene()
        };

        private static Skill[] _skills =
        {
            new AttackSkill(1, "스매시", "강하게 내려칩니다.", 10, 3, SkillType.Attack, 20),
            new AttackSkill(2, "후려치기", "강하게 후려칩니다.", 5, 1, SkillType.Attack, 10),
            new AttackSkill(3, "파이어볼", "불을 소환합니다.", 20, 3, SkillType.Attack, 30),
            new HealSkill(4, "치료하기", "HP를 회복합니다.", 30, 5, SkillType.Heal, 30)
        };

        public static void InitializeAll() 
        {
            SceneManager.Instance.InitializeScenes(_scenes);
            GameDataManager.Instance.InitializeSills(_skills);
        }
    }
}

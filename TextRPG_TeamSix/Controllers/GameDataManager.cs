using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Scenes;
using TextRPG_TeamSix.Skills;

namespace TextRPG_TeamSix.Controllers
{
    //게임 내에 존재하는 static 데이터들 관리.
    //싱글턴 구현
    //레벨 디자인된 JSON 파일을 로드한 뒤 게임 내에서 제공.
    internal class GameDataManager
    {
        public List<Skill> AllSkills { get; private set; }
        private GameDataManager()
        {
            AllSkills = new List<Skill>();
        }
        private static GameDataManager instance;
        public static GameDataManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameDataManager();
                }
                return instance;
            }
        }
        //SaveManger에서 JSON 파일을 로드하여 로드된 데이터로 Scenes 초기화
        //SaveManager에서 구현해야할지도..?
        public void InitializeSills(Skill[] skills)
        {
            foreach (Skill skill in skills)
            {
                InitializeSkill(skill);
            }
        }
        public void InitializeSkill(Skill skill)
        {
            AllSkills.Add(skill);
        }
    }
}

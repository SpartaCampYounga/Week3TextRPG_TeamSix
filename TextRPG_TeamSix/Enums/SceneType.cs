using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_TeamSix.Enums
{
    //각 씬 타입 Enum
    //씬 종류 추가될때마다 추가하기.
    internal enum SceneType
    {
        Skill,
        Main,
        PlayerSetup,
        Title,
        SkillLearn,
        Battle,
        Quest,
        Inventory,
        Store,
        Dungeon
    }
}

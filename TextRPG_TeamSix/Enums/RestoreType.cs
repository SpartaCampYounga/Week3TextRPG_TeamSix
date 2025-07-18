using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_TeamSix.Enums
{
    internal enum RestoreType
    {
        // 마나나 스테미나 포션 종류 추가할때. RestoreType Enum 추가하여 관리
        Health, 
        Mana,
        All // 모든 상태를 회복하는 포션
    }
}

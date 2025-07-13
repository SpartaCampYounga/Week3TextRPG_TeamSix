using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Enums;

namespace TextRPG_TeamSix.Character
{
    //능력치 관리
    //공격시 계산 로직, 데미지 로직 등 구현

    internal class Status
    {
        public uint Attack { get; private set; }
        public uint Defence { get; private set; }
        public uint Health { get; private set; }
        public ElementType ElementType { get; private set; }

        public Status()
        {
            Attack = 10;
            Defence = 5;
            Health = 100;
            ElementType = ElementType.None;
        }
    }
}

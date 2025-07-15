using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;

namespace TextRPG_TeamSix.Items
{
    //소비템 인터페이스
    internal interface IConsumable
    {
        void Consume(Character character);
    }
}

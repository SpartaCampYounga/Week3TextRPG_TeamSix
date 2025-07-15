using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;

namespace TextRPG_TeamSix.Controllers
{
    internal class SaveData //현재까지 플레이중인 정보를 저장
    {

        public Player PlayerToSave;
        //그 외에 도감, 던전 진행도 등 저장할 것들 필드로 삼고, 생성자에 입력. 
        public SaveData() 
        {
            PlayerToSave = PlayerManager.Instance.CurrentPlayer;
        }
    }
}

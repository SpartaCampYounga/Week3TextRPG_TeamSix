using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Dungeons;

namespace TextRPG_TeamSix.Controllers
{
    internal class SaveData //현재까지 플레이중인 정보를 저장
    {

        public Player PlayerSave;
        public List<uint> ClearedDungeonList;   //클리어된 던전 아이디만 저장
        //그 외에 도감, 던전 진행도 등 저장할 것들 필드로 삼고, 생성자에 입력. 
        public SaveData() 
        {
            PlayerSave.Clone(PlayerManager.Instance.CurrentPlayer);
            foreach(uint i in PlayerManager.Instance.ClearedDungeonList)
            {
                this.ClearedDungeonList.Add(i);
            }
        }
    }
}

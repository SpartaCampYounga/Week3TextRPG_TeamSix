using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Scenes;

namespace TextRPG_TeamSix.Controllers
{
    //현재 플레이어 정보를 로드/생성.
    //플레이어 싱글톤화하여 단순 저장하는 저장소 개념.   //전역에서 접근 가능함. //GameDataManager와 합칠지 고민
    internal class PlayerManager
    {
        public Player CurrentPlayer { get; private set; }
        private PlayerManager()
        {
            CurrentPlayer = new Player("SCV", JobType.Warrior);
        }
        private static PlayerManager instance;
        public static PlayerManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlayerManager();
                }
                return instance;
            }
        }

        public bool InitializePlayerFromSaveData(string playerName)
        {
            if(SaveManager.Instance.LoadGame(playerName))
            {
                //로드 성공!
                CurrentPlayer.Clone(SaveManager.Instance.SaveData.PlayerToSave);
                Console.WriteLine("플레이어 데이터를 불러왔습니다.");
                return true;
            }
            else
            {
                //로드 실패!
                return false;
            }
        }
    }
}

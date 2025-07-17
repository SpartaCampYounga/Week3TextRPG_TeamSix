using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Quests;
using TextRPG_TeamSix.Scenes;

namespace TextRPG_TeamSix.Controllers
{
    //현재 플레이어 정보를 로드/생성.
    //플레이어 싱글톤화하여 단순 저장하는 저장소 개념.   //전역에서 접근 가능함. //GameDataManager와 합칠지 고민
    internal class PlayerManager
    {
        public Player CurrentPlayer { get; private set; }
        public List<uint> AvailableDungeonList { get; private set;}
        private PlayerManager()
        {
            CurrentPlayer = new Player("PlayerManager", JobType.Warrior);
            AvailableDungeonList = new List<uint>();
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
            if (SaveManager.Instance.LoadGame(playerName))
            {
                //로드 성공!
                CurrentPlayer.Clone(SaveManager.Instance.SaveData.PlayerSave);
                foreach(uint id in SaveManager.Instance.SaveData.AvailableDungeonList)
                {
                    this.AvailableDungeonList.Add(id);
                }
                Console.WriteLine("플레이어 데이터를 불러왔습니다.");
                Console.WriteLine($"불러온 플레이어 이름: {SaveManager.Instance.SaveData.PlayerSave.Name}");
                Console.WriteLine($"CurrentPlayer 이름: {CurrentPlayer.Name}");
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

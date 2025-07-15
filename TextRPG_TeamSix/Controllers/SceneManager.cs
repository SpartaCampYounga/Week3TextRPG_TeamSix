using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Scenes;
using TextRPG_TeamSix.Stores;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Game
{

    //씬 전환 관리
    //Scene 인스턴스들 보관
    internal class SceneManager
    {
        // 캐시
        public Dictionary<SceneType, SceneBase> Scenes { get; private set; }

        //싱글톤
        private SceneManager()
        {
            Scenes = new Dictionary<SceneType, SceneBase>();
        }
        private static SceneManager instance;
        public static SceneManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new SceneManager();
                }
                return instance;
            }
        }

        //현재 씬 참조
        public SceneBase CurrentScene { get; private set; }

        //Scenes 딕셔너리 내부에서 같은 SceneType 키 값을 갖고 있는 씬을 찾아서 CurrentScene에 대입
        public void SetScene(SceneType type)
        {
            if (Scenes.ContainsKey(type))
            {
                CurrentScene = Scenes[type];
                CurrentScene.DisplayScene();
            }
            else
            {
                Console.WriteLine("씬 로드 실패");
                Console.WriteLine("아무키나 입력하면 시작으로 돌아갑니다.");
                Console.ReadKey();
                CurrentScene.DisplayScene();
            }
        }

        //SaveManger에서 JSON 파일을 로드하여 로드된 데이터로 Scenes 초기화
        //SaveManager에서 구현해야할지도..?
        public void InitializeScenes(SceneBase[] scenes)
        {
            foreach (SceneBase scene in scenes)
            {
                InitializeScene(scene);
            }
        }

        public void InitializeScene(SceneBase scene)
        {
            Scenes.Add(scene.SceneType, scene);
        }






        //아래 세이브&로드는 임시로 가져온것
        public void SavePlayer(Player player)
        {
            JsonSerializerSettings setting = JsonHelper.GetJsonSetting();
            // 파일 생성 후 쓰기
            File.WriteAllText(JsonHelper.path + $@"\\player_{player.Name}.json", JsonConvert.SerializeObject(player, setting));
            Console.WriteLine($"{player.Name}(이)가 저장되었습니다.");
        }
        public Player LoadPlayer(string playerName)
        {
            JsonSerializerSettings setting = JsonHelper.GetJsonSetting();

            Player player = null;
            try
            {
                //JsonConvert.PopulateObject(File.ReadAllText(path + $@"\\player_{playerName}.json"), player);
                player = JsonConvert.DeserializeObject<Player>(File.ReadAllText(JsonHelper.path + $@"\\player_{playerName}.json"), setting);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"{playerName}(은)는 존재하지 않습니다.");
                player = new Player(playerName);

            }
            return player;
        }
        // 여기까지 임시로 가져온것
    }
}

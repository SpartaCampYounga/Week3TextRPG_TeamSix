using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Scenes;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Game
{
    //Save, Load 기능을 담당.
    internal class SaveManager
    {

        public SaveData SaveData { get; private set; }

        //싱글톤
        private SaveManager()
        {
        }
        private static SaveManager instance;
        public static SaveManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SaveManager();
                }
                return instance;
            }
        }

        public void SaveGame()
        {
            //저장해야하는 데이터 리스트가 담김 (생성자에서)
            SaveData = new SaveData();

            JsonSerializerSettings setting = JsonHelper.GetJsonSetting();
            // 파일 생성 후 쓰기
            
            File.WriteAllText(JsonHelper.path + $@"\\save_{SaveData.PlayerToSave.Name}.json", JsonConvert.SerializeObject(SaveData, setting));
            Console.WriteLine($"{SaveData.PlayerToSave.Name}(이)가 저장되었습니다.");
        }
        public bool LoadGame(string playerName)
        {
            JsonSerializerSettings setting = JsonHelper.GetJsonSetting();

            try
            {
                //JsonConvert.PopulateObject(File.ReadAllText(path + $@"\\player_{playerName}.json"), player);
                SaveData = JsonConvert.DeserializeObject<SaveData>(File.ReadAllText(JsonHelper.path + $@"\\save_{playerName}.json"), setting);
                SaveData.PlayerToSave.Inventory.SetOwnerAgain(SaveData.PlayerToSave);   //직렬화시 순환 끊었던 것 다시 설정.


                Console.WriteLine($"SaveData를 불러왔습니다.");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"{playerName}(은)는 존재하지 않습니다.");
                return false;
            }
        }
    }
}
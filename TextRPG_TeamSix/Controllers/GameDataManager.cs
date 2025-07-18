using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Dungeons;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Quests;
using TextRPG_TeamSix.Scenes;
using TextRPG_TeamSix.Skills;
using TextRPG_TeamSix.Stores;

namespace TextRPG_TeamSix.Controllers
{
    //게임 내에 존재하는 static 데이터들 관리.
    //싱글턴 구현
    //레벨 디자인된 JSON 파일을 로드한 뒤 게임 내에서 제공.
    internal class GameDataManager
    {
        public List<Skill> AllSkills { get; private set; }
        public List<Item> AllItems { get; private set; }
        public List<Gatcha> AllGatchas { get; private set; }
        public List<Enemy> AllEnemies { get; private set; }
        public List<Dungeon> AllDungeons { get; private set; }
        public List<Quest> AllQuests { get; private set; } // 퀘스트 추가
        public List<Store> AllStores { get; private set; }
        private GameDataManager()
        {
            AllSkills = new List<Skill>();
            AllItems = new List<Item>();
            AllGatchas = new List<Gatcha>();
            AllEnemies = new List<Enemy>();
            AllDungeons = new List<Dungeon>();
            AllQuests = new List<Quest>(); // 퀘스트 추가
            AllStores = new List<Store>();
        }
        private static GameDataManager instance;
        public static GameDataManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameDataManager();
                }
                return instance;
            }
        }
        
        //SaveManger에서 JSON 파일을 로드하여 로드된 데이터로 Scenes 초기화
        
        //스킬 초기화
        public void InitializeSkills(Skill[] skills)
        {
            foreach (Skill skill in skills)
            {
                AllSkills.Add(skill);
            }
        }

        //아이템 초기화
        public void InitializeItems(Item[] items)
        {
            foreach (Item item in items)
            {
                AllItems.Add(item);
            }
        }
        //가챠 초기화
        public void InitializeGatchas(Gatcha[] gatchas)
        {
            foreach(Gatcha gatcha in gatchas)
            {
                AllGatchas.Add(gatcha);
            }

        }
        
        //적 초기화
        public void InitializeEnemies(Enemy[] enemies)
        {
            foreach(Enemy enemy in enemies)
            {
                AllEnemies.Add(enemy);
            }
        }
        
        //던전 초기화
        public void InitializeDungeons(Dungeon[] dungeons)
        {
            foreach (Dungeon dungeon in dungeons)
            {
                AllDungeons.Add(dungeon);
            }
        }

        //퀘스트 초기화
        public void InitializeQuests(Quest[] quests)
        {
            foreach (Quest quest in quests)
            {
                Quest temp = quest.CreateInstance();
                temp.Clone(quest);
                AllQuests.Add(temp);
            }
        }

        //상점 초기화
        public void InitializeStores(Store[] stores)
        {
            foreach (Store store in stores)
            {
                Store temp = store.CreateInstance();
                temp.Clone(store);
                AllStores.Add(temp);
            }
        }
    }
}

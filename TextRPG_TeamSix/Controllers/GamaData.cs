using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Dungeons;
using TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Quests;
using TextRPG_TeamSix.Skills;
using TextRPG_TeamSix.Scenes;
using TextRPG_TeamSix.Stores;

namespace TextRPG_TeamSix.Controllers
{
    internal class GamaData     //인게임에 저장되는 모든 데이터
    {
        public SceneBase[] _scenes { get; set; }
        public Skill[] _skills { get; set; }
        public Item[] _items { get; set; }
        public Gatcha[] _gatchas { get; set; }
        public Enemy[] _emenies { get; set; }
        public Dungeon[] _dungeons { get; set; }
        public Quest[] _quests { get; set; }
        public Store[]  _stores { get; set; }

        [JsonConstructor]
        public GamaData(SceneBase[] scenes, Skill[] skills, Item[] items, Gatcha[] gatchas, Enemy[] enemies, Dungeon[] gundeons, Quest[] quests, Store[] stores)
        {
            _scenes = scenes;
            _skills = skills;
            _items = items;
            _gatchas = gatchas;
            _emenies = enemies;
            _dungeons = gundeons;
            _quests = quests;
            _stores = stores;
        }
    }
}

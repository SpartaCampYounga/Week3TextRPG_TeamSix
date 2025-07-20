using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;

namespace TextRPG_TeamSix.Items
{
    internal class Gatcha
    {
        public string Name { get; private set; }    //GatchaType...? enum...?
        public List<Item> ItemList { get; private set; }
        [JsonConstructor]
        public Gatcha(string name, List<Item> itemList)
        {
            Name = name;
            ItemList = itemList;
        }

        public Item GetItem()
        {
            return ItemList[0]; //로직 구현
        }
    }
}

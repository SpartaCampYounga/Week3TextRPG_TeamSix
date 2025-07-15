using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TextRPG_TeamSix.Utilities
{
    internal class JsonHelper
    {
        //Json 관리 // 엑셀로 관리하는 법은..? 튜터님께 물어보자
        public static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\") + "\\Jsons";
        public static JsonSerializerSettings GetJsonSetting()
        {
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };
            setting.Converters.Add(new StringEnumConverter());
            return setting;
        }
    }
}

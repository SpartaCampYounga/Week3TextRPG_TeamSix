using System.ComponentModel.Design;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Items;
using TextRPG_TeamSix.Scenes;
using TextRPG_TeamSix.Skills;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameInitializer.InitializeAll();

            //Console.WriteLine("헤더");
            //Console.WriteLine("헤더");
            //Console.WriteLine("헤더");
            //List<string> selection = new List<string>()
            //{
            //    "1. 던전",
            //    "2. 마을",
            //    "3. 상점",
            //    "4. 상태창"
            //};
            //int index = TextDisplayer.PageNavigation(GameDataManager.Instance.AllSkills);
            //if (index >= 0)
            //{
            //    Console.WriteLine(index);
            //    Console.WriteLine(GameDataManager.Instance.AllSkills[index]);
            //}
            //else
            //{
            //    Console.WriteLine(index);
            //}
            //InputHelper.WaitResponse();

            SceneManager.Instance.SetScene(SceneType.Title);

        }
    }
}
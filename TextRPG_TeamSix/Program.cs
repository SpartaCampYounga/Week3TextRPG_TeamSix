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
            GameInitializer.InitializeFromJson();


           SceneManager.Instance.SetScene(SceneType.Battle);
            //SceneManager.Instance.SetScene(SceneType.Battle);



            //SceneManager.Instance.SetScene(SceneType.Title);
        }
    }
}
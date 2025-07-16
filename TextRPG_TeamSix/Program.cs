using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Scenes;

namespace TextRPG_TeamSix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameInitializer.InitializeAll();
            //SceneManager.Instance.SetScene(SceneType.Title);

            SceneManager.Instance.SetScene(SceneType.Battle);
        }
    }
}
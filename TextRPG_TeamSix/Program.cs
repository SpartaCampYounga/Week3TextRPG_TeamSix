using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Game;

namespace TextRPG_TeamSix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameInitializer.InitializeAll();
            SceneManager.Instance.SetScene(SceneManager.Instance.Scenes[0].SceneType);
        }
    }
}

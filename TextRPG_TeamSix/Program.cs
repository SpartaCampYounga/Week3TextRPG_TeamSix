using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;

namespace TextRPG_TeamSix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameInitializer.InitializeAll();
            SceneManager.Instance.SetScene(SceneType.Skill);
        }
    }
}
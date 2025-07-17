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

            //SceneManager.Instance.SetScene(SceneType.Title);
           //SceneManager.Instance.SetScene(SceneType.SkillLearn);
            //SceneManager.Instance.SetScene(SceneType.Battle);
            //Console.WriteLine("헤더");
            //Console.WriteLine("헤더");
            //Console.WriteLine("헤더");

            //int id = TextDisplayer.PageNavigation(GameDataManager.Instance.AllSkills);
            //if (id >= 0)
            //{
            //    Console.WriteLine(id);
            //    Skill selectedSkill = new AttackSkill(100, "", "", 0, 0, SkillType.Attack, 0);
            //    selectedSkill.Clone(GameDataManager.Instance.AllSkills[id].Id);
            //    Console.WriteLine(selectedSkill);
            //}else
            //{
            //    Console.WriteLine(id);

            SceneManager.Instance.SetScene(SceneType.Title);


        }
    }
}
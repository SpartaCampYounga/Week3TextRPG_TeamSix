using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Quests;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Scenes
{
    //모든 씬이 상속 받는 추상 클래스
    internal class QuestScene : SceneBase
    {
        public override SceneType SceneType => SceneType.Quest;

        // 필드로 선언
        private List<Quest> acceptedQuests = PlayerManager.Instance.AcceptedQuestList;
        int input;

        public override void DisplayScene()
        {
            //Console.Clear();
            //Console.WriteLine("QuestScene");
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine(new string('=', 120));
            //Console.WriteLine("모험가 사무소에 온것을 환영합니다.");
            //Console.WriteLine(new string('=', 120));
            FormatUtility.DisplayHeader("모험가 사무소에 오신 것을 환영합니다");

            // 헤더
            string header = "";
            header += FormatUtility.AlignLeftWithPadding("유형", 10) + " | ";
            header += FormatUtility.AlignLeftWithPadding("내용", 30) + " | ";
            header += FormatUtility.AlignLeftWithPadding("골드보상", 10) + " | ";
            header += FormatUtility.AlignLeftWithPadding("경험치보상", 10) + " | ";
            header += FormatUtility.AlignLeftWithPadding("목표", 10) + " | ";
            header += FormatUtility.AlignLeftWithPadding("진행", 7);
            Console.WriteLine(header);
            Console.WriteLine(new string('-', 120));
            Console.ResetColor();


            foreach (Quest quest in acceptedQuests)
            {
                Console.WriteLine(quest);
            }
            Console.WriteLine();
            List<string> selections = new List<string>()
            { 
                "새 퀘스트 받기",
                "퀘스트 완료 보상 확인"
            };

            input = TextDisplayer.SelectNavigation(selections);
            HandleInput();

            //while (true)
            //{
            //    Console.Clear();
            //    Console.WriteLine("QuestScene");
            //    Console.ForegroundColor = ConsoleColor.Green;
            //    Console.WriteLine(new string('=', 120));
            //    Console.WriteLine("모험가 사무소에 온것을 환영합니다.");
            //    Console.WriteLine(new string('=', 120));

            //    // 헤더
            //    string header = "";
            //    header += FormatUtility.AlignWithPadding("NO", 5) + " | ";
            //    header += FormatUtility.AlignWithPadding("유형", 15) + " | ";
            //    header += FormatUtility.AlignWithPadding("내용", 30) + " | ";
            //    header += FormatUtility.AlignWithPadding("보상", 30) + " | ";
            //    header += FormatUtility.AlignWithPadding("진행사항", 0);
            //    Console.WriteLine(header);
            //    Console.WriteLine(new string('-', 120));
            //    Console.ResetColor();

            //    // 퀘스트 리스트 출력
            //    for (int i = 0; i < availableQuests.Count; i++)
            //    {
            //        var quest = availableQuests[i];
            //        string reward = $"{quest.RewardGold}골드, {quest.RewardExp}경험치";
            //        string progress = "미완료";
            //        string row = "";
            //        row += FormatUtility.AlignWithPadding((i + 1).ToString(), 5) + " | ";
            //        row += FormatUtility.AlignWithPadding(quest.QuestType.ToString(), 15) + " | ";
            //        row += FormatUtility.AlignWithPadding(quest.Description, 30) + " | ";
            //        row += FormatUtility.AlignWithPadding(reward, 30) + " | ";
            //        row += FormatUtility.AlignWithPadding(progress, 0);
            //        Console.WriteLine(row);
            //    }

            //    // 헤더 닫기
            //    Console.ForegroundColor = ConsoleColor.Green;
            //    Console.WriteLine(new string('=', 120));
            //    Console.ResetColor();

            //    // 수락한 퀘스트 리스트 출력
            //    // 애너미나 던전에 대한 요소가 추후 카운트를 반영할때 어떻게 넣어야할지 고민이 필요
            //    // 퀘스트 완료에 따라 acceptedQuests에서 제거하는 로직도 필요
            //    if (acceptedQuests.Count > 0)
            //    {
            //        Console.WriteLine();
            //        Console.ForegroundColor = ConsoleColor.Yellow;
            //        Console.WriteLine(new string('=', 120));
            //        Console.WriteLine("[수락한 퀘스트]");
            //        Console.WriteLine(new string('-', 120));
            //        for (int i = 0; i < acceptedQuests.Count; i++)
            //        {
            //            var quest = acceptedQuests[i];
            //            string reward = $"{quest.RewardGold}골드, {quest.RewardExp}경험치"; // 이 보상을 나중에 player에게 보내야함
            //            string progress = $"진행중(0/{quest.Count})"; // 에너미나 던전에 대한 카운트값 구해야함
            //            string row = "";
            //            row += FormatUtility.AlignWithPadding((i + 1).ToString(), 5) + " | ";
            //            row += FormatUtility.AlignWithPadding(quest.QuestType.ToString(), 15) + " | ";
            //            row += FormatUtility.AlignWithPadding(quest.Description, 30) + " | ";
            //            row += FormatUtility.AlignWithPadding(reward, 30) + " | ";
            //            row += FormatUtility.AlignWithPadding(progress, 0);
            //            Console.WriteLine(row);
            //        }
            //        Console.WriteLine(new string('=', 120));
            //        Console.ResetColor();
            //        Console.WriteLine();
            //    }

            //    // 마을로 나가기 & 퀘스트 수락 처리
            //    if (availableQuests.Count == 0)
            //    {
            //        Console.WriteLine("수락 가능한 퀘스트가 없습니다. 0을 입력해 나가세요.");
            //    }
            //    else
            //    {
            //        Console.WriteLine("퀘스트 수락을 원하시면 번호를 입력해 주세요.");
            //    }
            //    Console.WriteLine("0. 나가기");
            //    Console.Write("번호를 입력해 주세요 : ");
            //    int input = InputHelper.GetIntegerRange(0, availableQuests.Count + 1); // +1은 0을 포함하기 위함

            //    if (input == 0)
            //    {
            //        SceneManager.Instance.SetScene(SceneType.Main);
            //        break;
            //    }
            //    else
            //    {
            //        // 퀘스트 수락 처리
            //        var selectedQuest = availableQuests[input - 1];
            //        acceptedQuests.Add(selectedQuest);
            //        availableQuests.RemoveAt(input - 1);

            //        Console.ForegroundColor = ConsoleColor.Cyan;
            //        Console.WriteLine($"'{selectedQuest.Description}' 퀘스트를 수락하였습니다!");
            //        Console.ResetColor();
            //        Console.WriteLine("계속하려면 아무 키나 누르세요...");
            //        Console.ReadKey();
            //    }
            //}
        }

        public override void HandleInput()
        {
            switch (input)
            {
                case -1:
                    SceneManager.Instance.SetScene(SceneType.Main);
                    break;
                case 0:
                    SceneManager.Instance.SetScene(SceneType.QuestAccept);
                    break;
                case 1:
                    SceneManager.Instance.SetScene(SceneType.QuestReward);
                    break;
                default:
                    break;
            }
        }
    }
}


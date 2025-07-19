using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamSix.Characters;
using TextRPG_TeamSix.Controllers;
using TextRPG_TeamSix.Enums;
using TextRPG_TeamSix.Game;
using TextRPG_TeamSix.Quests;
using TextRPG_TeamSix.Skills;
using TextRPG_TeamSix.Utilities;

namespace TextRPG_TeamSix.Scenes
{
    //모든 씬이 상속 받는 추상 클래스
    internal class QuestAcceptScene : SceneBase
    {
        public override SceneType SceneType => SceneType.QuestAccept;

        // 필드로 선언
        List<Quest> acceptedQuests = PlayerManager.Instance.AcceptedQuestList;
        List<Quest> availableQuests;
        int input;

        public override void DisplayScene()
        {
            // availableQuests는 acceptedQuests를 제외한 리스트로 만듦
            availableQuests = GameDataManager.Instance.AllQuests
                .Where(q => !acceptedQuests.Any(aq => aq.Id == q.Id))
                .ToList();

            //Console.Clear();
            //Console.WriteLine("QuestAcceptScene");
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine(new string('=', 120));
            //Console.WriteLine("퀘스트를 수락할 수 있습니다.");
            //Console.WriteLine(new string('=', 120));

            FormatUtility.DisplayHeader("퀘스트를 수락할 수 있습니다 ");

            ////플레이어 미보유 중인 퀘스트만 띄우기
            if (availableQuests.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("당장은 할 게 없는데?");
                Console.ResetColor();
                Console.WriteLine();
                input = -2;
                InputHelper.WaitResponse();
            }
            else
            {   // 헤더
                string header = "  ";
                header += FormatUtility.AlignLeftWithPadding("유형", 10) + " ┊ ";
                header += FormatUtility.AlignLeftWithPadding("내용", 30) + " ┊ ";
                header += FormatUtility.AlignLeftWithPadding("골드보상", 10) + " ┊ ";
                header += FormatUtility.AlignLeftWithPadding("경험치보상", 10) + " ┊ ";
                header += FormatUtility.AlignLeftWithPadding("목표", 10) + " ┊ ";
                header += FormatUtility.AlignLeftWithPadding("진행", 7);
                Console.WriteLine(header);
                Console.WriteLine(new string('═', 120));
                Console.ResetColor();
                Console.ResetColor();

                input = TextDisplayer.SelectNavigation(availableQuests);
            }
            HandleInput();
        }

        public override void HandleInput()
        {
            switch (input)
            {
                case -1:
                case -2:
                    SceneManager.Instance.SetScene(SceneType.Quest);    //0번 누르면 해당 타입의 씬 출력
                    break;
                default:
                    
                    Quest selectedQuest = availableQuests[input].CreateInstance();

                    if (PlayerManager.Instance.AcceptedQuestList.FirstOrDefault(x => x.Id == selectedQuest.Id) != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("이미 진행중인 퀘스트 입니다.");
                        Console.ResetColor();
                        InputHelper.WaitResponse();
                    }
                    else
                    {
                        selectedQuest.Clone(availableQuests[input]);

                        PlayerManager.Instance.AcceptedQuestList.Add(selectedQuest);  //테스트용 임시 처리

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"'{selectedQuest.Description}' 퀘스트를 수락하였습니다!");
                        Console.ResetColor();

                        InputHelper.WaitResponse();
                    }
                    SceneManager.Instance.SetScene(SceneType.QuestAccept);
                    break;
            }
        }
    }
}


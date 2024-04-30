using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle.Screen
{
    internal class QuestScreen
    {
        public static void Print()
        {
            List<Quest> quests = GameManager.Instance.quests;
            
            Console.Clear();

            ConsoleUtility.ShowTitle("■ Quest!! ■");
            Console.WriteLine("");

            Console.WriteLine("0. 나가기");
            for(int i = 0; i < quests.Count; i++)
            {
                quests[i].PrintQuestName(i+1);
            }

            int KeyInput = ConsoleUtility.PromptMenuChoice(0, quests.Count);

            switch (KeyInput)
            {
                case 0:
                    GameStartScreen.Print();
                    break;
                default:
                    GetQuest(quests[KeyInput - 1]);
                    break;
            }
            void GetQuest(Quest quest, string? prompt = null)
            {
                if (prompt != null)
                {
                    // 1초간 메시지를 띄운 다음에 다시 진행
                    Console.Clear();
                    ConsoleUtility.ShowTitle(prompt);
                    Thread.Sleep(1000);
                }

                Console.Clear();

                ConsoleUtility.ShowTitle("■ Quest!! ■");
                Console.WriteLine("");

                Console.WriteLine($"{quest.Name}");
                Console.WriteLine("");
                Console.WriteLine($"{quest.Bio}");
                Console.WriteLine("");
                Console.WriteLine($"{quest.Mission}");
                Console.WriteLine("");
                Console.WriteLine($"-보상-");
                Console.WriteLine("");

                if(quest.isCleared)
                {
                    Console.WriteLine("1. 수락");
                    Console.WriteLine("2. 거절");
                    int KeyInput = ConsoleUtility.PromptMenuChoice(1, 2);

                    switch (KeyInput)
                    {
                        case 1:
                            quest.isInProgress = true;
                            break;
                       case 2:
                            break;
                    }
                    QuestScreen.Print();
                }
                else
                {
                    Console.WriteLine("1. 보상 받기");
                    Console.WriteLine("2. 돌아가기");
                    int KeyInput = ConsoleUtility.PromptMenuChoice(1, 2);

                    switch (KeyInput)
                    {
                        case 1:
                            if(quest.isAlreadyCleared)
                            {
                                GetQuest(quest, "이미 보상을 수령 하였습니다.");
                            }
                            else
                            {
                                quest.GetReward();
                                QuestScreen.Print();
                            }
                            break;
                        case 2:
                            QuestScreen.Print();
                            break;
                    }
                }

                
            }
        }
    }
}

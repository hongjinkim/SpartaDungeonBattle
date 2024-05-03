using SpartaDungeonBattle.Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle
{
    internal class GameStartScreen
    {
        public static void Print(string? prompt = null)
        {
            if (prompt != null)
            {
                // 1초간 메시지를 띄운 다음에 다시 진행
                Console.Clear();
                ConsoleUtility.ShowTitle(prompt);
                Thread.Sleep(1000);
            }
            // 구성
            // 0. 화면 정리
            Console.Clear();

            // 1. 선택 멘트를 줌
            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.WriteLine("");

            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 전투 시작");
            Console.WriteLine("5. 휴식하기");
            Console.WriteLine("6. 퀘스트");
            Console.WriteLine("7. 저장하기");
            Console.WriteLine("");

            // 2. 선택한 결과를 검증함

            switch (ConsoleUtility.PromptMenuChoice(1, 7))
            {
                case 1:
                    StatusScreen.Print();
                    break;
                case 2:
                    InventoryScreen.Print();
                    break;
                case 3:
                    StoreScreen.Print();
                    break;
                case 4:
                    BattleScreen.Print();
                    break;
                case 5:
                    RestScreen.Print();
                    break;
                case 6:
                    QuestScreen.Print();
                    break;
                case 7:
                    SaveManager.SaveGame(GameManager.Instance);
                    GameStartScreen.Print("게임이 저장되었습니다.");
                    break;
            }
        }
    }
}

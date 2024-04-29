using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle.Screen
{
    internal class RestScreen
    {
        // 휴식하기
        public static void Print(string? prompt = null)
        {
            Player player = GameManager.Instance.player;

            if (prompt != null)
            {
                // 1초간 메시지를 띄운 다음에 다시 진행
                Console.Clear();
                ConsoleUtility.ShowTitle(prompt);
                Thread.Sleep(1000);
            }

            Console.Clear();

            ConsoleUtility.ShowTitle("■ 휴식하기 ■");
            ConsoleUtility.PrintTextHighlights("500 G 를 내면 체력을 회복 할 수 있습니다. (보유 골드 : ", player.Gold.ToString(), " G)");
            Console.WriteLine("");

            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");

            switch (ConsoleUtility.PromptMenuChoice(0, 1))
            {
                case 0:
                    //GameStartScreen();
                    break;
                case 1:
                    if (player.Gold >= 500)
                    {
                        player.Gold -= 500;
                        player.Health = 100;
                        RestScreen.Print("휴식을 완료했습니다.");
                    }
                    // 돈이 모자라는 경우
                    else
                    {
                        RestScreen.Print("Gold가 부족합니다.");
                    }
                    break;
            }
        }

    }
}

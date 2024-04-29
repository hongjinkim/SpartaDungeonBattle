using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle
{
    internal class StartScreen
    {
        public static void Print()
        {
            Console.WriteLine("1. 새로하기");
            Console.WriteLine("2. 이어하기");
            Console.WriteLine("");

            // 2. 선택한 결과를 검증함

            switch (ConsoleUtility.PromptMenuChoice(1, 2))
            {
                case 1:
                    //캐릭터 생성 화면으로 이동
                    break;
                case 2:
                    //불러오기 후 게임시작 화면으로 이동
                    //Instance.saveManager.LoadGame(this);
                    GameStartScreen.Print();
                    break;
            }
        }
    }
}

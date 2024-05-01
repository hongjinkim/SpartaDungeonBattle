using SpartaDungeonBattle.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle.Screen
{
    internal class NameScreen
    {
        private Player player;

        private void NameCreate()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요.");
            Console.Write(">> ");
            string playerName = Console.ReadLine();


            if (string.IsNullOrWhiteSpace(playerName))
            {
                Console.WriteLine("다시 입력해주세요");
                NameCreate();
                return;
            }

            Console.Clear();
            Console.WriteLine("직업을 골라주세요 (1.전사, 2.도적): ");
            string clas = Console.ReadLine();

            player = new Player(playerName);
            player.SelectClass(clas);

            StartScreen.Print();
        }
    }
}

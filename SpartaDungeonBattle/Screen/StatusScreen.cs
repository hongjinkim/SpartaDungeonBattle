using SpartaDungeonBattle.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle
{
    internal class StatusScreen
    {
        public static void Print()
        {
            Player player = GameManager.Instance.player;
            List<EquipItem> inventory = GameManager.Instance.inventory;

            Console.Clear();

            ConsoleUtility.ShowTitle("■ 상태보기 ■");
            Console.WriteLine("캐릭터의 정보가 표기됩니다.");

            ConsoleUtility.PrintTextHighlights("Lv. ", player.Level.ToString("00"));
            ConsoleUtility.PrintTextHighlights("Exp: ", player.Exp.ToString());
            Console.WriteLine("");
            Console.WriteLine($"{player.Name} ( {player.Class} )");

            // TODO : 능력치 강화분을 표현하도록 변경

            int bonusAtk = inventory.Select(item => item.isEquipped ? item.Str : 0).Sum();
            int bonusDef = inventory.Select(item => item.isEquipped ? item.Def : 0).Sum();
            int bonusHp = inventory.Select(item => item.isEquipped ? item.Hp : 0).Sum();

            ConsoleUtility.PrintTextHighlights("공격력 : ", (player.Strength_Default).ToString(), bonusAtk > 0 ? $" (+{bonusAtk})" : "");
            ConsoleUtility.PrintTextHighlights("방어력 : ", (player.Defence_Default).ToString(), bonusDef > 0 ? $" (+{bonusDef})" : "");
            ConsoleUtility.PrintTextHighlights("체 력 : ", (player.Health).ToString(), bonusHp > 0 ? $" (+{bonusHp})" : "");

            ConsoleUtility.PrintTextHighlights("Gold : ", player.Gold.ToString());
            Console.WriteLine("");

            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine("");

            switch (ConsoleUtility.PromptMenuChoice(0, 0))
            {
                case 0:
                    GameStartScreen.Print();
                    break;
            }

        }
    }
}

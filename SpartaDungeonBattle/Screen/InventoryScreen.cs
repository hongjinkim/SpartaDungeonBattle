using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle.Screen
{
    internal class InventoryScreen
    {
        public static void Print()
        {
            List<EquipItem> inventory = GameManager.Instance.inventory;

            Console.Clear();

            ConsoleUtility.ShowTitle("■ 인벤토리 ■");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < inventory.Count; i++)
            {
                inventory[i].PrintItemStatDescription();
            }

            Console.WriteLine("");
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");

            switch (ConsoleUtility.PromptMenuChoice(0, 1))
            {
                case 1:
                    EquipScreen();
                    break;
                case 0:
                    GameStartScreen.Print();
                    break;
            }
            void EquipScreen()
            {
                Console.Clear();

                ConsoleUtility.ShowTitle("■ 인벤토리 - 장착 관리 ■");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine("");
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < inventory.Count; i++)
                {
                    inventory[i].PrintItemStatDescription(true, i + 1); // 나가기가 0번 고정, 나머지가 1번부터 배정
                }
                Console.WriteLine("");
                Console.WriteLine("0. 나가기");

                int KeyInput = ConsoleUtility.PromptMenuChoice(0, inventory.Count);

                switch (KeyInput)
                {
                    case 0:
                        GameStartScreen.Print();
                        break;
                    default:
                        GameManager.Instance.EquipItem(KeyInput - 1);
                        EquipScreen();
                        break;
                }

            }
        }
        // 장비 관리 - 장착
        
    }
}

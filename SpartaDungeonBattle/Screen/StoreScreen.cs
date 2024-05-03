using SpartaDungeonBattle.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle.Screen
{
    internal class StoreScreen
    {
        public static void Print()
        {
            Player player = GameManager.Instance.player;
            List<EquipItem> inventory = GameManager.Instance.inventory;
            List<EquipItem> products = GameManager.Instance.products;

            Console.Clear();

            ConsoleUtility.ShowTitle("■ 상점 ■");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유 골드]");
            ConsoleUtility.PrintTextHighlights("", player.Gold.ToString(), " G");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < products.Count; i++)
            {
                products[i].PrintStoreItemDescription();
            }
            Console.WriteLine("");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");

            switch (ConsoleUtility.PromptMenuChoice(0, 2))
            {
                case 0:
                    GameStartScreen.Print();
                    break;
                case 1:
                    BuyScreen();
                    break;
                case 2:
                    SellScreen();
                    break;
            }
            // 상점 - 구매
            void BuyScreen(string? prompt = null)
            {
                if (prompt != null)
                {
                    // 1초간 메시지를 띄운 다음에 다시 진행
                    Console.Clear();
                    ConsoleUtility.ShowTitle(prompt);
                    Thread.Sleep(1000);
                }

                Console.Clear();

                ConsoleUtility.ShowTitle("■ 상점 ■");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine("");
                Console.WriteLine("[보유 골드]");
                ConsoleUtility.PrintTextHighlights("", player.Gold.ToString(), " G");
                Console.WriteLine("");
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < products.Count; i++)
                {
                    products[i].PrintStoreItemDescription(true, i + 1);
                }
                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");

                int keyInput = ConsoleUtility.PromptMenuChoice(0, products.Count);

                switch (keyInput)
                {
                    case 0:
                        StoreScreen.Print();
                        break;
                    default:
                        // 1 : 이미 구매한 경우
                        if (products[keyInput - 1].isAlreadyBuyed) // index 맞추기
                        {
                            BuyScreen("이미 구매한 아이템입니다.");
                        }
                        // 2 : 돈이 충분해서 살 수 있는 경우
                        else if (player.Gold >= products[keyInput - 1].Price)
                        {
                            player.Gold -= products[keyInput - 1].Price;
                            products[keyInput - 1].TogglePurchaseStatus();
                            inventory.Add(products[keyInput - 1]);
                            BuyScreen();
                        }
                        // 3 : 돈이 모자라는 경우
                        else
                        {
                            BuyScreen("Gold가 부족합니다.");
                        }
                        break;
                }
            }
            // 상점 - 판매
            void SellScreen()
            {
                Console.Clear();

                ConsoleUtility.ShowTitle("■ 상점 ■");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine("");
                Console.WriteLine("[보유 골드]");
                ConsoleUtility.PrintTextHighlights("", player.Gold.ToString(), " G");
                Console.WriteLine("");
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < inventory.Count; i++)
                {
                    inventory[i].PrintItemSellDescription(true, i + 1);
                }
                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");

                int keyInput = ConsoleUtility.PromptMenuChoice(0, products.Count);

                switch (keyInput)
                {
                    case 0:
                        StoreScreen.Print();
                        break;
                    default:
                        SellItem(keyInput - 1);
                        SellScreen();
                        break;
                }
                void SellItem(int idx)
                {
                    if (inventory[idx].isEquipped)
                    {
                        GameManager.Instance.EquipItem(idx);
                    }
                    inventory[idx].TogglePurchaseStatus();
                    player.Gold += (int)(inventory[idx].Price * 0.85f);
                    inventory.RemoveAt(idx);
                }
            }

        }
       
    }
}

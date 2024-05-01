using SpartaDungeonBattle.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle.Screen
{
    internal class BattleScreen
    {
        // 던전 입장
        public static void Print()
        {
            Player player = GameManager.Instance.player;

            List<Tuple<string, int, int>> Dungeons = new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("쉬운 던전", 5, 1000),
                new Tuple<string, int, int>("일반 던전", 11, 1700),
                new Tuple<string, int, int>("어려운 던전", 17, 2500),
            };
            Console.Clear();

            ConsoleUtility.ShowTitle("■ 던전 입장 ■");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다");
            Console.WriteLine("");

            Console.WriteLine($"1. {Dungeons[0].Item1}\t | 방어력 {Dungeons[0].Item2} 이상 권장");
            Console.WriteLine($"2. {Dungeons[1].Item1}\t | 방어력 {Dungeons[1].Item2} 이상 권장");
            Console.WriteLine($"3. {Dungeons[2].Item1}\t | 방어력 {Dungeons[2].Item2} 이상 권장");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");

            int keyInput = ConsoleUtility.PromptMenuChoice(0, Dungeons.Count);

            switch (keyInput)
            {
                case 0:
                    GameStartScreen.Print();
                    break;
                default:
                    DungeonClearScreen(Dungeons[keyInput - 1]);
                    break;
            }

            // 던전 결과 (클리어, 실패)
            void DungeonClearScreen(Tuple<string, int, int> Dungeon)
            {
                Console.Clear();

                if (isCleared(Dungeon.Item2))
                {
                    Random random = new Random();
                    int bonus = random.Next((int)player.Strength, (int)(player.Strength * 2));
                    int prize = Dungeon.Item3 + Dungeon.Item3 / 100 * bonus;
                    int healthLost = random.Next(20 + (Dungeon.Item2 - player.Defence), 35 + (Dungeon.Item2 - player.Defence));
                    ConsoleUtility.ShowTitle("■ 던전 클리어 ■");
                    Console.WriteLine($"축하합니다!! \n{Dungeon.Item1}을 클리어 하였습니다.");
                    Console.WriteLine("");
                    Console.WriteLine("[탐험 결과]");
                    ConsoleUtility.PrintTextHighlights("체력 ", $"{player.Health} -> {player.Health -= healthLost}", "");
                    ConsoleUtility.PrintTextHighlights("Gold ", $"{player.Gold} G -> {player.Gold += prize} G", "");
                    Console.WriteLine("");


                    player.ClearTimes++;
                    player.UpdateStatus();
                }
                else
                {
                    ConsoleUtility.ShowTitle("■ 던전 실패 ■");
                    Console.WriteLine($"아쉽습니다...\n{Dungeon.Item1}클리어에 실패 하였습니다");
                    Console.WriteLine("");
                    Console.WriteLine("[탐험 결과]");
                    ConsoleUtility.PrintTextHighlights("체력 ", $"{player.Health} -> {player.Health /= 2}", "");
                    Console.WriteLine("");
                }

                Console.WriteLine("0. 나가기");
                Console.WriteLine("");

                switch (ConsoleUtility.PromptMenuChoice(0, 0))
                {
                    case 0:
                        GameStartScreen.Print();
                        break;
                }
                bool isCleared(int difficulty)
                {
                    if (player.Defence < difficulty)
                    {
                        Random random = new Random();
                        int rnum = random.Next(100);
                        if (rnum < 40)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
        }
        
        
    }
}

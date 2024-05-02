using SpartaDungeonBattle.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpartaDungeonBattle.Screen
{
    internal class BattleScreen
    {
        enum Monsters
        {
            MINION,
            VOIDLING,
            SIEGEMINION
        }
        // 던전 입장
        public static void Print()
        {
            Console.Clear();

            Player player = GameManager.Instance.player;
            GameManager.Instance.tempExp = 0;

            if (player.Health == 0)
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("체력이 부족합니다");
                Thread.Sleep(1000);
                GameStartScreen.Print();
                return;
            }
            bool isPlayerPhase = true;
            int startHealth = player.Health;
            int startExp = player.Exp;
            int startLevel = player.Level;

            List<Monster> monsterSpawned = new List<Monster>();

            SpawnMonsters();

            PlayerPhase();

            void SpawnMonsters()
            {
                Random random = new Random();
                int spawnTimes = random.Next(4);
                int spawnIndex;
                int count = System.Enum.GetValues(typeof(Monsters)).Length;
                while (0 <= spawnTimes--)
                {
                    spawnIndex = random.Next(count - 1);
                    switch (spawnIndex)
                    {
                        case 0:
                            monsterSpawned.Add(new Minion());
                            break;
                        case 1:
                            monsterSpawned.Add(new Voidling());
                            break;
                        case 2:
                            monsterSpawned.Add(new SiegeMinion());
                            break;
                    }

                }
            }

            void Attack()
            {
                Console.Clear();

                ConsoleUtility.ShowTitle("Battle!!");

                for (int i = 0; i < monsterSpawned.Count(); i++)
                {
                    monsterSpawned[i].PrintMonsterDescription(true, i + 1);
                }

                Console.WriteLine("");
                player.PrintBattleDescription();
                Console.WriteLine("");
                Console.WriteLine("0. 취소");

                int keyInput = PromptMenuChoiceCheckDeath(0, monsterSpawned.Count());

                switch (keyInput)
                {
                    case 0:
                        PlayerPhase();
                        break;
                    default:
                        PrintResult(player, monsterSpawned[keyInput - 1], "공격");
                        break;
                }

                int PromptMenuChoiceCheckDeath(int min, int max)
                {
                    while (true)
                    {
                        Console.Write("원하시는 번호를 입력해주세요\n>> ");
                        if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                        {
                            if (choice != 0)
                            {
                                if (monsterSpawned[choice - 1].IsDead)
                                {
                                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                                }
                                else
                                {
                                    return choice;
                                }
                            }
                            else
                            {
                                return choice;
                            }

                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                        }
                    }
                }

            }
            void CheckClear()
            {
                int isCleared = 0;
                foreach (Monster monster in monsterSpawned)
                {
                    monster.PrintMonsterDescription();
                    if (monster.IsDead)
                    {
                        isCleared++;
                    }
                }
                if (isCleared == monsterSpawned.Count())
                {
                    BattleResult(true);
                    return;
                }
            }
            void PlayerPhase()
            {
                player.UpdateStatus();
                CheckClear();

                Console.Clear();
                ConsoleUtility.ShowTitle("Battle!!");
                Console.WriteLine("");

                foreach (Monster monster in monsterSpawned)
                {
                    monster.PrintMonsterDescription();
                }

                Console.WriteLine("");
                player.PrintBattleDescription();
                Console.WriteLine("");
                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 스킬");

                int keyInput = ConsoleUtility.PromptMenuChoice(1, 2);

                switch (keyInput)
                {
                    case 1:
                        Attack();
                        break;
                    case 2:
                        //Skill();
                        break;
                }
            }
            void EnemyPhase()
            {
                foreach (Monster monster in monsterSpawned)
                {
                    PrintResult(monster, player, "공격");
                    if (player.IsDead)
                    {
                        BattleResult(false);
                        return;
                    }
                }
                isPlayerPhase = true;
                PlayerPhase();
            }
            void PrintResult(ICharacter attacker, ICharacter underAttack, string attackType)
            {
                if (!attacker.IsDead)
                {
                    int health = underAttack.Health;
                    int damage = underAttack.TakeDamage(attacker.Attack);
                    Console.Clear();

                    ConsoleUtility.ShowTitle("Battle!!");
                    Console.WriteLine("");

                    Console.WriteLine($"{attacker.Name} 의 {attackType}!");
                    Console.WriteLine($"{underAttack.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
                    Console.WriteLine("");
                    Console.WriteLine($"{underAttack.Name}");
                    if (underAttack.IsDead)
                    {
                        Console.WriteLine($"HP {health} -> Dead");
                    }
                    else
                    {
                        Console.WriteLine($"HP {health} -> {underAttack.Health}");
                    }
                    Console.WriteLine("");
                    Console.WriteLine("0. 다음");
                    switch (ConsoleUtility.PromptMenuChoice(0, 0))
                    {
                        case 0:
                            CheckClear();
                            if (isPlayerPhase)
                            {
                                isPlayerPhase = !isPlayerPhase;
                                EnemyPhase();
                            }
                            break;
                    }
                }
                else
                {
                    CheckClear();
                    if (isPlayerPhase)
                    {
                        isPlayerPhase = !isPlayerPhase;
                        EnemyPhase();
                    }
                }
            }
            void BattleResult(bool isCleared)
            {
                Console.Clear();

                ConsoleUtility.ShowTitle("Battle!!");
                Console.WriteLine("");
                if (isCleared)
                {
                    player.Exp += GameManager.Instance.tempExp;
                    Console.WriteLine("Victory");
                    Console.WriteLine("");
                    Console.WriteLine($"던전에서 몬스터 {monsterSpawned.Count}마리를 잡았습니다.");
                    Console.WriteLine("");
                    Console.Write($"Lv.{startLevel} {player.Name}");
                    if (startLevel != player.Level)
                    {
                        Console.Write($" -> Lv.{player.Level} {player.Name}");
                    }
                    Console.WriteLine("");
                    Console.WriteLine($"HP {startHealth} -> {player.Health}");
                    Console.WriteLine($"exp {startExp} -> {player.Exp}");

                    Console.WriteLine("");
                    GetReward();
                }
                else
                {
                    Console.WriteLine("You Lose");
                    Console.WriteLine("");
                    Console.WriteLine($"Lv.{startLevel} {player.Name}");
                    Console.WriteLine($"HP {startHealth} -> {player.Health}");
                }
                Console.WriteLine("");
                Console.WriteLine("0. 다음");
                switch (ConsoleUtility.PromptMenuChoice(0, 0))
                {
                    case 0:
                        GameStartScreen.Print();
                        break;
                }
            }
            void GetReward()
            {
                Player player = GameManager.Instance.player;
                List<EquipItem> inventory = GameManager.Instance.inventory;
                List<EquipItem> potion = new List<EquipItem>();

                int gold = 0;
                int healthPotion = 0;
                int weapon = 0;

                Random random = new Random();
                foreach (var item in monsterSpawned)
                {
                    int drop = random.Next(0, 100);
                    if(drop >= 0 && drop < 50)
                    {
                        weapon++;
                    }
                    else if(drop >= 50 && drop < 90)
                    {
                        healthPotion++;
                    }
                    else
                    {
                         gold += 500;
                    }
                }

                Console.WriteLine("[획득 아이템]");
                if(gold > 0)
                {
                    Console.WriteLine($"{gold} Gold");
                }
                if(healthPotion > 0)
                {
                    Console.WriteLine($"포션 - {healthPotion}");
                }
                if(weapon > 0)
                {
                    Console.WriteLine($"낡은 검 - {weapon}");
                }
                player.Gold += gold;
                healthPotion += potion.Count;
                for(int i = 0; i < weapon; i++)
                {
                    inventory.Add(new EquipItem("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 2, 0, 0, ItemType.WEAPON, 600));
                }
        }
        }
    }
}

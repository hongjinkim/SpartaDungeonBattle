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
            List<EquipItem> inventory = GameManager.Instance.inventory;
            List<IItem> potion = GameManager.Instance.potion;
            GameManager.Instance.tempExp = 0;
            Random random = new Random();

            if (player.Health == 0)
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("체력이 부족합니다");
                Thread.Sleep(1000);
                GameStartScreen.Print();
                return;
            }
            bool isPlayerPhaseEnd = false;
            bool isEnemyPhase = false;
            int startHealth = player.Health;
            int startExp = player.Exp;
            int startMana = player.Mana;
            int startLevel = player.Level;

            List<Monster> monsterSpawned = new List<Monster>();

            SpawnMonsters();

            PlayerPhase();

            void SpawnMonsters()
            {
                int spawnTimes = random.Next(4);
                int spawnIndex;
                int count = System.Enum.GetValues(typeof(Monsters)).Length;
                while (0 <= spawnTimes--)
                {
                    spawnIndex = random.Next(count);
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
                        isPlayerPhaseEnd = true;
                        PrintResult(player,player.Attack, monsterSpawned[keyInput - 1], "공격");
                        break;
                }
            }
            bool CheckMana(int cost)
            { 
                if (player.Mana < cost)
                {
                    Console.Clear();
                    ConsoleUtility.ShowTitle("마나가 부족합니다");
                    Thread.Sleep(1000);
                    return false;
                }
                return true;
            }
            void Skill()
            {
                Console.Clear();

                ConsoleUtility.ShowTitle("Battle!!");

                for (int i = 0; i < monsterSpawned.Count(); i++)
                {
                    monsterSpawned[i].PrintMonsterDescription();
                }

                Console.WriteLine("");
                player.PrintBattleDescription();
                Console.WriteLine("");
                Console.WriteLine("1. 알파 스트라이크 - MP 10");
                Console.WriteLine("공격력 * 2 로 하나의 적을 공격합니다.");
                Console.WriteLine("2. 더블 스트라이크 - MP 15");
                Console.WriteLine("공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.");
                Console.WriteLine("0. 취소");

                int keyInput = ConsoleUtility.PromptMenuChoice(0, 2);

                switch (keyInput)
                {
                    case 0:
                        PlayerPhase();
                        break;
                    case 1:
                        if(CheckMana(10))
                        {
                            Skill_AlphaStrike();
                        }
                        else
                        {
                            Skill();
                        }
                        break;
                    case 2:
                        if (CheckMana(15))
                        {
                            Skill_DoubleStrike();
                        }
                        else
                        {
                            Skill();
                        }
                        break;
                }
            }
            void Skill_AlphaStrike()
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
                        Skill();
                        break;
                    default:
                        player.Mana -= 10;
                        isPlayerPhaseEnd = true;
                        PrintResult(player, player.Attack*2, monsterSpawned[keyInput - 1], "알파 스트라이크");
                        break;
                }
            }
            void Skill_DoubleStrike()
            {
                player.Mana -= 15;
                int num;
                while (true)
                {
                    num = random.Next(0, monsterSpawned.Count());
                    if (!monsterSpawned[num].IsDead)
                    {
                        break;
                    }
                }
                PrintResult(player, (int)(player.Attack * 1.5), monsterSpawned[num], "더블 스트라이크");
                isPlayerPhaseEnd = true;
                while (true)
                {
                    num = random.Next(0, monsterSpawned.Count());
                    if (!monsterSpawned[num].IsDead)
                    {
                        break;
                    }
                }
                PrintResult(player, (int)(player.Attack * 1.5), monsterSpawned[num], "더블 스트라이크");
            }
            void UseItem()
            {
                Console.Clear();

                Console.WriteLine("");
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < potion.Count; i++)
                {
                    potion[i].PrintPotionDescription(true, i + 1); // 나가기가 0번 고정, 나머지가 1번부터 배정
                }
                Console.WriteLine("");
                Console.WriteLine("0. 나가기");

                int KeyInput = ConsoleUtility.PromptMenuChoice(0, potion.Count);

                switch (KeyInput)
                {
                    case 0:
                        PlayerPhase();
                        break;
                    default:
                        potion[KeyInput - 1].Use();
                        UseItem();
                        break;
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
                Console.WriteLine("3. 아이템 사용");

                int keyInput = ConsoleUtility.PromptMenuChoice(1, 3);

                switch (keyInput)
                {
                    case 1:
                        Attack();
                        break;
                    case 2:
                        Skill();
                        break;
                    case 3:
                        UseItem();
                        break;
                }
            }
            void EnemyPhase()
            {
                isEnemyPhase = true;
                foreach(Monster monster in monsterSpawned)
                {
                    PrintResult(monster,monster.Attack, player, "공격");
                    if(player.IsDead)
                    {
                        BattleResult(false);
                        return;
                    }
                }
                isEnemyPhase = false;
                isPlayerPhaseEnd = false;
                PlayerPhase();
            }
            bool IsCritical()
            {
                int num = random.Next(100);
                if(num < 15)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            bool IsMiss(string attackType)
            {
                if(attackType != "공격")
                {
                    return false;
                }
                int num = random.Next(100);
                if (num < 10)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            void AttackOrSkill(ICharacter attacker, int attackPower, ICharacter underAttack, string attackType)
            {
                Console.Clear();

                ConsoleUtility.ShowTitle("Battle!!");
                Console.WriteLine("");

                int health = underAttack.Health;
                int damage;
                bool isMiss = IsMiss(attackType);
                if (isMiss)
                {
                    Console.WriteLine($"{attacker.Name} 의 {attackType}!");
                    Console.Write($"{underAttack.Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                }
                else
                {
                    bool isCritical = IsCritical();
                    if (isCritical)
                    {
                        damage = underAttack.TakeDamage((int)(attackPower * 1.6f));
                    }
                    else
                    {
                        damage = underAttack.TakeDamage(attackPower);
                    }

                    Console.WriteLine($"{attacker.Name} 의 {attackType}!");
                    Console.Write($"{underAttack.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
                    if (isCritical)
                    {
                        Console.WriteLine(" - 치명타 공격!!");
                    }
                    else
                    {
                        Console.WriteLine("");
                    }
                    Console.WriteLine("");
                    Console.WriteLine($"{underAttack.Name}");
                    if (underAttack.IsDead)
                    {
                        Console.WriteLine($"HP {health} -> Dead");
                        if(underAttack.GetType() == typeof(Minion))
                        {
                            GameManager.Instance.quests[0].MissionComplete(true);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"HP {health} -> {underAttack.Health}");
                    }
                }
            }
            void PrintResult(ICharacter attacker,int attackPower, ICharacter underAttack, string attackType)
            {
                if(!attacker.IsDead)
                {
                    AttackOrSkill(attacker, attackPower, underAttack, attackType);
    
                    Console.WriteLine("");
                    Console.WriteLine("0. 다음");
                    switch (ConsoleUtility.PromptMenuChoice(0, 0))
                    {
                        case 0:
                            CheckClear();
                            if (isPlayerPhaseEnd)
                            {
                                if(!isEnemyPhase)
                                    EnemyPhase();
                            }
                            break;
                    }
                }
                else
                {
                    CheckClear();
                    if (isPlayerPhaseEnd)
                    {
                        if (!isEnemyPhase)
                            EnemyPhase();
                    }
                }
            }
            void BattleResult(bool isCleared)
            {
                Console.Clear();

                ConsoleUtility.ShowTitle("Battle!!");
                Console.WriteLine("");

                player.Mana += 10;
                if(player.Mana > player.ManaMax)
                {
                    player.Mana = player.ManaMax;
                }

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
                    Console.WriteLine($"MP {startMana} -> {player.Mana}");
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
                    Console.WriteLine($"MP {startMana} -> {player.Mana}");
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
            void GetReward()
            {      
                int gold = 0;
                int healthPotion = 0;
                int weapon = 0;

                Random random = new Random();
                for(int i = 0; i < monsterSpawned.Count; i++)
                {
                    int drop = random.Next(0, 100);
                    if (drop >= 0 && drop < 50)
                    {
                        weapon++;
                    }
                    else if (drop >= 50 && drop < 90)
                    {
                        healthPotion++;
                    }
                    else
                    {
                        gold += 500;
                    }
                }

                Console.WriteLine("[획득 아이템]");
                if (gold > 0)
                {
                    Console.WriteLine($"{gold} Gold");
                }
                if (healthPotion > 0)
                {
                    Console.WriteLine($"포션 - {healthPotion}");
                }
                if (weapon > 0)
                {
                    Console.WriteLine($"낡은 검 - {weapon}");
                }
                player.Gold += gold;
                potion[0].Quantity = potion[0].Quantity + healthPotion;
                for (int i = 0; i < weapon; i++)
                {
                    inventory.Add(new EquipItem("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 2, 0, 0, ItemType.WEAPON, 600));
                }
            }
        }
    }
}

using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpartaDungeonBattle
{
    public class GameManager
    {

        internal Player player;
        internal List<Item> inventory;
        internal List<Item> products;

        string path = Directory.GetCurrentDirectory() + "/save/";

        private void Save()
        {
            string playerJson = JsonSerializer.Serialize(player);
            string inventoryJson = JsonSerializer.Serialize(inventory);
            string productsJson = JsonSerializer.Serialize(products);


            File.WriteAllText(path + "player.json", playerJson);
            File.WriteAllText(path + "inventory.json", inventoryJson);
            File.WriteAllText(path + "products.json", productsJson);

        }
        public void Load()
        {
            string playerJson = File.ReadAllText(path + "player.json");
            string inventoryJson = File.ReadAllText(path + "inventory.json");
            string productsJson = File.ReadAllText(path + "products.json");

            player = JsonSerializer.Deserialize<Player>(playerJson);
            inventory = JsonSerializer.Deserialize<List<Item>>(inventoryJson);
            products = JsonSerializer.Deserialize<List<Item>>(productsJson);
        }

        public GameManager()
        {
            InitializeGame();
        }
        private void InitializeGame()
        {
            player = new Player("홍진");
            player.UpdateStatus();
            inventory = new List<Item>();
            products = new List<Item>
            {
                new Item("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 0,5,0, ItemType.ARMOR, 200),
                new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0,9,0, ItemType.ARMOR, 500),
                new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0,15,0, ItemType.ARMOR, 3500),
                new Item("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 2,0,0, ItemType.WEAPON, 600),
                new Item("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다", 5,0,0, ItemType.WEAPON, 1500),
                new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7,0,0, ItemType.WEAPON, 300)
            };
        }

        public void StartGame()
        {
            Console.Clear();
            ConsoleUtility.PrintGameHeader();
            StartScreen();
        }

        private void StartScreen(string? prompt = null)
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
            Console.WriteLine("4. 던전 입장");
            Console.WriteLine("5. 휴식하기");
            Console.WriteLine("6. 저장하기");
            Console.WriteLine("7. 불러오기");
            Console.WriteLine("");

            // 2. 선택한 결과를 검증함

            switch (ConsoleUtility.PromptMenuChoice(1, 7))
            {
                case 1:
                    StatusScreen();
                    break;
                case 2:
                    InventoryScreen();
                    break;
                case 3:
                    StoreScreen();
                    break;
                case 4:
                    DungeonScreen();
                    break;
                case 5:
                    RestScreen();
                    break;
                case 6:
                    Save();
                    StartScreen("게임이 저장되었습니다.");
                    break;
                case 7:
                    Load();
                    StartScreen("게임을 불러왔습니다.");
                    break;
            }
            StartScreen();
        }
        // 상태 보기
        private void StatusScreen()
        {
            Console.Clear();

            ConsoleUtility.ShowTitle("■ 상태보기 ■");
            Console.WriteLine("캐릭터의 정보가 표기됩니다.");

            ConsoleUtility.PrintTextHighlights("Lv. ", player.Level.ToString("00"));
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
                    StartScreen();
                    break;
            }

        }
        // 장비 관리
        private void InventoryScreen()
        {
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
                    StartScreen();
                    break;
            }

        }
        // 장비 관리 - 장착
        private void EquipScreen()
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
                    StartScreen();
                    break;
                default:
                    EquipItem(KeyInput - 1);
                    EquipScreen();
                    break;
            }

        }
        // 상점
        private void StoreScreen()
        {
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
                    StartScreen();
                    break;
                case 1:
                    BuyScreen();
                    break;
                case 2:
                    SellScreen();
                    break;
            }

        }
        // 상점 - 구매
        private void BuyScreen(string? prompt = null)
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
                    StoreScreen();
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
        private void SellScreen()
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
                    StoreScreen();
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
                    EquipItem(idx);
                }
                inventory[idx].TogglePurchaseStatus();
                player.Gold += (int)(inventory[idx].Price * 0.85f);
                inventory.RemoveAt(idx);
            }
        }
        // 던전 입장
        private void DungeonScreen()
        {
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
                    StartScreen();
                    break;
                default:
                    DungeonClearScreen(Dungeons[keyInput-1]);
                    break;
            }
        }
        // 던전 결과 (클리어, 실패)
        private void DungeonClearScreen(Tuple<string, int, int> Dungeon)
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
                ConsoleUtility.PrintTextHighlights("Gold ", $"{player.Gold} G -> {player.Gold+=prize} G", "");
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

            switch (ConsoleUtility.PromptMenuChoice(0,0))
            {
                case 0:
                    StartScreen();
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
        // 휴식하기
        private void RestScreen(string? prompt = null)
        {
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

            switch (ConsoleUtility.PromptMenuChoice(0,1))
            {
                case 0:
                    StartScreen();
                    break;
                case 1:
                  if (player.Gold >= 500)
                    {
                        player.Gold -= 500;
                        player.Health = 100;
                        RestScreen("휴식을 완료했습니다.");
                    }
                    // 돈이 모자라는 경우
                    else
                    {
                        RestScreen("Gold가 부족합니다.");
                    }
                    break;
            }
        }

        // Inventory 내에서 idx번째 아이템을 장착하거나 해제
        private void EquipItem(int idx)
        {
            ItemType type = inventory[idx].Type;
            if (inventory[idx].isEquipped) // 장착중이라면 해제
            {
                inventory[idx].ToggleEquipStatus();
                if (type == ItemType.WEAPON)
                {
                    player.EquippedWeapon = null;
                }
                else if (type == ItemType.ARMOR)
                {
                    player.EquippedArmor = null;
                }
            }
            else                        // 해제중이라면 장착
            {
                if (type == ItemType.WEAPON)
                {
                    player.EquippedWeapon = inventory[idx];
                    foreach (Item item in inventory)
                    {
                        if (item.isEquipped && item.Type == ItemType.WEAPON)
                        {
                            inventory[idx].ToggleEquipStatus();
                        }
                    }
                }
                else if (type == ItemType.ARMOR)
                {
                    player.EquippedArmor = inventory[idx];
                    foreach (Item item in inventory)
                    {
                        if (item.isEquipped && item.Type == ItemType.ARMOR)
                        {
                            item.ToggleEquipStatus();
                        }
                    }
                }
                inventory[idx].ToggleEquipStatus();
            }
            player.UpdateStatus();
        }

        //저장
        
    }
   
    // 메인 메서드
    class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.StartGame();
        }  
    }
}
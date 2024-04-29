using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using SpartaDungeonBattle.Class;

namespace SpartaDungeonBattle
{
    public class GameManager
    {
        public static GameManager Instance;
        public SaveManager saveManager = new SaveManager(); 
        public Player player = new Player("");
        public List<EquipItem> inventory = new List<EquipItem>();
        public List<EquipItem> products = new List<EquipItem>();

        public GameManager()
        {
            InitializeGame();
        }
        // 게임 초기화
        private void InitializeGame()
        {
            player = new Player("홍진");
            player.UpdateStatus();
            inventory = new List<EquipItem>();
            products = new List<EquipItem>
            {
                new EquipItem("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 0,5,0, ItemType.ARMOR, 200),
                new EquipItem("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0,9,0, ItemType.ARMOR, 500),
                new EquipItem("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0,15,0, ItemType.ARMOR, 3500),
                new EquipItem("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 2,0,0, ItemType.WEAPON, 600),
                new EquipItem("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다", 5,0,0, ItemType.WEAPON, 1500),
                new EquipItem("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7,0,0, ItemType.WEAPON, 300)
            };
        }
        // 게임 시작
        public void StartGame()
        {
            if(Instance == null)
            {
                Instance = this;
            }

            Console.Clear();
            ConsoleUtility.PrintGameHeader();
            StartScreen.Print();
        }
       
       
        // Inventory 내에서 idx번째 아이템을 장착하거나 해제
        public void EquipItem(int idx)
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
                    foreach (EquipItem item in inventory)
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
                    foreach (EquipItem item in inventory)
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
    }
}

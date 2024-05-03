using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using SpartaDungeonBattle.Class;
using System.Reflection.Emit;

namespace SpartaDungeonBattle
{
    [Serializable]
    public class GameManager
    {
        public static GameManager Instance;
     
        public Player player = new Player("");
        public List<EquipItem> inventory = new List<EquipItem>();
        public List<EquipItem> products = new List<EquipItem>();
        public List<IItem> potion = new List<IItem>();
        public List<Quest> quests = new List<Quest>();

        public int tempExp = 0;

        public GameManager()
        {
            InitializeGame();
        }
        // 게임 초기화
        private void InitializeGame()
        {
            player = new Player("홍진");
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
            potion = new List<IItem>
            {
                new HealthPotion()
            };
            quests = new List<Quest>{
                new Quest("마을을 위협하는 미니언 처리",
                            "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\r\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\r\n모험가인 자네가 좀 처치해주게!",
                            "미니언 5마리 처치", 5, new List<EquipItem> { new EquipItem("쓸만한 방패", "그냥 저냥 쓸만한 방패 입니다.", 0, 7, 0, ItemType.ARMOR, 350) }, 1500),
                new Quest("장비를 장착해보자", "장비를 장착하면 더욱 더 강해질 수 있다! 인벤토리 - 장착 관리 로 가서 장비를 장착해 보자",
                            "장비 장착하기", 1, new List<EquipItem> { new EquipItem("보급형 단검", "아무한테나 막 주는 그런 검입니다.", 2, 0, 0, ItemType.WEAPON, 400),
                                                                     new EquipItem("보급형 갑옷", "아무한테나 막 주는 그런 갑옷입니다.", 0, 7, 0, ItemType.ARMOR, 400) }, 600),
                new Quest("더욱 더 강해지기!", "강해져보자",
                            "레벨 5 달성하기", 5, new List<EquipItem> { new EquipItem("강철 검", "강철로 된 쓰기 좋은 검 입니다.", 10, 0, 0, ItemType.WEAPON, 3000) }, 2500)
            };
        }
        // 게임 시작
        public void StartGame()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            player.UpdateStatus();
            quests[2].MissionComplete(false);
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
                // 2번 퀘스트 성공 조건
                quests[1].MissionComplete(true);
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

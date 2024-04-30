using SpartaDungeonBattle.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle
{
    public struct QuestReward
    {
        public EquipItem equipItem;
        public int gold;
    }
    [Serializable]
    public class Quest
    {
        public string Name { get; }
        public string Bio { get; }
        public string Mission { get; }
        public bool isInProgress { get; set; }
        public bool isCleared { get; set; }
        public bool isAlreadyCleared { get; set; }
        public QuestReward questReward { get; set; }
        

        public Quest(string name, string bio, string mission)
        {
            Name = name;
            Bio = bio;
            Mission = mission;
            isInProgress = false;
            isCleared = false;
            isAlreadyCleared = false;
        }

        internal void PrintQuestName(int idx = 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write($"{idx}");
            Console.ResetColor();
           
            Console.WriteLine($". {Name}");
        }
        //internal void PrintQuestDescription(int idx = 0)
        //{
        //    Console.ForegroundColor = ConsoleColor.DarkMagenta;
        //    Console.Write($"{idx}");
        //    Console.ResetColor();

        //    Console.WriteLine($". {Name}");
        //}
        internal void GetReward()
        {
            Player player = GameManager.Instance.player;
            List<EquipItem> inventory = GameManager.Instance.inventory;

            player.Gold += questReward.gold;
            inventory.Add(questReward.equipItem);

            isAlreadyCleared = true;
        }
    }
}

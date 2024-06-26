﻿using SpartaDungeonBattle.Class;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpartaDungeonBattle
{
   
    [Serializable]
    
    public class Quest
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Mission { get; set; }
        public int MissionCurrent { get; set; }
        public int MissionGoal { get; set; }
        public bool isInProgress { get; set; }
        public bool isCleared { get; set; }
        public bool isAlreadyCleared { get; set; }
        public List<EquipItem> RewardEquipItems { get; set; }
        //public List<UsableItem> usableItems;
        public int RewardGold { get; set; }

        [JsonConstructor]
        public Quest(string name, string bio, string mission, int missionGoal, List<EquipItem> rewardEquipItems, int rewardGold)
        {
            Name = name;
            Bio = bio;
            Mission = mission;
            MissionCurrent = 0;
            MissionGoal = missionGoal;
            isInProgress = false;
            isAlreadyCleared = false;
            RewardEquipItems = rewardEquipItems;
            RewardGold = rewardGold;
        }

        internal void PrintQuestName(int idx = 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write($"{idx}");
            Console.ResetColor();
           
            Console.WriteLine($". {Name}");
        }
        internal void PrintQuestDescription()
        {
            Console.WriteLine($"{Name}");
            Console.WriteLine("");
            Console.WriteLine($"{Bio}");
            Console.WriteLine("");
            Console.WriteLine($"{Mission} ({MissionCurrent} / {MissionGoal})");
            Console.WriteLine("");
            Console.WriteLine($"-보상-");
            foreach (EquipItem equipItem in RewardEquipItems)
            {
                Console.WriteLine($"{equipItem.Name}");
            }
            Console.WriteLine($"{RewardGold} G");

            Console.WriteLine("");
        }
        internal void GetReward()
        {
            Player player = GameManager.Instance.player;
            List<EquipItem> inventory = GameManager.Instance.inventory;

            foreach(EquipItem equipItem in RewardEquipItems)
            {
                if (equipItem != null)
                {
                    inventory.Add(equipItem);
                }
            }
            
            if(RewardGold > 0)
            {
                player.Gold += RewardGold;
            }
            isAlreadyCleared = true;
        }

        internal void MissionComplete(bool CountAfterProgress)
        {
            if(isInProgress || !CountAfterProgress)
            {
                MissionCurrent++;
                if(MissionCurrent >= MissionGoal)
                {
                    isCleared = true;
                }
                if(MissionCurrent > MissionGoal)
                {
                    MissionCurrent = MissionGoal;
                }
            }

        }
    }
}

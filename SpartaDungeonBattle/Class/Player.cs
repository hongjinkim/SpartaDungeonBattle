using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle.Class
{
    public class Player : ICharacter
    {
        public int ClearTimes { get; set; }

        public int Level { get; set; }
        public int Exp { get; set; }
        
        public string Name { get; set; }
        public string Class { get; set; }
        public float Strength_Default { get; set; }
        public float Strength { get; set; }
        public int Defence_Default { get; set; }
        public int Defence { get; set; }
        public int HealthMax { get; set; }
        public int Health { get; set; }
        public int Gold { get; set; }
        public EquipItem EquippedWeapon { get; set; }
        public EquipItem EquippedArmor { get; set; }

        public int requiredExpAdjust = 20;
        public int requiredExp = 10;
        public bool IsDead => Health <= 0;
        private int AttackAdjust => (int)Math.Ceiling(Strength*0.1);
        public int Attack => new Random().Next((int)Strength - AttackAdjust, (int)Strength + AttackAdjust); // 공격력은 랜덤

        public Player(string name)
        {
            ClearTimes = 0;
            Level = 1;
            Name = name;
            Class = "전사";
            Strength_Default = 10;
            Defence_Default = 5;
            HealthMax = 100;
            Health = HealthMax;
            Gold = 1500;
        }

        public void SelectClass(int idx)
        {
            switch (idx)
            {
                case 1:
                    Class = "전사";
                    Strength_Default = 15;
                    Defence_Default = 5;
                    HealthMax = 100;
                    Health = HealthMax;
                    break;

                case 2:
                    Class = "도적";
                    Strength_Default = 17;
                    Defence_Default = 3;
                    HealthMax = 80;
                    Health = HealthMax;
                    break;
            }

            UpdateStatus();
        }

     
        //public void GainExperience(int experience)
        //{
        //    Exp += experience;

        //    if (Level == 1)
        //    {
        //        requiredExp = 10;
        //        totalExp();
        //    }
        //    else if (Level == 2)
        //    {
        //        requiredExp = 35;
        //        totalExp();
        //    }
        //    else if (Level == 3)
        //    {
        //        requiredExp = 65;
        //        totalExp();
        //    }
        //    else if (Level == 4)
        //    {
        //        requiredExp = 100;
        //        totalExp();
        //    }
        //}

        public void UpdateStatus()
        {
            if (requiredExp <= Exp)
            {
                Level++;
                requiredExpAdjust += 5;
                requiredExp += requiredExpAdjust;
                GameManager.Instance.quests[2].MissionComplete(false, Level);
                Strength_Default += 0.5f;
                Defence_Default += 1;
            }
            if (EquippedWeapon == null)
            {
                Strength = Strength_Default;
            }
            else
            {
                Strength = Strength_Default + EquippedWeapon.Str;
            }
            if (EquippedArmor == null)
            {
                Defence = Defence_Default;
            }
            else
            {
                Defence = Defence_Default + EquippedArmor.Def;
            }
        }
        public int TakeDamage(int damage)
        {
            int tempDamage = (int)Math.Ceiling(damage * (10f / (10f + Defence)));
            Health -= tempDamage;
            if (Health <= 0)
            {
                Health = 0;
            }
            return tempDamage;
        }

        internal void PrintBattleDescription()
        {
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{Level} {Name} ({Class})");
            Console.WriteLine($"HP {Health} / {HealthMax}");
        }
    }
}

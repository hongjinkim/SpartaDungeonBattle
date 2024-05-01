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
        public string Name { get; set; }
        public string Class { get; set; }
        public float Strength_Default { get; set; }
        public float Strength { get; set; }
        public int Defence_Default { get; set; }
        public int Defence { get; set; }
        public int Health { get; set; }
        public int Gold { get; set; }
        public EquipItem EquippedWeapon { get; set; }
        public EquipItem EquippedArmor { get; set; }

        public bool IsDead => Health <= 0;
        public int Attack => new Random().Next(); // 공격력은 랜덤

        public Player(string name)
        {
            ClearTimes = 0;
            Level = 1;
            Name = name;
            Class = "전사";
            Strength_Default = 10;
            Defence_Default = 5;
            Health = 100;
            Gold = 1500;
        }

        public void SelectClass(string clas)
        {
            switch (clas)
            {
                case "1":
                    Class = "전사";
                    Strength_Default = 15;
                    Defence_Default = 5;
                    Health = 100;
                    break;

                case "2":
                    Class = "도적";
                    Strength_Default = 17;
                    Defence_Default = 3;
                    Health = 80;
                    break;

                default:
                    Console.WriteLine("존재하지 않는 직업입니다.");
                    Console.WriteLine("직업을 다시 선택해주세요.");
                    string newClass = Console.ReadLine();
                    Console.Clear();
                    SelectClass(newClass);
                    return;
            }

            UpdateStatus();
        }




        public void UpdateStatus()
        {
            if (ClearTimes == Level)
            {
                Level++;
                GameManager.Instance.quests[2].MissionComplete(false,Level);
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
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (IsDead) Console.WriteLine($"{Name}이(가) 죽었습니다.");
            else Console.WriteLine($"{Name}이(가) {damage}의 데미지를 받았습니다. 남은 체력: {Health}");
        }
    }
}

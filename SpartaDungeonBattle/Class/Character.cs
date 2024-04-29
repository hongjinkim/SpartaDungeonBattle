using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle
{
    public interface ICharacter
    {
        int Level { get; set; }
        string Name { get; }
        int Health { get; set; }
        float Strength { get; set; }
        int Attack { get; }
        bool IsDead { get; }
        void TakeDamage(int damage);
    }

    // 전사 클래스
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
        public void UpdateStatus()
        {
            if (ClearTimes == Level)
            {
                Level++;
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

    // 몬스터 클래스
    public class Monster : ICharacter
    {
        public int Level { get; set; }
        public string Name { get; }
        public int Health { get; set; }
        public float Strength { get; set; }
        public int Attack => new Random().Next(); // 공격력은 랜덤

        public bool IsDead => Health <= 0;

        public Monster(int level, string name, int health, int strength)
        {
            Level = level;
            Name = name;
            Health = health;
            Strength = strength;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (IsDead) Console.WriteLine($"{Name}이(가) 죽었습니다.");
            else Console.WriteLine($"{Name}이(가) {damage}의 데미지를 받았습니다. 남은 체력: {Health}");
        }
    }

    // 미니언 클래스
    public class Minion : Monster
    {
        public Minion() : base(2,"미니언",15,  5) { } 
    }
    // 공허충 클래스
    public class Voidling : Monster
    {
        public Voidling() : base(3, "공허충", 10, 9) { }
    }
    // 대포미니언 클래스
    public class SiegeMinion : Monster
    {
        public SiegeMinion() : base(5, "대포미니언", 25, 8) { }
    }
    
}

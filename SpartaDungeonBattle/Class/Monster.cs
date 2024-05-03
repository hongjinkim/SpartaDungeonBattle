using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle.Class
{
    // 몬스터 클래스
    public class Monster : ICharacter
    {
        public int Level { get; set; }
        public string Name { get; }
        public int Health { get; set; }
        public float Strength { get; set; }
        public int Defence { get; set; }
        private int AttackAdjust => (int)Math.Ceiling(Strength * 0.1);
        public int Attack => new Random().Next((int)Strength - AttackAdjust, (int)Strength + AttackAdjust); // 공격력은 랜덤

        public bool IsDead => Health <= 0;

        public Monster(int level, string name, int health, int strength, int defence)
        {
            Level = level;
            Name = $"Lv.{Level.ToString()} " + name;
            Health = health;
            Strength = strength;
            Defence = defence;
        }

        public virtual int TakeDamage(int damage)
        {
            Health -= damage;
            if(Health <=0)
            {
                GameManager.Instance.tempExp += Level;
                Health = 0;
            }
            return damage;
        }

        internal void PrintMonsterDescription(bool withNumber = false, int idx = 0)
        {
            Console.Write("- ");
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write($"{idx} ");
                Console.ResetColor();
            }
            if (IsDead)
            {
                Console.WriteLine($"{Name} Dead");
            }
            else
            {
                Console.WriteLine($"{Name} HP {Health}");
            }
        }
    }

    // 미니언 클래스
    public class Minion : Monster
    {
        public Minion() : base(2, "미니언", 15, 5, 3){ }
            
    }
    // 공허충 클래스
    public class Voidling : Monster
    {
        public Voidling() : base(3, "공허충", 10, 9, 5) { }
    }
    // 대포미니언 클래스
    public class SiegeMinion : Monster
    {
        public SiegeMinion() : base(5, "대포미니언", 25, 8, 4) { }
    }
}

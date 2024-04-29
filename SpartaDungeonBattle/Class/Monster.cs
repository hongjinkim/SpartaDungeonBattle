using System;
using System.Collections.Generic;
using System.Linq;
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
                Console.WriteLine($"Lv.{Level} {Name} Dead");
            }
            else
            {
                Console.WriteLine($"Lv.{Level} {Name} HP {Health}");
            }
        }
    }

    // 미니언 클래스
    public class Minion : Monster
    {
        public Minion() : base(2, "미니언", 15, 5) { }
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

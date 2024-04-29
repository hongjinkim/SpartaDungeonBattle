using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle
{
    public interface IItem
    {
        string Name { get; }
        void Use(Player player); // 전사에게 아이템을 사용하는 메서드
    }

    // 체력 포션 클래스
    public class HealthPotion : IItem
    {
        public string Name => "체력 포션";

        public void Use(Player player)
        {
            Console.WriteLine("체력 포션을 사용합니다. 체력이 50 증가합니다.");
            player.Health += 50;
            if (player.Health > 100) player.Health = 100;
        }
    }

    // 공격력 포션 클래스
    public class StrengthPotion : IItem
    {
        public string Name => "공격력 포션";

        public void Use(Player player)
        {
            Console.WriteLine("공격력 포션을 사용합니다. 공격력이 10 증가합니다.");
            player.Strength += 10;
        }
    }
}

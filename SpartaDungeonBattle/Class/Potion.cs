using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle.Class
{
    [Serializable]
    public class HealthPotion : IItem
    {
        public string Name => "체력 포션";
        public string Bio => "체력을 50 회복 시켜주는 포션입니다.";
        public int Quantity { get; set; }
        public HealthPotion()
        {
            Quantity = 3;
        }
        public void Use()
        {
            if (Quantity > 0)
            {
                Quantity--;
                Player player = GameManager.Instance.player;
                // 1초간 메시지를 띄운 다음에 다시 진행
                Console.Clear();
                ConsoleUtility.ShowTitle("체력 포션을 사용합니다. 체력이 50 증가합니다.");
                Thread.Sleep(1000);  
                player.Health += 50;
                if (player.Health > player.HealthMax)
                    player.Health = player.HealthMax;
            }
            else
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("아이템이 부족합니다.");
                Thread.Sleep(1000);
            }

        }
        public void PrintPotionDescription(bool withNumber = false, int idx = 0)
        {
            Console.Write("- ");
            // 장착관리 전용
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("{0} ", idx);
                Console.ResetColor();
                Console.Write(ConsoleUtility.PadRightForMixedText(Name, 15));
            }
            else Console.Write(ConsoleUtility.PadRightForMixedText(Name, 18));

            Console.WriteLine($"{Bio} | (남은 수량 : {Quantity})");
        }
    }
}

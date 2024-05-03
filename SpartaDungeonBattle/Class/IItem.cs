using SpartaDungeonBattle.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpartaDungeonBattle
{
    public interface IItem
    {
        string Name { get; }
        string Bio { get; }
        int Quantity { get; set; }
        void Use(); // 전사에게 아이템을 사용하는 메서드
        void PrintPotionDescription(bool withNumber = false, int idx = 0);
    }

    // 체력 포션 클래스
   

    // 공격력 포션 클래스
    //public class StrengthPotion : IItem
    //{
    //    public int Quantity;
    //    public string Name => "공격력 포션";

    //    public void Use()
    //    {
    //        Player player = GameManager.Instance.player;
    //        Console.WriteLine("공격력 포션을 사용합니다. 공격력이 10 증가합니다.");
    //        player.Strength += 10;
    //    }
    //}
    
}

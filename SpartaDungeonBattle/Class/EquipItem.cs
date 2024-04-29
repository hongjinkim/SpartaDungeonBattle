using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json.Serialization;

namespace SpartaDungeonBattle
{
    //장비 타입을 Weapon, Armor 두가지로 사용
    [Serializable]
    public enum ItemType
    {
        WEAPON,
        ARMOR
    }
    // 장비 클래스
    [Serializable]
    public class EquipItem
    {
        public string Name { get; set; }
        public string Bio { get; set; }

        public ItemType Type { get; set;  }

        public int Str { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }

        public int Price { get; set; }

        public bool isEquipped { get; set; }
        public bool isAlreadyBuyed { get; set; }

        public EquipItem(string name, string bio, int str,int def, int hp, ItemType type, int price)
        {
            this.Name = name;
            this.Bio = bio;        
            this.Type = type;

            this.Str = str;
            this.Def = def;
            this.Hp = hp;

            this.isEquipped = false;
            this.Price = price;
            this.isAlreadyBuyed = false;
        }

        internal void PrintItemStatDescription(bool withNumber = false, int idx = 0)
        {
            Console.Write("- ");
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write($"{idx} ");
                Console.ResetColor();
            }
            if (isEquipped)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("E");
                Console.ResetColor();
                Console.Write("]");
                Console.Write(ConsoleUtility.PadRightForMixedText(Name, 9));
            }
            else Console.Write(ConsoleUtility.PadRightForMixedText(Name, 12)); ;

            Console.Write(" | ");

            if (Str != 0) Console.Write($"공격력 {(Str >= 0 ? "+" : "")}{Str} ");
            if (Def != 0) Console.Write($"방어력 {(Def >= 0 ? "+" : "")}{Def} ");
            if (Hp != 0) Console.Write($"체  력 {(Hp >= 0 ? "+" : "")}{Hp} ");

            Console.Write(" | ");

            Console.WriteLine(Bio);
        }
        public void PrintStoreItemDescription(bool withNumber = false, int idx = 0)
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

            Console.Write(" | ");

            if (Str != 0) Console.Write($"공격력 {(Str >= 0 ? "+" : "")}{Str} ");
            if (Def != 0) Console.Write($"방어력 {(Def >= 0 ? "+" : "")}{Def} ");
            if (Hp != 0) Console.Write($"체  력 {(Hp >= 0 ? "+" : "")}{Hp}");

            Console.Write(" | ");

            Console.Write(ConsoleUtility.PadRightForMixedText(Bio, 24));

            Console.Write(" | ");

            if (isAlreadyBuyed)
            {
                Console.WriteLine("구매완료");
            }
            else
            {
                ConsoleUtility.PrintTextHighlights("", Price.ToString(), " G");
            }
        }

        internal void PrintItemSellDescription(bool withNumber = false, int idx = 0)
        {
            Console.Write("- ");
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write($"{idx} ");
                Console.ResetColor();
            }
            if (isEquipped)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("E");
                Console.ResetColor();
                Console.Write("]");
                Console.Write(ConsoleUtility.PadRightForMixedText(Name, 9));
            }
            else Console.Write(ConsoleUtility.PadRightForMixedText(Name, 12)); ;

            Console.Write(" | ");

            if (Str != 0) Console.Write($"공격력 {(Str >= 0 ? "+" : "")}{Str} ");
            if (Def != 0) Console.Write($"방어력 {(Def >= 0 ? "+" : "")}{Def} ");
            if (Hp != 0) Console.Write($"체  력 {(Hp >= 0 ? "+" : "")}{Hp} ");

            Console.Write(" | ");
            Console.Write(ConsoleUtility.PadRightForMixedText(Bio, 24));

            Console.Write(" | ");
            Console.WriteLine(Price*0.85);
        }

        internal void ToggleEquipStatus()
        {
            isEquipped = !isEquipped;
        }

        internal void TogglePurchaseStatus()
        {
            isAlreadyBuyed = !isAlreadyBuyed;
        }
    }
    
}

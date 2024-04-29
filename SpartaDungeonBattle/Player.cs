using System.Numerics;

namespace SpartaDungeonBattle
{
    [Serializable]
    internal class Player
    {
        public int ClearTimes { get; set; }

        public int Level { get; set; }
        public string Name { get; }
        public string Class { get; }
        public float Strength_Default { get; private set; }
        public float Strength { get; set; }
        public int Defence_Default { get; private set; }
        public int Defence { get; set; }
        public int Health { get; set; }
        public int Gold { get; set; }
        public Item EquippedWeapon { get; set; }
        public Item EquippedArmor { get; set; }
        
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
    }
}

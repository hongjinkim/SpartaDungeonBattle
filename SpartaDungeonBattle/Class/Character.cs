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
}

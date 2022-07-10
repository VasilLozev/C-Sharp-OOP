using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes
{
    public class Mace : Weapon
    {
        public Mace(string name, int durability) : base(name, durability)
        {
            name = Name;
            durability = Durability;
        }
    }
}

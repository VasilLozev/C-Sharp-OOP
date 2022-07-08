using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes
{
    public class Barbarian : Hero
    {
        public Barbarian(string name, int health, int armour) : base(name, health, armour)
        {
            name = Name;
            health = Health;
            armour = Armour;
        }
    }
}

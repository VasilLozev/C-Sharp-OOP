using System;
using System.Collections.Generic;
using System.Text;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Inventory;

namespace WarCroft.Entities.Characters
{
    public class Warrior : Character, IAttacker
    {
        public Warrior(string name) : base(name, 100f, 50f, 40f, new Satchel())
        {
            
        }

        public void Attack(Character character)
        {
            if (IsAlive && character.IsAlive)
            {
                if (this == character)
                {
                    throw new InvalidOperationException("Cannot attack self!");
                }
                character.TakeDamage(this.AbilityPoints);
            }
        }    
    }
}

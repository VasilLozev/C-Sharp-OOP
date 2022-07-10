using System;

using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters.Contracts
{
	public abstract class Character
	{
		// TODO: Implement the rest of the class.

		public bool IsAlive { get; set; } = true;

		protected void EnsureAlive()
		{
			if (!this.IsAlive)
			{
				throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
			}
		}
		protected string _name;
		public string Name
		{
			get { return _name; }
			set
			{
				if (value == null || value == string.Empty)
				{
					throw new ArgumentException("Name cannot be null or whitespace!");
				}
				_name = value;
			}
		}
		protected float _maxHealth;
		public float BaseHealth { get { return _maxHealth; } set { _maxHealth = value; } }
		protected float _health;
		public float Health
		{
			get { return _health; }
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				_health = value;
			}
		}
		public float BaseArmor { get; set; }
		protected float _armor;
		public float Armor
		{
			get { return _armor; }
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				_armor = value;
			}
		}

		public float AbilityPoints { get; set; }

		public Bag Bag { get; set; }

		public void TakeDamage(double hitPoints)
        {
            if (IsAlive)
            {
                if (Armor < hitPoints)
                {
					
					Health -= ((float)hitPoints - Armor);
					Armor = 0;
					if (Health == 0)
                    {
						IsAlive = false;
                    }
					return;
                }
				Armor -= (float)hitPoints;
            }
        }
		public void UseItem(Item item)
        {
            if (IsAlive)
            {
				  item.AffectCharacter(this);	
            }
        }
        public Character(string name, double health, double armor, double abilityPoints, Bag bag)
		{
			Name = name;
			Health = (float)health;
			BaseHealth = (float)health;
			Armor = (float)armor;
			BaseArmor = (float)armor;
			AbilityPoints = (float)abilityPoints;
			this.Bag = bag;
        }
	}
}
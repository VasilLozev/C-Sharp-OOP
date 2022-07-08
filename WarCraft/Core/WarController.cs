using System;
using System.Collections.Generic;
using System.Linq;
using WarCroft.Constants;
using WarCroft.Entities.Characters;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Core
{
	public class WarController
	{
		List<Character> party = new List<Character>();
	    Stack<Item> item_pool = new Stack<Item>();
		public WarController()
		{
		}

		public string JoinParty(string[] args)
		{

			if (args[0] == "Warrior")
			{

				party.Add(new Warrior(args[1]));
				return $"{args[1] + SuccessMessages.JoinParty}!";

			}
			else if (args[0] == "Priest")
			{
				party.Add(new Priest(args[1]));
				return $"{args[1]} joined the party!";
			}
			else
			{
				throw new ArgumentException($"Parameter Error: Invalid character type \"{0}\"!");
			}
		}
		public string AddItemToPool(string[] args)
		{

			if (args[0] != "FirePotion" && args[0] != "HealthPotion")
			{
				throw new ArgumentException($"Parameter Error: Invalid item \"{args[0]}\"!");
			}
			if (args[0] == "FirePotion")
			{
				item_pool.Push(new FirePotion());
				return $"{args[0]} added to pool.";
			}
			else if (args[0] == "HealthPotion")
			{
				item_pool.Push(new HealthPotion());
				return $"{args[0]} added to pool.";
			}
			return null;
		}

		
		public string PickUpItem(string[] args)
		{
			
			foreach (var character in party)
			{
				if (character.Name == args[0])
				{
					if (item_pool.Count == 0)
					{
						throw new InvalidOperationException("Invalid Operation: No items left in pool!");
					}
					string name = item_pool.Peek().GetType().Name;
                    character.Bag.AddItem(item_pool.Pop());
					

					return $"{character.Name} picked up {name}!";
				}
			}
			throw new ArgumentException($"Parameter Error: Character {args[0]} not found!");
		}
		public string UseItem(string[] args)
		{
			foreach (var character in party)
			{
				if (character.Name == args[0])
				{
				character.UseItem(character.Bag.GetItem(args[1]));
					
					return $"{character.Name} used {args[1]}.";
				}
			}
			throw new ArgumentException($"Parameter Error: Character {args[0]} not found!");
		}
		public string GetStats()
		{
			var sortedFactors = party.OrderByDescending(x => x.IsAlive).ThenByDescending(x => x.Health).ToList();

			string Join = "";
			foreach (var character in sortedFactors)
			{
				Join += string.Join("", $"{character.Name} - HP: {character.Health}/{character.BaseHealth}, AP: {character.Armor}/{character.BaseArmor}, ");
				string status = "";
				if (character.IsAlive)
				{
					status = "Alive";
				}
				else
				{
					status = "Dead";
				}
				Join += string.Join("", $"Status: {status}\r\n");

			}
			return Join;
		}

		public string Attack(string[] args)
		{
			string attacker = args[0];
			string receiver = args[1];
			Warrior warrior = null;
			Character DamageReceiver = null;
			foreach (var character in party)
			{
				if (character.Name == attacker)
				{
					if (character is Priest || !character.IsAlive)
					{
						throw new ArgumentException($"Parameter Error: {attacker} cannot attack!");
					}
					warrior = (Warrior)character;
				}
				if (character.Name == receiver)
				{
					if (!character.IsAlive)
					{
						throw new InvalidOperationException($"Invalid Operation: {ExceptionMessages.AffectedCharacterDead}");
					}
                    DamageReceiver = character;
				}
			}
			if (warrior == null)
			{
				throw new ArgumentException($"Parameter Error: Character {attacker} not found!");
			}
			if (DamageReceiver == null)
			{
				throw new ArgumentException($"Parameter Error: Character {receiver} not found!");
			}
			warrior.Attack(DamageReceiver);
			string Join = "";
			Join += $"{attacker} attacks {receiver} for {warrior.AbilityPoints} hit points! {receiver} has {DamageReceiver.Health}/{DamageReceiver.BaseHealth} HP and {DamageReceiver.Armor}/{DamageReceiver.BaseArmor} AP left!";
            if (!DamageReceiver.IsAlive)
            {
				Join += string.Join("", $"\r\n{receiver} is dead!");
            }
			return Join;
		}
	

		public string Heal(string[] args)
		{
			string healer = args[0];
			string healed = args[1];
			Priest priest = null;
			Character healReceiver = null;
			foreach (var character in party)
			{
				if (character.Name == healer)
				{
					if (character is Warrior || !character.IsAlive)
					{
						throw new ArgumentException($"Parameter Error: {healer} cannot heal!");
					}
					priest = (Priest)character;
				}
				if (character.Name == healed) {
					if (!character.IsAlive)
					{
						throw new ArgumentException($"Parameter Error:  {healer} cannot heal!");
					}			
					healReceiver = character;
				}
			}
			if (priest == null)
			{
				throw new ArgumentException($"Parameter Error: Character {healer} not found!");
			}
			if (healReceiver == null)
			{
				throw new ArgumentException($"Parameter Error: Character {healed} not found!");
			}
			priest.Heal(healReceiver);
			return $"{priest.Name} heals {healReceiver.Name} for {priest.AbilityPoints}! {healReceiver.Name} has {healReceiver.Health} health now!";
		}
	}
}

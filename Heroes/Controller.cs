using Heroes.Core.Contracts;
using Heroes.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes
{
    public class Controller : IController
    {
        private HeroRepository<Hero> heroes = new HeroRepository<Hero>();
        private WeaponRepository<Weapon> weapons = new WeaponRepository<Weapon>();
        public string AddWeaponToHero(string weaponName, string heroName)
        {
            if (this.heroes.FindByName(heroName) == null)
            {
                throw new InvalidOperationException($"Hero { heroName } does not exist.");
            }
            else if (this.weapons.FindByName(weaponName) == null)
            {
                throw new InvalidOperationException($"Weapon {weaponName } does not exist.");
            }
            else if (this.heroes.FindByName(heroName).Weapon != null)
            {
                throw new InvalidOperationException($"Hero { heroName } is well-armed.");
            }
            else
            {
                this.heroes.FindByName(heroName).AddWeapon(this.weapons.FindByName(weaponName));
                this.weapons.Remove(this.weapons.FindByName(weaponName));
                if (this.heroes.FindByName(heroName).Weapon is Claymore)
                {
                    {  
                        return $"Hero {heroName} can participate in battle using a claymore.";
                    }
                }
                else
                {      
                    return $"Hero {heroName} can participate in battle using a mace.";
                }
            }
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            if (this.heroes.FindByName(name) != null)
            {
                throw new InvalidOperationException($"The hero { name } already exists.");
            }
            else if (type != "Knight" && type != "Barbarian")
            {
                throw new InvalidOperationException($"Invalid hero type.");
            }
            
            else if (type == "Knight")
            {
                this.heroes.Add(new Knight(name, health, armour));
                return $"Successfully added Sir { name } to the collection.";
            }
            else 
            {
                heroes.Add(new Barbarian(name, health, armour));
                return $"Successfully added Barbarian { name } to the collection.";
            }  
        }

        public string CreateWeapon(string type, string name, int durability)
        {
            if (this.weapons.FindByName(name) != null)
            {
                throw new InvalidOperationException($"The weapon { name } already exists.");
            }
            if (name == string.Empty || name == null)
            {
                throw new ArgumentException("Weapon type cannot be null or empty.");
            }
            if (type != "Claymore" && type != "Mace")
            {
                throw new InvalidOperationException("Invalid weapon type.");
            }
            if (durability < 0)
            {
                throw new ArgumentException("Durability cannot be below 0.");
            }
            else if (type == "Claymore")
            {
                this.weapons.Add(new Claymore(name, durability));
            }
            else
            {
                this.weapons.Add(new Mace(name, durability));
            }         
            return $"A { type.ToLower() } {name } is added to the collection.";
        }

        public string HeroReport()
        {
            List <IHero> barbarians= new List<IHero>();
            List<IHero> knights = new List<IHero>();
            foreach (var item in this.heroes.Models)
            {
                if (item is Barbarian)
                {
                    barbarians.Add(item);
                }
                else
                {
                    knights.Add(item);
                }
            }
                string barbarian = "";
            if (barbarians.Count > 1)
            {
                barbarians.Sort((x, y) => x.Health);
                for (int i = 1; i < barbarians.Count; i++)
                {
                    if (barbarians[i - 1].Health == barbarians[i].Health)
                    {
                        barbarians.GetRange(i - 1, i).Sort((x, y) => string.Compare(x.Name, y.Name));
                    }
                }

                for (int i = 0; i < barbarians.Count; i++)
                {
                    barbarian +=string.Join( barbarian, $"Barbarian: { barbarians[i].Name } \n\r" +
                                            $"--Health: { barbarians[i].Health } \n\r" +
                                            $"--Armour: { barbarians[i].Armour } \n\r");
                    if (barbarians[i].Weapon == null)
                    {
                        barbarian += string.Join(barbarian, "--Weapon: Unarmed\r\n");
                    }
                    else
                    {
                        barbarian += string.Join(barbarian, $"--Weapon: { barbarians[i].Weapon.Name }\r\n");
                    }
                }
            }
            else
            {
                barbarian = string.Join(barbarian, $"Barbarian: { barbarians[0].Name } \n\r" +
                                            $"--Health: { barbarians[0].Health } \n\r" +
                                            $"--Armour: { barbarians[0].Armour } \n\r");
                if (barbarians[0].Weapon == null)
                {
                    barbarian += string.Join(barbarian,$"--Weapon: Unarmed\r\n");
                }
                else
                {
                    barbarian += string.Join(barbarian, $"--Weapon: { barbarians[0].Weapon.Name }\r\n");
                }
            }
            string knight = "";
            if (knights.Count > 1)
            {
                knights.Sort((x, y) => x.Health);
                for (int i = 1; i < knights.Count; i++)
                {
                    if (knights[i].Health == knights[i-1].Health)
                    {
                        knights.GetRange(i, i-1).Sort((x, y) => string.Compare(x.Name, y.Name));
                    }
                }
                for (int i = 0; i < knights.Count; i++)
                { 
                    knight += string.Join(knight, $"Knight: { knights[i].Name } \n\r" +
                                            $"--Health: { knights[i].Health } \n\r" +
                                            $"--Armour: { knights[i].Armour } \n\r");

                    if (knights[i].Weapon == null)
                    {
                        knight += string.Join(knight, "--Weapon: Unarmed\r\n");
                    }
                
                    else
                    {
                        knight += string.Join(knight, $"--Weapon: { knights[i].Weapon.Name }\r\n");
                    }
                }
            }
            else
            {
                if (knights[0].Weapon == null)
                {
                    knight += string.Join(knight, $"Knight: { knights[0].Name } \n\r" +
                                                  $"--Health: { knights[0].Health } \n\r" +
                                                  $"--Armour: { knights[0].Armour } \n\r" + 
                                                  $"--Weapon: Unarmed\r\n");
                }
                else
                {
                    knight += string.Join(knight, $"Knight: { knights[0].Name } \n\r" +
                                                $"--Health: { knights[0].Health } \n\r" +
                                                $"--Armour: { knights[0].Armour } \n\r" +
                                                $"--Weapon: { knights[0].Weapon.Name }\r\n");
                }
            }
            
            string all = "";
            all = string.Join(all, barbarian, knight);
            return all;
        }

        public string StartBattle()
        {
            Map map = new Map();
            List<IHero> players = new List<IHero>();
            
            foreach (var item in this.heroes.Models)
            {

                if (item.IsAlive == true && item.Weapon != null)
                {
                    if (item.Weapon.Durability > 0)
                    {
                        players.Add(item);
                    }
                                                     
                }
            }
           return map.Fight(players);
        }                
        public Controller()
        {

        }
    }
}

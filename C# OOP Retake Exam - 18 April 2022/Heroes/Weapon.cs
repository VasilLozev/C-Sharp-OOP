using Heroes.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes
{
    public abstract class Weapon : IWeapon
    {
        protected string _name;
        public string Name
        {
            get
            {
                if (_name == null)
                {
                    throw new ArgumentException("Weapon type cannot be null or empty.");
                }
                if (_name == string.Empty)
                {
                    throw new ArgumentException("Weapon type cannot be null or empty.");
                }
                 return _name;     
            }            
        }

        protected int _durability;
        public int Durability
        {
            get
            {
                if (_durability < 0)
                {
                    throw new ArgumentException("Durability cannot be below 0.");
                }
               
                    return this._durability;        
            } 
        }
        public int DoDamage()
        {
            
            if (this._durability == 0)
            {
                return 0;
            }
            this._durability -= 1;
             if (this is Mace)
            {
                return 25;
            }
            else if (this is Claymore)
            {
                return 20;
            }
             return default; 
        }
        public Weapon(string name, int durability)
        {
            _name = name;
            _durability = durability;
        }
    }
}

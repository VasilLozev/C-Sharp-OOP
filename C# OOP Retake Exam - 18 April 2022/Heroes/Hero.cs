using Heroes.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes
{
    public abstract class Hero : IHero
    {
        protected string _name;
        public string Name
        {
            get
            {
                if (this._name == null)
                {
                    throw new ArgumentException("Hero name cannot be null or empty.");
                }
                if (this._name == string.Empty)
                {
                    throw new ArgumentException("Hero name cannot be null or empty.");
                }
                    return this._name;
            }
        }
        protected int _health;
        public int Health
        {
            get
            {
                if (this._health < 0)
                {
                    throw new ArgumentException("Hero health cannot be below 0.");
                }
                else
                {
                    return this._health;
                }   
            }
        }
        protected int _armuor;
        public int Armour
        {
            get
            {
                if (this._armuor < 0)
                {
                    throw new ArgumentException("Hero armour cannot be below 0.");
                }
                return  this._armuor;
            }
        }
        protected IWeapon _weapon;
        public IWeapon Weapon
        {
            get
            {
                return this._weapon;
            }
        }
       
        public bool IsAlive
        {
            get
            {
                if (this._health > 0)
                {
                    return true;
                }
                
                return false;
            }
        }

        public void AddWeapon(IWeapon weapon)
        {
            if (this._weapon == null)
            {
                this._weapon = weapon;
            }        
        }
        
        public void TakeDamage(int points)
        {
            if ((this._armuor - points) < 0)
            {    
                this._health -= Math.Abs(this._armuor - points);
                if (_health < 0)
                {
                    this._health = 0;   
                }
                this._armuor = 0;
            }
            else
            {
                this._armuor -= points;
            }
        }
        public Hero(string name, int health, int armour)
        {
            this._name = name;
            this._health = health;
            this._armuor = armour;
        }
    }
}

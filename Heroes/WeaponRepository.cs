using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Heroes.Models.Contracts;

namespace Heroes
{
    public class WeaponRepository<Weapon> : IRepository<IWeapon>
    {
        protected List<IWeapon> _models = new List<IWeapon>();
        public IReadOnlyCollection<IWeapon> Models 
        {
            get
            {
                return this._models.AsReadOnly();
            }
        }
       
        public void Add(IWeapon model)
        {
            this._models.Add(model);
        }
        public IWeapon FindByName(string name)
        {
            foreach (var item in this.Models)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            return null;
        }
        public bool Remove(IWeapon model)
        {
            if (this._models.Remove(model))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Heroes.Models.Contracts;

namespace Heroes
{
    public class HeroRepository<Hero> : IRepository<IHero>
    {
        protected List<IHero> _models = new List<IHero>();
        public IReadOnlyCollection<IHero> Models
        {
            get
            {
                return this._models.AsReadOnly();
            }
        }
        
        public void Add(IHero model)
        {
           this._models.Add(model);
        }

        public IHero FindByName(string name)
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
        public bool Remove(IHero model)
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

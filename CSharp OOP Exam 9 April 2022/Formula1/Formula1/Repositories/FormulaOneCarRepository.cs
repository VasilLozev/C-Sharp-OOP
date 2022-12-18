using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula1.Repositories
{
    public class FormulaOneCarRepository : IRepository<IFormulaOneCar>
    {
        public IReadOnlyCollection<IFormulaOneCar> Models => _models.AsReadOnly();
       
        private List<IFormulaOneCar> _models;

        public void Add(IFormulaOneCar model)
        {
            _models.Add(model);
        }

        public IFormulaOneCar FindByName(string name)
        {
            return _models.FirstOrDefault(x => x.Model == name);
        }

        public bool Remove(IFormulaOneCar model)
        {
            return _models.Remove(model);
        }
        public FormulaOneCarRepository()
        {
            _models = new List<IFormulaOneCar>();
        }
    }
}

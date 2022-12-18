using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula1.Repositories
{
    public class RaceRepository : IRepository<IRace>
    {
        public IReadOnlyCollection<IRace> Models => _models.AsReadOnly();
       
        private List<IRace> _models;

        public void Add(IRace race)
        {
            _models.Add(race);
        }

        public IRace FindByName(string raceName)
        {
            return _models.FirstOrDefault(x => x.RaceName == raceName);
            
        }

        public bool Remove(IRace race)
        {
            return _models.Remove(race);
        }
        public RaceRepository()
        {
            _models = new List<IRace>();
        }
    }
}
